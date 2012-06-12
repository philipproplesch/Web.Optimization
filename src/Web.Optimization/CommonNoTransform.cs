using System.Web.Optimization;

namespace Web.Optimization
{
    public class CommonNoTransform : IBundleTransform
    {
        private readonly string _contentType;

        public CommonNoTransform(string contentType)
        {
            _contentType = contentType;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            response.ContentType = _contentType;
        }
    }
}
