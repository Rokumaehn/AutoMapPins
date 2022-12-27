using System.Reflection.Emit;
using UnityEngine;

namespace AutoMapPins
{
    class PinnedObject: MonoBehaviour
    {
        public Minimap.PinData pin;

        public string Label { get; private set; }
        public string EnabledBy { get; private set; }
        
        public void Init(string label, string enabledBy)
        {
            Label = label;
            EnabledBy = enabledBy;
            if (Mod.IsEnabled(enabledBy))
            {
                ShowPin();
            }
            Mod.AddPinnedObject(this);
            Mod.Log.LogInfo(string.Format("Tracking: {0} at {1} {2} {3}", label, transform.position.x, transform.position.y, transform.position.z));
        }

        private void ShowPin()
        {
            pin = Minimap.instance.AddPin(transform.position, Minimap.PinType.Icon3, Label, false, false);
            visible = true;
        }

        void OnDestroy()
        {
            if (pin != null && Minimap.instance != null)
            {
                Minimap.instance.RemovePin(pin);
                visible = false;
                Mod.Log.LogInfo(string.Format("Removing: {0} at {1} {2} {3}", pin.m_name, transform.position.x, transform.position.y, transform.position.z));
            }
            Mod.RemovePinnedObject(this);
        }

        private bool visible;
        public bool IsVisible
        {
            get => visible;
            set
            {
                if (value)
                {
                    ShowPin();
                }
                else
                {
                    Minimap.instance.RemovePin(pin);
                }
                visible = value;
            }
        }
    }
}
