using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService
{
    public static class Helper
    {
        private static IReadOnlyDictionary<string, string> _settings = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>
        {
            { "ConnectionString", "Data Source=DESKTOP-D4KP4PD;Initial Catalog=FoodService;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"}
        });

        private static string GetSetting(string key)
        {
            try
            {
                return _settings[key];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
        }

        public static string GetConnectionString()
        {
            return GetSetting("ConnectionString");
        }
    }
}
