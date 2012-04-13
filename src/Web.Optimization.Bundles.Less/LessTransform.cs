using System.Web.Optimization;
using Web.Optimization.Common;

namespace Web.Optimization.Bundles.Less
{
    public class LessTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            response.ContentType = ContentTypes.Css;
            response.Content = dotless.Core.Less.Parse(response.Content);
        }
    }
}