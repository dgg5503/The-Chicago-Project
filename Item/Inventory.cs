using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject.Item
{
    class Inventory
    {
        private List<Item> inventory;
        private int primary;
        private int secondary;

        public Inventory() {
            inventory = new List<Item>();
            primary = -1;
            secondary = -1;
        }

        public void Add(Item item) {
            inventory.Add(item);
            if (item is Weapon)
                //if it is a primary
                if (primary == -1)
                    primary = inventory.Count - 1;
                //if it's a secondary
        }

        public Weapon getEquippedPrimary() {
            return primary == -1 ? null : inventory[primary] as Weapon;
        }
    }
}
