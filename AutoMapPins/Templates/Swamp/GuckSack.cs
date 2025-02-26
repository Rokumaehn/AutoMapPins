﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Swamp
{
    [Template]
    internal class GuckSack : AxeTemplate
    {
        public GuckSack()
        {
            // Via Axe and Pickaxe
            // Disappears
            Matcher = new HoverTextRegex("\\$item_destructible_gucksack");
            FirstBiome = Biome.SWAMPS;
            SingleLabel = "Guck Sack";
            MultipleLabel = "{0} Guck Sacks";
        }
    }
}
