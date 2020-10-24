using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MusicBuilder.Registry;
using MusicBuilder.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MusicBuilder.Tiles
{
    public abstract class Noteblock<T> : ModTile where T : ModItem
    {
        private const int min = 0, max = 127;
        public static Point16 selection;

        public override string Texture => "MusicBuilder/Tiles/NoteblockBorder";

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void SetDefaults()
        {
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            Main.tileFrameImportant[base.Type] = true;
            base.drop = ModContent.ItemType<T>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault(Name);
            Registries.noteData.Add(NOTE, new NoteData(new Color(0, 0, 0), ColorUtils.ColorHue(Main.rand.NextDouble()), Name, Type));

            AddMapEntry(Registries.noteData[NOTE].bgc, name);

        }

        public override void HitWire(int i, int j)
        {
            SoundManager.PlaySound(
                new Point16(i, j),
                NOTE,
                DataCore.extField[i, j].data0,
                (ushort)((DataCore.extField[i, j].data1 << 8) | DataCore.extField[i, j].data2),
                DataCore.extField[i, j].data3
            );
        }

        public override void PostSetDefaults()
        {
            Main.tileNoSunLight[base.Type] = false;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Color color = ColorUtils.ColorBlend(Registries.noteData[NOTE].bgc, new Color(0, 0, 0), SoundManager.GetProgress(new Point16(i, j)));
            r = color.R / 255.0f * 1.2f;
            g = color.G / 255.0f * 1.2f;
            b = color.B / 255.0f * 1.2f;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Color c = Registries.noteData[NOTE].bgc;
            c = ColorUtils.ColorBlend(c, new Color(c.R / 2, c.G / 2, c.B / 2), SoundManager.GetProgress(new Point16(i, j)));

            int num = (i * 0x10) - ((int)Main.screenPosition.X) + Main.offScreenRange;
            int num2 = ((j * 0x10) - ((int)Main.screenPosition.Y)) + Main.offScreenRange;
            spriteBatch.Draw(ModContainer.TextureBorderNT, new Vector2(num, num2), Lighting.GetColor(i, j, c));
            spriteBatch.Draw(ModContainer.TexturePitchNT, new Vector2(num, num2), new Rectangle(0x12 * (DataCore.extField[i, j].data0 % 12), 0, 0x10, 0x10), Lighting.GetColor(i, j, Registries.noteData[NOTE].txt));
            spriteBatch.Draw(ModContainer.TextureOctaveNT, new Vector2(num + 8, num2 + 8), new Rectangle(6 * (DataCore.extField[i, j].data0 / 12), 0, 6, 6), Lighting.GetColor(i, j, Registries.noteData[NOTE].txt));
            return false;
        }

        //TODO: add velocity change support.
        public override bool RightClick(int i, int j)
        {
            if (!Main.player[Main.myPlayer].mouseInterface)
            {
                if (Main.keyState.IsKeyDown(Keys.LeftAlt))
                {
                    Main.NewText("Note block selected (" + i + "," + j + ")");
                    selection = new Point16(i, j);
                    HitWire(i, j);
                }
                else if (Main.keyState.IsKeyDown(Keys.LeftShift))
                {
                    DataCore.extField[i, j].data0 = (byte)((DataCore.extField[i, j].data0 + 12) % max);
                    HitWire(i, j);
                }
                else
                {
                    DataCore.extField[i, j].data0 = (byte)((DataCore.extField[i, j].data0 + 1) % max);
                    HitWire(i, j);
                }
                return true;
            }
            return false;
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            if (DataCore.extField[i, j].data0 > 127)
                DataCore.extField[i, j].data0 = 0;
            if (DataCore.extField[i, j].data3 > 127)
                DataCore.extField[i, j].data3 = 127;
            return true;
        }

        public virtual Prog NOTE { get; }
    }
}