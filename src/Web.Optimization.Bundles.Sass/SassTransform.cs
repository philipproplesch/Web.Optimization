using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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

        public SassTransform()
        {
            //var initializeMethod =
            //    typeof (SassCompiler)
            //        .GetMethod("Initialize",
            //                   BindingFlags.NonPublic | BindingFlags.Instance);

            //initializeMethod.Invoke(_compiler, null);
        }

        private static readonly Regex s_sassImportRegex =
            new Regex("@import [\"|'](.+)[\"|'];", RegexOptions.Compiled);

        public void Process(BundleContext context, BundleResponse response)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var builder = new StringBuilder();

            foreach (var file in response.Files)
            {
                using (var compiler = _compilerPool.GetInstance())
                {
                    var content = File.ReadAllText(file.FullName, Encoding.UTF8);
                    var matches = s_sassImportRegex.Matches(content);

                    var directory = Path.GetDirectoryName(file.FullName);
                    var extension = Path.GetExtension(file.Name);

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
                            file.FullName,
                            false,
                            dependencies));
                }
            }

            stopwatch.Stop();

            response.ContentType = ContentTypes.Css;
            response.Content = builder.ToString();
        }
    }
}
