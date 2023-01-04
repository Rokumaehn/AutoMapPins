using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Ocean
{
    [Template]
    internal class Leviathan : MineTemplate
    {
        public Leviathan()
        {
            // Stays
            IngameType = typeof(MineRock);
            Matcher = new InstanceNameRegex("Leviathan" + CLONE);
            FirstBiome = Biome.OCEANS;
            SingleLabel = "Leviathan";
            MultipleLabel = "{0} Leviathans";
        }
    }
}
