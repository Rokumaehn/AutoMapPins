using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mountain
{
    [Template]
    internal class MountainCave : DungeonTemplate
    {
        public MountainCave()
        {
            Matcher = new InstanceNameRegex("MountainCave" + DIGITS + CLONE);
            FirstBiome = Biome.MOUNTAINS;
            SingleLabel = "Mountain Cave";
            MultipleLabel = "{0} Mountain Caves";
        }
    }
}
