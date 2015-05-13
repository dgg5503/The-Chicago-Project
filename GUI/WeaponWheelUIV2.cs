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


    class WeaponWheelUIV2 : Control
    {
        //TO-DO:
        /* - Start from center of stats square.
         * - Go out a given radius
         * - divide 2pi by number of weapons to get angles to place weps at.
         * 
         * 
         */

        // Fields
        // This information...
        private float sideLength;
        private float weaponButtonSideLength;

        // Inventory
        private Inventory currentInventory;
        private ButtonContainer<Weapon> currentWeaponButton;

        /// <summary>
        /// Gets whether or not the inventory for this weapon wheel has been loaded.
        /// </summary>
        public bool IsInventoryLoaded 
        {
            get 
            {
                if (currentInventory == null)
                    return false;
                return true;
            } 
        } 

        // Controls to be modified
        private ItemStatsUI weaponStatsContainer;


        // Wheely stuff
        // radius from center of weapon stat square to a weapon button!
        private float radius;

        // angle interval for weapon boxs.
        private float angleInterval;


        /// <summary>
        /// MUST BE SQUARE IN SIZE!
        /// </summary>
        public WeaponWheelUIV2(float sideLength)
        {
            this.sideLength = sideLength;
            this.Size = new Vector2(sideLength, sideLength);
            this.Border = new BorderInfo(Sprites.guiSpritesDictionary["weapon_wheel_border"]);
            this.Alignment = ControlAlignment.Center;

            // Weapon stats container
            weaponStatsContainer = new ItemStatsUI(this.Size * .4f);
            weaponStatsContainer.Border = new BorderInfo(Sprites.guiSpritesDictionary["weapon_wheel_border"]);
            weaponStatsContainer.Alignment = ControlAlignment.Center;
            Add(weaponStatsContainer);

            //radius = 10;

            // 


        }

        // <summary>
        /// Load the inventory you want to view.
        /// </summary>
        /// <param name="inventory"></param>
        public void Load(Inventory inventory)
        {
            // Load inventory
            currentInventory = inventory;
            
            // Get just weps. (need where?)
            List<Weapon> weapons = currentInventory.EntityInventory.Where(item => item is Weapon).Cast<Weapon>().ToList();

            // Find angle interval
            angleInterval = (float)(2 * Math.PI) / currentInventory.Holster.Length;

            // weapon button side length
            // TO-DO automatic!!
            weaponButtonSideLength = 75;

            // or weaponStatsContainer.Size.X / 2?
            radius = ((sideLength / 2) - (weaponButtonSideLength / 2));

            // Create button with weapon image and name.
            // if current weapon, highlight button and place stats in center.
            float currentAngle = 0;
            foreach(Weapon weapon in weapons)
            {
                ButtonContainer<Weapon> weaponButton = new ButtonContainer<Weapon>();
                weaponButton.Size = new Vector2(weaponButtonSideLength, weaponButtonSideLength);
                weaponButton.Location = new Vector2(radius * (float)Math.Cos(currentAngle), radius * (float)Math.Sin(currentAngle));
                weaponButton.Data = weapon;

                if(weapon == currentInventory.EntityInventory[currentInventory.ActiveWeapon])
                {
                    weaponButton.DefaultBorder = new BorderInfo(5, Color.Purple);
                    weaponStatsContainer.Load(weapon);
                    currentWeaponButton = weaponButton;
                }
                
                weaponButton.Fill = new FillInfo(weapon.previewSprite.Texture, Color.White);
                weaponButton.Text = weapon.name;
                weaponButton.Click += weaponButton_Click;
                weaponButton.Alignment = ControlAlignment.Center;
                Add(weaponButton);

                currentAngle += angleInterval;
            }
            
            
           
        }

        void weaponButton_Click(object sender, EventArgs e)
        {
            ButtonContainer<Weapon> tmpButton = sender as ButtonContainer<Weapon>;
            if(tmpButton != null)
            {
                currentInventory.ActiveWeapon = currentInventory.EntityInventory.IndexOf(tmpButton.Data);
                currentWeaponButton.DefaultBorder = new BorderInfo(1, Color.Black);
                tmpButton.DefaultBorder = new BorderInfo(5, Color.Purple);
                currentWeaponButton = tmpButton;
                weaponStatsContainer.Load(tmpButton.Data);
            }
        }


    }
}
