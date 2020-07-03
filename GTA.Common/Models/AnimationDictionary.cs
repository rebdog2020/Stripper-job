using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace GTA.Common.Models
{
    public class AnimationDictionary
    {
        public string DictionaryName { get; set; }
        public IEnumerable<Animation> Animations { get; set; }
    }
}
