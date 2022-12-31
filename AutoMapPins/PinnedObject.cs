using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace AutoMapPins
{
    class PinnedObject : MonoBehaviour
    {
        private Minimap.PinData pin;

        public PinTemplate Template { get; private set; }
        public PinnedObjectGroup Group { get; private set; }
        
        public void Init(PinTemplate template)
        {
            Template = template;

            if (Template.IsGrouped)
            {
                Group = PinnedObjectGroup.FindOrCreateFor(this);
                Group.Add(this);
            }

            if (Mod.IsEnabled(Template.ConfigurationKey))
            {
                if (Template.IsGrouped)
                {
                    Group.UpdatePinVisibility();
                }
                else
                {
                    ShowPin();
                }
            }
            Mod.AddPinnedObject(this);
            Mod.Log.LogInfo(string.Format("Tracking: {0} at {1} {2} {3}", Template.Label, transform.position.x, transform.position.y, transform.position.z));
        }

        private void ShowPin()
        {
            pin = Minimap.instance.AddPin(transform.position, Minimap.PinType.Icon3, Template.Label, false, false);
            visible = true;
        }

        private void HidePin()
        {
            if (pin != null && Minimap.instance != null)
            {
                Minimap.instance.RemovePin(pin);
            }
            visible = false;
        }

        public void UpdatePinVisibility()
        {
            var toshow = Mod.IsEnabled(Template.ConfigurationKey);
            if (toshow != visible)
            {
                IsVisible = toshow;
            }

            if (Template.IsGrouped)
            {
                Group.UpdatePinVisibility();
            }
        }

        void OnDestroy()
        {
            HidePin();

            if (Group != null)
            {
                Group.Remove(this);
            }

            Mod.Log.LogInfo(string.Format("Removing: {0} at {1} {2} {3}", pin?.m_name, transform.position.x, transform.position.y, transform.position.z));
            Mod.RemovePinnedObject(this);
        }

        private bool visible;
        public bool IsVisible
        {
            get => visible;
            set
            {
                if (Template.IsGrouped)
                {
                    Group.UpdatePinVisibility();
                }
                else
                {
                    if (value)
                    {
                        ShowPin();
                    }
                    else
                    {
                        HidePin();
                    }
                }

                visible = value;
            }
        }
    }
}
