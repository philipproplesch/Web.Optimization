using System;
using System.Web.Mvc;

namespace Web.Optimization.Extensions
{
    public static class HtmlHelperExtensions
    {
        [Obsolete]
        public static MvcHtmlString RenderBundle(
            this HtmlHelper instance, string bundleVirtualPath)
        {
            throw new ApplicationException("...");
        }
    }
}
