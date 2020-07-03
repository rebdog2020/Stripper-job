using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using GTA.Common.Factory;
using GTA.Common.Models;
using GTA.Server.Stripper.Models;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;

namespace GTA.Client.Stripper.Services
{
    public class StrippingClientService : BaseScript
    {
        private Config _config;
        private bool _isStripping = false;
        private DateTime _payDay;
        private IAnimationFactory _animationFactory;
        private DateTime _animationSwitch;
        private Animation randomAnimation;
        private AnimationDictionary animationDictionary;
        private bool isNearStrippingSpot = false;
        public StrippingClientService()
        {
            try
            {
                _config = ConfigService.LoadConfig<Config>("GTA.Client.Stripper", "Config.json", "CLIENTSTRIPPER");
                //TODO implement IOC
                _animationFactory = new AnimationFactory(_config);

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

                if (Game.IsControlJustPressed(0, Control.Pickup) || Game.IsControlJustPressed(0, Control.Context) && !_isStripping)
                {
                    _payDay = DateTime.Now.AddSeconds(_config.TimeBetweenTick);
                    _animationSwitch = DateTime.Now.AddSeconds(_config.AnimationSwitch);
                    _isStripping = true;
                    await PlayEmote();
                }
                // check animation still playing
                _isStripping = IsEntityPlayingAnim(GetPlayerPed(-1), animationDictionary?.DictionaryName, randomAnimation?.AnimationName, 3);
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
            }

        }

        private bool IsPedNearStrippingLocation()
        {
            var playerLoc = Game.PlayerPed.Position;
            foreach (var strippingLoc in _config.StrippingLocations)
            {
                if (GetDistanceBetweenCoords(playerLoc.X, playerLoc.Y, playerLoc.Z, strippingLoc.X, strippingLoc.Y, strippingLoc.Z, false) <= 1)
                {
                    if (!isNearStrippingSpot)
                    {
                        EsxService.ESX.ShowNotification($"Press 'E' to start stripping");
                        isNearStrippingSpot = true;
                    }
                    
                    return true;
                }
            }

            isNearStrippingSpot = false;
            return false;
        }

        async Task PlayEmote()
        {
            animationDictionary = (_config.Animations.ToArray()[new Random().Next(0, _config.Animations.Count - 1)]);
            while (!HasAnimDictLoaded(animationDictionary.DictionaryName))
            {
                RequestAnimDict(animationDictionary.DictionaryName);
                await Delay(10);
            }

            randomAnimation = animationDictionary.Animations.ToArray()[new Random().Next(0, _config.Animations.Count - 1)];
            Debug.WriteLine($"Playing animation : {randomAnimation.AnimationName}");
            TaskPlayAnim(GetPlayerPed(-1), animationDictionary.DictionaryName, randomAnimation.AnimationName, 2.0F, 2.0F, -1, 1, 0,
                false, false, false);
        }

    }
}
