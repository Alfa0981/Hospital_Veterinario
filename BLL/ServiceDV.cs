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
        private readonly string[] tablasVerificables = {"Usuarios","Mascotas"};

        public long CalcularDVH(string concatenado)
        {
            long sum = 0;
            byte[] bytes = Encoding.UTF8.GetBytes(concatenado);
            for (int i = 0; i < bytes.Length; i++)
                sum += bytes[i] * (i + 1);
            return sum;
        }

        /*public void RecalcularTodo()
        {
            foreach (var tabla in tablasVerificables)
            {
                var dt = repo.ObtenerTablaCompleta(tabla);
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["Id"]); // asumir campo "Id" como clave
                    string texto = string.Join("", row.ItemArray);
                    long nuevoDVH = CalcularDVH(texto);

                    repo.GuardarDVH(tabla, id, nuevoDVH);
                }
            }
        }*/

        /*public List<BE.VerificacionResultadoClass> VerificarIntegridad()
        {
            var resultados = new List<BE.VerificacionResultadoClass>();

            foreach (var tabla in tablasVerificables)
            {
                var resultado = new BE.VerificacionResultadoClass { Tabla = tabla };

                var dt = repo.ObtenerTablaCompleta(tabla);
                var dvhsAlmacenados = repo.ObtenerDVHs(tabla);

                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["Id"]);
                    string texto = string.Join("", row.ItemArray);
                    long calculado = CalcularDVH(texto);

                    var dvhAlmacenado = dvhsAlmacenados.FirstOrDefault(d => d.IdRegistro == id)?.DVH ?? 0;

                    if (calculado != dvhAlmacenado)
                        resultado.RegistrosAlterados.Add(id);
                }

                resultados.Add(resultado);
            }

            return resultados;
        }*/
        /*public List<BE.VerificacionResultadoClass> VerificarIntegridad()
        {
            //var resultados = new List<BE.VerificacionResultadoClass>();
            var resultados = new List<BE.VerificacionResultadoClass>();

            foreach (var tabla in tablasVerificables)
            {
                var resultado = new BE.VerificacionResultadoClass { Tabla = tabla };

                //var dt = repo.ObtenerTablaCompleta(tabla);
                var dt = repo.ObtenerTablaCompleta(tabla);
                if (dt == null || dt.Columns.Count == 0)
                    throw new Exception($"La tabla '{tabla}' no tiene columnas. Posiblemente no se cargó bien.");

                var dvhsAlmacenados = repo.ObtenerDVHs(tabla);

                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["id"]);
                    string texto = string.Join("", row.ItemArray);
                    long calculado = CalcularDVH(texto);

                    var dvhAlmacenado = dvhsAlmacenados.FirstOrDefault(d => d.IdRegistro == id)?.DVH ?? 0;

                    if (calculado != dvhAlmacenado)
                    {
                        resultado.RegistrosAlterados.Add(id);

                        // comparar columna por columna
                        foreach (DataColumn col in dt.Columns)
                        {
                            string valorActual = row[col]?.ToString() ?? "";//calcular dvv de col
                            string valorConcatenado = valorActual; //comparamos dvv calculado con guardado
                            //si falla, sabemos columna y row

                            // Simular DVH solo con esta columna
                            long dvCol = CalcularDVH(valorConcatenado);

                            // Probar si el DV cambia significativamente quitando esta columna
                            var rowSinCol = dt.Clone();
                            rowSinCol.ImportRow(row);
                            rowSinCol.Rows[0][col] = ""; // simularla vacía
                            string textoSinCol = string.Join("", rowSinCol.Rows[0].ItemArray);
                            long dvSinEsa = CalcularDVH(textoSinCol);

                            if (Math.Abs(calculado - dvSinEsa) > 0) // Hay diferencia
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
        }*/
        /*public List<BE.VerificacionResultadoClass> VerificarIntegridad()
        {
            var resultados = new List<BE.VerificacionResultadoClass>();

            foreach (var tabla in tablasVerificables)
            {
                var resultado = new BE.VerificacionResultadoClass { Tabla = tabla };
                var dt = repo.ObtenerTablaCompleta(tabla);
                if (dt == null || dt.Columns.Count == 0)
                    throw new Exception($"La tabla '{tabla}' no tiene columnas. Posiblemente no se cargó bien.");

                // Verificación por DVH (registro a registro)
                var dvhsAlmacenados = repo.ObtenerDVHs(tabla);
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["id"]);
                    string texto = string.Join("", row.ItemArray);
                    long calculado = CalcularDVH(texto);

                    long almacenado = dvhsAlmacenados
                        .FirstOrDefault(d => d.IdRegistro == id)?.DVH ?? 0;

                    if (calculado != almacenado)
                        resultado.RegistrosAlterados.Add(id);
                }
                /*
                // Verificación por DVV (columna a columna)
                var dvvsAlmacenados = repo.ObtenerDVVs(tabla);
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "id") continue;

                    // Calcular DVV de esta columna (como en RecalcularDVV)
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.Append(row[col]?.ToString() ?? "");
                    }
                    long dvvCalculado = CalcularDVH(sb.ToString());

                    // Comparar con DVV almacenado
                    dvvsAlmacenados.TryGetValue(col.ColumnName, out long dvvAlmacenado);
                    if (dvvCalculado != dvvAlmacenado)
                    {
                        // Si hay diferencia, marcar cada fila como sospechosa en esa columna
                        foreach (DataRow row in dt.Rows)
                        {
                            resultado.ColumnasAlteradas.Add(new BE.ColumnaAlterada
                            {
                                IdRegistro = Convert.ToInt32(row["id"]),
                                Columna = col.ColumnName
                            });
                        }
                    }
                }

                resultados.Add(resultado);
            }

            return resultados;
        }*/
        /* var dvvsAlmacenados = repo.ObtenerDVVs(tabla);
         foreach (DataColumn col in dt.Columns)
         {
             if (col.ColumnName == "id") continue;

             StringBuilder sb = new StringBuilder();
             foreach (DataRow row in dt.Rows)
             {
                 sb.Append(row[col]?.ToString() ?? "");
             }

             long dvvCalculado = CalcularDVH(sb.ToString());
             /*dvvsAlmacenados.TryGetValue(col.ColumnName, out long dvvAlmacenado);

             if (dvvCalculado != dvvAlmacenado)
             {
                 // 🔍 Solo asociar a los registros ya marcados como alterados
                 foreach (DataRow row in dt.Rows)
                 {
                     int id = Convert.ToInt32(row["id"]);
                     if (resultado.RegistrosAlterados.Contains(id))
                     {
                         resultado.ColumnasAlteradas.Add(new BE.ColumnaAlterada
                         {
                             IdRegistro = id,
                             Columna = col.ColumnName
                         });
                     }
                 }*/
        /* dvvsAlmacenados.TryGetValue(col.ColumnName, out long dvvAlmacenado);
         if (dvvCalculado != dvvAlmacenado)
         {
             // verificar columna en registros marcados como alterados
             foreach (DataRow row in dt.Rows)
             {
                 int id = Convert.ToInt32(row["id"]);
                 if (!resultado.RegistrosAlterados.Contains(id))
                     continue;

                 // Simular el DVH quitando esta columna del registro
                 object[] itemArray = (object[])row.ItemArray.Clone();
                 int colIndex = dt.Columns.IndexOf(col);
                 itemArray[colIndex] = ""; // vaciar esa columna
                 string textoSinCol = string.Join("", itemArray);
                 long dvhSinCol = CalcularDVH(textoSinCol);

                 string textoOriginal = string.Join("", row.ItemArray);
                 long dvhOriginal = CalcularDVH(textoOriginal);

                 if (dvhOriginal != dvhSinCol)
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

}*/

        /*public List<BE.VerificacionResultadoClass> VerificarIntegridad()
        {
            var resultados = new List<BE.VerificacionResultadoClass>();

            foreach (var tabla in tablasVerificables)
            {
                var resultado = new BE.VerificacionResultadoClass { Tabla = tabla };
                var dt = repo.ObtenerTablaCompleta(tabla);
                if (dt == null || dt.Columns.Count == 0)
                    throw new Exception($"La tabla '{tabla}' no tiene columnas. Posiblemente no se cargó bien.");

                var dvhsAlmacenados = repo.ObtenerDVHs(tabla);

                // 1. Verificación por DVH (registro a registro)
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["id"]);
                    string texto = string.Join("", row.ItemArray);
                    long calculado = CalcularDVH(texto);

                    long almacenado = dvhsAlmacenados
                        .FirstOrDefault(d => d.IdRegistro == id)?.DVH ?? 0;

                    if (calculado != almacenado)
                        resultado.RegistrosAlterados.Add(id);
                }

                // 2. Verificación por DVV (columna a columna)
                var dvvsAlmacenados = repo.ObtenerDVVs(tabla);
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "id") continue;

                    // Calcular DVV de esta columna
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.Append(row[col]?.ToString() ?? "");
                    }
                    long dvvCalculado = CalcularDVH(sb.ToString());

                    // Comparar con DVV almacenado
                    dvvsAlmacenados.TryGetValue(col.ColumnName, out long dvvAlmacenado);
                    if (dvvCalculado != dvvAlmacenado)
                    {
                        // Verificar en qué registros alterados se nota la diferencia al quitar esta columna
                        foreach (DataRow row in dt.Rows)
                        {
                            int id = Convert.ToInt32(row["id"]);
                            if (!resultado.RegistrosAlterados.Contains(id)) continue;

                            // Simular el DVH quitando esta columna
                            object[] itemArray = (object[])row.ItemArray.Clone();
                            int colIndex = dt.Columns.IndexOf(col);
                            itemArray[colIndex] = ""; // vaciar esa columna
                            string textoSinCol = string.Join("", itemArray);
                            long dvhSinCol = CalcularDVH(textoSinCol);

                            string textoOriginal = string.Join("", row.ItemArray);
                            long dvhOriginal = CalcularDVH(textoOriginal);

                            if (dvhOriginal != dvhSinCol)
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
        }*/

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








        /*public void RecalcularDVH()
        {
            string[] tablas = new string[] { "Usuarios", "Mascotas" };
            foreach (string tabla in tablas)
            {
                var registros = repo.ObtenerTablaCompleta(tabla);
                repo.EliminarDVHsExistentes(tabla); // limpia los DVHs viejos (por si existen)

                foreach (DataRow row in registros.Rows)
                {
                    int idRegistro = Convert.ToInt32(row["id"]);
                    string concatenado = string.Join("", row.ItemArray);
                    long dvh = CalcularDVH(concatenado);

                    repo.InsertarDVH(tabla, idRegistro, dvh);
                }
            }
        }*/

        /*public void RecalcularDVH()
        {
            foreach (string tabla in tablasVerificables)
            {
                var registros = repo.ObtenerTablaCompleta(tabla);
                repo.EliminarDVHPorCampo(tabla); // eliminamos todo primero

                foreach (DataRow row in registros.Rows)
                {
                    int idRegistro = Convert.ToInt32(row["id"]);
                    foreach (DataColumn col in registros.Columns)
                    {
                        if (col.ColumnName.ToLower() == "id") continue;

                        string valor = row[col]?.ToString() ?? "";
                        long dvhCampo = CalcularDVH(valor);
                        repo.InsertarDVHPorCampo(tabla, idRegistro, col.ColumnName, dvhCampo);
                    }
                }
            }
        }*/
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


        /*public void RecalcularDVV()
        {
            string[] tablas = new string[] { "Usuarios", "Mascotas" };
            foreach (string tabla in tablas)
            {
                var dt = repo.ObtenerTablaCompleta(tabla);
                
                repo.EliminarDVVsExistentes(tabla); // limpia los DVVs viejos

                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "id") continue; // no se calcula DVV para el ID

                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.Append(row[col]?.ToString() ?? "");
                    }

                    long dvv = CalcularDVH(sb.ToString());
                    repo.InsertarDVV(tabla, col.ColumnName, dvv);
                }
            }
        }*/

        /*public List<BE.ColumnaAlterada> VerificarColumnasAlteradas(string tabla)
        {
            var columnasAlteradas = new List<BE.ColumnaAlterada>();
            var dt = repo.ObtenerTablaCompleta(tabla);
            var dvvsAlmacenados = repo.ObtenerDVVs(tabla);

            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName == "id") continue;

                StringBuilder sb = new StringBuilder();
                foreach (DataRow row in dt.Rows)
                {
                    sb.Append(row[col]?.ToString() ?? "");
                }

                long dvCalculado = CalcularDVH(sb.ToString());
                dvvsAlmacenados.TryGetValue(col.ColumnName, out long almacenado);

                if (dvCalculado != almacenado)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        columnasAlteradas.Add(new BE.ColumnaAlterada
                        {
                            IdRegistro = Convert.ToInt32(row["id"]),
                            Columna = col.ColumnName
                        });
                    }
                }
            }

            return columnasAlteradas;
        }*/


    }
}
