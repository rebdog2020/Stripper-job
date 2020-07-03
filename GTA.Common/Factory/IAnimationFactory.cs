using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA.Common.Models;

namespace GTA.Common.Factory
{
    public interface IAnimationFactory
    {
        IEnumerable<Animation> GetAnimations(string dictionary);
    }
}
