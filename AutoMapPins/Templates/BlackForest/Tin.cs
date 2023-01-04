using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.BlackForest
{
    [Template]
    internal class Tin : MineTemplate
    {
        public Tin()
        {
            Matcher = new HoverTextRegex("\\$piece_deposit_tin");
            FirstBiome = Biome.BLACK_FORESTS;
            SingleLabel = "Tin";
            MultipleLabel = "{0} Tin";
        }
    }
}
