using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject.Item
{
    class Weapon : Item
    {
        private int rateOfFire;
        private int damage;
        private double reloadTime;
        private string name;

        /// <summary>
        /// Creates a new Weapon Object
        /// </summary>
        /// <param name="rof">The rate of fire of the the Weapon</param>
        /// <param name="dam">The amount of damage the gun does</param>
        /// <param name="reload">The reload time, between 1 and 4 seconds</param>
        /// <param name="name">The name of the gun</param>
        public Weapon(int rof, int dam, double reload, string name)
        {
            rateOfFire = rof;
            damage = dam;
            reloadTime = reload;
            this.name = name;
        }

        /// <summary>
        /// Gets and sets the rate of fire of the gun
        /// </summary>
        public int RateOfFire { get { return rateOfFire; } set { rateOfFire = value; } }

        /// <summary>
        /// Gets and sets the Damage of the gun
        /// Must be between 1 and 3
        /// </summary>
        public int Damage
        {
            get { return damage; } 
            
            set
            {
                if(value >= 3 || value <= 1)
                {
                    return;
                }

                damage = value;
            }
        }

        /// <summary>
        /// Gets and sets the Reload Time
        /// </summary>
        public double ReloadTime
        {
            get { return reloadTime; }
            
            set
            {
                if(value < 1D || value > 4D)
                {
                    return;
                }

                reloadTime = value;
            }
        }

        /// <summary>
        /// Gets and sets the name of the string
        /// </summary>
        public string Name { get { return name; } set { name = value; } }
    }
}
