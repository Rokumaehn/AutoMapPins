using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mistlands
{
    [Template]
    internal class DvergrTreasure : PickableTemplate
    {
        public DvergrTreasure()
        {
            Matcher = new InstanceNameRegex("Pickable_DvergrMineTreasure" + DIGITS_BRACED_OPTIONAL + CLONE);
            FirstBiome = Biome.MISTLANDS;
            SingleLabel = "Dvergr Treasure";
            MultipleLabel = "{0} Dvergr Treasures";
        }
    }
}
