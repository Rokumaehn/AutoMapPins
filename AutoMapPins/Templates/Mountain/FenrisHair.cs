using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mountain
{
    [Template]
    internal class FenrisHair : PickableTemplate
    {
        public FenrisHair()
        {
            // Stays
            Matcher = new InstanceNameRegex("hanging_hairstrands" + DIGITS_BRACED_OPTIONAL + CLONE);
            FirstBiome = Biome.MOUNTAINS;
            SingleLabel = "Fenris Hairstrand";
            MultipleLabel = "{0} Fenris Hairstrands";
        }
    }
}
