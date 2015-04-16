using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.GUI.Forms;

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
    class WeaponUI : Control
    {
        public WeaponUI()
        {
            // Properties for this class
            this.Alignment = ControlAlignment.Left;
            this.Size = new Vector2(200, 100);

            // Init other things.
            Container weaponAmmoContainer = new Container();
            weaponAmmoContainer.Size = new Vector2(190, 90);
            weaponAmmoContainer.parent = this;
            Add(weaponAmmoContainer);

            Container weaponImgInfoContainer = new Container();
            weaponImgInfoContainer.Size = new Vector2(90, 90);
            weaponImgInfoContainer.parent = weaponAmmoContainer;
            weaponAmmoContainer.Add(weaponImgInfoContainer);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Fill, this.GlobalLocation(), Color.White);
            spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
