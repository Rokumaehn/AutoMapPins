using AutoMapPins.Templates;
using HarmonyLib;
using System.Linq;
using System.Security.Policy;
using UnityEngine;

namespace AutoMapPins
{
    [HarmonyPatch(typeof(Pickable), "Awake")]
    class PickablePatchSpawn
    {
        private static readonly PinTemplate[] TEMPLATES = new PinTemplate[] {
            Pin.Name("Pickable_Flint" + Mod.CLONE),
            Pin.Name("Pickable_Stone" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("Pickable_Branch" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),

            Pin.Name("Pickable_SurtlingCoreStand" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE).Lbl("Surtling Core").Grp(), // stays, placed > 5000

            Pin.Name("Pickable_MeatPile" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)                                      // stays, placed > 5000
                    .Lbl("Meat Pile").Nbl(Cat.HARVESTABLES).Grp(),
            Pin.Name("Pickable_MountainCaveCrystal" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)                           // stays, placed > 5000
                    .Lbl("Cave Crystal").Nbl(Cat.HARVESTABLES).Grp(),

            Pin.Name("Pickable_RoyalJelly" + Mod.CLONE).Lbl("Royal Jelly").Nbl(Cat.HARVESTABLES).Grp(),                 // disappears, placed > 5000
            Pin.Name("Pickable_BlackCoreStand" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)                                // stays, placed > 5000
                    .Lbl("Black Core").Nbl(Cat.HARVESTABLES).Grp(),
            
            Pin.Name("Pickable_Fishingrod" + Mod.CLONE),
        };

        private static void Postfix(ref Pickable __instance)
        {
            Pickable obj = __instance;
            var template = TEMPLATES.FirstOrDefault(t => t.IsMatch(obj));
            if (template == null) template = TemplateRegistry.Find(__instance);

            if (template == null)
            {
                Mod.LogUnmatchedName(typeof(Pickable), obj.name);
                Mod.LogUnmatchedHover(typeof(Pickable), obj.GetComponent<HoverText>()?.m_text);
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