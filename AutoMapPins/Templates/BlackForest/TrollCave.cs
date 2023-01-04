using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.BlackForest
{
    [Template]
    internal class TrollCave : DungeonTemplate
    {
        public TrollCave()
        {
            Matcher = new InstanceNameRegex("TrollCave" + DIGITS + CLONE);
            FirstBiome = Biome.BLACK_FORESTS;
            SingleLabel = "Troll Cave";
            MultipleLabel = "{0} Troll Caves";
        }
    }
}
