using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Plains
{
    [Template]
    internal class Barley : SeedTemplate
    {
        public Barley()
        {
            // Disappears
            // Overlaps with Pickable
            Matcher = new InstanceNameRegex("Pickable_Barley_Wild" + DIGITS_BRACED_OPTIONAL + CLONE);
            FirstBiome = Biome.PLAINS;
            SingleLabel = "Barley";
            MultipleLabel = "{0} Barley";
        }
    }
}
