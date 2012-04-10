using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Optimization;
using Jurassic;

namespace Web.Optimization.Bundles.CoffeeScript
{
    /// <summary>
    /// Transforms CoffeeScript into JavaScript.
    /// </summary>
    public class CoffeeScriptTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            var coffeeScriptPath =
                Path.Combine(
                    HttpRuntime.AppDomainAppPath,
                    "Scripts",
                    "coffee-script.js");

            if (!File.Exists(coffeeScriptPath))
            {
                throw new FileNotFoundException(
                    "Could not find coffee-script.js beneath the ~/Scripts directory.");
            }

            var coffeeScriptCompiler =
                File.ReadAllText(coffeeScriptPath, Encoding.UTF8);

            var engine = new ScriptEngine();
            engine.Execute(coffeeScriptCompiler);

            // Initializes a wrapper function for the CoffeeScript compiler.
            engine.Execute("var compile = function (src) { return CoffeeScript.compile(src, { bare: true }); };");

            try
            {
                var js = engine.CallGlobalFunction("compile", response.Content);
                
                response.ContentType = "application/javascript";
                response.Content = js.ToString();
            }
            catch
            {
                throw;
            }
        }
    }
}
