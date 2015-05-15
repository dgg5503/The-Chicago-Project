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
    /// <summary>
    /// Will only display stats in a formated way.
    /// </summary>
    class ItemStatsUI : Control
    {
        // Item to display stats of
        private Item.Item item;

        // get current item
        public Item.Item Item { get { return item; } }

        // Controls to be modified
        private Label headerLabel;
        private Container informationContainer;

        public ItemStatsUI(Vector2 size)
        {
            this.Size = size;

            // Header label
            headerLabel = new Label();
            headerLabel.Alignment = ControlAlignment.CenterX;
            headerLabel.Location = new Vector2(0, size.Y * .025f);
            Add(headerLabel);

            // Information container
            informationContainer = new Container();
            informationContainer.Alignment = ControlAlignment.Center;
            informationContainer.Size = new Vector2(size.X * .90f, size.Y * .80f);
            informationContainer.Location = new Vector2(0, size.Y * .05f);
            Add(informationContainer);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //headerLabel.Location = new Vector2(headerLabel.parent.Size.X / 2 - headerLabel.Size.X / 2, headerLabel.Location.Y);
        }

        public void Load(Item.Item item)
        {
            informationContainer.Clear();

            this.item = item;

            // include info that all items have (excluding image)
            headerLabel.Text = item.name;

            // if its a weapon, display weapon stats.
            if(item is Weapon)
            {
                Weapon weapon = (Weapon)item;
                
                // rate of fire
                // ammo (current / total)
                // spread
                // damage

                string info = "Rate of Fire: " + weapon.rateOfFire + "\nLoaded Ammo: " + weapon.LoadedAmmo + "\nAmmo Left: " + weapon.Ammo + "\nAccuracy: " + (100 - weapon.spread) + "%\nDamage: " + weapon.Damage;

                Label otherInfoLabel = new Label();
               
                otherInfoLabel.Alignment = ControlAlignment.Center;
                otherInfoLabel.Text = info;
                otherInfoLabel.WordWrap = false;
                informationContainer.Add(otherInfoLabel);

                return;
            }
        }
    }
}
