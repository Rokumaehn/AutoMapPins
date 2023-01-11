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
using AutoMapPins.Templates;

namespace AutoMapPins
{
    [BepInPlugin(MOD_ID, "Auto Map Pins", "1.3.0")]
    class Mod : BaseUnityPlugin
    {
        public static Mod Instance { get; private set; }

        public const string MOD_ID = "Kempeth_AutoMapPins";
        public const int MAX_PIN_HEIGHT = 4000;

        public const string DIGITS = "\\d+";
        public const string DIGITS_BRACED_OPTIONAL = "( \\(\\d+\\))?";
        public const string CLONE = "\\(Clone\\)";

        public static ManualLogSource Log;

        internal static ConfigEntry<bool> showHarvestables;
        private const bool showHarvestablesDefault = true;

        internal static ConfigEntry<bool> showUncategorized;
        private const bool showUncategorizedDefault = false;

        internal static ConfigEntry<bool> useSmallerIcons;
        internal static ConfigEntry<int> padToWidth;
        internal static ConfigEntry<float> fontFactor;

        void Awake()
        {
            Instance = this;

            var harmony = new Harmony(MOD_ID);
            harmony.PatchAll();
            Mod.Log = this.Logger;

            showHarvestables = this.Config.Bind<bool>(
                "Categories",
                "Harvestables",
                showHarvestablesDefault,
                "Show harvestables like Berries and Mushrooms");
            showUncategorized = this.Config.Bind<bool>(
                "Categories",
                "Uncategorized",
                showUncategorizedDefault,
                "Uncategorized things. WARNING: That's gonna be a lot!");


            useSmallerIcons = this.Config.Bind<bool>(
                "Display",
                "Smaller Icons",
                false,
                "Will reduce the size of icons by 25% and text by about 50%");

            padToWidth = this.Config.Bind<int>(
                "Debug",
                "Pad to Width",
                60,
                "This is the amount of spaces the pin text will be padded to");
            fontFactor = this.Config.Bind<float>(
                "Debug",
                "Font Factor",
                1.8f,
                "This is the estimated factor by which normal characters are wider than spaces");

            showHarvestables.SettingChanged += ObjectRegistry.SettingChanged;
            showUncategorized.SettingChanged += ObjectRegistry.SettingChanged;

            Assets.Init();

            TemplateRegistry.Init();
        }

        internal static bool IsEnabled(PinTemplate template)
        {
            var regCfg = ConfigRegistry.IsEnabled(template);

            if (regCfg.HasValue) return regCfg.Value;
            else return IsEnabled(template.ConfigurationKey);
        }

        internal static bool IsEnabled(String id)
        {
            if (id == Cat.HARVESTABLES.Key)
            {
                return showHarvestables.Value;
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

        public static string Wrap(string orig)
        {
            if (useSmallerIcons.Value)
            {
                var n = Math.Max(0, (int)Math.Ceiling((padToWidth.Value - orig.Length * fontFactor.Value) / 2.0));
                var pad = new string('\u00A0', n);
                return pad + orig.Replace(' ', '\u00A0') + pad;
            }
            else return orig;
        }
    }
}