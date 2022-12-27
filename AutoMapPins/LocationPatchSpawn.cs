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
        private static readonly PinTemplate<Location>[] TEMPLATES = new PinTemplate<Location>[]
        {
            Pin.Make(Pin.Name<Location>("StoneHouse" + Mod.DIGITS), "Stone House"),                                     // Location with loot
            Pin.Make(Pin.Name<Location>("StoneTower" + Mod.DIGITS), "Stone Tower"),                                     // Location with loot
            Pin.Make(Pin.Name<Location>("Waymarker" + Mod.DIGITS), "Waymarker"),                                        // Point of interest
            //Pin.Make(Pin.Name<Location>("" + Mod.DIGITS)),

            Pin.Make(Pin.Name<Location>("AbandonedLogCabin" + Mod.DIGITS + Mod.CLONE), "Log Cabin"),                    // Location with loot
            Pin.Make(Pin.Name<Location>("^Crypt" + Mod.DIGITS + Mod.CLONE), "Burial Chambers", Mod.CATEGORY_DUNGEONS),  // Dungeon
            Pin.Make(Pin.Name<Location>("Dolmen" + Mod.DIGITS + Mod.CLONE), "Dolmen"),
            Pin.Make(Pin.Name<Location>("DrakeNest" + Mod.DIGITS + Mod.CLONE), "Drake Nest"),                           // tested overlaps with dragon egg
            Pin.Make(Pin.Name<Location>("GoblinCamp" + Mod.DIGITS + Mod.CLONE), "Goblin Camp"),                         // tested
            Pin.Make(Pin.Name<Location>("Grave" + Mod.DIGITS + Mod.CLONE), "Grave"),                                    // Point of interest / Spawner in Swamp
            Pin.Make(Pin.Name<Location>("Greydwarf_camp" + Mod.DIGITS + Mod.CLONE), "Greydwarf Nest (Spawner)"),        // tested
            Pin.Make(Pin.Name<Location>("InfestedTree" + Mod.DIGITS + Mod.CLONE), "Infested Tree"),                     // tested overlaps with guck sacks
            Pin.Make(Pin.Name<Location>("MountainGrave" + Mod.DIGITS + Mod.CLONE), "Mountain Grave"),
            Pin.Make(Pin.Name<Location>("MountainCave" + Mod.DIGITS + Mod.CLONE), "Mountain Cave", Mod.CATEGORY_DUNGEONS),// Dungeon
            Pin.Make(Pin.Name<Location>("MountainWell" + Mod.DIGITS + Mod.CLONE), "Mountain Well"),                     // Point of interest
            Pin.Make(Pin.Name<Location>("Mistlands_DvergrTownEntrance" + Mod.DIGITS + Mod.CLONE), "Infested Mine", Mod.CATEGORY_DUNGEONS),// Dungeon
            Pin.Make(Pin.Name<Location>("Mistlands_Excavation" + Mod.DIGITS + Mod.CLONE), "Excavation"),                // Location with loot
            Pin.Make(Pin.Name<Location>("Mistlands_Giant" + Mod.DIGITS + Mod.CLONE), "Giant"),                          // petrified bone
            Pin.Make(Pin.Name<Location>("Mistlands_GuardTower" + Mod.DIGITS + "_new" + Mod.CLONE), "Guard Tower"),      // Location with loot
            Pin.Make(Pin.Name<Location>("Mistlands_GuardTower" + Mod.DIGITS + "_ruined_new\\d*" + Mod.CLONE), "Ruined Guard Tower"),//Point of interest
            Pin.Make(Pin.Name<Location>("Mistlands_Lighthouse" + Mod.DIGITS + "_new" + Mod.CLONE), "Lighthouse"),       // Location with loot
            Pin.Make(Pin.Name<Location>("Mistlands_RoadPost" + Mod.DIGITS + Mod.CLONE), "Road Post"),                   // Has a "lanternpost"
            Pin.Make(Pin.Name<Location>("Mistlands_RockSpire" + Mod.DIGITS + Mod.CLONE), "Rock Spire"),                 // Point of interest
            Pin.Make(Pin.Name<Location>("Mistlands_Statue" + Mod.DIGITS + Mod.CLONE), "Statue"),                        // Point of interest
            Pin.Make(Pin.Name<Location>("Mistlands_StatueGroup" + Mod.DIGITS + Mod.CLONE), "Statue Group"),             // Point of interest
            Pin.Make(Pin.Name<Location>("Mistlands_Swords" + Mod.DIGITS + Mod.CLONE), "Ancient Armor"),                 // Point of interest
            Pin.Make(Pin.Name<Location>("Mistlands_Harbour" + Mod.DIGITS + Mod.CLONE), "Harbour"),                      // Location with loot
            Pin.Make(Pin.Name<Location>("Mistlands_Viaduct" + Mod.DIGITS + Mod.CLONE), "Viaduct"),                      // Point of interest
            Pin.Make(Pin.Name<Location>("Ruin" + Mod.DIGITS + Mod.CLONE), "Ruin"),                                      // Swamp Tower Spawner / Plains Location with loot
            Pin.Make(Pin.Name<Location>("ShipSetting" + Mod.DIGITS + Mod.CLONE), "Ship Stonering"),                     // Point of interest
            Pin.Make(Pin.Name<Location>("ShipWreck" + Mod.DIGITS + Mod.CLONE), "Ship Wreck"),                           // Location with loot
            Pin.Make(Pin.Name<Location>("StoneHenge" + Mod.DIGITS + Mod.CLONE), "Stonehenge"),                          // Point of interest
            Pin.Make(Pin.Name<Location>("StoneTowerRuins" + Mod.DIGITS + Mod.CLONE), "Stone Tower Ruins"),              // Location with loot
            Pin.Make(Pin.Name<Location>("SunkenCrypt" + Mod.DIGITS + Mod.CLONE), "Sunken Crypt", Mod.CATEGORY_DUNGEONS),// Dungeon
            Pin.Make(Pin.Name<Location>("SwampHut" + Mod.DIGITS + Mod.CLONE), "Swamp Hut"),                             // Point of interest
            Pin.Make(Pin.Name<Location>("SwampWell" + Mod.DIGITS + Mod.CLONE), "Swamp Well"),                           // Point of interest
            Pin.Make(Pin.Name<Location>("TarPit" + Mod.DIGITS + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("TrollCave" + Mod.DIGITS + Mod.CLONE), "Troll Cave", Mod.CATEGORY_DUNGEONS),    // Dungeon
            Pin.Make(Pin.Name<Location>("WoodHouse" + Mod.DIGITS + Mod.CLONE), "Wood House"),                           // Location with loot
            Pin.Make(Pin.Name<Location>("WoodVillage" + Mod.DIGITS + Mod.CLONE), "Wood Village"),                       // Location with loot
            //Pin.Make(Pin.Name<Location>("" + Mod.DIGITS + Mod.CLONE)),

            Pin.Make(Pin.Name<Location>("DrakeLorestone" + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("Eikthyrnir" + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("Bonemass" + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("FireHole" + Mod.CLONE), "Surtling Spawner"),                                   // Spawner
            Pin.Make(Pin.Name<Location>("Meteorite" + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("Runestone_Meadows" + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("Runestone_Mountains" + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("Runestone_Draugr" + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("Runestone_Greydwarfs" + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("Runestone_Swamps" + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("Runestone_Plains" + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("Runestone_BlackForest" + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("Runestone_Boars" + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("Runestone_Mistlands" + Mod.CLONE)),
            Pin.Make(Pin.Name<Location>("StoneCircle" + Mod.CLONE), "Stone Circle"),
            //Pin.Make(Pin.Name<Location>("" + Mod.CLONE)),
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
                    pin.Init(template.Label, template.EnabledBy);
                }
            }
        }
    }
}
