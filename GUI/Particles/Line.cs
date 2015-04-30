
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TheChicagoProject.GUI.Particles
{
    public class Line : Particle
    {
        /*
         * To-do
         * - Duration (how long to be displayed for on the screen)
         * - From what to where.
         * - Color
         * - thickness
         * 
         * IWGTI:
         * - animation
         * - textured pattern
         * 
         */
        
        // Fields

        // Graphics
        private Color color;
        private int width;
        private Vector2 source;
        private Vector2 target;

        public Line(Color color, int width, Vector2 source, Vector2 target, double duration) : base(duration, false)
        {
            this.width = width;
            this.source = source;
            this.target = target;
            this.color = color;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float rotation = (float)Math.Atan2(target.Y - source.Y, target.X - source.X);
            Vector2 scale = new Vector2(Vector2.Distance(target, source), width);
            spriteBatch.Draw(RenderManager.Pixel, source, null, color, rotation, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
