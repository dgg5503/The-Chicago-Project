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
    //Douglas Gliner
    /// <summary>
    /// TO-DO:
    /// - Weapon image
    /// - Weapon ammo
    /// 
    /// IF WE GET TO IT:
    /// - Secondary weaponry
    ///     - Image
    ///     - Ammo
    /// </summary>
    class WeaponInfoUI : Control
    {
        // Important GUI controls that will need to be updated
        private Label weaponNameLbl;
        private Label ammoCurrentInClipLbl;
        private Label ammoTotalLbl;
        private Container weaponImageContainer;

        // Weapon to track
        private Item.Item item;

        /// <summary>
        /// Get or set the entity which is being tracked by this GUI element
        /// </summary>
        public Item.Item Item
        {
            get
            {
                return item;
            }

            set
            {
                if (value != null)
                {
                    weaponNameLbl.Text = value.name;
                    //weaponImageContainer.Fill = value.image;
                }
                else
                {
                    weaponNameLbl.Text = "";
                    //weaponImageContainer.Fill = empty;
                }
                item = value;
            }
        }

        public WeaponInfoUI()
        {
            // Properties for this class
            this.Alignment = ControlAlignment.Left;
            this.Size = new Vector2(180, 100);

            // Init other things.
            // Holds weapon and ammo info
            Container weaponAmmoContainer = new Container();
            weaponAmmoContainer.Size = new Vector2(this.Size.X - 10, this.Size.Y - 10);
            weaponAmmoContainer.Alignment = ControlAlignment.Center;
            weaponAmmoContainer.parent = this;
            Add(weaponAmmoContainer);

            // Holds weapon img and name
            Container weaponImgInfoContainer = new Container();
            weaponImgInfoContainer.Size = new Vector2(100, 90);
            weaponImgInfoContainer.parent = weaponAmmoContainer;
            weaponAmmoContainer.Add(weaponImgInfoContainer);

            // Weapon image
            weaponImageContainer = new Container();
            weaponImageContainer.Size = new Vector2(64, 64);
            weaponImageContainer.Location = new Vector2(0, -5);
            weaponImageContainer.Alignment = ControlAlignment.Center;
            weaponImageContainer.parent = weaponImgInfoContainer;
            weaponImgInfoContainer.Add(weaponImageContainer);

            // Weapon name
            weaponNameLbl = new Label();
            weaponNameLbl.Size = new Vector2(90, 10);
            weaponNameLbl.Location = new Vector2(0, 30);
            weaponNameLbl.Text = "";
            weaponNameLbl.TextAlignment = TextAlignment.Center;
            weaponNameLbl.Alignment = ControlAlignment.Center;
            weaponNameLbl.parent = weaponImgInfoContainer;
            weaponImgInfoContainer.Add(weaponNameLbl);

            // Ammo container
            Container ammoInfoContainer = new Container();
            ammoInfoContainer.Size = new Vector2(75, 90);
            ammoInfoContainer.Alignment = ControlAlignment.Right;
            ammoInfoContainer.parent = weaponAmmoContainer;
            weaponAmmoContainer.Add(ammoInfoContainer);

            // Ammo top
            ammoCurrentInClipLbl = new Label();
            ammoCurrentInClipLbl.Size = new Vector2(75, 90);
            ammoCurrentInClipLbl.Location = new Vector2(0, 20);
            ammoCurrentInClipLbl.Text = "";
            ammoCurrentInClipLbl.TextAlignment = TextAlignment.Center;
            ammoCurrentInClipLbl.Alignment = ControlAlignment.Center;
            ammoCurrentInClipLbl.parent = ammoInfoContainer;
            ammoInfoContainer.Add(ammoCurrentInClipLbl);

            // Ammo divider
            Container ammoDivisorContainer = new Container();
            ammoDivisorContainer.Size = new Vector2(75, 10);
            ammoDivisorContainer.Location = new Vector2(0, 0);
            ammoDivisorContainer.Alignment = ControlAlignment.Center;
            ammoDivisorContainer.parent = ammoInfoContainer;
            ammoInfoContainer.Add(ammoDivisorContainer);

            // Ammo bottom
            ammoTotalLbl = new Label();
            ammoTotalLbl.Size = new Vector2(75, 90);
            ammoTotalLbl.Location = new Vector2(0, 50);
            ammoTotalLbl.Text = "";
            ammoTotalLbl.TextAlignment = TextAlignment.Center;
            ammoTotalLbl.Alignment = ControlAlignment.Center;
            ammoTotalLbl.parent = ammoInfoContainer;
            ammoInfoContainer.Add(ammoTotalLbl);

        }
        
        public override void Update(GameTime gameTime)
        {
            //weaponNameLbl;
            if (item != null)
            {
                Weapon weapon = item as Weapon;
                if (weapon != null)
                {
                    ammoCurrentInClipLbl.Text = "" + weapon.LoadedAmmo + "";
                    ammoTotalLbl.Text = "" + weapon.maxClip + "";
                }
            }
            else
            {
                ammoCurrentInClipLbl.Text = "";
                ammoTotalLbl.Text = "";
            }
            //weaponImageContainer;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
