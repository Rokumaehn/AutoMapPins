using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mountain
{
    [Template]
    internal class DragonEgg : PickableTemplate
    {
        public DragonEgg()
        {
            // Stays
            Matcher = new InstanceNameRegex("Pickable_DragonEgg" + CLONE);
            FirstBiome = Biome.MOUNTAINS;
            SingleLabel = "Dragon Egg";
            MultipleLabel = "{0} Dragon Eggs";
        }
    }
}
