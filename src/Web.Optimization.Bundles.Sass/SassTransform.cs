using System.Collections.Generic;
using System.Text;
using System.Web.Optimization;
using SassAndCoffee.Ruby.Sass;
using Web.Optimization.Common;

namespace Web.Optimization.Bundles.Sass
{
    public class SassTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            var builder = new StringBuilder();

            var compiler = new SassCompiler();
            foreach (var file in response.Files)
            {
                builder.AppendLine(
                    compiler.Compile(
                        file.FullName,
                        false,
                        new List<string>()));
            }

            response.ContentType = ContentTypes.Css;
            response.Content = builder.ToString();
        }
    }
}
