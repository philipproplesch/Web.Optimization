using System;
using System.IO;
using System.Text;
using System.Web.Optimization;
using Web.Optimization.Common;

namespace Web.Optimization.Bundles.CoffeeScript
{
    /// <summary>
    /// Transforms CoffeeScript into JavaScript, but supports plain JavaScript as well.
    /// </summary>
    public class CombinedCoffeeScriptTransform : IBundleTransform
    {
        private readonly bool _bare;

        public CombinedCoffeeScriptTransform() { }

        public CombinedCoffeeScriptTransform(bool bare = true)
        {
            _bare = bare;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            var builder = new StringBuilder();

            foreach (var file in response.Files)
            {
                IBundleTransform transform = null;

                if (file.Extension.Equals(
                    ".coffee",
                    StringComparison.OrdinalIgnoreCase))
                {
                    transform = new CoffeeScriptTransform(_bare);
                }
                else if (file.Extension.Equals(
                    ".js",
                    StringComparison.OrdinalIgnoreCase))
                {
                    transform = new CommonNoTransform(ContentTypes.JavaScript);
                }

                if (transform == null || !File.Exists(file.FullName))
                {
                    continue;
                }

                response.Content =
                    File.ReadAllText(file.FullName, Encoding.UTF8);

                transform.Process(context, response);

                builder.AppendLine(response.Content);
            }

            response.ContentType = ContentTypes.JavaScript;
            response.Content = builder.ToString();
        }
    }
}