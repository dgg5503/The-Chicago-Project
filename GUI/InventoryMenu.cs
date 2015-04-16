using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.Item;
using TheChicagoProject.GUI.Forms;


namespace TheChicagoProject.GUI
{
    public class InventoryMenu : Control
    {
        private DragableMatrix matrix;
        private DragableMatrix equipd;

        private bool isInventoryLoaded;

        public bool IsInventoryLoaded { get { return isInventoryLoaded; } }

        /// <summary>
        /// Constructor:
        /// - Init size
        /// - Init location
        /// - InitializeForms function.
        /// </summary>
        public InventoryMenu()
        {
            isInventoryLoaded = false;

            this.Size = new Vector2(600, 350);
            //this.Location = new Vector2(0, 0);
            this.Location = new Vector2((RenderManager.ViewportWidth / 2) - (this.Size.X / 2), (RenderManager.ViewportHeight / 2) - (this.Size.Y / 2));

            Container multiMatContainer = new Container();
            multiMatContainer.Size = new Vector2(DragableControl.SIDE_LENGTH * 5 + 100, DragableControl.SIDE_LENGTH * 3 + 20);
            multiMatContainer.Location = new Vector2(10, (this.Size.Y / 2) - (DragableControl.SIDE_LENGTH * 3 + 20) / 2);
            multiMatContainer.Alignment = ControlAlignment.Right;
            multiMatContainer.parent = this;
            Add(multiMatContainer);

            

            Container equipContainer = new Container();
            equipContainer.Size = new Vector2(DragableControl.SIDE_LENGTH, DragableControl.SIDE_LENGTH);
            equipContainer.Location = new Vector2(10, 10);
            equipContainer.Alignment = ControlAlignment.Left;
            equipContainer.parent = multiMatContainer;
            multiMatContainer.Add(equipContainer);

            equipd = new DragableMatrix(1, 1, 0);
            equipd.Alignment = ControlAlignment.Center;
            equipd.parent = equipContainer;
            equipContainer.Add(equipd);

            Container matrixContainer = new Container();
            matrixContainer.Size = new Vector2(DragableControl.SIDE_LENGTH * 5 + 20, DragableControl.SIDE_LENGTH * 3 + 20);
            matrixContainer.Location = new Vector2(0, (multiMatContainer.Size.Y / 2) - (DragableControl.SIDE_LENGTH * 3 + 20) / 2);
            matrixContainer.Alignment = ControlAlignment.Right;
            matrixContainer.parent = multiMatContainer;
            multiMatContainer.Add(matrixContainer);


            matrix = new DragableMatrix(5, 3, 5);
            matrix.Alignment = ControlAlignment.Center;
            matrix.parent = matrixContainer;
            matrixContainer.Add(matrix);

            Button closeButton = new Button();
            closeButton.Size = new Vector2(110, 20);
            closeButton.Location = new Vector2(480, 300);
            closeButton.Text = "Close Inventory";
            closeButton.Click += closeButton_Click;
            closeButton.parent = this;
            Add(closeButton);

        }

        void closeButton_Click(object sender, EventArgs e)
        {
            Close();
            Game1.state = GameState.Game;
        }

        public void Load(Inventory inventory)
        {
            foreach (Item.Item i in inventory.EntityInventory)
                matrix.AddToMatrix(new DragableControl(i));

            isInventoryLoaded = true;
        }

        public void Close()
        {
            matrix.Clear();
            isInventoryLoaded = false;
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
