using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mistlands
{
    [Template]
    internal class DvergrLantern : PickableTemplate
    {
        public DvergrLantern()
        {
            // Disappears
            Matcher = new InstanceNameRegex("Pickable_DvergrLantern" + DIGITS_BRACED_OPTIONAL + CLONE);
            FirstBiome = Biome.MISTLANDS;
            SingleLabel = "Lantern";
            MultipleLabel = "{0} Lanterns";
        }
    }
}
