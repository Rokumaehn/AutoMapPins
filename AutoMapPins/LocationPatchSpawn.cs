using HarmonyLib;
using System;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using UnityEngine;

namespace AutoMapPins
{
    [HarmonyPatch(typeof(Location), "Awake")]
    class LocationPatchSpawn
    {
        private static readonly PinTemplate[] TEMPLATES = new PinTemplate[]
        {
            Pin.Name("StoneHouse" + Mod.DIGITS).Lbl("Stone House"),                                                     // Location with loot
            Pin.Name("StoneTower" + Mod.DIGITS).Lbl("Stone Tower"),                                                     // Location with loot
            Pin.Name("Waymarker" + Mod.DIGITS).Lbl("Waymarker"),                                                        // Point of interest
            //Pin.Make(Pin.Name("" + Mod.DIGITS)),

            Pin.Name("AbandonedLogCabin" + Mod.DIGITS + Mod.CLONE).Lbl("Log Cabin"),                                    // Location with loot
            Pin.Name("^Crypt" + Mod.DIGITS + Mod.CLONE).Lbl("Burial Chambers").Nbl(Cat.DUNGEONS),                       // Dungeon
            Pin.Name("Dolmen" + Mod.DIGITS + Mod.CLONE).Lbl("Dolmen"),
            Pin.Name("DrakeNest" + Mod.DIGITS + Mod.CLONE).Lbl("Drake Nest"),                                           // tested overlaps with dragon egg
            Pin.Name("GoblinCamp" + Mod.DIGITS + Mod.CLONE).Lbl("Goblin Camp"),                                         // tested
            Pin.Name("Grave" + Mod.DIGITS + Mod.CLONE).Lbl("Grave"),                                                    // Point of interest / Spawner in Swamp
            Pin.Name("Greydwarf_camp" + Mod.DIGITS + Mod.CLONE).Lbl("Greydwarf Nest (Spawner)"),                        // tested
            Pin.Name("InfestedTree" + Mod.DIGITS + Mod.CLONE).Lbl("Infested Tree"),                                     // tested overlaps with guck sacks
            Pin.Name("MountainGrave" + Mod.DIGITS + Mod.CLONE).Lbl("Mountain Grave"),
            Pin.Name("MountainCave" + Mod.DIGITS + Mod.CLONE).Lbl("Mountain Cave").Nbl(Cat.DUNGEONS),                   // Dungeon
            Pin.Name("MountainWell" + Mod.DIGITS + Mod.CLONE).Lbl("Mountain Well"),                                     // Point of interest
            Pin.Name("Mistlands_DvergrTownEntrance" + Mod.DIGITS + Mod.CLONE).Lbl("Infested Mine").Nbl(Cat.DUNGEONS),   // Dungeon
            Pin.Name("Mistlands_Excavation" + Mod.DIGITS + Mod.CLONE).Lbl("Excavation"),                                // Location with loot
            Pin.Name("Mistlands_Giant" + Mod.DIGITS + Mod.CLONE).Lbl("Giant"),                                          // petrified bone
            Pin.Name("Mistlands_GuardTower" + Mod.DIGITS + "_new" + Mod.CLONE).Lbl("Guard Tower"),                      // Location with loot
            Pin.Name("Mistlands_GuardTower" + Mod.DIGITS + "_ruined_new\\d*" + Mod.CLONE).Lbl("Ruined Guard Tower"),    // Point of interest
            Pin.Name("Mistlands_Lighthouse" + Mod.DIGITS + "_new" + Mod.CLONE).Lbl("Lighthouse"),                       // Location with loot
            Pin.Name("Mistlands_RoadPost" + Mod.DIGITS + Mod.CLONE).Lbl("Road Post"),                                   // Has a "lanternpost"
            Pin.Name("Mistlands_RockSpire" + Mod.DIGITS + Mod.CLONE).Lbl("Rock Spire"),                                 // Point of interest
            Pin.Name("Mistlands_Statue" + Mod.DIGITS + Mod.CLONE).Lbl("Statue"),                                        // Point of interest
            Pin.Name("Mistlands_StatueGroup" + Mod.DIGITS + Mod.CLONE).Lbl("Statue Group"),                             // Point of interest
            Pin.Name("Mistlands_Swords" + Mod.DIGITS + Mod.CLONE).Lbl("Ancient Armor"),                                 // Point of interest
            Pin.Name("Mistlands_Harbour" + Mod.DIGITS + Mod.CLONE).Lbl("Harbour"),                                      // Location with loot
            Pin.Name("Mistlands_Viaduct" + Mod.DIGITS + Mod.CLONE).Lbl("Viaduct"),                                      // Point of interest
            Pin.Name("Ruin" + Mod.DIGITS + Mod.CLONE).Lbl("Ruin"),                                                      // Swamp Tower Spawner / Plains Location with loot
            Pin.Name("ShipSetting" + Mod.DIGITS + Mod.CLONE).Lbl("Ship Stonering"),                                     // Point of interest
            Pin.Name("ShipWreck" + Mod.DIGITS + Mod.CLONE).Lbl("Ship Wreck"),                                           // Location with loot
            Pin.Name("StoneHenge" + Mod.DIGITS + Mod.CLONE).Lbl("Stonehenge"),                                          // Point of interest
            Pin.Name("StoneTowerRuins" + Mod.DIGITS + Mod.CLONE).Lbl("Stone Tower Ruins"),                              // Location with loot
            Pin.Name("SunkenCrypt" + Mod.DIGITS + Mod.CLONE).Lbl("Sunken Crypt").Nbl(Cat.DUNGEONS),                     // Dungeon
            Pin.Name("SwampHut" + Mod.DIGITS + Mod.CLONE).Lbl("Swamp Hut"),                                             // Point of interest
            Pin.Name("SwampWell" + Mod.DIGITS + Mod.CLONE).Lbl("Swamp Well"),                                           // Point of interest
            Pin.Name("TarPit" + Mod.DIGITS + Mod.CLONE),
            Pin.Name("TrollCave" + Mod.DIGITS + Mod.CLONE).Lbl("Troll Cave").Nbl(Cat.DUNGEONS),                         // Dungeon
            Pin.Name("WoodHouse" + Mod.DIGITS + Mod.CLONE).Lbl("Wood House"),                                           // Location with loot
            Pin.Name("WoodVillage" + Mod.DIGITS + Mod.CLONE).Lbl("Wood Village"),                                       // Location with loot
            //Pin.Make(Pin.Name("" + Mod.DIGITS + Mod.CLONE)),

            Pin.Name("StartTemple" + Mod.CLONE),
            Pin.Name("DrakeLorestone" + Mod.CLONE),
            Pin.Name("Eikthyrnir" + Mod.CLONE),
            Pin.Name("Bonemass" + Mod.CLONE),
            Pin.Name("GDKing" + Mod.CLONE),
            Pin.Name("FireHole" + Mod.CLONE).Lbl("Surtling Spawner"),                                                   // Spawner
            Pin.Name("Meteorite" + Mod.CLONE),
            Pin.Name("Runestone_Meadows" + Mod.CLONE),
            Pin.Name("Runestone_Mountains" + Mod.CLONE),
            Pin.Name("Runestone_Draugr" + Mod.CLONE),
            Pin.Name("Runestone_Greydwarfs" + Mod.CLONE),
            Pin.Name("Runestone_Swamps" + Mod.CLONE),
            Pin.Name("Runestone_Plains" + Mod.CLONE),
            Pin.Name("Runestone_BlackForest" + Mod.CLONE),
            Pin.Name("Runestone_Boars" + Mod.CLONE),
            Pin.Name("Runestone_Mistlands" + Mod.CLONE),
            Pin.Name("StoneCircle" + Mod.CLONE).Lbl("Stone Circle"),
            //Pin.Make(Pin.Name("" + Mod.CLONE)),
        };


        private static void Postfix(ref Location __instance)
        {
            Location obj = __instance;
            var template = TEMPLATES.FirstOrDefault(t => t.IsMatch(obj));

            if (template == null)
            {
                Mod.LogUnmatchedName(typeof(Location), obj.name);
                Mod.LogUnmatchedHover(typeof(Location), obj.GetComponent<HoverText>()?.m_text);
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
