//Josiah DeVizia

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Item;
using Microsoft.Xna.Framework;

namespace TheChicagoProject.Entity
{

    public enum Direction
    {
        Up = 0,
        UpRight = 1,
        Right = 2,
        DownRight = 3,
        Down = 4,
        DownLeft = 5,
        Left = 6,
        UpLeft = 7
    }

    public class Player : LivingEntity
    {
        private int cash;
        private int questPoints;
        private int lives;
        private Random rand;
        GameTime gameTime;
        public Quests.QuestLog log;

        public Player(Rectangle location, string fileName) : base(location, fileName)
        {
            cash = 40;
            questPoints = 0;
            lives = 4;
            direction = Direction.Up;
            rand = new Random();
        }

        //properties
        public int Cash
        {
            get { return Cash; }
            set
            {
                if(value < 0)
                    cash = value;
            }
        }

        public int QuestPoints { get { return questPoints; } 
            set 
            { 
                questPoints = value;
                lives = QuestPointsToLives(questPoints);
                /****************************************************
                 *                                                  *
                 *    Display Message About getting a new heart?    *
                 *                                                  *
                 ****************************************************/
            }
        }

        /// <summary>
        /// Reloads the active weapon
        /// </summary>
        public void Reload()
        {
            inventory.GetEquippedPrimary().Reload(inventory.GetEquippedPrimary().maxClip);
        }

        /// <summary>
        /// Interacts with the environment
        /// </summary>
        public void Interact()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// calculates the number of lives the player should have based on the number of quest points
        /// </summary>
        /// <param name="qPoints"></param>
        /// <returns></returns>
        private int QuestPointsToLives(int qPoints)
        {
            int newLives = 4 + (int)(2 * System.Math.Sqrt(qPoints));
            return newLives;
        }

        /// <summary>
        /// Gives the player a certain amount of cash
        /// </summary>
        /// <param name="amount">Amount of cash given to the player</param>
        public void AwardCash(int amount)
        {
            Cash += amount;
        }

        public override void Update(GameTime time, EntityManager em)
        {
            base.Update(time, em);
            gameTime = time;
        }
    }
}
