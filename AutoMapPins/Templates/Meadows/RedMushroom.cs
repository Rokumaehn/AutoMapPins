using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Meadows
{
    [Template]
    internal class RedMushroom : MushroomTemplate
    {
        public RedMushroom()
        {
            // Stays
            Matcher = new InstanceNameRegex("Pickable_Mushroom" + DIGITS_BRACED_OPTIONAL + CLONE);
            FirstBiome = Biome.MEADOWS;
            SingleLabel = "Mushroom";
            MultipleLabel = "{0} Mushrooms";
        }
    }
}
