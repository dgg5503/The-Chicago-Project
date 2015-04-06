using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using TheChicagoProject.Entity;

namespace TheChicagoProject.Quests
{
    //Sean Levorse
    /// <summary>
    /// Represents a quest and stores information about the quest
    /// </summary>
    public class Quest
    {
        //fields
        private int status;                 //0: Unavailable, 1: Unstarted, 2: In progress, 3: complete
        private string objective;
        private string description;
        private string name;
        private Vector2 startPoint;
        private int reward;
        private int cashReward;
        private WorldManager worldManager;

        public List<Entity.Entity> entitites;

        //properties
        public int Status { get { return status; }
            set
            {
                //detect invalid arguments
                if (value < 0 || value > 3)
                    throw new ArgumentOutOfRangeException("Status must be an integer between 0 nd 3.");
                status = value;
            }
        }
        public string Objective { get { return objective; } }
        public string Description { get { return description; } }
        public string Name { get { return name; } }
        public Vector2 StartPoint { get { return startPoint; } }
        public int Reward {get{return reward;}
            set 
            {
                //Quests can't have negative rewards
                if (value < 0)
                    reward = 0;
                else
                    reward = value;
            }
        }
        public int CashReward { get { return cashReward; }
            set
            {
                if (value < 0)
                    cashReward = 0;
                else
                    cashReward = value;
            }
        }

        //Constructor
        public Quest(string name, string objective, string description, Vector2 start, WorldManager worldManager, int reward = 1, int cashReward = 10)
        {
            this.name = name;
            this.objective = objective;
            this.description = description;
            this.startPoint = start;
            this.Reward = reward;
            this.CashReward = cashReward;
            this.worldManager = worldManager;
            status = 0;
        }

        /// <summary>
        /// Updates the player's quest log, gives them the reward and updates this quest object's status
        /// </summary>
        /// <param name="player">The player object</param>
        public void Completed(Player player)
        {
            status = 3;
            player.Cash += cashReward;
            player.QuestPoints += reward;
        }

        /// <summary>
        /// Sets the quest's status to in progress(2)
        /// </summary>
        public virtual void StartQuest()
        {
            status = 2;
            //initialize each entity
            foreach (Entity.Entity entity in entitites)
            {
                worldManager.CurrentWorld.manager.AddEntity(entity); //not entitity
                Console.WriteLine("Entity Added");
            }
        }

        /// <summary>
        /// makes the quest available
        /// </summary>
        public virtual void SetAvailable()
        {
            status = 1;
        }

        /// <summary>
        /// sets the status to be unavailable
        /// </summary>
        public virtual void SetUnavailable()
        {
            status = 0;
        }

        /// <summary>
        /// Checks to see if the quest has been completed
        /// </summary>
        public virtual void Update(){}
        


    }
}
