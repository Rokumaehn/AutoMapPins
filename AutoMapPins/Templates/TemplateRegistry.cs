using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates
{
    internal class TemplateRegistry
    {
        private static List<PinTemplate> Templates = new List<PinTemplate>();


        internal static void Init()
        {
            var toload = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttribute(typeof(TemplateAttribute)) != null)
                .ToArray();
#if DEBUG
            Mod.Log.LogInfo(String.Format("Initializing Pin Template Registry: {0} Templates found", toload.Length));
#endif

            foreach (var t in toload)
            {
#if DEBUG
                Mod.Log.LogInfo(String.Format("Constructing template: {0}", t));
#endif
                var ctor = t.GetConstructor(new Type[] { });
                var obj = (PinTemplate)ctor.Invoke(new System.Object[] { });

#if DEBUG
                Mod.Log.LogInfo(String.Format("Added template: {0}", obj.SingleLabel));
#endif
                Templates.Add(obj);
                ConfigRegistry.Bind(obj);
            }
        }

        internal static PinTemplate Find(MonoBehaviour obj)
        {
            return Templates.FirstOrDefault(t => t.IsMatchV2(obj));
        }
    }
}
