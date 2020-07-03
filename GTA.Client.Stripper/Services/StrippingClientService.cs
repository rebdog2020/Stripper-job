using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using GTA.Client.Stripper.Models;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;

namespace GTA.Client.Stripper.Services
{
    public class StrippingClientService : BaseScript
    {
        private Config _config;
        private bool _isStripping = false;
        private DateTime _payDay;
        public StrippingClientService()
        {
            try
            {
                _config = ConfigService.LoadConfig<Config>("GTA.Client.Stripper", "Config.json", "CLIENTSTRIPPER");

                if (_config.Debug)
                {
                    Debug.WriteLine($"Config was loaded with {JsonConvert.SerializeObject(_config)}");
                }


            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error loading config {e.Message}");
            }

            Tick += StrippingClientService_Tick;
        }

        private async Task StrippingClientService_Tick()
        {
            if (IsPedNearStrippingLocation())
            {
                _isStripping = IsEntityPlayingAnim(GetPlayerPed(-1), "mini@strip_club@private_dance@part2", "priv_dance_p2", 3);
                if (_isStripping)
                {
                    if (DateTime.Now >= _payDay)
                    {
                        TriggerServerEvent("stripperPayment", GetPlayerServerId(NetworkGetEntityOwner(GetPlayerPed(-1))), new Action<int>((x) =>
                        {
                            EsxService.ESX.ShowNotification($"You just got tipped ${x}");
                        }));
                        _payDay = DateTime.Now.AddSeconds(_config.TimeBetweenTick);
                    }
                }
                if (Game.IsControlJustPressed(0, Control.Pickup) || Game.IsControlJustPressed(0, Control.Context))
                {
                    _payDay = DateTime.Now.AddSeconds(_config.TimeBetweenTick);
                    await PlayEmote();
                }
            }

        }

        private bool IsPedNearStrippingLocation()
        {
            var playerLoc = Game.PlayerPed.Position;
            foreach (var strippingLoc in _config.StrippingLocations)
            {
                if (GetDistanceBetweenCoords(playerLoc.X, playerLoc.Y, playerLoc.Z, strippingLoc.X, strippingLoc.Y, strippingLoc.Z, false) <= 3)
                {
                    return true;
                }
            }

            return false;
        }

        async Task PlayEmote()
        {
            while (!HasAnimDictLoaded("mini@strip_club@private_dance@part2"))
            {
                RequestAnimDict("mini@strip_club@private_dance@part2");
                await Delay(10);
            }

            TaskPlayAnim(GetPlayerPed(-1), "mini@strip_club@private_dance@part2", "priv_dance_p2", 2.0F, 2.0F, -1, 1, 0,
                false, false, false);
        }
    }
}
