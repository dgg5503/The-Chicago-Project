//Josiah S DeVizia

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Item;
using Microsoft.Xna.Framework;

namespace TheChicagoProject.Entity
{

    /// <summary>
    /// The direction a Living Entity is facing
    /// The values are coded so that they can be casted
    /// to ints for use in the code
    /// </summary>
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

        /// <summary>
        /// Creates a new Player
        /// </summary>
        /// <param name="location">A rectangle representing the location of the player</param>
        /// <param name="fileName">The location of the sprite in the files</param>
        public Player(Rectangle location, string fileName) : base(location, fileName)
        {
            cash = 40;
            questPoints = 0;
            lives = 4;
            direction = Direction.Up;
            rand = new Random();
            log = new Quests.QuestLog();
            health = lives;
        }

        /// <summary>
        /// Gets or sets the amount of cash the player has
        /// </summary>
        public int Cash
        {
            get { return Cash; }
            set
            {
                if(value < 0)
                    cash = value;
            }
        }

        //Sean Levorse
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

        //Sean Levorse
        /// <summary>
        /// calculates the number of lives the player should have based on the number of quest points
        /// </summary>
        /// <param name="qPoints">the number of quest points</param>
        /// <returns>the number of lives that the player should have at max health</returns>
        private int QuestPointsToLives(int qPoints)
        {
            int newLives = 4 + (int)(2 * System.Math.Sqrt(qPoints));
            return newLives;
        }

        //Sean Levorse
        /// <summary>
        /// Gives the player a certain amount of cash
        /// </summary>
        /// <param name="amount">Amount of cash given to the player</param>
        public void AwardCash(int amount)
        {
            Cash += amount;
        }

        /// <summary>
        /// Updates the time and EntityManager the player uses
        /// </summary>
        /// <param name="time">The game time</param>
        /// <param name="em">The entity manager that links to the player</param>
        public override void Update(GameTime time, EntityManager em)
        {
            base.Update(time, em);
            gameTime = time;
        }
    }
}
