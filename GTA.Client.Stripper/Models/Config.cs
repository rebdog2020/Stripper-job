using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace GTA.Client.Stripper.Models
{
    public class Config
    {
        public int MoneyPerTick { get; set; }
        public int TimeBetweenTick { get; set; }
        public bool Debug { get; set; }
        public List<Vector3> StrippingLocations { get; set; }
    }
}
