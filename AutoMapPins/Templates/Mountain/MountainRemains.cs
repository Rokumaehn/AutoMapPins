using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mountain
{
    [Template]
    internal class MountainRemains : RemainsTemplate
    {
        public MountainRemains()
        {
            Matcher = new InstanceNameRegex("Pickable_MountainRemains" + DIGITS + "_buried" + CLONE);
            FirstBiome = Biome.MOUNTAINS;
            SingleLabel = "M. Remains";
            MultipleLabel = "{0} M. Remains";
        }
    }
}
