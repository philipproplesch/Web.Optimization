using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Optimization;
using Web.Optimization.Common;

namespace Web.Optimization.Bundles.Less
{
    public class LessTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            var builder = new StringBuilder();

            foreach (var file in response.Files)
            {
                if (!File.Exists(file.FullName))
                    continue;

                var content = ResolveImports(file);
                builder.AppendLine(dotless.Core.Less.Parse(content));
            }

            response.ContentType = ContentTypes.Css;
            response.Content = builder.ToString();
        }

        private static readonly Regex s_lessImport = 
            new Regex("@import [\"|'](.+)[\"|'];", RegexOptions.Compiled);

        private static string ResolveImports(FileInfo file)
        {
            var content = File.ReadAllText(file.FullName, Encoding.UTF8);

            content =
                s_lessImport.Replace(
                    content,
                    match =>
                        {
                            var name = match.Groups[1].Value;

                            var import =
                                Path.Combine(
                                    file.Directory.FullName,
                                    name);

                            if (!File.Exists(import))
                            {
                                throw new ConfigurationErrorsException(
                                    string.Concat("Unable to resolve import ", name));
                            }

                            return match.Value.Replace(name, import);
                        });

            return content;
        }
    }
}