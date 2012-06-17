using System.Web.Optimization;
using Web.Optimization.Common;
using Yahoo.Yui.Compressor;

namespace Web.Optimization.Bundles.YUI
{
    public class YuiJsMinify : JsMinify
    {
        public override void Process(BundleContext context, BundleResponse response)
        {
            var compressor = new JavaScriptCompressor();
            
            response.Content = compressor.Compress(response.Content);
            response.ContentType = ContentTypes.JavaScript;
        }
    }
}