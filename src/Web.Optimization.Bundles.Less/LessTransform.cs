using System.Web.Optimization;

namespace Web.Optimization.Bundles.Less
{
    public class LessTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            response.ContentType = "text/css";
            response.Content = dotless.Core.Less.Parse(response.Content);
        }
    }
}