using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Douglas Gliner
namespace TheChicagoProject.GUI.Particles
{
    class RectangleOutline : Particle
    {
        // Graphical
        private List<Line> lines;
        private Color color;
        private int width;

        public RectangleOutline(RotatedRectangle rotRect, Color color, int width) : base(5, false)
        {
            this.color = color;
            this.width = width;

            lines = new List<Line>();
            lines.Add(new Line(color, width, rotRect.LowerLeftCorner(), rotRect.LowerRightCorner(), 5));
            lines.Add(new Line(color, width, rotRect.LowerLeftCorner(), rotRect.UpperLeftCorner(), 5));
            lines.Add(new Line(color, width, rotRect.LowerRightCorner(), rotRect.UpperRightCorner(), 5));
            lines.Add(new Line(color, width, rotRect.UpperLeftCorner(), rotRect.UpperRightCorner(), 5));
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Line line in lines)
                line.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Line line in lines)
                line.Draw(spriteBatch);
        }
    }
}
