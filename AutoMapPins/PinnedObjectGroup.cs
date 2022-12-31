using System.Linq;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using System;

namespace AutoMapPins
{
    class PinnedObjectGroup
    {
        public const double GROUP_DISTANCE = 12; //8 is almost enough, 10 looks good so far
        public Minimap.PinData Pin { get; set; }
        public PinTemplate Template { get; set; }
        public List<PinnedObject> Objects { get; private set; } = new List<PinnedObject>();
        public List<Vector3> KnownLocations { get; private set; } = new List<Vector3>();
        public Vector3 Middle { get; private set; }
        public bool IsVisible { get; private set; }


        public bool Accepts(PinnedObject obj)
        {
            return obj.Template == Template &&
                Objects.Any(o =>
                    Math.Sqrt(Math.Pow(o.transform.position.x - obj.transform.position.x, 2) + Math.Pow(o.transform.position.z - obj.transform.position.z, 2)) < GROUP_DISTANCE
                );
        }

        public void Add(PinnedObject obj)
        {
            Objects.Add(obj);
            if (!KnownLocations.Contains(obj.transform.position))
            {
                KnownLocations.Add(obj.transform.position);

                var avgX = KnownLocations.Select(p => p.x).Sum() / KnownLocations.Count;
                var avgY = KnownLocations.Select(p => p.y).Sum() / KnownLocations.Count;
                var avgZ = KnownLocations.Select(p => p.z).Sum() / KnownLocations.Count;

                Middle = new Vector3(avgX, avgY, avgZ);
            }

            UpdatePinVisibility();
        }

        public void Remove(PinnedObject obj)
        {
            Objects.Remove(obj);

            if (Objects.Count == 0)
            {
                AllGroups.Remove(this);
            }

            UpdatePinVisibility();
        }

        public void UpdatePinVisibility()
        {
            bool toShow = Mod.IsEnabled(Template.ConfigurationKey) && Objects.Count > 0;

            IsVisible = toShow;
            if (IsVisible)
            {
                if (Pin != null)
                {
                    Minimap.instance.RemovePin(Pin);
                }

                var txt = (KnownLocations.Count > 1 ? (KnownLocations.Count + " ") : "") + Template.Label;

                Pin = Minimap.instance.AddPin(Middle, Minimap.PinType.Icon3, txt, false, false);
            }
            else if (Pin != null && Minimap.instance != null)
            {
                Minimap.instance.RemovePin(Pin);
            }
        }



        private static readonly List<PinnedObjectGroup> AllGroups = new List<PinnedObjectGroup>();

        public static PinnedObjectGroup FindOrCreateFor(PinnedObject obj)
        {
            var group = AllGroups.FirstOrDefault(g => g.Accepts(obj));

            if (group == null)
            {
                group = new PinnedObjectGroup() { Template = obj.Template };
                AllGroups.Add(group);
            }
            return group;
        }
    }
}
