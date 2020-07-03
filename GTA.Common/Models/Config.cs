using System.Collections.Generic;
using CitizenFX.Core;
using GTA.Common.Models;

namespace GTA.Server.Stripper.Models
{
    public class Config
    {
        public int MoneyPerTick { get; set; }
        public int TimeBetweenTick { get; set; }
        public bool Debug { get; set; }
        public List<Vector3> StrippingLocations { get; set; }
        public List<AnimationDictionary> Animations { get; set; }
        public int AnimationSwitch { get; set; }
    }
}
