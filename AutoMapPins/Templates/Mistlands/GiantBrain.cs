using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mistlands
{
    [Template]
    internal class GiantBrain : MineTemplate
    {
        public GiantBrain()
        {
            Matcher = new HoverTextRegex("\\$piece_giant_brain");
            FirstBiome = Biome.MISTLANDS;
            SingleLabel = "Giant Brain";
            MultipleLabel = "{0} Giant Brains";
        }
    }
}
