using AutoMapPins.Templates;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Patches
{
    internal static class MinimapExt
    {
        public static List<Minimap.PinData> GetPins(this Minimap me)
        {
            return (List<Minimap.PinData>)Traverse.Create(me).Field("m_pins").GetValue();
        }

        public static Minimap.PinData FindSimilarPin(this Minimap me, Vector3 position, String name)
        {
            return me.GetPins().FirstOrDefault(p =>
                Math.Sqrt(
                    Math.Pow(p.m_pos.x - position.x, 2) +
                    Math.Pow(p.m_pos.y - position.y, 2) +
                    Math.Pow(p.m_pos.z - position.z, 2)
                ) < 1f &&
                p.m_name.Contains(name.Replace(' ', '\u00A0'))
            );
        }

        public static void RegisterExistingPins(this Minimap me)
        {
            var pins = me.GetPins();

            Mod.Log.LogError(String.Format("Loaded with {0} Pins", pins.Count));

            foreach (var pin in pins)
            {
                var template = TemplateRegistry.Find(pin);

                if (template != null)
                {
                    pin.m_icon = template.Icon;
                    pin.m_name = Mod.Wrap(template.Label);
                }
            }
        }
    }

    [HarmonyPatch(typeof(Minimap), "LoadMapData")]
    class MinimapLoadMapData
    {
        private static void Postfix(ref Minimap __instance)
        {
            __instance.RegisterExistingPins();
        }
    }
}
