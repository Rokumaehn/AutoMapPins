using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AutoMapPins
{
    internal class Assets
    {
        public static Sprite AxeSprite { get; private set; }
        public static Sprite AxeSprite48 { get; private set; }
        public static Sprite BerrySprite { get; private set; }
        public static Sprite BerrySprite48 { get; private set; }
        public static Sprite DungeonSprite { get; private set; }
        public static Sprite DungeonSprite48 { get; private set; }
        public static Sprite FlowerSprite { get; private set; }
        public static Sprite FlowerSprite48 { get; private set; }
        public static Sprite HandSprite { get; private set; }
        public static Sprite HandSprite48 { get; private set; }
        public static Sprite MineSprite { get; private set; }
        public static Sprite MineSprite48 { get; private set; }
        public static Sprite MushroomSprite { get; private set;  }
        public static Sprite MushroomSprite48 { get; private set; }
        public static Sprite SeedSprite { get; private set; }
        public static Sprite SeedSprite48 { get; private set; }



        public static void Init()
        {
            AxeSprite = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.AxeIcon));
            AxeSprite48 = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.AxeIcon48));
            BerrySprite = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.BerryIcon));
            BerrySprite48 = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.BerryIcon48));
            DungeonSprite = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.DungeonIcon));
            DungeonSprite48 = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.DungeonIcon48));
            FlowerSprite = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.FlowerIcon));
            FlowerSprite48 = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.FlowerIcon48));
            HandSprite = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.HandIcon));
            HandSprite48 = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.HandIcon48));
            MineSprite = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.MineIcon));
            MineSprite48 = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.MineIcon48));
            MushroomSprite = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.MushroomIcon));
            MushroomSprite48 = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.MushroomIcon48));
            SeedSprite = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.SeedIcon));
            SeedSprite48 = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.SeedIcon48));
        }

        internal static Texture2D LoadTextureFromRaw(byte[] bytes)
        {
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(bytes);

            return tex;
        }

        /*internal static Texture2D LoadTextureFromBitmap(System.Drawing.Bitmap bmp)
        {

            if (bmp != null)
            {
                Mod.Log.LogError(string.Format("Loading texture from Bitmap {0}", bmp));
                Mod.Log.LogInfo(bmp);
                Texture2D tex = new Texture2D(2, 2);

                MemoryStream buffer = new MemoryStream();
                bmp.Save(buffer, bmp.RawFormat);
                buffer.Position = 0;
                tex.LoadImage(buffer.ToArray());

                return tex;
            }
            else
            {
                Mod.Log.LogError("Loading Mine Pin Texture failed");
            }
            return null;
        }*/

        public static Sprite LoadSpriteFromTexture(Texture2D SpriteTexture, float PixelsPerUnit = 100f)
        {
#if DEBUG
            Mod.Log.LogError(string.Format("Making Sprite from Texture {0}", SpriteTexture));
            Mod.Log.LogInfo(SpriteTexture);
#endif

            return (bool)(Object)SpriteTexture
                ? Sprite.Create(SpriteTexture, new Rect(0.0f, 0.0f, (float)SpriteTexture.width, (float)SpriteTexture.height), new Vector2(0.0f, 0.0f), PixelsPerUnit)
                : (Sprite)null;
        }
    }
}