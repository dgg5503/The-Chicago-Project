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
        public int activeWeapon;

        /// <summary>
        /// Attacks
        /// </summary>
        /// <param name="type">0 is primary fire, 1 is secondary</param>
        public void Attack(int type)
        {

        }
    }
}
