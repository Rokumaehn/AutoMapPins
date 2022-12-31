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

            Pin.Name("Pickable_Mushroom" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)                                      // stays
                    .Lbl("Mushroom").Nbl(Cat.HARVESTABLES).Grp(),
            Pin.Name("Pickable_Dandelion" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)                                     // stays
                    .Lbl("Dandelion").Nbl(Cat.FLOWERS).Grp(),
            Pin.Name("Pickable_ForestCryptRemains" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)               // stays
                    .Lbl("F. Remains").Nbl(Cat.HARVESTABLES).Grp(),
            Pin.Name("RaspberryBush" + Mod.CLONE).Lbl("Raspberries").Nbl(Cat.HARVESTABLES).Grp(),                       // stays

            Pin.Name("BlueberryBush" + Mod.CLONE).Lbl("Blueberries").Nbl(Cat.HARVESTABLES).Grp(),                       // stays
            Pin.Name("Pickable_Thistle" + Mod.CLONE).Lbl("Thistle").Nbl(Cat.FLOWERS).Grp(),                             // stays
            Pin.Name("Pickable_Mushroom_yellow" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)                               // stays, placed > 5000
                    .Lbl("Yellow Mushroom").Nbl(Cat.HARVESTABLES).Grp(),
            Pin.Name("Pickable_SurtlingCoreStand" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE).Lbl("Surtling Core").Grp(), // stays, placed > 5000
            Pin.Name("Pickable_SeedCarrot" + Mod.CLONE).Lbl("Carrot Seeds"),                                            // disappears, overlaps with Destructible

            Pin.Name("Pickable_SeedTurnip" + Mod.CLONE).Lbl("Turnip Seeds"),                                            // disappears, overlaps with Destructible

            Pin.Name("Pickable_MeatPile" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)                                      // stays, placed > 5000
                    .Lbl("Meat Pile").Nbl(Cat.HARVESTABLES).Grp(),
            Pin.Name("Pickable_MountainCaveCrystal" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)                           // stays, placed > 5000
                    .Lbl("Cave Crystal").Nbl(Cat.HARVESTABLES).Grp(),
            Pin.Name("Pickable_DragonEgg" + Mod.CLONE).Lbl("Dragon Egg").Nbl(Cat.HARVESTABLES).Grp(),                   // stays
            Pin.Name("Pickable_MountainRemains" + Mod.DIGITS + "_buried" + Mod.CLONE)
                    .Lbl("M. Remains").Nbl(Cat.HARVESTABLES).Grp(),
            Pin.Name("hanging_hairstrands" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)                                    // stays
                    .Lbl("Fenris Hair").Nbl(Cat.HARVESTABLES).Grp(),

            Pin.Name("Pickable_Flax_Wild" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE).Lbl("Flax"),                        // disappears, overlaps with Destructible
            Pin.Name("Pickable_Barley_Wild" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE).Lbl("Barley"),                    // disappears, overlaps with Destructible
            Pin.Name("Pickable_Tar(Big)?" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)                                     // disappears
                    .Lbl("Tar").Nbl(Cat.HARVESTABLES).Grp(),
            Pin.Name("CloudberryBush" + Mod.CLONE).Lbl("Cloudberries").Nbl(Cat.HARVESTABLES).Grp(),                     // stays
            Pin.Name("goblin_totempole" + Mod.CLONE).Lbl("Fuling Totem").Nbl(Cat.HARVESTABLES).Grp(),                   // stays 

            Pin.Name("Pickable_Mushroom_Magecap" + Mod.CLONE).Lbl("Magecap").Nbl(Cat.HARVESTABLES).Grp(),               // stays
            Pin.Name("Pickable_Mushroom_JotunPuffs" + Mod.CLONE)                                                        // stays
                    .Lbl("Jotun Puffs").Nbl(Cat.HARVESTABLES).Grp(),
            Pin.Name("Pickable_DvergrLantern" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)                                 // disappears
                    .Lbl("Lantern").Nbl(Cat.HARVESTABLES).Grp(),
            Pin.Name("Pickable_DvergrStein" + Mod.CLONE).Lbl("Dvergr Stein").Nbl(Cat.HARVESTABLES).Grp(),
            Pin.Name("Pickable_RoyalJelly" + Mod.CLONE).Lbl("Royal Jelly").Nbl(Cat.HARVESTABLES).Grp(),                 // disappears, placed > 5000
            Pin.Name("Pickable_BlackCoreStand" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)                                // stays, placed > 5000
                    .Lbl("Black Core").Nbl(Cat.HARVESTABLES).Grp(),
            Pin.Name("Pickable_DvergrMineTreasure" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)
                    .Lbl("Dvergr Treasure").Nbl(Cat.HARVESTABLES).Grp(),
            
            Pin.Name("Pickable_Fishingrod" + Mod.CLONE),
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
                    pin.Init(template);
                }
            }
        }
    }
}