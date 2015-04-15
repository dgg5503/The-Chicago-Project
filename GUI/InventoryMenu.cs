using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.GUI.Forms;


namespace TheChicagoProject.GUI
{
    public class InventoryMenu : Control
    {
        private DragableMatrix matrix;

        /// <summary>
        /// Constructor:
        /// - Init size
        /// - Init location
        /// - InitializeForms function.
        /// </summary>
        public InventoryMenu()
        {
            this.Size = new Vector2(600, 350);
            //this.Location = new Vector2(0, 0);
            this.Location = new Vector2((RenderManager.ViewportWidth / 2) - (this.Size.X / 2), (RenderManager.ViewportHeight / 2) - (this.Size.Y / 2));

            matrix = new DragableMatrix(5, 5, 2);
            matrix.Location = new Vector2((this.Size.X / 2) - (matrix.Size.X / 2) + 75, (this.Size.Y / 2) - (matrix.Size.Y / 2));
            matrix.AddToMatrix(new DragableControl("ITEM A"));
            matrix.AddToMatrix(new DragableControl("ITEM B"));
            matrix.AddToMatrix(new DragableControl("ITEM C"));
            matrix.AddToMatrix(new DragableControl("ITEM D"));
            matrix.AddToMatrix(new DragableControl("ITEM E"));
            matrix.AddToMatrix(new DragableControl("ITEM F"));
            matrix.AddToMatrix(new DragableControl("ITEM G"));
            matrix.parent = this;
            this.Add(matrix);    
        }

        /// <summary>
        /// Draw method override for unique drawings of each GUI control.
        /// </summary>
        /// <param name="spriteBatch">Spritebatch for drawing.</param>
        /// <param name="gameTime">Gametime for animation / countdown within GUI</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Fill, this.GlobalLocation(), Color.White);
            spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
