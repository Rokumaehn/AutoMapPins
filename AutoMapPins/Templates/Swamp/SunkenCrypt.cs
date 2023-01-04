using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Swamp
{
    [Template]
    internal class SunkenCrypt : DungeonTemplate
    {
        public SunkenCrypt()
        {
            Matcher = new InstanceNameRegex("SunkenCrypt" + DIGITS + CLONE);
            FirstBiome = Biome.SWAMPS;
            SingleLabel = "Sunken Crypt";
            MultipleLabel = "{0} Sunken Crypts";
        }
    }
}
