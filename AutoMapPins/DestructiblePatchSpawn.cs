using HarmonyLib;
using System.Linq;
using UnityEngine;

namespace AutoMapPins
{
    [HarmonyPatch(typeof(Destructible), "Start")]
    class DestructiblePatchSpawn
    {
        private static readonly PinTemplate<Destructible>[] TEMPLATES = new PinTemplate<Destructible>[]
        {
            Pin.Make(Pin.Hover<Destructible>("\\$piece_deposit_tin"), "Tin", Mod.CATEGORY_MINEABLES),
            Pin.Make(Pin.Hover<Destructible>("\\$piece_deposit_copper"), "Copper", Mod.CATEGORY_MINEABLES),
            Pin.Make(Pin.Hover<Destructible>("\\$piece_deposit_obsidian"), "Obsidian", Mod.CATEGORY_MINEABLES),
            Pin.Make(Pin.Hover<Destructible>("\\$piece_deposit_silver(vein)?"), "Silver", Mod.CATEGORY_MINEABLES),
            Pin.Make(Pin.Hover<Destructible>("\\$piece_mudpile"), "Iron", Mod.CATEGORY_MINEABLES),
            Pin.Make(Pin.Hover<Destructible>("\\$item_destructible_gucksack"), "Guck Sack"),

            Pin.Make(Pin.Hover<Destructible>("\\$prop_beech")),
            Pin.Make(Pin.Hover<Destructible>("\\$prop_fir")),
            Pin.Make(Pin.Hover<Destructible>("\\$prop_treelog")),
            Pin.Make(Pin.Hover<Destructible>("\\$prop_yggashoot")),
            Pin.Make(Pin.Hover<Destructible>("\\$enemy_greydwarfspawner")),
            Pin.Make(Pin.Hover<Destructible>("\\$enemy_skeletonspawner")),
            Pin.Make(Pin.Hover<Destructible>("\\$enemy_draugrspawner")),
            Pin.Make(Pin.Hover<Destructible>("\\$piece_giant_helmet")),
            Pin.Make(Pin.Hover<Destructible>("\\$piece_giant_sword")),
            Pin.Make(Pin.Hover<Destructible>("\\$piece_giant_bone")),
            Pin.Make(Pin.Hover<Destructible>("\\$piece_giant_brain")),

            Pin.Make(Pin.Name<Destructible>("barrell" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("blackmarble_altar_crystal" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("blackmarble_post" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("BlueberryBush" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("Bush" + Mod.DIGITS + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("Bush" + Mod.DIGITS + "_en" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("Bush" + Mod.DIGITS + "_heath" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("CastleKit_metal_groundtorch_unlit" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("caverock_ice_pillar_wall" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("caverock_ice_stalagmite" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("caverock_ice_stalagtite" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("cliff_mistlands" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("cloth_hanging_door" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("cloth_hanging_door_double" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("cloth_hanging_long" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("CloudberryBush" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("CreepProp_hanging" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("CreepProp_egg_hanging" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("Crow" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("dvergrprops_barrel" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("dvergrprops_pickaxe" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("dvergrtown_arch" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("dvergrtown_creep_door" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("dvergrtown_wood_beam" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("dvergrtown_wood_support" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("dvergrtown_wood_pole" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("dvergrtown_wood_wall" + Mod.DIGITS + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("fenrirhide_hanging" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("Greydwarf_Root" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("hanging_hairstrands" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("HeathRockPillar" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("highstone" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("ice" + Mod.DIGITS + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("ice_rock" + Mod.DIGITS + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("marker" + Mod.DIGITS + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("MountainGraveStone" + Mod.DIGITS + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("MountainKit_brazier" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("mountainkit_chair" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("MountainKit_wood_gate" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("Pickable_Barley_Wild" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Barley", Mod.CATEGORY_SEEDS),
            Pin.Make(Pin.Name<Destructible>("Pickable_Flax_Wild" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE), "Flax", Mod.CATEGORY_SEEDS),
            Pin.Make(Pin.Name<Destructible>("Pickable_MountainCaveCrystal" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("Pickable_RoyalJelly" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("Pickable_SeedCarrot" + Mod.CLONE), "Carrot Seeds", Mod.CATEGORY_SEEDS),
            Pin.Make(Pin.Name<Destructible>("Pickable_SeedTurnip" + Mod.CLONE), "Turnip Seeds", Mod.CATEGORY_SEEDS),
            Pin.Make(Pin.Name<Destructible>("RaspberryBush" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("Rock_" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("Rock_" + Mod.DIGITS + "_plains" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("rock" + Mod.DIGITS + "_forest" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("rock" + Mod.DIGITS + "_heath" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("rock" + Mod.DIGITS + "_mountain" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("rock" + Mod.DIGITS + "_coast" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("rock_mistlands" + Mod.DIGITS + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("RockDolmen_" + Mod.DIGITS + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("root" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("Seagal" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("SeekerEgg" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("shrub_" + Mod.DIGITS + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("shrub_" + Mod.DIGITS + "_heath" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("stubbe" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("trader_wagon_destructable" + Mod.CLONE)),
            Pin.Make(Pin.Name<Destructible>("widestone" + Mod.CLONE)),
        };

        private static void Postfix(ref Destructible __instance)
        {
            Destructible obj = __instance;
            var template = TEMPLATES.FirstOrDefault(t => t.IsMatch(obj));

            if (template == null)
            {
                Mod.LogUnmatchedName(typeof(Destructible), obj.name);
                Mod.LogUnmatchedHover(typeof(Destructible), obj.GetComponent<HoverText>()?.m_text);
            }
            else if (!System.String.IsNullOrWhiteSpace(template.Label) && template.IsEnabled())
            {
                var hovertextcomp = obj.GetComponent<HoverText>();
                var height = __instance.gameObject.transform.position.y;

                if (height < Mod.MAX_PIN_HEIGHT)
                {
                    var pin = ((Component)__instance).gameObject.AddComponent<PinnedObject>();
                    pin.Init(template.Label);
                }
            }
        }
    }
}
