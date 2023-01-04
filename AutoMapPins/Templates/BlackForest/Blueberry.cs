using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.BlackForest
{
    [Template]
    internal class Blueberry : BerryTemplate
    {
        public Blueberry()
        {
            // Stays
            Matcher = new InstanceNameRegex("BlueberryBush" + CLONE);
            FirstBiome = Biome.BLACK_FORESTS;
            SingleLabel = "Blueberry";
            MultipleLabel = "{0} Blueberries";
        }
    }
}
