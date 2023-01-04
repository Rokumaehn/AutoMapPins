using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.BlackForest
{
    [Template]
    internal class YellowMushroom : BerryTemplate
    {
        public YellowMushroom()
        {
            // Stays
            Matcher = new InstanceNameRegex("Pickable_Mushroom_yellow" + DIGITS_BRACED_OPTIONAL + CLONE);
            FirstBiome = Biome.BLACK_FORESTS;
            SingleLabel = "Yellow Mushroom";
            MultipleLabel = "{0} Yellow Mushrooms";
        }
    }
}
