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
        private WinCondition winCondition;
        private LivingEntity enemyToKill;
        private LivingEntity recipient;
        private Item.Item delivery;
        private Item.Item findThis; //Why are variable names that mean something so hard.
        private Player player;

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
        public WinCondition WinCondition { get { return winCondition; } set { winCondition = value; } }
        public LivingEntity EnemyToKill { get { return enemyToKill; } set { enemyToKill = value; } }
        public LivingEntity Recipient { get { return recipient; } set { recipient = value; } }
        public Item.Item Delivery { get { return delivery; } set { delivery = value; } }
        public Item.Item FindThis { get { return findThis; } set { findThis = value; } }

        //Constructor
        public Quest(string name, string objective, string description, Vector2 start, Player player, WorldManager worldManager, WinCondition winCondition, int reward = 1, int cashReward = 10)
        {
            this.name = name;
            this.objective = objective;
            this.description = description;
            this.startPoint = start;
            this.Reward = reward;
            this.CashReward = cashReward;
            this.player = player;
            this.worldManager = worldManager;
            this.winCondition = winCondition;
            entitites = new List<Entity.Entity>();
            status = 0;
        }

        /// <summary>
        /// Updates the player's quest log, gives them the reward and updates this quest object's status
        /// </summary>
        /// <param name="player">The player object</param>
        public void Completed()
        {
            status = (int)State.Completed;
            player.Cash += cashReward;
            player.QuestPoints += reward;
        }

        /// <summary>
        /// Sets the quest's status to in progress(2)
        /// </summary>
        public virtual void StartQuest()
        {
            status = (int)State.InProgress;
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
            status = (int)State.Unstarted;
        }

        /// <summary>
        /// sets the status to be unavailable
        /// </summary>
        public virtual void SetUnavailable()
        {
            status = (int)State.Unavailable;
        }

        /// <summary>
        /// Checks to see if the quest has been completed
        /// </summary>
        public virtual void Update()
        {
            switch (winCondition)
            {
                case WinCondition.EnemyDies:
                    if (enemyToKill.health <= 0)
                    {
                        this.Completed();
                    }
                    break;
                case WinCondition.AllEnemiesDead:
                    bool allDead = true;
                    foreach(LivingEntity enemy in entitites)
                    {
                        if (enemy.health > 0)
                            allDead = false;
                    }
                    if(allDead)
                    {
                        this.Completed();
                    }
                    break;
                case WinCondition.ObtainItem:
                    if(player.inventory.EntityInventory.Contains(FindThis))
                    {
                        this.Completed();
                    }
                    break;
                case WinCondition.DeliverItem:
                    throw new NotImplementedException("Please implement Player's Interact() method before implenting this");
                default:
                    break;
            }
        }
        


    }//class

    public enum WinCondition
    {
        EnemyDies,
        AllEnemiesDead,
        ObtainItem,
        DeliverItem
    }

    public enum State
    {
        Unavailable = 0,
        Unstarted = 1,
        InProgress = 2,
        Completed = 3
    }
}
