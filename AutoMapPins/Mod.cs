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
    [BepInPlugin(MOD_ID, "Auto Map Pins", "1.0.0")]
    class Mod : BaseUnityPlugin
    {
        public const string MOD_ID = "Kempeth_AutoMapPins";
        public const int MAX_PIN_HEIGHT = 4000;

        public const string DIGITS = "\\d+";
        public const string DIGITS_BRACED_OPTIONAL = "( \\(\\d+\\))?";
        public const string CLONE = "\\(Clone\\)";

        public static ManualLogSource Log;

        public const string CATEGORY_MINEABLES = "Category.Mineables";
        internal static ConfigEntry<bool> showMineables;
        private const bool showMineablesDefault = true;

        public const string CATEGORY_DUNGEONS = "Category.Dungeons";
        internal static ConfigEntry<bool> showDungeons;
        private const bool showDungeonsDefault = true;

        public const string CATEGORY_SEEDS = "Category.Seeds";
        internal static ConfigEntry<bool> showSeeds;
        private const bool showSeedsDefault = true;

        public const string CATEGORY_UNCATEOGRIZED = null;
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
                nameof(showMineables),
                showMineablesDefault,
                "Show mineable nodes like Ores, Meterorites and Leviathans");
            showDungeons = this.Config.Bind<bool>(
                "Categories",
                nameof(showDungeons),
                showDungeonsDefault,
                "Show dungeon entrances");
            showSeeds = this.Config.Bind<bool>(
                "Categories",
                nameof(showSeeds),
                showSeedsDefault,
                "Show seeds");
            showUncategorized = this.Config.Bind<bool>(
                "Categories",
                nameof(showUncategorized),
                showUncategorizedDefault,
                "Uncategorized things. WARNING: That's gonna be a lot!");

            showMineables.SettingChanged += Category_SettingChanged;
            showDungeons.SettingChanged += Category_SettingChanged;
            showSeeds.SettingChanged += Category_SettingChanged;
            showUncategorized.SettingChanged += Category_SettingChanged;
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
            Log.LogInfo(String.Format("Setting has changed. Rechecking {0} Pinned Objects", pinnedObjects.Count));
            foreach (var pin in pinnedObjects)
            {
                if (pin.IsVisible != IsEnabled(pin.EnabledBy))
                {
                    pin.IsVisible = IsEnabled(pin.EnabledBy);
                }
            }
        }

        internal static bool IsEnabled(String id)
        {
            if (id == CATEGORY_MINEABLES)
            {
                return showMineables.Value;
            }
            else if (id == CATEGORY_DUNGEONS)
            {
                return showDungeons.Value;
            }
            else if (id == CATEGORY_SEEDS)
            {
                return showSeeds.Value;
            }
            else if (id == CATEGORY_UNCATEOGRIZED)
            {
                return showUncategorized.Value;
            }
            return false;
        }


        private static Dictionary<Type,List<string>> UnmatchedNames = new Dictionary<Type, List<string>>();
        private static Dictionary<Type, List<string>> UnmatchedHovers = new Dictionary<Type, List<string>>();

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
                Log.LogWarning(String.Format("New unmatched {0} discovered with name {1}. #{2}", t, name, UnmatchedNames.Count));
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
                Log.LogWarning(String.Format("New unmatched {0} discovered with hover {1}. #{2}", t, hover, UnmatchedHovers.Count));
            }
        }
    }
}