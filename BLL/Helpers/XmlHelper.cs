using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BLL.Helpers
{
    public static class XmlHelper
    {
        public static void GuardarLista<T>(List<T> lista, string ruta)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (TextWriter writer = new StreamWriter(ruta))
            {
                serializer.Serialize(writer, lista);
            }
        }

        public static List<T> LeerLista<T>(string ruta)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (TextReader reader = new StreamReader(ruta))
            {
                return (List<T>)serializer.Deserialize(reader);
            }
        }
    }
}
