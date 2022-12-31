using HarmonyLib;
using System.Linq;
using UnityEngine;

namespace AutoMapPins
{
    [HarmonyPatch(typeof(Destructible), "Start")]
    class DestructiblePatchSpawn
    {
        private static readonly PinTemplate[] TEMPLATES = new PinTemplate[]
        {
            Pin.Hovr("\\$piece_deposit_tin").Lbl("Tin").Nbl(Cat.MINEABLES).Grp(),
            Pin.Hovr("\\$piece_deposit_copper").Lbl("Copper").Nbl(Cat.MINEABLES).Grp(),
            Pin.Hovr("\\$piece_deposit_obsidian").Lbl("Obsidian").Nbl(Cat.MINEABLES).Grp(),
            Pin.Hovr("\\$piece_deposit_silver(vein)?").Lbl("Silver").Nbl(Cat.MINEABLES).Grp(),
            Pin.Hovr("\\$piece_mudpile").Lbl("Iron").Nbl(Cat.MINEABLES).Grp(),
            Pin.Hovr("\\$item_destructible_gucksack").Lbl("Guck Sack").Grp(),

            Pin.Hovr("\\$prop_beech"),
            Pin.Hovr("\\$prop_treestump"),//Possibly same as Beech_Stub(Clone)
            Pin.Hovr("\\$prop_fir"),
            Pin.Hovr("\\$prop_treelog"),
            Pin.Hovr("\\$prop_yggashoot"),
            Pin.Hovr("\\$enemy_greydwarfspawner"),
            Pin.Hovr("\\$enemy_skeletonspawner"),
            Pin.Hovr("\\$enemy_draugrspawner"),
            Pin.Hovr("\\$piece_giant_helmet"),
            Pin.Hovr("\\$piece_giant_sword"),
            Pin.Hovr("\\$piece_giant_bone"),
            Pin.Hovr("\\$piece_giant_brain"),

            Pin.Name("barrell" + Mod.CLONE),
            Pin.Name("blackmarble_altar_crystal" + Mod.CLONE),
            Pin.Name("blackmarble_post" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("BlueberryBush" + Mod.CLONE),
            Pin.Name("Bush" + Mod.DIGITS + Mod.CLONE),
            Pin.Name("Bush" + Mod.DIGITS + "_en" + Mod.CLONE),
            Pin.Name("Bush" + Mod.DIGITS + "_heath" + Mod.CLONE),
            Pin.Name("CastleKit_metal_groundtorch_unlit" + Mod.CLONE),
            Pin.Name("caverock_ice_pillar_wall" + Mod.CLONE),
            Pin.Name("caverock_ice_stalagmite" + Mod.CLONE),
            Pin.Name("caverock_ice_stalagtite" + Mod.CLONE),
            Pin.Name("cliff_mistlands" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("cloth_hanging_door" + Mod.CLONE),
            Pin.Name("cloth_hanging_door_double" + Mod.CLONE),
            Pin.Name("cloth_hanging_long" + Mod.CLONE),
            Pin.Name("CloudberryBush" + Mod.CLONE),
            Pin.Name("CreepProp_hanging" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("CreepProp_egg_hanging" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("Crow" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("dvergrprops_barrel" + Mod.CLONE),
            Pin.Name("dvergrprops_pickaxe" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("dvergrtown_arch" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("dvergrtown_creep_door" + Mod.CLONE),
            Pin.Name("dvergrtown_wood_beam" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("dvergrtown_wood_support" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("dvergrtown_wood_pole" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("dvergrtown_wood_wall" + Mod.DIGITS + Mod.CLONE),
            Pin.Name("fenrirhide_hanging" + Mod.CLONE),
            Pin.Name("Greydwarf_Root" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("hanging_hairstrands" + Mod.CLONE),
            Pin.Name("HeathRockPillar" + Mod.CLONE),
            Pin.Name("highstone" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("ice" + Mod.DIGITS + Mod.CLONE),
            Pin.Name("ice_rock" + Mod.DIGITS + Mod.CLONE),
            Pin.Name("marker" + Mod.DIGITS + Mod.CLONE),
            Pin.Name("MountainGraveStone" + Mod.DIGITS + Mod.CLONE),
            Pin.Name("MountainKit_brazier" + Mod.CLONE),
            Pin.Name("mountainkit_chair" + Mod.CLONE),
            Pin.Name("mountainkit_table" + Mod.CLONE),
            Pin.Name("MountainKit_wood_gate" + Mod.CLONE),
            Pin.Name("Pickable_Barley_Wild" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE).Lbl("Barley").Nbl(Cat.SEEDS).Grp(),
            Pin.Name("Pickable_Flax_Wild" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE).Lbl("Flax").Nbl(Cat.SEEDS).Grp(),
            Pin.Name("Pickable_MountainCaveCrystal" + Mod.CLONE),
            Pin.Name("Pickable_RoyalJelly" + Mod.CLONE),
            Pin.Name("Pickable_SeedCarrot" + Mod.CLONE).Lbl("Carrot Seeds").Nbl(Cat.SEEDS).Grp(),
            Pin.Name("Pickable_SeedTurnip" + Mod.CLONE).Lbl("Turnip Seeds").Nbl(Cat.SEEDS).Grp(),
            Pin.Name("RaspberryBush" + Mod.CLONE),
            Pin.Name("Rock_" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("Rock_" + Mod.DIGITS + "_plains" + Mod.CLONE),
            Pin.Name("rock" + Mod.DIGITS + "_forest" + Mod.CLONE),
            Pin.Name("rock" + Mod.DIGITS + "_heath" + Mod.CLONE),
            Pin.Name("rock" + Mod.DIGITS + "_mountain" + Mod.CLONE),
            Pin.Name("rock" + Mod.DIGITS + "_coast" + Mod.CLONE),
            Pin.Name("rock_mistlands" + Mod.DIGITS + Mod.CLONE),
            Pin.Name("RockDolmen_" + Mod.DIGITS + Mod.CLONE),
            Pin.Name("root" + Mod.DIGITS + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("Seagal" + Mod.CLONE),
            Pin.Name("SeekerEgg" + Mod.DIGITS_BRACED_OPTIONAL + Mod.CLONE),
            Pin.Name("shrub_" + Mod.DIGITS + Mod.CLONE),
            Pin.Name("shrub_" + Mod.DIGITS + "_heath" + Mod.CLONE),
            Pin.Name("stubbe" + Mod.CLONE),
            Pin.Name("trader_wagon_destructable" + Mod.CLONE),
            Pin.Name("widestone" + Mod.CLONE),
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
