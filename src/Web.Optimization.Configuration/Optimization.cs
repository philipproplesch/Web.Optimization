using System.Configuration;

namespace Web.Optimization.Configuration
{
    public class Optimization
    {
        private static readonly OptimizationSection s_config;

        static Optimization()
        {
            s_config = 
                (OptimizationSection) ConfigurationManager.GetSection("optimization");
        }

        public static OptimizationSection Config
        {
            get
            {
                return s_config;
            }
        }
    }
}