using System;
using System.Xml.Serialization;
using System.IO;


namespace Microsoft.Web.Graph.WebRole.Util
{
    public class XmlReader
    {
        public static T GetOject<T>(string content)
        {

            XmlSerializer ser = new XmlSerializer(typeof(T));
            TextReader reader = new StringReader(content);
            T result = default(T);
            try
            {
                result = (T)ser.Deserialize(reader);

            }
            catch (Exception e)
            {

            }
            finally
            {
                reader.Close();
            }
            return result;
        }
    }
}
