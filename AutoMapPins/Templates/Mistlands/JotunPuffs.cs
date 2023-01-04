using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mistlands
{
    [Template]
    internal class JotunPuffs : MushroomTemplate
    {
        public JotunPuffs()
        {
            // Stays
            Matcher = new InstanceNameRegex("Pickable_Mushroom_JotunPuffs" + CLONE);
            FirstBiome = Biome.MISTLANDS;
            SingleLabel = "Jotun Puffs";
            MultipleLabel = "{0} Jotun Puffs";
        }
    }
}
