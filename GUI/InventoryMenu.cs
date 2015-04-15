using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.GUI.Forms;


namespace TheChicagoProject.GUI
{
    class InventoryMenu : Control
    {
        private DragableContainer[,] items;

        /// <summary>
        /// Constructor:
        /// - Init size
        /// - Init location
        /// - InitializeForms function.
        /// </summary>
        public InventoryMenu()
        {
            this.Size = new Vector2(600, 350);
            this.Location = new Vector2((RenderManager.ViewportWidth / 2) - (this.Size.X / 2), (RenderManager.ViewportHeight / 2) - (this.Size.Y / 2));



            InitializeForms();
        }

        /// <summary>
        /// Init all optional form related things here that are not required. Also can add more controls to this one!
        /// </summary>
        public void InitializeForms()
        {
            items = new DragableContainer[3, 3];

            for (int x = 0; x < items.GetLength(0); x++)
                for (int y = 0; y < items.GetLength(1); y++)
                {
                    items[x, y] = new DragableContainer(items, String.Format("{0} {1}", x, y), new Vector2((70 * x) + 10, (70 * y) + 10)) { parent = this };

                    Add(items[x, y]);
                }

            
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
