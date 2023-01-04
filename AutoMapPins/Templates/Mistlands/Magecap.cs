using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mistlands
{
    [Template]
    internal class Magecap : MushroomTemplate
    {
        public Magecap()
        {
            // Stays
            Matcher = new InstanceNameRegex("Pickable_Mushroom_Magecap" + CLONE);
            FirstBiome = Biome.MISTLANDS;
            SingleLabel = "Magecap";
            MultipleLabel = "{0} Magecaps";
        }
    }
}
