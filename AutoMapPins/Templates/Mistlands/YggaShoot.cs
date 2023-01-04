using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates.Mistlands
{
    [Template]
    internal class YggaShoot : AxeTemplate
    {
        public YggaShoot()
        {
            Matcher = new HoverTextRegex("\\$prop_yggashoot");
            FirstBiome = Biome.MISTLANDS;
            SingleLabel = "Yggdrasil Shoot";
            MultipleLabel = "{0} Yggdrasil Shoots";
        }
    }
}
