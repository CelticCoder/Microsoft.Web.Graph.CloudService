using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Microsoft.Web.Graph.WebRole.Util
{
    public class BlobManager
    {
        private static string defaultCulture = "en-us";
        public static string ReadContent(string connectString, string containter, string culture, string path)
        {
            culture = culture.ToLower();
            string result = ReadContentInternal(connectString, containter, culture, path);
            if(result==null&&(!defaultCulture.Equals(culture)))
            {
                result = ReadContentInternal(connectString, containter, defaultCulture, path);
            }
            return result;
        }

        public static string ReadContentInternal(string connectString, string containter, string culture, string path)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containter);
            CloudBlockBlob blob = container.GetBlockBlobReference(culture+"/"+path);
            Stream stream = null;
            try
            {
                stream = blob.OpenRead();
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch(Exception e)
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return null;
        }
    }
}