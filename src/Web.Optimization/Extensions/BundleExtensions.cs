using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace Web.Optimization.Extensions
{
    public static class BundleExtensions
    {
        public static void AddRemoteFile(
            this Bundle instance, Uri uri, bool throwIfNotExist = true)
        {
            var fileName = Path.GetFileName(uri.LocalPath);

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("uri");

            var extension = Path.GetExtension(fileName).ToUpperInvariant();

            var path =
                Path.Combine(
                    HttpRuntime.AppDomainAppPath,
                    Equals(extension, ".JS") ? "Scripts" : "Content",
                    "Remote",
                    fileName);

            var virtualPath =
                path.Replace(HttpRuntime.AppDomainAppPath, "~/")
                    .Replace("\\", "/");

            if (File.Exists(path))
            {
                instance.AddFile(virtualPath, throwIfNotExist);
                return;
            }

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(uri);
                var response = request.GetResponse();

                using (var stream = response.GetResponseStream())
                {
                    if (stream == null)
                        throw new Exception();

                    // Would be awesome to add a file by passing 
                    // the stream or just the file content, but NO PATH!

                    using (var reader = new StreamReader(stream))
                    {
                        var content = reader.ReadToEnd();

                        var directory = Path.GetDirectoryName(path);

                        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                            Directory.CreateDirectory(directory);

                        File.WriteAllText(
                            path,
                            content,
                            Encoding.UTF8);

                        instance.AddFile(virtualPath, throwIfNotExist);
                    }
                }
            }
            catch
            {
                if (throwIfNotExist)
                    throw;
            }
        }
    }
}