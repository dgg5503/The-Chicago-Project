using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.GUI.Forms;
using TheChicagoProject.Item;

//Douglas Gliner
namespace TheChicagoProject.GUI
{
    
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
        private Label isReloadingLbl;
        private ProgressBar reloadBar;
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

                    if (value.previewSprite != null)
                        weaponImageContainer.Fill = new FillInfo(value.previewSprite.Texture, Color.White);
                    else
                        weaponImageContainer.Fill = null;
                }
                else
                {
                    weaponNameLbl.Text = "";
                    weaponImageContainer.Fill = null;
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
            //weaponAmmoContainer.parent = this;
            Add(weaponAmmoContainer);

            // Holds weapon img and name
            Container weaponImgInfoContainer = new Container();
            weaponImgInfoContainer.Size = new Vector2(100, 90);
            //weaponImgInfoContainer.Border = new BorderInfo(Sprites.guiSpritesDictionary["circle_border"]);
            
            //weaponImgInfoContainer.parent = weaponAmmoContainer;
            weaponAmmoContainer.Add(weaponImgInfoContainer);

            // Weapon image
            weaponImageContainer = new Container();
            weaponImageContainer.Fill = null;
            //weaponImageContainer.Border = new BorderInfo(Sprites.guiSpritesDictionary["circle_border"]);
            weaponImageContainer.Size = new Vector2(64, 64);
            weaponImageContainer.Location = new Vector2(0, -5);
            weaponImageContainer.Alignment = ControlAlignment.Center;
            //weaponImageContainer.parent = weaponImgInfoContainer;
            weaponImgInfoContainer.Add(weaponImageContainer);

            // Weapon name
            weaponNameLbl = new Label();
            weaponNameLbl.Location = new Vector2(0, 35);
            //weaponNameLbl.Text = "";
            weaponNameLbl.Alignment = ControlAlignment.Center;
            //weaponNameLbl.parent = weaponImgInfoContainer;
            weaponImgInfoContainer.Add(weaponNameLbl);

            // Ammo container
            Container ammoInfoContainer = new Container();
            ammoInfoContainer.Size = new Vector2(75, 90);
            ammoInfoContainer.Alignment = ControlAlignment.Right;
            //ammoInfoContainer.parent = weaponAmmoContainer;
            weaponAmmoContainer.Add(ammoInfoContainer);

            // Ammo top
            ammoCurrentInClipLbl = new Label();
            ammoCurrentInClipLbl.Location = new Vector2(0, -20);
            ammoCurrentInClipLbl.Text = "";
            ammoCurrentInClipLbl.Alignment = ControlAlignment.Center;
            //ammoCurrentInClipLbl.parent = ammoInfoContainer;
            ammoInfoContainer.Add(ammoCurrentInClipLbl);

            // Ammo divider
            reloadBar = new ProgressBar(new Vector2(75, 10));
            reloadBar.ProgressColor = Color.Black;
            reloadBar.MaxValue = 100;
            reloadBar.CurrentValue = 0;
            reloadBar.IncludeText = String.Empty;
            reloadBar.Alignment = ControlAlignment.Center;
            //reloadBar.parent = ammoInfoContainer;
            ammoInfoContainer.Add(reloadBar);

            isReloadingLbl = new Label();
            isReloadingLbl.Scale = .5f;
            isReloadingLbl.Alignment = ControlAlignment.Center;
            isReloadingLbl.Text = "";
            //isReloadingLbl.parent = reloadBar;
            reloadBar.Add(isReloadingLbl);
            /*
            Container ammoDivisorContainer = new Container();
            ammoDivisorContainer.Size = new Vector2(75, 10);
            ammoDivisorContainer.Location = new Vector2(0, 0);
            ammoDivisorContainer.Alignment = ControlAlignment.Center;
            ammoDivisorContainer.parent = ammoInfoContainer;
            ammoInfoContainer.Add(ammoDivisorContainer);
            */
            // Ammo bottom
            ammoTotalLbl = new Label();
            ammoTotalLbl.Location = new Vector2(0, 20);
            ammoTotalLbl.Text = "";
            ammoTotalLbl.Alignment = ControlAlignment.Center;
            //ammoTotalLbl.parent = ammoInfoContainer;
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
                    if (weapon.Ammo <= 1000)
                        ammoTotalLbl.Text = "" + weapon.Ammo + "";
                    else
                        ammoTotalLbl.Text = "-----";
                    reloadBar.MaxValue = weapon.ReloadTime * 1000;
                    reloadBar.CurrentValue = weapon.Reloading ? Game1.Instance.worldManager.CurrentWorld.manager.GetPlayer().LastShot : 0;

                    if (weapon.Reloading)
                        isReloadingLbl.Text = "Reloading...";
                    else
                        isReloadingLbl.Text = "";
                }
            }
            else
            {
                ammoCurrentInClipLbl.Text = "";
                ammoTotalLbl.Text = "";
                isReloadingLbl.Text = "";
                reloadBar.MaxValue = 100;
                reloadBar.CurrentValue = 0;
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
