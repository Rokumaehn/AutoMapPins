using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Meadows
{
    [Template]
    internal class Raspberry : BerryTemplate
    {
        public Raspberry()
        {
            // Stays
            Matcher = new InstanceNameRegex("RaspberryBush" + CLONE);
            FirstBiome = Biome.MEADOWS;
            SingleLabel = "Raspberry";
            MultipleLabel = "{0} Raspberries";
        }
    }
}
