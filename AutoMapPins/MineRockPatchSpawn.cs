using AutoMapPins.Templates;
using HarmonyLib;
using System.Linq;
using System.Security.Policy;
using UnityEngine;

namespace AutoMapPins
{
    [HarmonyPatch(typeof(MineRock), "Start")]
    class MineRockPatchSpawn
    {
        private static readonly PinTemplate[] TEMPLATES = new PinTemplate[]
        {
        };

        private static void Postfix(ref MineRock __instance)
        {
            MineRock obj = __instance;
            var template = TEMPLATES.FirstOrDefault(t => t.IsMatch(obj));
            if (template == null) template = TemplateRegistry.Find(__instance);

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
                    pin.Init(template);
                }
            }
        }
    }
}
