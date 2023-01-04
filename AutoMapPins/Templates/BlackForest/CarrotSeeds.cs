using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.BlackForest
{
    [Template]
    internal class CarrotSeeds : SeedTemplate
    {
        public CarrotSeeds()
        {
            // Disappears
            // Overlaps with Destructable
            Matcher = new InstanceNameRegex("Pickable_SeedCarrot" + CLONE);
            FirstBiome = Biome.BLACK_FORESTS;
            SingleLabel = "Carrot Seeds";
            MultipleLabel = "{0} Carrot Seeds";
        }
    }
}
