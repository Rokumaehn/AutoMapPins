using BepInEx.Configuration;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapPins
{
    internal class ConfigRegistry
    {
        private static Dictionary<string, ConfigEntry<bool>> enabled = new Dictionary<string, ConfigEntry<bool>>();


        internal static void Bind(PinTemplate template)
        {
#if DEBUG
            Mod.Log.LogInfo(String.Format("Binding Pin Template Config: {0}", template.SingleLabel));
#endif
            if (!enabled.ContainsKey(template.SingleLabel))
            {
                var cfg = Mod.Instance.Config.Bind<bool>(template.FirstBiome.ToString(), template.SingleLabel, false, "Whether to show this on the map or not");
                cfg.SettingChanged += ObjectRegistry.SettingChanged;
                enabled.Add(template.SingleLabel, cfg);
            }
        }

        internal static bool? IsEnabled(PinTemplate template)
        {
            if (!enabled.ContainsKey(template.SingleLabel)) return null;
            else return enabled[template.SingleLabel].Value;
        }
    }
}
