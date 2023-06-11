using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieModel.Config
{
    public static class ConnectionConfig
    {
        public static string GetConnectionString()
        {
            string jsonFilePath = "appsettings.json";
            string jsonString = File.ReadAllText(jsonFilePath);
            dynamic json = JsonConvert.DeserializeObject(jsonString);
            return json.ConnectionString;
        }
    }
}
