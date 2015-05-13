using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Douglas Gliner
namespace TheChicagoProject.GUI.Forms
{
    class Bar : Control
    {
        private Color progressColor;
        private Vector2 scale;

        public Color ProgressColor { get { return progressColor; } set { progressColor = value; } }
        public Vector2 Scale { get { return scale; } set { scale = value; } }

        public Bar()
        {
            Fill = null;
            Border = null;
            progressColor = Color.Green;
            scale = new Vector2(Size.X, Size.Y);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(RenderManager.Pixel, this.GlobalLocation(), null, progressColor, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
