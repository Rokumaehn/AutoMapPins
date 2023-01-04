using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Meadows
{
    [Template]
    internal class ForestRemains : RemainsTemplate
    {
        public ForestRemains()
        {
            // Stays
            Matcher = new InstanceNameRegex("Pickable_ForestCryptRemains" + DIGITS + DIGITS_BRACED_OPTIONAL + CLONE);
            FirstBiome = Biome.MEADOWS;
            SingleLabel = "F. Remains";
            MultipleLabel = "{0} F. Remains";
        }
    }
}
