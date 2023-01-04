using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Plains
{
    [Template]
    internal class Tar : PickableTemplate
    {
        public Tar()
        {
            // Disappears
            Matcher = new InstanceNameRegex("Pickable_Tar(Big)?" + DIGITS_BRACED_OPTIONAL + CLONE);
            FirstBiome = Biome.PLAINS;
            SingleLabel = "Tar";
            MultipleLabel = "{0} Tar";
        }
    }
}
