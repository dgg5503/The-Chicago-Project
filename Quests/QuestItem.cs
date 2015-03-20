using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Item;

namespace TheChicagoProject.Quests
{
    //Sean Levorse
    /// <summary>
    /// An item that may be important to the quest
    /// </summary>
    class QuestItem
    {
        //fields
        private string name;
        private Item.Item item;
        private Entity.Entity entity; //an optional entity that could represent a physical item

        //properties
        public string Name { get { return name; } }
        public Item.Item Item { get { return item; } set { item = value; } }

        //constructor
        public QuestItem(string name, Entity.Entity entity = null)
        {
            this.name = name;
            this.entity = entity;
        }
    }
}
