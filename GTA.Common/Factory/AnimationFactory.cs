using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA.Common.Models;
using GTA.Server.Stripper.Models;

namespace GTA.Common.Factory
{
    public class AnimationFactory : IAnimationFactory   
    {
        private readonly Config _config;

        public AnimationFactory(Config config)
        {
            _config = config;
        }
        public IEnumerable<Animation> GetAnimations(string dictionary)
        {
            return _config.Animations.FirstOrDefault(x => x.DictionaryName == dictionary)?.Animations ?? new List<Animation>();
        }
    }
}
