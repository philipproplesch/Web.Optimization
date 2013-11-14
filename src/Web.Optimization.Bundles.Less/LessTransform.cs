using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Optimization;
using Web.Optimization.Common;
using dotless.Core.configuration;

namespace Web.Optimization.Bundles.Less
{
    public class LessTransform : IBundleTransform
    {
        private readonly DotlessConfiguration _configuration;

        public LessTransform(DotlessConfiguration configuration)
        {
            _configuration = configuration;
        }

        public LessTransform()
            : this(DotlessConfiguration.GetDefaultWeb())
        { }

        public void Process(BundleContext context, BundleResponse response)
        {
            var builder = new StringBuilder();

            foreach (var file in response.Files)
            {
                var path =
                   context.HttpContext.Server.MapPath(
                       file.IncludedVirtualPath);

                var fileInfo = new FileInfo(path);

                if (!fileInfo.Exists)
                {
                    continue;
                }

                var content = ResolveImports(fileInfo);

                builder.AppendLine(
                    _configuration.Web
                        ? dotless.Core.LessWeb.Parse(content, _configuration)
                        : dotless.Core.Less.Parse(content, _configuration));
            }

            response.ContentType = ContentType.Css;
            response.Content = builder.ToString();
        }

        private static readonly Regex s_lessImportRegex =
            new Regex("@import [\"|'](.+)[\"|'];", RegexOptions.Compiled);

        private static string ResolveImports(FileInfo file)
        {
            var content = File.ReadAllText(file.FullName, Encoding.UTF8);

            return s_lessImportRegex.Replace(
                content,
                match =>
                {
                    var import = match.Groups[1].Value;

                    // Is absolute path?
                    Uri uri;
                    if (Uri.TryCreate(import, UriKind.Absolute, out uri))
                    {
                        return match.Value;
                    }

                    var path =
                        Path.Combine(
                            file.Directory.FullName,
                            import);

                    if (!File.Exists(path))
                    {
                        throw new ApplicationException(
                            string.Concat("Unable to resolve import ", import));
                    }

                    return match.Value.Replace(import, path);
                });
        }
    }
}