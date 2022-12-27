using HarmonyLib;
using System.Linq;
using System.Security.Policy;
using UnityEngine;

namespace AutoMapPins
{
    [HarmonyPatch(typeof(Pickable), "Awake")]
    class PickablePatchSpawn
    {
        private static readonly PinTemplate<Pickable>[] TEMPLATES = new PinTemplate<Pickable>[] {
            Pin.Make(Pin.Name<Pickable>("Pickable_Flint" + Mod.CLONE)),
            Pin.Make(Pin.Name<Pickable>("Pickable_Stone" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Pickable>("Pickable_Branch" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Pickable>("Pickable_Mushroom" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Mushroom"),            // stays
            Pin.Make(Pin.Name<Pickable>("Pickable_Dandelion" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Dandelion"),                       // stays
            Pin.Make(Pin.Name<Pickable>("Pickable_ForestCryptRemains" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "F. Remains"),// stays
            Pin.Make(Pin.Name<Pickable>("RaspberryBush" + Mod.CLONE), "Raspberries"),                                                       // stays

            Pin.Make(Pin.Name<Pickable>("Pickable_Thistle" + Mod.CLONE), "Thistle"),                                                        // stays
            Pin.Make(Pin.Name<Pickable>("Pickable_Mushroom_yellow" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Yellow Mushroom"),           // stays, placed > 5000
            Pin.Make(Pin.Name<Pickable>("Pickable_SurtlingCoreStand" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Surtling Core"),           // stays, placed > 5000
            Pin.Make(Pin.Name<Pickable>("Pickable_SeedCarrot" + Mod.CLONE), "Carrot Seeds"),                                                // disappears, overlaps with Destructible
            Pin.Make(Pin.Name<Pickable>("BlueberryBush" + Mod.CLONE), "Blueberries"),                                                       // stays

            Pin.Make(Pin.Name<Pickable>("Pickable_SeedTurnip" + Mod.CLONE), "Turnip Seeds"),                                                // disappears, overlaps with Destructible

            Pin.Make(Pin.Name<Pickable>("Pickable_MeatPile" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Meat Pile"),                        // stays, placed > 5000
            Pin.Make(Pin.Name<Pickable>("Pickable_MountainCaveCrystal" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Cave Crystal"),          // stays, placed > 5000
            Pin.Make(Pin.Name<Pickable>("Pickable_DragonEgg" + Mod.CLONE), "Dragon Egg"),                                                   // stays
            Pin.Make(Pin.Name<Pickable>("Pickable_MountainRemains" + Mod.DIGITS + "_buried" + Mod.CLONE), "M. Remains"),
            Pin.Make(Pin.Name<Pickable>("hanging_hairstrands" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Fenris Hair"),                    // stays

            Pin.Make(Pin.Name<Pickable>("Pickable_Flax_Wild" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Flax"),                            // disappears, overlaps with Destructible
            Pin.Make(Pin.Name<Pickable>("Pickable_Barley_Wild" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Barley"),                        // disappears, overlaps with Destructible
            Pin.Make(Pin.Name<Pickable>("Pickable_Tar(Big)?" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Tar"),                             // disappears
            Pin.Make(Pin.Name<Pickable>("CloudberryBush" + Mod.CLONE), "Cloudberries"),                                                     // stays
            Pin.Make(Pin.Name<Pickable>("goblin_totempole" + Mod.CLONE), "Fuling Totem"),                                                   // stays 

            Pin.Make(Pin.Name<Pickable>("Pickable_Mushroom_Magecap" + Mod.CLONE), "Magecap"),                                               // stays
            Pin.Make(Pin.Name<Pickable>("Pickable_Mushroom_JotunPuffs" + Mod.CLONE), "Jotun Puffs"),                                        // stays
            Pin.Make(Pin.Name<Pickable>("Pickable_DvergrLantern" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Lantern"),                     // disappears
            Pin.Make(Pin.Name<Pickable>("Pickable_DvergrStein" + Mod.CLONE), "Stein"),
            Pin.Make(Pin.Name<Pickable>("Pickable_RoyalJelly" + Mod.CLONE), "Royal Jelly"),                                                 // disappears, placed > 5000
            Pin.Make(Pin.Name<Pickable>("Pickable_BlackCoreStand" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Black Core"),                 // stays, placed > 5000
            Pin.Make(Pin.Name<Pickable>("Pickable_DvergrMineTreasure" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Dvergr Treasure"),

            Pin.Make(Pin.Name<Pickable>("Pickable_Fishingrod" + Mod.CLONE)),


        };

        private static void Postfix(ref Pickable __instance)
        {
            Pickable obj = __instance;
            var template = TEMPLATES.FirstOrDefault(t => t.IsMatch(obj));

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
                    pin.Init(template.Label, template.EnabledBy);
                }
            }
        }
    }
}