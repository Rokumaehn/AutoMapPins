using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.BlackForest
{
    [Template]
    internal class Thistle : FlowerTemplate
    {
        public Thistle()
        {
            // Stays
            Matcher = new InstanceNameRegex("Pickable_Thistle" + CLONE);
            FirstBiome = Biome.BLACK_FORESTS;
            SingleLabel = "Thistle";
            MultipleLabel = "{0} Thistles";
        }
    }
}
