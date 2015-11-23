using CourseServer.Contract;
using CourseServer.Utils;
using System;
using System.IO;
using System.Text;

namespace CourseServer.Framework
{
    public class FileSystemCacheDriver : ICacheDriver
    {
        public const string TAG = "FileSystemCacheDriver";

        public const string CACHE_PATH = "cache";

        public FileSystemCacheDriver()
        {
            // Remove all of cache from disk
            if (Directory.Exists(CACHE_PATH))
            {
                Directory.Delete(CACHE_PATH, true);
                Dumper.Log(TAG, "All cache has been removed.");
            }

            Directory.CreateDirectory(CACHE_PATH);
            Dumper.Log(TAG, "Create a folder for the cache driver.");
        }

        public void Cache(string key, string data)
        {
            string cacheFn = GetCacheFileName(key);
            string tmpFn = GetTempFileName();
            // Remove the cache file first to avoid the old content read
            // by other thread before the latest content to be updated.
            if (isCached(key))
            {
                File.Delete(cacheFn);
            }

            FileStream oStream = null;
            StreamWriter writer = null;
            try
            {
                // Write the content to the temporay file
                // then other thread wouldn't read this
                // cache before the process has been completed.
                oStream = new FileStream(tmpFn, FileMode.Create);
                writer = new StreamWriter(oStream);

                writer.Write(data.ToCharArray());
                writer.Flush();
                // Close the file handle before move the file
                writer.Close();
                oStream.Close();

                File.Move(tmpFn, cacheFn);

                Dumper.Log(TAG, 
                    string.Format("The data for {0} has been cached to the file: {1}", key, cacheFn));
            }
            catch (Exception e)
            {
                Dumper.Log(TAG, "An error occur when try to cache the data: " + e.Message);
            }
            finally
            {
                if (oStream != null && oStream.CanRead)
                    oStream.Close();

                if (writer != null && oStream.CanWrite)
                    writer.Close();
            }
        }

        public string LoadCache(string key)
        {
            // No cache file found
            if (!isCached(key))
                return null;

            string cacheFn = GetCacheFileName(key);

            int len = 0;
            char[] buffer = new char[1024];
            StringBuilder sBuilder = new StringBuilder();

            FileStream oStream = null;
            StreamReader reader = null;
            try
            {
                oStream = new FileStream(cacheFn, FileMode.Open);
                reader = new StreamReader(oStream);

                while ((len = reader.ReadBlock(buffer, 0, 1024)) != 0)
                {
                    sBuilder.Append(new string(buffer, 0, len));
                }
            }
            catch (Exception e)
            {
                Dumper.Log(TAG, "An error occur when try to read the data form cache: " + e.Message);
            }
            finally
            {
                if (oStream != null)
                    oStream.Close();

                if (reader != null)
                    reader.Close();
            }

            return sBuilder.ToString();
        }

        public bool isCached(string key)
        {
            return File.Exists(GetCacheFileName(key));
        }

        public string GetCacheKey(Type classes, string methodName)
        {
            if (TextUtils.isEmpty(methodName))
                return null;

            return GenericUtils.GetHash(classes.FullName + "@" + methodName);
        }

        private string GetTempFileName()
        {
            return GetCacheFileName(GenericUtils.GetUniqueString());
        }

        private string GetCacheFileName(string key)
        {
            return CACHE_PATH + "/" + key;
        }
    }
}
