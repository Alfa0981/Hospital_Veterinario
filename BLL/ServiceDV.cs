using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceDV
    {
        private readonly DAL.MpDV repo = new DAL.MpDV();
        private readonly string[] tablasVerificables = { "Usuarios", "Mascotas" };

        /// <summary>
        /// Calcula el DVH (Dígito Verificador Horizontal) de una cadena de texto,
        /// aplicando una fórmula de suma ponderada sobre los bytes UTF-8 del texto.
        /// </summary>
        public long CalcularDVH(string concatenado)
        {
            long sum = 0;
            byte[] bytes = Encoding.UTF8.GetBytes(concatenado);
            for (int i = 0; i < bytes.Length; i++)
                sum += bytes[i] * (i + 1);
            return sum;
        }

        /// <summary>
        /// Verifica la integridad de las tablas configuradas (Usuarios, Mascotas)
        /// comparando los DVH globales y por campo con los valores almacenados.
        /// Devuelve una lista de resultados indicando los registros y columnas alteradas.
        /// </summary>
        public List<BE.VerificacionResultadoClass> VerificarIntegridad()
        {
            var resultados = new List<BE.VerificacionResultadoClass>();

            foreach (var tabla in tablasVerificables)
            {
                var resultado = new BE.VerificacionResultadoClass
                {
                    Tabla = tabla
                };

                var dt = repo.ObtenerTablaCompleta(tabla);
                if (dt == null || dt.Columns.Count == 0)
                    throw new Exception($"La tabla '{tabla}' no tiene columnas. Posiblemente no se cargó bien.");

                var dvhsPorRegistro = repo.ObtenerDVHs(tabla);
                var registrosAlterados = new HashSet<int>();

                // Paso 1: Verificar DVH global por registro
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["id"]);
                    string concatenado = string.Join("", dt.Columns.Cast<DataColumn>().Select(c => row[c]?.ToString() ?? ""));
                    long dvhCalculado = CalcularDVH(concatenado);

                    long dvhAlmacenado = dvhsPorRegistro.FirstOrDefault(d => d.IdRegistro == id)?.DVH ?? 0;

                    if (dvhCalculado != dvhAlmacenado)
                    {
                        registrosAlterados.Add(id);
                        resultado.RegistrosAlterados.Add(id);
                    }
                }

                // Paso 2: Verificar DVH por campo para cada registro alterado
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["id"]);
                    if (!registrosAlterados.Contains(id)) continue;

                    var dvhsCampo = repo.ObtenerDVHPorCampo(tabla, id);

                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName.ToLower() == "id") continue;

                        string valorActual = row[col]?.ToString() ?? "";
                        long dvhCalculado = CalcularDVH(valorActual);

                        if (dvhsCampo.TryGetValue(col.ColumnName, out long dvhEsperado))
                        {
                            if (dvhCalculado != dvhEsperado)
                            {
                                resultado.ColumnasAlteradas.Add(new BE.ColumnaAlterada
                                {
                                    IdRegistro = id,
                                    Columna = col.ColumnName
                                });
                            }
                        }
                    }
                }

                resultados.Add(resultado);
            }

            return resultados;
        }

        /// <summary>
        /// Recalcula y actualiza los DVH globales y por campo para todas las tablas verificables.
        /// Elimina primero los valores existentes y luego los genera nuevamente a partir de los datos actuales.
        /// </summary>
        public void RecalcularDVH()
        {
            string[] tablas = new string[] { "Usuarios", "Mascotas" };
            foreach (string tabla in tablas)
            {
                var registros = repo.ObtenerTablaCompleta(tabla);

                repo.EliminarDVHsExistentes(tabla);        // limpia DVH global
                repo.EliminarDVHPorCampo(tabla); // limpia DVH por campo

                foreach (DataRow row in registros.Rows)
                {
                    int id = Convert.ToInt32(row["id"]);

                    // 1. Guardar DVH global del registro
                    string concatenado = string.Join("", row.ItemArray);
                    long dvhGlobal = CalcularDVH(concatenado);
                    repo.InsertarDVH(tabla, id, dvhGlobal);

                    // 2. Guardar DVH por campo
                    foreach (DataColumn col in registros.Columns)
                    {
                        if (col.ColumnName.ToLower() == "id") continue;

                        string valor = row[col]?.ToString() ?? "";
                        long dvhCampo = CalcularDVH(valor);
                        repo.InsertarDVHPorCampo(tabla, id, col.ColumnName, dvhCampo);
                    }
                }
            }
        }
    }
}
