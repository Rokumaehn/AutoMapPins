using HarmonyLib;
using System.Linq;
using System.Security.Policy;
using UnityEngine;

namespace AutoMapPins
{
    [HarmonyPatch(typeof(MineRock), "Start")]
    class MineRockPatchSpawn
    {
        private static readonly PinTemplate<MineRock>[] TEMPLATES = new PinTemplate<MineRock>[]
        {
            Pin.Make(Pin.Name<MineRock>("Leviathan" + Mod.CLONE), "Leviathan", Mod.CATEGORY_MINEABLES), // correct
            Pin.Make(Pin.Name<MineRock>("MineRock_Meteorite" + Mod.CLONE), "Glowing Metal", Mod.CATEGORY_MINEABLES), //correct
        };

        private static void Postfix(ref MineRock __instance)
        {
            MineRock obj = __instance;
            var template = TEMPLATES.FirstOrDefault(t => t.IsMatch(obj));

            if (template == null)
            {
                Mod.LogUnmatchedName(typeof(MineRock), obj.name);
                Mod.LogUnmatchedHover(typeof(MineRock), obj.GetComponent<HoverText>()?.m_text);
            }
            else if (!System.String.IsNullOrWhiteSpace(template.Label))
            {
                var hovertextcomp = obj.GetComponent<HoverText>();
                var height = __instance.gameObject.transform.position.y;

                if (height < Mod.MAX_PIN_HEIGHT)
                {
                    var pin = ((Component)__instance).gameObject.AddComponent<PinnedObject>();
                    pin.Init(template.Label, template.EnabledBy);
                }
            }
        }
    }
}
