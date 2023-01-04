using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mountain
{
    [Template]
    internal class Silver : MineTemplate
    {
        public Silver()
        {
            Matcher = new HoverTextRegex("\\$piece_deposit_silver(vein)?");
            FirstBiome = Biome.MOUNTAINS;
            SingleLabel = "Silver";
            MultipleLabel = "{0} Silver";
        }
    }

    [Template]
    internal class PartialSilver : MineTemplate
    {
        public PartialSilver()
        {
            IngameType = typeof(MineRock5);
            Matcher = new InstanceNameRegex("silver(vein)?_frac" + CLONE);
            FirstBiome = Biome.MOUNTAINS;
            SingleLabel = "Partial Silver";
            MultipleLabel = "{0} Partial Silver";
        }
    }
}
