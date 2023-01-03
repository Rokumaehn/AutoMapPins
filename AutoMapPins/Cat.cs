﻿using System;
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
        public static readonly Cat DUNGEONS = new Cat("Category.Dungeons", Assets.DungeonSprite, Assets.DungeonSprite48);
        public static readonly Cat FLOWERS = new Cat("Category.Flowers", Assets.FlowerSprite, Assets.FlowerSprite);
        public static readonly Cat HARVESTABLES = new Cat("Category.Harvestables", Assets.BerrySprite, Assets.BerrySprite48);
        public static readonly Cat MINEABLES = new Cat("Category.Mineables", Assets.MineSprite, Assets.MineSprite48);
        public static readonly Cat SEEDS = new Cat("Category.Seeds", Assets.SeedSprite, Assets.SeedSprite48);



        internal string Key { get; private set; }
        internal Sprite Icon => Mod.useSmallerIcons.Value ? icon48 : icon64;
        private Sprite icon64;
        private Sprite icon48;


        internal Cat(string key, Sprite icon64, Sprite icon48)
        {
            Key = key;
            this.icon64 = icon64;
            this.icon48 = icon48;
        }
    }
}
