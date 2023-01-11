using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins.Templates
{
    internal class TemplateAttribute : Attribute
    {
    }

    internal abstract class PickableTemplate : PinTemplate
    {
        public PickableTemplate()
        {
            IngameType = typeof(Pickable);
            IsGrouped = true;
            GroupingDistance = DISTANCE_MUSHROOMS;
            NormalIcon = Assets.HandSprite;
            SmallerIcon = Assets.HandSprite48;
        }
    }

    internal abstract class BerryTemplate : PinTemplate
    {
        public BerryTemplate()
        {
            IngameType = typeof(Pickable);
            IsGrouped = true;
            GroupingDistance = DISTANCE_MUSHROOMS;
            NormalIcon = Assets.BerrySprite;
            SmallerIcon = Assets.BerrySprite48;
        }
    }

    internal abstract class FlowerTemplate : PinTemplate
    {
        public FlowerTemplate()
        {
            IngameType = typeof(Pickable);
            IsGrouped = true;
            GroupingDistance = DISTANCE_MUSHROOMS;
            NormalIcon = Assets.FlowerSprite;
            SmallerIcon = Assets.FlowerSprite48;
        }
    }

    internal abstract class RemainsTemplate : PinTemplate
    {
        public RemainsTemplate()
        {
            IngameType = typeof(Pickable);
            IsGrouped = true;
            GroupingDistance = DISTANCE_MUSHROOMS;
            NormalIcon = Assets.HandSprite;
            SmallerIcon = Assets.HandSprite48;
        }
    }

    internal abstract class MushroomTemplate : PinTemplate
    {
        public MushroomTemplate()
        {
            IngameType = typeof(Pickable);
            IsGrouped = true;
            GroupingDistance = DISTANCE_MUSHROOMS;
            NormalIcon = Assets.MushroomSprite;
            SmallerIcon = Assets.MushroomSprite48;
        }
    }

    internal abstract class SeedTemplate : PinTemplate
    {
        public SeedTemplate()
        {
            IngameType = typeof(Destructible);
            IsGrouped = true;
            GroupingDistance = DISTANCE_MUSHROOMS;
            NormalIcon = Assets.SeedSprite;
            SmallerIcon = Assets.SeedSprite48;
        }
    }

    internal abstract class MineTemplate : PinTemplate
    {
        public MineTemplate()
        {
            IngameType = typeof(Destructible);
            IsGrouped = true;
            GroupingDistance = DISTANCE_MUSHROOMS;
            NormalIcon = Assets.MineSprite;
            SmallerIcon = Assets.MineSprite48;
        }
    }

    internal abstract class AxeTemplate : PinTemplate
    {
        public AxeTemplate()
        {
            IngameType = typeof(Destructible);
            IsGrouped = true;
            GroupingDistance = DISTANCE_MUSHROOMS;
            NormalIcon = Assets.AxeSprite;
            SmallerIcon = Assets.AxeSprite48;
        }
    }

    internal abstract class DungeonTemplate : PinTemplate
    {
        public DungeonTemplate()
        {
            IngameType = typeof(Location);
            NormalIcon = Assets.DungeonSprite;
            SmallerIcon = Assets.DungeonSprite48;
            IsPersistent = true;
        }
    }
}
