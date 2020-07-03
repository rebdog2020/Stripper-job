using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace GTA.Client.Stripper.Services
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
                    Debug.WriteLine($"ESX {esx != null}");
                })});
                await Delay(500);
            }

        }
    }
}
