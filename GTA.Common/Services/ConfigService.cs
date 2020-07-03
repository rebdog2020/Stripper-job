using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;

namespace GTA.Client.Stripper.Services
{
    public static class ConfigService
    {
        private static readonly Dictionary<string, object> ConfigCache = new Dictionary<string, object>();

        public static T LoadConfig<T>(string resourceName, string filename, string key)
        {
            if (ConfigCache.ContainsKey(key))
            {
                return (T) ConfigCache[key];
            }
            else
            {
                var resourceData = LoadResourceFile(resourceName, filename);
                Debug.WriteLine($"Resource data : {resourceData}");
                var config = JsonConvert.DeserializeObject<T>(resourceData);
                ConfigCache.Add(key,config);
                return config;
            }
        }
    }
}
