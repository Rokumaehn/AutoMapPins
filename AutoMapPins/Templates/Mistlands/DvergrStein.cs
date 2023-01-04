using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mistlands
{
    [Template]
    internal class DvergrStein : PickableTemplate
    {
        public DvergrStein()
        {
            Matcher = new InstanceNameRegex("Pickable_DvergrStein" + CLONE);
            FirstBiome = Biome.MISTLANDS;
            SingleLabel = "Dvergr Stein";
            MultipleLabel = "{0} Dvergr Stein";
        }
    }
}
