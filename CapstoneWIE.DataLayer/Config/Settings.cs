using System.Configuration;

namespace CapstoneWIE.DataLayer.Config
{
    public static class Settings
    {
        private static string _connectionString;

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                    _connectionString = ConfigurationManager.ConnectionStrings["CapstoneDB"].ConnectionString;

                return _connectionString;
            }
        }
    }
}