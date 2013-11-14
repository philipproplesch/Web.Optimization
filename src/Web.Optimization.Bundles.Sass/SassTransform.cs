using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Optimization;
using SassAndCoffee.Core;
using SassAndCoffee.Ruby.Sass;
using Web.Optimization.Common;

namespace Web.Optimization.Bundles.Sass
{
    public class SassTransform : IBundleTransform
    {
        private readonly Pool<ISassCompiler, SassCompilerProxy> _compilerPool =
            new Pool<ISassCompiler, SassCompilerProxy>(() => new SassCompiler());

        private static readonly Regex s_sassImportRegex =
            new Regex("@import [\"|'](.+)[\"|'];", RegexOptions.Compiled);

        public void Process(BundleContext context, BundleResponse response)
        {
            var builder = new StringBuilder();

            foreach (var file in response.Files)
            {
                var path =
                   context.HttpContext.Server.MapPath(
                       file.IncludedVirtualPath);

                using (var compiler = _compilerPool.GetInstance())
                {
                    var content = File.ReadAllText(path, Encoding.UTF8);
                    var matches = s_sassImportRegex.Matches(content);

                    var directory = Path.GetDirectoryName(path);
                    var extension = Path.GetExtension(path);

                    var dependencies = new List<string>();
                    foreach (Match match in matches)
                    {
                        var import = match.Groups[1].Value;

                        dependencies.Add(
                            Path.Combine(
                                directory,
                                string.Concat("_", import, extension)));
                    }

                    builder.AppendLine(
                        compiler.Compile(
                            path,
                            !context.HttpContext.IsDebuggingEnabled,
                            dependencies));
                }
            }

            response.ContentType = ContentType.Css;
            response.Content = builder.ToString();
        }
    }
}
