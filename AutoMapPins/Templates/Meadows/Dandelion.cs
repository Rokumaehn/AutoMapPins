using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Meadows
{
    [Template]
    internal class Dandelion : FlowerTemplate
    {
        public Dandelion()
        {
            // Stays
            Matcher = new InstanceNameRegex("Pickable_Dandelion" + DIGITS_BRACED_OPTIONAL + CLONE);
            FirstBiome = Biome.MEADOWS;
            SingleLabel = "Dandelion";
            MultipleLabel = "{0} Dandelions";
        }
    }
}
