using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject.Quests
{
    /// <summary>
    /// Essentially a list of quests for plots that works polymorphically with quest logs
    /// </summary>
    class Storyline : Quest
    {
        //Fields
        private List<Quest> quests;

        //properties
        /// <summary>
        /// Accesses one quest in the sotry line
        /// </summary>
        /// <param name="index">The index of the quest</param>
        /// <returns>The Quest</returns>
        public Quest this[int index]
        {
            get 
            {
                if (index < 0 || index >= quests.Count)
                    throw new IndexOutOfRangeException("Index must be between 0 and the length of the number of quests");
                else
                    return quests[index];
            }

            set
            {
                if (index < 0)
                    throw new IndexOutOfRangeException("Index must be greater than 0");
                else if (index >= quests.Count)
                    quests.Add(value);
                else
                    quests[index] = value;
            }
        }

        //Constructor
        public Storyline(string name, string objective, string description, Quest firstQuest, int reward = 0, int cashReward = 0) : base(name, objective, description, firstQuest.StartPoint, reward, cashReward)
        {
            quests.Add(firstQuest);
        }
    }
}
