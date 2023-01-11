using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using AutoMapPins.Patches;
using System.Linq;
using System;

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

            if (Mod.IsEnabled(Template))
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
            ObjectRegistry.AddPinnedObject(this);
#if DEBUG
            Mod.Log.LogInfo(string.Format("Tracking: {0} at {1} {2} {3}", Template.Label, transform.position.x, transform.position.y, transform.position.z));
#endif
        }

        private void ShowPin()
        {
            if (Template.IsPersistent)
            {
                var existing = Minimap.instance.FindSimilarPin(transform.position, Template.Label);

                if (existing != null)
                {
                    pin = existing;
                    pin.m_name = Mod.Wrap(Template.Label);
                }
                else
                {
                    pin = Minimap.instance.AddPin(transform.position, Minimap.PinType.Icon3, Mod.Wrap(Template.Label), true, false);
                }
                if (Template.Icon != null)
                {
                    pin.m_icon = Template.Icon;
                }
            }
            else
            {
                pin = Minimap.instance.AddPin(transform.position, Minimap.PinType.Icon3, Mod.Wrap(Template.Label), false, false);
                if (Template.Icon != null)
                {
                    pin.m_icon = Template.Icon;
                }
            }
            visible = true;
        }

        private void HidePin()
        {
            if (pin != null && Minimap.instance != null && !Template.IsPersistent)
            {
                Minimap.instance.RemovePin(pin);
            }
            visible = false;
        }

        public void UpdatePinVisibility()
        {
            var toshow = Mod.IsEnabled(Template);
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
            if (!Template.IsPersistent)
            {
                HidePin();
            }

            if (Group != null)
            {
                Group.Remove(this);
            }

#if DEBUG
            Mod.Log.LogInfo(string.Format("Removing: {0} at {1} {2} {3}", pin?.m_name, transform.position.x, transform.position.y, transform.position.z));
#endif
            ObjectRegistry.RemovePinnedObject(this);
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
