using System.Web.Optimization;
using Microsoft.Ajax.Utilities;
using Web.Optimization.Common;

namespace Web.Optimization.Bundles.AjaxMin
{
    public class AjaxMinJsMinify : JsMinify
    {
        public override void Process(BundleContext context, BundleResponse response)
        {
            var minifier = new Minifier();
            response.Content = minifier.MinifyJavaScript(response.Content);
            response.ContentType = ContentTypes.JavaScript;
        }
    }
}