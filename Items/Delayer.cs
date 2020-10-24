using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MusicBuilder.Registry;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MusicBuilder.Utils;

namespace MusicBuilder.Items
{
    public abstract class Delayer<T> : ModItem where T : ModTile
    {

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            spriteBatch.Draw(ModContainer.TextureBorderDI, position, new Rectangle(0, 0, 0x10, 0x10), Registries.delayData[DELAY].bgc);
            spriteBatch.Draw(ModContainer.TextureInsideDI, position, new Rectangle(0, 0, 0x10, 0x10), Registries.delayData[DELAY].lit);
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Vector2 position = base.item.Center - Main.screenPosition;
            Vector2 origin = new Vector2(base.item.width * 0.5f, base.item.height * 0.5f);
            SpriteEffects effects = (base.item.direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.spriteBatch.Draw(ModContainer.TextureBorderDI, position, new Rectangle(0, 0, 0x10, 0x10), Registries.delayData[DELAY].bgc, rotation, origin, scale, effects, 0f);
            Main.spriteBatch.Draw(ModContainer.TextureInsideDI, position, new Rectangle(0, 0, 0x10, 0x10), Registries.delayData[DELAY].lit, rotation, origin, scale, effects, 0f);
            return false;
        }

        public override void SetDefaults()
        {
            base.item.width = 0x10;
            base.item.height = 0x10;
            base.item.maxStack = 0x3e7;
            base.item.useTurn = true;
            base.item.autoReuse = true;
            base.item.useAnimation = 15;
            base.item.useTime = 10;
            base.item.useStyle = ItemUseStyleID.Swing;
            base.item.consumable = true;
            base.item.createTile = ModContent.TileType<T>();
            base.item.placeStyle = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Delayer (" + DELAY + ")");
            Tooltip.SetDefault("Right click to change facing direction.");
        }

        public abstract int DELAY { get; }

        public override string Texture => "MusicBuilder/Items/DelayerBorder";
    }

    public class Delayer0 : Delayer<Tiles.Delayer0>
    {
        public override int DELAY => 0;
    }

    public class Delayer1 : Delayer<Tiles.Delayer1>
    {
        public override int DELAY => 1;
    }

    public class Delayer2 : Delayer<Tiles.Delayer2>
    {
        public override int DELAY => 2;
    }

    public class Delayer3 : Delayer<Tiles.Delayer3>
    {
        public override int DELAY => 3;
    }

    public class Delayer4 : Delayer<Tiles.Delayer4>
    {
        public override int DELAY => 4;
    }

    public class Delayer5 : Delayer<Tiles.Delayer5>
    {
        public override int DELAY => 5;
    }

    public class Delayer6 : Delayer<Tiles.Delayer6>
    {
        public override int DELAY => 6;
    }

    public class Delayer7 : Delayer<Tiles.Delayer7>
    {
        public override int DELAY => 7;
    }

    public class Delayer8 : Delayer<Tiles.Delayer8>
    {
        public override int DELAY => 8;
    }

    public class Delayer9 : Delayer<Tiles.Delayer9>
    {
        public override int DELAY => 9;
    }

    public class Delayer10 : Delayer<Tiles.Delayer10>
    {
        public override int DELAY => 10;
    }

    public class Delayer11 : Delayer<Tiles.Delayer11>
    {
        public override int DELAY => 11;
    }

    public class Delayer12 : Delayer<Tiles.Delayer12>
    {
        public override int DELAY => 12;
    }

    public class Delayer13 : Delayer<Tiles.Delayer13>
    {
        public override int DELAY => 13;
    }

    public class Delayer14 : Delayer<Tiles.Delayer14>
    {
        public override int DELAY => 14;
    }

    public class Delayer15 : Delayer<Tiles.Delayer15>
    {
        public override int DELAY => 15;
    }

    public class Delayer16 : Delayer<Tiles.Delayer16>
    {
        public override int DELAY => 16;
    }

    public class Delayer32 : Delayer<Tiles.Delayer32>
    {
        public override int DELAY => 32;
    }

    public class Delayer64 : Delayer<Tiles.Delayer64>
    {
        public override int DELAY => 64;
    }

    public class Delayer128 : Delayer<Tiles.Delayer128>
    {
        public override int DELAY => 128;
    }

    public class Delayer256 : Delayer<Tiles.Delayer256>
    {
        public override int DELAY => 256;
    }

}