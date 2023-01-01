using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AutoMapPins
{
    [BepInPlugin(MOD_ID, "Auto Map Pins", "1.1.0")]
    class Mod : BaseUnityPlugin
    {
        public const string MOD_ID = "Kempeth_AutoMapPins";
        public const int MAX_PIN_HEIGHT = 4000;

        public const string DIGITS = "\\d+";
        public const string DIGITS_BRACED_OPTIONAL = "( \\(\\d+\\))?";
        public const string CLONE = "\\(Clone\\)";

        public static ManualLogSource Log;

        internal static ConfigEntry<bool> showMineables;
        private const bool showMineablesDefault = true;

        internal static ConfigEntry<bool> showDungeons;
        private const bool showDungeonsDefault = true;

        internal static ConfigEntry<bool> showSeeds;
        private const bool showSeedsDefault = true;

        internal static ConfigEntry<bool> showHarvestables;
        private const bool showHarvestablesDefault = true;

        internal static ConfigEntry<bool> showFlowers;
        private const bool showFlowersDefault = false;

        internal static ConfigEntry<bool> showUncategorized;
        private const bool showUncategorizedDefault = false;


        private static readonly List<PinnedObject> pinnedObjects = new List<PinnedObject>();

        void Awake()
        {
            var harmony = new Harmony(MOD_ID);
            harmony.PatchAll();
            Mod.Log = this.Logger;

            showMineables = this.Config.Bind<bool>(
                "Categories",
                "Mineables",
                showMineablesDefault,
                "Show mineable nodes like Ores, Meterorites and Leviathans");
            showDungeons = this.Config.Bind<bool>(
                "Categories",
                "showDungeons",
                showDungeonsDefault,
                "Show dungeon entrances");
            showSeeds = this.Config.Bind<bool>(
                "Categories",
                "Seeds",
                showSeedsDefault,
                "Show seeds");
            showHarvestables = this.Config.Bind<bool>(
                "Categories",
                "Harvestables",
                showHarvestablesDefault,
                "Show harvestables like Berries and Mushrooms");
            showFlowers = this.Config.Bind<bool>(
                "Categories",
                "Flowers",
                showFlowersDefault,
                "Show flowers like Dandelions and Thistles");
            showUncategorized = this.Config.Bind<bool>(
                "Categories",
                "Uncategorized",
                showUncategorizedDefault,
                "Uncategorized things. WARNING: That's gonna be a lot!");

            showMineables.SettingChanged += Category_SettingChanged;
            showDungeons.SettingChanged += Category_SettingChanged;
            showSeeds.SettingChanged += Category_SettingChanged;
            showHarvestables.SettingChanged += Category_SettingChanged;
            showFlowers.SettingChanged += Category_SettingChanged;
            showUncategorized.SettingChanged += Category_SettingChanged;

            Mod.Log.LogInfo("Initializing Assets");
            Assets.Init();
            Mod.Log.LogInfo("Finished initializing Assets");
        }

        public static void AddPinnedObject(PinnedObject pin)
        {
            pinnedObjects.Add(pin);
        }
        public static void RemovePinnedObject(PinnedObject pin)
        {
            pinnedObjects.Remove(pin);
        }

        private void Category_SettingChanged(object sender, EventArgs e)
        {
#if DEBUG
            Log.LogInfo(String.Format("Setting has changed. Rechecking {0} Pinned Objects", pinnedObjects.Count));
#endif
            foreach (var pin in pinnedObjects)
            {
                pin.UpdatePinVisibility();
            }
        }

        internal static bool IsEnabled(String id)
        {
            if (id == Cat.MINEABLES.Key)
            {
                return showMineables.Value;
            }
            else if (id == Cat.DUNGEONS.Key)
            {
                return showDungeons.Value;
            }
            else if (id == Cat.SEEDS.Key)
            {
                return showSeeds.Value;
            }
            else if (id == Cat.HARVESTABLES.Key)
            {
                return showHarvestables.Value;
            }
            else if (id == Cat.FLOWERS.Key)
            {
                return showFlowers.Value;
            }
            else if (id == Cat.UNCATEGORIZED?.Key)
            {
                return showUncategorized.Value;
            }
            return false;
        }


        private static readonly Dictionary<Type,List<string>> UnmatchedNames = new Dictionary<Type, List<string>>();
        private static readonly Dictionary<Type, List<string>> UnmatchedHovers = new Dictionary<Type, List<string>>();

        public static void LogUnmatchedName(Type t, string name)
        {
            if (!UnmatchedNames.ContainsKey(t))
            {
                UnmatchedNames.Add(t, new List<string>());
            }
            if (!UnmatchedNames[t].Contains(name))
            {
                UnmatchedNames[t].Add(name);

                // Look in BepInEx\LogOutput.log
#if DEBUG
                Log.LogWarning(String.Format("New unmatched {0} discovered with name {1}. #{2}", t, name, UnmatchedNames.Count));
#endif
            }
        }

        public static void LogUnmatchedHover(Type t, string hover)
        {
            if (String.IsNullOrWhiteSpace(hover)) return;

            if (!UnmatchedHovers.ContainsKey(t))
            {
                UnmatchedHovers.Add(t, new List<string>());
            }
            if (!UnmatchedHovers[t].Contains(hover))
            {
                UnmatchedHovers[t].Add(hover);

                // Look in BepInEx\LogOutput.log
#if DEBUG
                Log.LogWarning(String.Format("New unmatched {0} discovered with hover {1}. #{2}", t, hover, UnmatchedHovers.Count));
#endif
            }
        }
    }
}