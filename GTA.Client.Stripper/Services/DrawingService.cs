using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using GTA.Server.Stripper.Models;
using static CitizenFX.Core.Native.API;

namespace GTA.Client.Stripper.Services
{
    public class DrawingService: BaseScript
    {
        private Config _config;

        public DrawingService()
        {
            try
            {
                _config = ConfigService.LoadConfig<Config>("GTA.Client.Stripper", "Config.json", "CLIENTSTRIPPER");
                Tick += OnTick;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error loading config - Drawing service {e.Message}");
            }
           
        }

        private async Task OnTick()
        {
            DrawStripperLocations();
           await Task.FromResult(0);
        }

        private void DrawStripperLocations()
        {
            try
            {
                foreach (var loc in _config.StrippingLocations)
                {
                    DrawMarker(25, loc.X, loc.Y, loc.Z, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 1.001F, 1.001F, 0.5001F, 255, 100, 100, 200,
                        false, false, 2, false, null, null, false);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error placing marker");
            }
            
        }
    }
}
