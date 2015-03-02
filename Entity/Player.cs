using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Item;

namespace TheChicagoProject.Entity
{
    class Player : Entity
    {
        public Weapon[] holster;
        private int activeWeapon;

        /// <summary>
        /// Attacks
        /// </summary>
        /// <param name="type">0 is primary fire, 1 is secondary</param>
        public void Attack(int type)
        {

        }

        /// <summary>
        /// Reloads the active weapon
        /// </summary>
        public void Reload()
        {

        }

        /// <summary>
        /// Interacts with the environment
        /// </summary>
        public void Interact()
        {

        }

        /// <summary>
        /// Gets and sets the active weapon, if the incoming switch is invalid, it changes it to the first weapon
        /// </summary>
        public int ActiveWeapon
        {
            get
            {
                return activeWeapon;
            }

            set
            {
                if (holster[value] == null)
                    activeWeapon = 1;
            }
        }
    }
}
