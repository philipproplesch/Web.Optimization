using System.Web;

namespace Web.Optimization.Extensions
{
    public static class StringExtensions
    {
         public static string ToVirtualPath(this string instance)
         {
             return
                 instance
                     .Replace(HttpRuntime.AppDomainAppPath, "~/")
                     .Replace("\\", "/");
         }
    }
}