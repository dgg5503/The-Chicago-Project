using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Entity;
using Microsoft.Xna.Framework;

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
        private QuestNode head;
        private int completed;
        public List<Quest> current;

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

        /// <summary>
        /// the number of quests completed
        /// </summary>
        public int NumCompleted { get { return completed; } }

        //Constructor
        public Storyline(string name, string objective, string description, Vector2 StartPoint, Player player, WorldManager worldManager, WinCondition winCondition, int reward = 0, int cashReward = 0) 
            : base(name, objective, description, StartPoint, player, winCondition, reward, cashReward)
        {
            quests = new List<Quest>();
        }

        /// <summary>
        /// Adds quests to a quest log
        /// </summary>
        /// <param name="questToAdd">The quest</param>
        /// <param name="prerequisites">A list of prerequisite Quests</param>
        public void Add(Quest questToAdd, params Quest [] prerequisites)
        {
            if(head == null && prerequisites.Length != 0)
            {
                throw new ArgumentException("The first quest cannot have prerequisites");
            }
            else if(head == null)
            {
                head = new QuestNode(questToAdd);
            }

        }

        ///<summary>
        /// updates all of the quests in the storyline
        /// </summary>
        public override void Update()
        {
            foreach(Quest quest in quests)
            {
                quest.Update();
                
            }
        }

        /// <summary>
        /// Node for building a graph of quest prerequisites
        /// </summary>
        private class QuestNode
        {
            //properties
            public List<Quest> nextQuests;
            public Quest data;
            public List<Quest> prerQuests;

            //constructor
            public QuestNode(Quest quest)
            {
                data = quest;
                nextQuests = new List<Quest>();
                prerQuests = new List<Quest>();
            }
        }
    }
}
