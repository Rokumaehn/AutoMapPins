using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Plains
{
    [Template]
    internal class Cloudberry : BerryTemplate
    {
        public Cloudberry()
        {
            // Stays
            Matcher = new InstanceNameRegex("CloudberryBush" + CLONE);
            FirstBiome = Biome.PLAINS;
            SingleLabel = "Cloudberry";
            MultipleLabel = "{0} Cloudberrys";
        }
    }
}
