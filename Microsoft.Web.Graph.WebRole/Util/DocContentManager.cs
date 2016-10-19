using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Concurrent;

namespace Microsoft.Web.Graph.WebRole.Util
{
    public class DocContentManager
    {
        private static string blobConnectString = "DefaultEndpointsProtocol=https;AccountName=ashirstest;AccountKey=TX6Zkn/STQbiE9GBRK6aF54BwKxpeqoXIN2C9wg0kGtzNebfrkRrKEwGrZjehLwgzfJWXvd47HGOYe1XYrXPUw==";
        private static string docBlobContainer = "dynamicdoc";
        private static ConcurrentDictionary<string, Toc> tocDic = new ConcurrentDictionary<string, Toc>();
        private static ConcurrentDictionary<string, DocSets> docSetsDic = new ConcurrentDictionary<string, DocSets>();

        public static Toc GetToc(string culture, string docPath)
        {
            Toc result = null;
            int firstSlashIndex = docPath.IndexOf("/");
            if (firstSlashIndex < 0)
            {
                return null;
            }
            string productCategory = docPath.Substring(0, firstSlashIndex);
            string docPathTrimProduct = docPath.Substring(firstSlashIndex + 1);
            firstSlashIndex = docPathTrimProduct.IndexOf("/");
            if (firstSlashIndex < 0)
            {
                return null;
            }
            string docSetCategory = docPathTrimProduct.Substring(0, firstSlashIndex);
            result = GetCachedToc(culture, productCategory, docSetCategory);
            if (result == null)
            {
                string tocFilePath = productCategory + "/" + docSetCategory + "/toc.xml";
                string tocContent = BlobManager.ReadContent(blobConnectString, docBlobContainer, culture, tocFilePath);
                if (tocContent != null)
                {
                    result = XmlReader.GetOject<Toc>(tocContent);
                    if (result != null)
                    {
                        result.RootPath = docSetCategory;
                        result.UrlPrefix = "/" + culture + result.UrlPrefix + productCategory + "/" + docSetCategory + "/";
                        CacheToc(culture, productCategory, docSetCategory, result);
                        BuildTheTocChildMap(result);
                    }
                }
            }
            return result;
        }

        public static DocSets GetDocSets(string culture, string docPath)
        {
            DocSets result = null;
            int firstSlashIndex = docPath.IndexOf("/");
            if (firstSlashIndex < 0)
            {
                return null;
            }
            string productCategory = docPath.Substring(0, firstSlashIndex);
            string docPathTrimProduct = docPath.Substring(firstSlashIndex + 1);
            result = GetCachedDocSets(culture, productCategory);
            if (result == null)
            {
                string docSetFilePath = productCategory + "/DocSets.xml";
                string docSetContent = BlobManager.ReadContent(blobConnectString, docBlobContainer, culture, docSetFilePath);

                if (docSetContent != null)
                {
                    result = XmlReader.GetOject<DocSets>(docSetContent);
                    if(result!=null)
                    {
                        CacheDocSets(culture, productCategory, result);
                    }
                }
            }
            return result;
        }

        public static string GetDocShortPath(string docPath)
        {
            int firstSlashIndex = docPath.IndexOf("/");
            if (firstSlashIndex < 0)
            {
                return null;
            }
            string productCategory = docPath.Substring(0, firstSlashIndex);
            string docPathTrimProduct = docPath.Substring(firstSlashIndex + 1);
            firstSlashIndex = docPathTrimProduct.IndexOf("/");
            if (firstSlashIndex < 0)
            {
                return null;
            }
            return docPathTrimProduct.Substring(firstSlashIndex + 1);
        }

        public static string GetDocContent(string culture,string docPath)
        {
            return BlobManager.ReadContent(blobConnectString, docBlobContainer, culture, docPath+".htm");
        }

        public static void ClearTocCache()
        {
            tocDic.Clear();
        }

        public static void ClearTocCache(string culture, string productCategory, string docSetCategory)
        {
            string key = culture + "_" + productCategory + "_" + docSetCategory;
            Toc result = null;
            tocDic.TryRemove(key,out result);
        }

        public static void ClearDocSetsCache()
        {
            docSetsDic.Clear();
        }

        public static void ClearDocSetsCache(string culture,string productCategory)
        {
            string key = culture + "_" + productCategory;
            DocSets result = null;
            docSetsDic.TryRemove(key, out result);
        }
        private static Toc GetCachedToc(string culture, string productCategory, string docSetCategory)
        {
            string key = culture + "_" + productCategory + "_" + docSetCategory;
            if(tocDic.ContainsKey(key))
            {
                return tocDic[key];
            }
            return null;

        }

        private static void CacheToc(string culture, string productCategory, string docSetCategory, Toc toc)
        {
            string key = culture + "_" + productCategory + "_" + docSetCategory;
            if (!tocDic.ContainsKey(key))
            {
                tocDic.TryAdd(key, toc);
            }
        }

        private static DocSets GetCachedDocSets(string culture, string productCategory)
        {
            string key = culture + "_" + productCategory;
            if(docSetsDic.ContainsKey(key))
            {
                return docSetsDic[key];
            }
            return null;
        }

        private static void CacheDocSets(string culture, string productCategory, DocSets docSets)
        {
            string key = culture + "_" + productCategory;
            if (!docSetsDic.ContainsKey(key))
            {
                docSetsDic.TryAdd(key, docSets);
            }
        }

        private static void BuildTheTocChildMap(Toc toc)
        {
            BuildTheTocChildMap(toc.TocItem, new List<TocItem>());
        }

        private static void BuildTheTocChildMap(TocItem[] items, List<TocItem> parents)
        {
            foreach (TocItem item in items)
            {
                foreach (TocItem parent in parents)
                {
                    if (parent.Children == null)
                    {
                        parent.Children = ";";
                    }
                    parent.Children = parent.Children + item.url + ";";
                }
                if (item.TocItem1 != null)
                {
                    List<TocItem> newParents = new List<TocItem>();
                    if (parents.Count > 0)
                    {
                        TocItem[] oldParents = new TocItem[parents.Count];
                        parents.CopyTo(oldParents);
                        newParents = oldParents.ToList();
                    }
                    newParents.Add(item);
                    BuildTheTocChildMap(item.TocItem1, newParents);
                }
            }
        }
    }
}