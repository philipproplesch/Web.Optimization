using System.Collections.Generic;

namespace Web.Optimization.Common
{
    public class FileTypes
    {
        public static IEnumerable<string> ScriptExtensions =
            new[]
                {
                    ".JS",
                    ".COFFEE",
                };

        public static IEnumerable<string> StyleExtensions =
            new[]
                {
                    ".CSS",
                    ".LESS",
                    ".SASS",
                    ".SCSS",
                };
    }
}
