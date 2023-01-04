using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mistlands
{
    [Template]
    internal class InfestedMine : DungeonTemplate
    {
        public InfestedMine()
        {
            Matcher = new InstanceNameRegex("Mistlands_DvergrTownEntrance" + DIGITS + CLONE);
            FirstBiome = Biome.MISTLANDS;
            SingleLabel = "Infested Mine";
            MultipleLabel = "{0} Infested Mines";
        }
    }
}
