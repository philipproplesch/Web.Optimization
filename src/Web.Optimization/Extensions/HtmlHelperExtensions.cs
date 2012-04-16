using System;
using System.Linq;
#if DEBUG
using System.Text;
#endif
using System.Web.Mvc;
using System.Web.Optimization;
using Web.Optimization.Common;

namespace Web.Optimization.Extensions
{
    public static class HtmlHelperExtensions
    {
        private const string ScriptTag =
            "<script src=\"{0}\"></script>";

        private const string LinkTag =
            "<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />";

        public static MvcHtmlString RenderBundle(
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

            Func<string, string, string> generateMarkup =
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
                    generateMarkup(
                        file.Extension,
                        file.FullName.ToVirtualPath().TrimStart('~'))); // Get rid of the '~'.
            }

            return new MvcHtmlString(builder.ToString());
#else
            var file = files.FirstOrDefault();
            
            if (file == null)
                return new MvcHtmlString(string.Empty);

            return new MvcHtmlString(
                generateMarkup(
                    file.Extension,
                    BundleTable.Bundles.ResolveBundleUrl(bundleVirtualPath)));
#endif
        }
    }
}
