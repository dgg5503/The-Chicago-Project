using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject.Quests
{
    /// <summary>
    /// An item that may be important to the quest
    /// </summary>
    class QuestItem
    {
        //fields
        private string name;

        //properties
        public string Name { get { return name; } }

        //constructor
        public QuestItem(string name)
        {
            this.name = name;
        }
    }
}
