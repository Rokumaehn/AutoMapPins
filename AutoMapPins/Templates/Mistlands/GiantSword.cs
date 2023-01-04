using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mistlands
{
    [Template]
    internal class GiantSword : MineTemplate
    {
        public GiantSword()
        {
            Matcher = new HoverTextRegex("\\$piece_giant_sword");
            FirstBiome = Biome.MISTLANDS;
            SingleLabel = "Giant Sword";
            MultipleLabel = "{0} Giant Swords";
        }
    }
}
