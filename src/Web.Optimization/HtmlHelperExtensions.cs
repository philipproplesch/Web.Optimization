using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Optimization;
using Web.Optimization.Common;
using Web.Optimization.Extensions;

namespace Web.Optimization
{
    public static class HtmlHelperExtensions
    {
        private const string ScriptTag =
            "<script src='{0}'></script>";

        private const string LinkTag =
            "<link href='{0}' rel='stylesheet' type='text/css' />";

        public static MvcHtmlString RenderBundleContent(
            this HtmlHelper instance, string bundleVirtualPath)
        {
            var bundle = BundleTable.Bundles.GetBundleFor(bundleVirtualPath);
            if (bundle == null)
                return new MvcHtmlString(string.Empty);

            Func<string, bool> isJs =
                extension =>
                FileTypes.ScriptExtensions.Any(
                    x => x.Equals(
                        extension,
                        StringComparison.OrdinalIgnoreCase));

            Func<string, string, string> buildMarkup =
                (extension, path) =>
                string.Format(
                    isJs(extension)
                        ? ScriptTag
                        : LinkTag,
                    path);

            var files =
                bundle.EnumerateFiles(
                    new BundleContext(
                        instance.ViewContext.HttpContext,
                        BundleTable.Bundles,
                        bundleVirtualPath));

#if DEBUG
            var builder = new StringBuilder();

            foreach (var file in files)
            {
                builder.AppendLine(
                    buildMarkup(
                        file.Extension,
                        file.FullName.ToVirtualPath().TrimStart('~'))); // Get rid of the '~'.
            }

            return new MvcHtmlString(builder.ToString());
#else
            var file = files.FirstOrDefault();
            
            if (file == null)
                return new MvcHtmlString(string.Empty);

            return new MvcHtmlString(
                buildMarkup(
                    file.Extension,
                    BundleTable.Bundles.ResolveBundleUrl(bundleVirtualPath)));
#endif
        }
    }
}
