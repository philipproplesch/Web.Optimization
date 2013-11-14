using System.Web.Optimization;
using Web.Optimization.Common;
using Yahoo.Yui.Compressor;

namespace Web.Optimization.Bundles.YUI
{
    public class YuiJsMinify : JsMinify
    {
        public override void Process(BundleContext context, BundleResponse response)
        {
            response.ContentType = ContentType.JavaScript;

            if (context.HttpContext.IsDebuggingEnabled)
            {
                return;
            }

            var compressor = new JavaScriptCompressor();
            response.Content = compressor.Compress(response.Content);
        }
    }
}