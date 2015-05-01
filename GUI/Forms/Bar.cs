using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheChicagoProject.GUI.Forms
{
    class Bar : Control
    {
        public Color ProgressColor { get; set; }
        public Vector2 Scale { get; set; }

        public Bar()
        {

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(RenderManager.Pixel, this.GlobalLocation(), null, ProgressColor, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
