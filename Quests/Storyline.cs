using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Entity;

namespace TheChicagoProject.Quests
{
    //Sean Levorse
    /// <summary>
    /// Essentially a list of quests for plots that works polymorphically with quest logs
    /// </summary>
    public class Storyline : Quest
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
        public Storyline(string name, string objective, string description, Quest firstQuest, Player player, WorldManager worldManager, WinCondition winCondition, int reward = 0, int cashReward = 0) : base(name, objective, description, firstQuest.StartPoint, player, worldManager, winCondition, reward, cashReward)
        {
            quests = new List<Quest>();
            quests.Add(firstQuest);
        }

        public override void Update()
        {
            foreach(Quest quest in quests)
            {
                quest.Update();
            }
        }
    }
}
