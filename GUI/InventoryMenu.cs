using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.Item;
using TheChicagoProject.GUI.Forms;

//Douglas Gliner
namespace TheChicagoProject.GUI
{
    
    public class InventoryMenu : Control
    {
        //private DragableMatrix matrix;
        private DragableMatrixV2 inventoryMatrix;
        private DragableMatrixV2 currentWeaponMatrix;
        //private DragableMatrix equipd;
        //private DragableControl equipd;

        private Inventory currentInventory;

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

            this.Size = new Vector2(650, 450);
            this.Alignment = ControlAlignment.Center;

            // Header container
            Container headerContainer = new Container()
            {
                Alignment = ControlAlignment.Left,
                Size = new Vector2(this.Size.X, this.Size.Y / 16),
                parent = this
            };
            Add(headerContainer);

            // Header label
            Label headerLabel = new Label()
            {
                Alignment = ControlAlignment.Center,
                Text = "INVENTORY",
                parent = headerContainer
            };
            headerContainer.Add(headerLabel);

            // Exit inventory button
            Button xButton = new Button()
            {
                Size = new Vector2(15, 15),
                Location = new Vector2(5, (headerContainer.Size.Y / 2) - 7.5f),
                Alignment = ControlAlignment.Right,
                Text = "X",
                parent = headerContainer
            };
            xButton.Click += closeButton_Click;
            headerContainer.Add(xButton);

            
            // main container
            Container multiMatContainer = new Container();
            multiMatContainer.Size = new Vector2(this.Size.X * .90f, this.Size.Y * .80f);
            multiMatContainer.Alignment = ControlAlignment.Center;
            //multiMatContainer.parent = this;
            

            // Action button container.
            Container actionButtonsContainer = new Container()
            {
                Size = new Vector2(multiMatContainer.Size.X, this.Size.Y * .10f),
                Location = new Vector2(0, multiMatContainer.Size.Y / 2 + this.Size.Y * .05f),
                Alignment = ControlAlignment.Center,
                parent = this
            };
            Add(actionButtonsContainer);

            // Close inventory button.
            Button closeButton = new Button();
            closeButton.Size = new Vector2(110, 20);
            closeButton.Location = new Vector2(10, actionButtonsContainer.Size.Y / 2 - 10);
            closeButton.Alignment = ControlAlignment.Right;
            closeButton.Text = "Close Inventory";
            closeButton.Click += closeButton_Click;
            //closeButton.parent = actionButtonsContainer;
            actionButtonsContainer.Add(closeButton);

            // Inventory matrix.
            inventoryMatrix = new DragableMatrixV2(new Vector2(multiMatContainer.Size.X * .80f, multiMatContainer.Size.Y * .75f), 12);
            inventoryMatrix.Location = new Vector2(10, multiMatContainer.Size.Y / 2 - inventoryMatrix.Size.Y / 2);
            inventoryMatrix.Alignment = ControlAlignment.Right;
            //inventoryMatrix.parent = multiMatContainer;
            multiMatContainer.Add(inventoryMatrix);

            // Current wep matrix.
            currentWeaponMatrix = new DragableMatrixV2(new Vector2(inventoryMatrix.ContainerSideLength, inventoryMatrix.ContainerSideLength), 1);
            currentWeaponMatrix.Location = new Vector2(10, multiMatContainer.Size.Y / 2 - inventoryMatrix.Size.Y / 2);
            currentWeaponMatrix.Alignment = ControlAlignment.Left;
            //currentWeaponMatrix.parent = multiMatContainer;
            multiMatContainer.Add(currentWeaponMatrix);

            // Add down here for overlap.
            Add(multiMatContainer);  
        }

        void closeButton_Click(object sender, EventArgs e)
        {
            Close();
            Game1.state = GameState.Game;
        }

        /// <summary>
        /// Load the inventory you want to view.
        /// </summary>
        /// <param name="inventory"></param>
        public void Load(Inventory inventory)
        {
            currentInventory = inventory;

            foreach (Item.Item i in inventory.EntityInventory.Where(i => !(i.Equals(inventory.EntityInventory[inventory.ActiveWeapon]))))
                inventoryMatrix.AddToMatrix(i);

            
            if (inventory.ActiveWeapon != -1)
                currentWeaponMatrix.AddToMatrix(inventory.EntityInventory[inventory.ActiveWeapon]);
            
            isInventoryLoaded = true;
        }

        public void Close()
        {
            // set active weapon if changed...

            Item.Item item = currentWeaponMatrix[0];
            if (item != null && item != currentInventory.EntityInventory[currentInventory.ActiveWeapon])
                currentInventory.ActiveWeapon = currentInventory.EntityInventory.IndexOf(item);

            currentWeaponMatrix.Clear();
            inventoryMatrix.Clear();

            currentInventory = null;
            isInventoryLoaded = false;
        }

        /// <summary>
        /// Draw method override for unique drawings of each GUI control.
        /// </summary>
        /// <param name="spriteBatch">Spritebatch for drawing.</param>
        /// <param name="gameTime">Gametime for animation / countdown within GUI</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Fill, this.GlobalLocation(), Color.White);
            //spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
