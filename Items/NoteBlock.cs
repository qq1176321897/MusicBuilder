using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MusicBuilder.Registry;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MusicBuilder.Utils;

namespace MusicBuilder.Items
{
    public abstract class Noteblock<T> : ModItem where T : ModTile
    {
        public override void AddRecipes()
        {
            Recipe recipe = Mod.CreateRecipe(Type);
            recipe.AddIngredient(ItemID.Wire, 1);
            recipe.Register();
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            spriteBatch.Draw(ModContainer.TextureBorderNI, position, new Rectangle(0, 0, 0x10, 0x10), Registries.noteData[NOTE].txt);
            spriteBatch.Draw(ModContainer.TextureInsideNI, position, new Rectangle(0, 0, 0x10, 0x10), Registries.noteData[NOTE].bgc);
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Vector2 position = base.item.Center - Main.screenPosition;
            Vector2 origin = new Vector2(base.item.width * 0.5f, base.item.height * 0.5f);
            SpriteEffects effects = (base.item.direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.spriteBatch.Draw(ModContainer.TextureBorderNI, position, new Rectangle(0, 0, 0x10, 0x10), Registries.noteData[this.NOTE].txt, rotation, origin, scale, effects, 0f);
            Main.spriteBatch.Draw(ModContainer.TextureInsideNI, position, new Rectangle(0, 0, 0x10, 0x10), Registries.noteData[this.NOTE].bgc, rotation, origin, scale, effects, 0f);
            return true;
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
            DisplayName.SetDefault(Name + " Noteblock");
            Tooltip.SetDefault("Right click to increase pitch, hit with a hammer to decrease pitch.\nHolding shift makes it jump by an octave instead.");

        }

        public abstract Prog NOTE { get; }

        public override string Texture => "MusicBuilder/Items/NoteblockBorder";
    }
}
