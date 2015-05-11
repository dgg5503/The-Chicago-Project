using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.GUI.Forms;
using TheChicagoProject.Item;

namespace TheChicagoProject.GUI
{
    // Douglas Gliner
    class WeaponWheelUI : Control
    {
        // Inventory stuff
        private Inventory currentInventory;
        private bool isInventoryLoaded;

        // Controls modified throughout the class
        private Container weaponButtonsContainer;
        private Button currentActive;

        public bool IsInventoryLoaded { get { return isInventoryLoaded; } }

        public WeaponWheelUI()
        {
            this.Size = new Vector2(500, 200);
            this.Alignment = ControlAlignment.Center;

            currentActive = null;

            // Close button
            Button closeButton = new Button();
            closeButton.Size = new Vector2(15, 15);
            closeButton.Location = new Vector2(5, 5);
            closeButton.Alignment = ControlAlignment.Right;
            closeButton.Text = "X";
            closeButton.Click += closeButton_Click;
            //closeButton.parent = this;
            Add(closeButton);

            // Weapon Buttons container.
            weaponButtonsContainer = new Container();
            weaponButtonsContainer.Size = new Vector2(450, 150);
            weaponButtonsContainer.Alignment = ControlAlignment.Center;
            //weaponButtonsContainer.parent = this;
            Add(weaponButtonsContainer);
            
            
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
            /*
            currentInventory = inventory;

            foreach (Item.Item i in inventory.EntityInventory.Where(i => !(i.Equals(inventory.EntityInventory[inventory.ActiveWeapon]))))
                matrix.AddToMatrix(new DragableControl(i));

            if (inventory.ActiveWeapon != -1)
                matrix.AddOutsideMatrix(new DragableControl(inventory.EntityInventory[inventory.ActiveWeapon]), new Vector2(-90, -10));

            isInventoryLoaded = true;
             * */
            currentInventory = inventory;
            int count = 0;
            foreach(int i in inventory.Holster)
            {
                Weapon weapon = inventory.EntityInventory[i] as Weapon;

                Button weaponInfoRoot = new Button();
                weaponInfoRoot.Location = new Vector2((weaponButtonsContainer.Size.X / inventory.Holster.Length) * count, 0);
                weaponInfoRoot.Size = new Vector2(weaponButtonsContainer.Size.X / inventory.Holster.Length, weaponButtonsContainer.Size.Y);
                weaponInfoRoot.Text = weapon.name;
                weaponInfoRoot.Click += weaponInfoRoot_Click;

                if(i == inventory.ActiveWeapon)
                {
                    weaponInfoRoot.DefaultBorder = new BorderInfo(weaponInfoRoot.DefaultBorder.width, Color.Purple);
                    weaponInfoRoot.DefaultFill = new FillInfo(Color.LightGray);
                    weaponInfoRoot.HoverFill = new FillInfo(Color.LightGray);
                    currentActive = weaponInfoRoot;
                }

                //weaponInfoRoot.parent = weaponButtonsContainer;
                weaponButtonsContainer.Add(weaponInfoRoot);

                // image
                // name
                // stats

                count++;
            }

            isInventoryLoaded = true;
        }

        void weaponInfoRoot_Click(object sender, EventArgs e)
        {
            // set the weapon on this button to current...
            Button clicked = sender as Button;
            if (clicked != null)
            {
                string clickedWeaponName = clicked.Text;

                // find weapon in inventory and set it, update UI
                foreach (int i in currentInventory.Holster)
                {
                    Weapon weapon = currentInventory.EntityInventory[i] as Weapon;
                    if(weapon.name == clickedWeaponName)
                    {
                        currentInventory.ActiveWeapon = i;

                        currentActive.DefaultFill = new FillInfo(Color.Gray);
                        currentActive.DefaultBorder = new BorderInfo(clicked.Border.Value.width, Color.Black);
                        currentActive.HoverFill = new FillInfo(Color.Gray);

                        clicked.DefaultBorder = new BorderInfo(clicked.DefaultBorder.width, Color.Purple);
                        clicked.DefaultFill = new FillInfo(Color.LightGray);
                        clicked.HoverFill = new FillInfo(Color.LightGray);

                        currentActive = clicked;

                        return;
                    }
                }
            }
        }

        public void Close()
        {
            /*
            // set active weapon if changed...
            Item.Item item = matrix.GetItemFromLocation(new Vector2(-80, 0));
            if (item != null && item != currentInventory.EntityInventory[currentInventory.ActiveWeapon])
                currentInventory.ActiveWeapon = currentInventory.EntityInventory.IndexOf(item);

            currentInventory = null;
            matrix.Clear();
            isInventoryLoaded = false;
             * */

            currentInventory = null;
            weaponButtonsContainer.Clear();
            isInventoryLoaded = false;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Fill, this.GlobalLocation(), Color.White);
            //spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
