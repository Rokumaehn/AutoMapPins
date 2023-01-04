using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mountain
{
    [Template]
    internal class Obsidian : MineTemplate
    {
        public Obsidian()
        {
            GroupingDistance = DISTANCE_OBSIDIAN;
            Matcher = new HoverTextRegex("\\$piece_deposit_obsidian");
            FirstBiome = Biome.MOUNTAINS;
            SingleLabel = "Obsidian";
            MultipleLabel = "{0} Obsidian";
        }
    }
}
