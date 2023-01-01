using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoMapPins
{
    internal class Cat
    {
        public static readonly Cat UNCATEGORIZED = null;
        public static readonly Cat DUNGEONS = new Cat("Category.Dungeons", Assets.DungeonSprite);
        public static readonly Cat FLOWERS = new Cat("Category.Flowers", Assets.FlowerSprite);
        public static readonly Cat HARVESTABLES = new Cat("Category.Harvestables", Assets.BerrySprite);
        public static readonly Cat MINEABLES = new Cat("Category.Mineables", Assets.MineSprite);
        public static readonly Cat SEEDS = new Cat("Category.Seeds", Assets.SeedSprite);



        internal string Key { get; private set; }
        internal Sprite Icon { get; private set; }



        internal Cat(string key, Sprite icon)
        {
            Key = key;
            Icon = icon;
        }
    }
}
