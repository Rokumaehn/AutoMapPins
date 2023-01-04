using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.BlackForest
{
    [Template]
    internal class BurialChambers : DungeonTemplate
    {
        public BurialChambers()
        {
            Matcher = new InstanceNameRegex("^Crypt" + DIGITS + CLONE);
            FirstBiome = Biome.BLACK_FORESTS;
            SingleLabel = "Burial Chambers";
            MultipleLabel = "{0} Burial Chambers";
        }
    }
}
