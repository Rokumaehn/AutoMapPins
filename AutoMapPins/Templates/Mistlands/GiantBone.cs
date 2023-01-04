using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mistlands
{
    [Template]
    internal class GiantBone : MineTemplate
    {
        public GiantBone()
        {
            Matcher = new HoverTextRegex("\\$piece_giant_bone");
            FirstBiome = Biome.MISTLANDS;
            SingleLabel = "Giant Bone";
            MultipleLabel = "{0} Giant Bones";
        }
    }
}
