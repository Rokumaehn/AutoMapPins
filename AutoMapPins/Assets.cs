using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AutoMapPins
{
    internal class Assets
    {
        public static Sprite BerrySprite { get; private set; }
        public static Sprite DungeonSprite { get; private set; }
        public static Sprite FlowerSprite { get; private set; }
        public static Sprite MineSprite { get; private set; }
        public static Sprite SeedSprite { get; private set; }



        public static void Init()
        {
            BerrySprite = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.BerryIcon));
            DungeonSprite = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.DungeonIcon));
            FlowerSprite = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.FlowerIcon));
            MineSprite = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.MineIcon));
            SeedSprite = Assets.LoadSpriteFromTexture(Assets.LoadTextureFromRaw(AutoMapPins.Properties.Resources.SeedIcon));
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
            Mod.Log.LogError(string.Format("Making Sprite from Texture {0}", SpriteTexture));
            Mod.Log.LogInfo(SpriteTexture);

            return (bool)(Object)SpriteTexture
                ? Sprite.Create(SpriteTexture, new Rect(0.0f, 0.0f, (float)SpriteTexture.width, (float)SpriteTexture.height), new Vector2(0.0f, 0.0f), PixelsPerUnit)
                : (Sprite)null;
        }
    }
}