using System.Collections.Generic;
using CitizenFX.Core;

namespace GTA.Server.Stripper.Models
{
    public class Config
    {
        public int MoneyPerTick { get; set; }
        public int TimeBetweenTick { get; set; }
        public bool Debug { get; set; }
        public List<Vector3> StrippingLocations { get; set; }
    }
}
