using System.Web.Optimization;
using Web.Optimization.Common;
using Yahoo.Yui.Compressor;

namespace Web.Optimization.Bundles.YUI
{
    public class YuiCssMinify : CssMinify
    {
        public override void Process(BundleContext context, BundleResponse response)
        {
            response.ContentType = ContentType.Css;

            if (context.HttpContext.IsDebuggingEnabled)
            {
                return;
            }

            var compressor = new CssCompressor();
            response.Content = compressor.Compress(response.Content);
        }    
    }
}