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
        public Weapon[] holster;
        private int activeWeapon;
        private int cash;
        private int questPoints;
        private int lives;
        public static Direction direction;
        private Random rand;
        GameTime gameTime;
        public Quests.QuestLog log;

        private double lastShot;    //The time of the last time the player shot

        public Player(Rectangle location) : base(null, location)
        {
            holster = new Weapon[10];
            activeWeapon = 0;
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
        /// The player attacks
        /// </summary>
        /// <param name="type">0 is primary fire, 1 is secondary</param>
        public override void Attack(int type)
        {
            if(type == 0)
            {
                double trajectory = rand.NextDouble() * holster[activeWeapon].Accuracy * (double)(rand.Next(2) - 1);
                trajectory += (double)((int)direction * System.Math.PI / 8D);
                EntityManager.FireBullet(location.X, location.Y, System.Math.Cos(trajectory), System.Math.Sin(trajectory));
            }
        }

        /// <summary>
        /// Reloads the active weapon
        /// </summary>
        public void Reload()
        {
            holster[activeWeapon].Reload(holster[activeWeapon].MaxClip);
        }

        /// <summary>
        /// Interacts with the environment
        /// </summary>
        public void Interact()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets and sets the active weapon, if the incoming switch is invalid, it changes it to the first weapon
        /// </summary>
        public int ActiveWeapon
        {
            get
            {
                return activeWeapon;
            }

            set
            {
                if (holster[value] == null)
                    activeWeapon = 1;
            }
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

        public override void Update(GameTime time)
        {
            gameTime = time;
        }
    }
}
