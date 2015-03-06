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
        private int maxClip;
        private int loadedAmmo;

        /// <summary>
        /// Creates a new Weapon Object
        /// </summary>
        /// <param name="rof">The rate of fire of the the Weapon</param>
        /// <param name="dam">The amount of damage the gun does</param>
        /// <param name="reload">The reload time, between 1 and 4 seconds</param>
        /// <param name="name">The name of the gun</param>
        /// <param name="maxClip">The amount of ammo in a fully loaded clip</param>
        public Weapon(int rof, int dam, double reload, string name, int maxClip)
        {
            rateOfFire = rof;
            damage = dam;
            reloadTime = reload;
            this.name = name;
            this.maxClip = maxClip;
            loadedAmmo = maxClip;
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
        /// Gets the amount of ammo loaded into the gun
        /// </summary>
        public int LoadedAmmo { get { return loadedAmmo; } }

        /// <summary>
        /// Gets the amount of ammo in a loaded clip
        /// </summary>
        public int MaxClip { get { return maxClip; } }

        /// <summary>
        /// Gets and sets the name of the string
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        /// <summary>
        /// Reloads the gun
        /// </summary>
        /// <param name="ammo"></param>
        /// <returns>The amount of ammmo loaded into the gun</returns>
        public int Reload(int ammo)
        {
            if(ammo >= maxClip)
            {
                loadedAmmo = maxClip;
                return maxClip;
            }
            else if(ammo > 0)
            {
                loadedAmmo = ammo;
                return ammo;
            }
            else
            {
                return 0;
            }
        }
    }
}
