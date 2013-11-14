using System.Web.Optimization;
using Microsoft.Ajax.Utilities;
using Web.Optimization.Common;

namespace Web.Optimization.Bundles.AjaxMin
{
    public class AjaxMinCssMinify : CssMinify
    {
        public override void Process(BundleContext context, BundleResponse response)
        {
            response.ContentType = ContentType.JavaScript;
            
            if (context.HttpContext.IsDebuggingEnabled)
            {
                return;
            }

            var minifier = new Minifier();
            response.Content = minifier.MinifyStyleSheet(response.Content);
        }    
    }
}