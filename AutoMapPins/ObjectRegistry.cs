using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapPins
{
    internal class ObjectRegistry
    {
        private static readonly List<PinnedObject> pinnedObjects = new List<PinnedObject>();



        public static void AddPinnedObject(PinnedObject pin)
        {
            pinnedObjects.Add(pin);
        }

        public static void RemovePinnedObject(PinnedObject pin)
        {
            pinnedObjects.Remove(pin);
        }



        internal static void SettingChanged(object sender, EventArgs e)
        {
#if DEBUG
            Mod.Log.LogInfo(String.Format("Setting has changed. Rechecking {0} Pinned Objects", pinnedObjects.Count));
#endif
            foreach (var pin in pinnedObjects)
            {
                pin.UpdatePinVisibility();
            }
        }
    }
}
