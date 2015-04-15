using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.GUI.Forms;

namespace TheChicagoProject.GUI
{
    /*
    public class DragableContainer : Control
    {
        /// <summary>
        /// The current dragable control in this container.
        /// </summary>
        private DragableControl currentControl;

        /// <summary>
        /// Returns the current dragable control in this container.
        /// </summary>
        public DragableControl CurrentControl { get { return currentControl; } set { currentControl = value; Clear(); Add(currentControl); } }

        /// <summary>
        /// Holds a dragable control
        /// </summary>
        /// <param name="dragableControl">The dragable control you want in this container.</param>
        public DragableContainer(DragableControl[,] containerMatrix, string text, Vector2 location)
        {
            this.Size = new Vector2(70, 70);
            this.Location = location;

            currentControl = new DragableControl(text, containerMatrix);

            
            if (currentControl != null)
            {
                currentControl.parent = this;
                Add(currentControl);
            }
            
                // WHY
        }

        

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Fill, this.GlobalLocation(), Color.White);
            spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }

        
    }
     */
}
