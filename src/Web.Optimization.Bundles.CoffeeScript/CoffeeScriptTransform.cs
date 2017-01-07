using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Optimization;
using Jurassic;
using Web.Optimization.Common;

namespace Web.Optimization.Bundles.CoffeeScript
{
    /// <summary>
    /// Transforms CoffeeScript into JavaScript.
    /// </summary>
    public class CoffeeScriptTransform : IBundleTransform
    {
        private readonly bool _bare;
        private readonly string _scriptPath;

        public CoffeeScriptTransform(bool bare, string scriptPath)
        {
            _bare = bare;
            _scriptPath = scriptPath;
        }

        public CoffeeScriptTransform(bool bare)
            : this(bare, "Scripts\\coffee-script.js")
        {
            _bare = bare;
        }

        public CoffeeScriptTransform()
            : this(true)
        {
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            var coffeeScriptPath =
                Path.Combine(
                    HttpRuntime.AppDomainAppPath,
                    _scriptPath);

            if (!File.Exists(coffeeScriptPath))
            {
                throw new FileNotFoundException(
                    $"Could not find coffee-script.js beneath the provided {_scriptPath} directory.");
            }

            var coffeeScriptCompiler =
                File.ReadAllText(coffeeScriptPath, Encoding.UTF8);

            var engine = new ScriptEngine();
            engine.Execute(coffeeScriptCompiler);

            // Initializes a wrapper function for the CoffeeScript compiler.
            var wrapperFunction =
                string.Format(
                    "var compile = function (src) {{ return CoffeeScript.compile(src, {{ bare: {0} }}); }};",
                    _bare.ToString(CultureInfo.InvariantCulture).ToLower());

            engine.Execute(wrapperFunction);

            var js = engine.CallGlobalFunction("compile", response.Content);

            response.ContentType = ContentType.JavaScript;
            response.Content = js.ToString();
        }
    }
}
