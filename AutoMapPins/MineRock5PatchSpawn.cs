using HarmonyLib;
using System.Linq;
using System.Security.Policy;
using UnityEngine;

namespace AutoMapPins
{
    [HarmonyPatch(typeof(MineRock5), "Start")]
    class MineRock5PatchSpawn
    {
        private static readonly PinTemplate[] TEMPLATES = new PinTemplate[]
        {
            Pin.Name("rock\\d+_copper_frac"+Mod.CLONE).Lbl("partial Copper").Nbl(Cat.MINEABLES).Grp(),
            Pin.Name("mudpile_frac"+Mod.CLONE).Lbl("partial Iron").Nbl(Cat.MINEABLES).Grp(),
            Pin.Name("silver(vein)?_frac"+Mod.CLONE).Lbl("partial Silver").Nbl(Cat.MINEABLES).Grp(),

            Pin.Name("rock\\d+_forest_frac"+Mod.CLONE),
            Pin.Name("rock\\d+_mountain_frac"+Mod.CLONE),
        };

        private static void Postfix(ref MineRock5 __instance)
        {
            MineRock5 obj = __instance;
            var template = TEMPLATES.FirstOrDefault(t => t.IsMatch(obj));

            if (template == null)
            {
                Mod.LogUnmatchedName(typeof(MineRock5), obj.name);
                Mod.LogUnmatchedHover(typeof(MineRock5), obj.GetComponent<HoverText>()?.m_text);
            }
            else if (!System.String.IsNullOrWhiteSpace(template.Label))
            {
                var hovertextcomp = obj.GetComponent<HoverText>();
                var height = __instance.gameObject.transform.position.y;

                if (height < Mod.MAX_PIN_HEIGHT)
                {
                    var pin = ((Component)__instance).gameObject.AddComponent<PinnedObject>();
                    pin.Init(template);
                }
            }
        }
    }
}
