using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Ashlands
{
    [Template]
    internal class Meteorite : MineTemplate
    {
        public Meteorite()
        {
            IngameType = typeof(MineRock);
            Matcher = new InstanceNameRegex("MineRock_Meteorite" + CLONE);
            FirstBiome = Biome.ASHLANDS;
            SingleLabel = "Glowing Metal";
            MultipleLabel = "{0} Glowing Metals";
        }
    }
}
