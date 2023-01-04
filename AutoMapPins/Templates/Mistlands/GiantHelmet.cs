using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mistlands
{
    [Template]
    internal class GiantHelmet : MineTemplate
    {
        public GiantHelmet()
        {
            Matcher = new HoverTextRegex("\\$piece_giant_helmet");
            FirstBiome = Biome.MISTLANDS;
            SingleLabel = "Giant Helmet";
            MultipleLabel = "{0} Giant Helmets";
        }
    }
}
