using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using Newtonsoft.Json;

namespace GTA.Server.Stripper.Services
{
    public class EsxService : BaseScript
    {
        public static dynamic ESX = null;
        public EsxService()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            while (ESX == null)
            {
                TriggerEvent("esx:getSharedObject", new object[] { new Action<dynamic>(esx => {
                    ESX = esx;
                    if (ESX != null)
                    {
                        Debug.WriteLine($"ESX IS NOT NULL SERVER : {esx != null}");
                    }
                    
                })});
                await Delay(500);
            }
        }
    }
}
