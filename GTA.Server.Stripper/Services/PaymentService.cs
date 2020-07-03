using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using GTA.Client.Stripper.Services;
using GTA.Server.Stripper.Models;
using static CitizenFX.Core.Native.API;

namespace GTA.Server.Stripper.Services
{
    public class PaymentService : BaseScript
    {
        private Config _config;
        public PaymentService()
        {
             _config = ConfigService.LoadConfig<Config>("GTA.Client.Stripper", "Config.json", "CLIENTSTRIPPER");
            EventHandlers["stripperPayment"] += new Action<Player,int, NetworkCallbackDelegate>(OnEvent);
        }

        private void OnEvent([FromSource] Player ped,int serverId, NetworkCallbackDelegate dCallbackDelegate)
        {
            try
            {
                var xPlayer = EsxService.ESX.GetPlayerFromId(serverId);
                xPlayer.addMoney(_config.MoneyPerTick);
                dCallbackDelegate.Invoke(_config.MoneyPerTick);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error paying {e.Message}");
            }
           
        }
    }
}
