using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject.Item
{
    public class Inventory
    {
        private List<Item> inventory;
        private int[] holster;
        private int activeWeapon;

        //property to get items from the inventory more easily. If you remove this, make it possible to access the stuff in the inventory from the outside please. - Sean
        public List<Item> EntityInventory { get { return inventory; } }

        /// <summary>
        /// Gets and sets the active weapon, if the incoming switch is invalid, it changes it to the first weapon
        /// </summary>
        public int ActiveWeapon {
            get { return activeWeapon; }
            set {
                if (value == -1)
                    activeWeapon = 0;
                else
                    activeWeapon = value;
            }
        }

        public Inventory() {
            inventory = new List<Item>();
            holster = new int[10];
            activeWeapon = -1;
        }

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        public void Add(Item item) {
            inventory.Add(item);
            if (item is Weapon)
                for (int x = 0; x < holster.Length; x++)
                    if (holster[x] == 0)
                        holster[x] = inventory.Count - 1;
        }

        /// <summary>
        /// Returns the currently equipped primary weapon, if any.
        /// </summary>
        /// <returns>The primary weapon, if any. Otherwise returns null.</returns>
        public Weapon GetEquippedPrimary() {
            return activeWeapon == -1 ? null : inventory[activeWeapon] as Weapon;
        }
    }
}
