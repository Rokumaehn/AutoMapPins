using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Plains
{
    [Template]
    internal class Flax : PickableTemplate
    {
        public Flax()
        {
            // Disappears
            // Overlaps with Destructable
            Matcher = new InstanceNameRegex("Pickable_Flax_Wild" + DIGITS_BRACED_OPTIONAL + CLONE);
            FirstBiome = Biome.PLAINS;
            SingleLabel = "Flax";
            MultipleLabel = "{0} Flax";
        }
    }
}
