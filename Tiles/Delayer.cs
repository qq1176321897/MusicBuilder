using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MusicBuilder.Registry;
using MusicBuilder.Utils;
using Terraria;
using Terraria.ModLoader;

namespace MusicBuilder.Tiles
{
    public abstract class Delayer<T> : ModTile where T : ModItem
    {
        private static int[,] direction = new int[,] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };

        public override string Texture => "MusicBuilder/Tiles/DelayerBorder";

        public override bool RightClick(int i, int j)
        {
            if (!Main.player[Main.myPlayer].mouseInterface)
            {
                byte num;
                DataCore.extField[i, j].data1 = (byte)((num = DataCore.extField[i, j].data1) + 1);
                if (num >= 4)
                {
                    DataCore.extField[i, j].data1 = 1;
                }
                return true;
            }
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void HitWire(int i, int j)
        {
            if (DataCore.extField[i, j].data1 == 0) return;

            Scheduler.Schedule(i, j, DELAY, delegate (int x, int y, object param)
            {
                byte dir = (byte)param;
                for (int k = 0; k < 4; ++k)
                {
                    if (((k + 3 - dir) & 0x03) == 0) continue;

                    int xt = x + direction[k, 0], yt = y + direction[k, 1];
                    Wiring.TripWire(xt, yt, 1, 1);
                    if (k + 1 == dir && Registries.delayers.Contains(Main.tile[xt, yt].type))
                        TileLoader.HitWire(xt, yt, Main.tile[xt, yt].type);
                }
            }, DataCore.extField[i, j].data1);
        }

        public override void PostSetDefaults()
        {
            Main.tileNoSunLight[base.Type] = false;
            Registries.delayers.Add(base.Type);
        }

        public override void SetDefaults()
        {
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = true;
            drop = ModContent.ItemType<T>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Delayer" + DELAY);

            Color lit = ColorUtils.ColorHue(Main.rand.NextDouble());
            Color bg = new Color((lit.R + 0) / 4, (lit.G + 0) / 4, (lit.B + 0) / 4);
            Color off = new Color((lit.R + 0) / 2, (lit.G + 0) / 2, (lit.B + 0) / 2);
            Registries.delayData.Add(DELAY, new DelayData(bg, off, lit, Type));

            AddMapEntry(lit, name);

        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Color color = ColorUtils.ColorBlend(Registries.delayData[DELAY].lit, new Color(0, 0, 0), Scheduler.GetProgress(i, j));
            r = color.R / 255.0f;
            g = color.G / 255.0f;
            b = color.B / 255.0f;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(((i * 0x10) - ((int)Main.screenPosition.X)) + Main.offScreenRange, ((j * 0x10) - ((int)Main.screenPosition.Y)) + Main.offScreenRange);
            DelayData data = Registries.delayData[DELAY];
            Color c = ColorUtils.ColorBlend(data.lit, data.off, Scheduler.GetProgress(i, j));
            spriteBatch.Draw(ModContainer.TextureBorderDT, position, Lighting.GetColor(i, j, data.bgc));
            spriteBatch.Draw(ModContainer.TextureInsideDT, position, new Rectangle(0x12 * DataCore.extField[i, j].data1, 0, 0x10, 0x10), Lighting.GetColor(i, j, c));
            return false;
        }

        public abstract int DELAY { get; }
    }

    public class Delayer0 : Delayer<Items.Delayer0>
    {
        public override int DELAY => 0;
    }

    public class Delayer1 : Delayer<Items.Delayer1>
    {
        public override int DELAY => 1;
    }

    public class Delayer2 : Delayer<Items.Delayer2>
    {
        public override int DELAY => 2;
    }

    public class Delayer3 : Delayer<Items.Delayer3>
    {
        public override int DELAY => 3;
    }

    public class Delayer4 : Delayer<Items.Delayer4>
    {
        public override int DELAY => 4;
    }

    public class Delayer5 : Delayer<Items.Delayer5>
    {
        public override int DELAY => 5;
    }

    public class Delayer6 : Delayer<Items.Delayer6>
    {
        public override int DELAY => 6;
    }

    public class Delayer7 : Delayer<Items.Delayer7>
    {
        public override int DELAY => 7;
    }

    public class Delayer8 : Delayer<Items.Delayer8>
    {
        public override int DELAY => 8;
    }

    public class Delayer9 : Delayer<Items.Delayer9>
    {
        public override int DELAY => 9;
    }

    public class Delayer10 : Delayer<Items.Delayer10>
    {
        public override int DELAY => 10;
    }

    public class Delayer11 : Delayer<Items.Delayer11>
    {
        public override int DELAY => 11;
    }

    public class Delayer12 : Delayer<Items.Delayer12>
    {
        public override int DELAY => 12;
    }

    public class Delayer13 : Delayer<Items.Delayer13>
    {
        public override int DELAY => 13;
    }

    public class Delayer14 : Delayer<Items.Delayer14>
    {
        public override int DELAY => 14;
    }

    public class Delayer15 : Delayer<Items.Delayer15>
    {
        public override int DELAY => 15;
    }

    public class Delayer16 : Delayer<Items.Delayer16>
    {
        public override int DELAY => 16;
    }

    public class Delayer32 : Delayer<Items.Delayer32>
    {
        public override int DELAY => 32;
    }

    public class Delayer64 : Delayer<Items.Delayer64>
    {
        public override int DELAY => 64;
    }

    public class Delayer128 : Delayer<Items.Delayer128>
    {
        public override int DELAY => 128;
    }

    public class Delayer256 : Delayer<Items.Delayer256>
    {
        public override int DELAY => 256;
    }

}