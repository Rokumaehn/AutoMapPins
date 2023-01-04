using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.BlackForest
{
    [Template]
    internal class Copper : MineTemplate
    {
        public Copper()
        {
            Matcher = new HoverTextRegex("\\$piece_deposit_copper");
            FirstBiome = Biome.BLACK_FORESTS;
            SingleLabel = "Copper";
            MultipleLabel = "{0} Copper";
        }
    }

    [Template]
    internal class PartialCopper : MineTemplate
    {
        public PartialCopper()
        {
            IngameType = typeof(MineRock5);
            Matcher = new InstanceNameRegex("rock\\d+_copper_frac" + CLONE);
            FirstBiome = Biome.BLACK_FORESTS;
            SingleLabel = "Partial Copper";
            MultipleLabel = "{0} Partial Copper";
        }
    }
}
