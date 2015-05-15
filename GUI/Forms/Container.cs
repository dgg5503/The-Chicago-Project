using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.GUI;

//Douglas Gliner
namespace TheChicagoProject.GUI.Forms
{

    
    public class Container : Control
    {
        // Autoresize to fit largest child?
        private bool autoResize;

        // largest size...
        private Vector2 largestSize;

        /// <summary>
        /// Autoresize the control to fit the largest child.
        /// </summary>
        public bool AutoResize { get { return autoResize; } set { autoResize = value; } }

        public Container()
        {
            autoResize = false;

            largestSize = Vector2.Zero;

            foreach(Control c in Controls)
                if (c.Size.X > largestSize.X && c.Size.Y > largestSize.Y)
                    largestSize = c.Size;
        }

        public override void Update(GameTime gameTime)
        {
            // If drawing, the texture has already been loaded! AutoResize here.
            if(autoResize)
                this.Size = largestSize;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Fill, this.GlobalLocation(), Color.White);
            //spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            
            base.Draw(spriteBatch, gameTime);
        }
    }
}
