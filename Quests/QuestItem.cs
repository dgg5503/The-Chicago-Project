using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Item;

namespace TheChicagoProject.Quests
{
    /// <summary>
    /// An item that may be important to the quest
    /// </summary>
    class QuestItem
    {
        //fields
        private string name;
        private Item.Item item;

        //properties
        public string Name { get { return name; } }
        public Item.Item Item { get { return item; } set { item = value; } }

        //constructor
        public QuestItem(string name)
        {
            this.name = name;
        }
    }
}
