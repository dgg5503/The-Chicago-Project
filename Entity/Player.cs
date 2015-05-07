﻿//Josiah S DeVizia

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Item;
using TheChicagoProject.GUI;
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
        
        private int questPoints;
        private int lives;
        private Random rand;
        GameTime gameTime;
        public Quests.QuestLog log;
        public Point cursor;

        /// <summary>
        /// Creates a new Player
        /// </summary>
        /// <param name="location">A rectangle representing the location of the player</param>
        /// <param name="fileName">The location of the sprite in the files</param>
        public Player(FloatRectangle location, Sprite sprite) : base(location, sprite, 10, null, 40)
        {
            //cash = 40;
            questPoints = 0;
            lives = 4;
            direction = Direction.Up;
            rand = new Random();
            log = new Quests.QuestLog();
            health = lives;
        }

        public Point Cursor
        {
            set { cursor = value; }
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
            inventory.GetEquippedPrimary().Reloading = true;
            
        }

        /// <summary>
        /// Interacts with the environment
        /// </summary>
        /*
        public void Interact()
        {
            throw new NotImplementedException();
        }
         */

        /*
        /// <summary>
        /// Attacks
        /// </summary>
        /// <param name="type">Type of Fire</param>
        /// <param name="weapon">Weapon used to fire, cannot be null if primary fire</param>
        public override void Attack(int type, Weapon weapon = null)
        {
            if (type == 0)
            {
                if(weapon == null)
                {
                    throw new ArgumentException();
                }
                if (lastShot > (60000D / (weapon.rateOfFire)))
                {
                    //int magnitude = (int) Math.Sqrt(Math.Pow(cursor.X, 2) + Math.Pow(cursor.Y, 2));
                    //Game1.Instance.worldManager.CurrentWorld.manager.FireBullet(location.X, location.Y, (float)(cursor.X / magnitude), (float)(cursor.Y / magnitude), inventory.GetEquippedPrimary().Damage, this);

                    // need to get FRONT face, location will not work since that is not rotated, only the sprite is.
                    // we must utilize facedirection to calculate where on the outside of the sprite the shooting location should be set then apply an offset for weaponry...
                    Game1.Instance.worldManager.CurrentWorld.manager.FireBullet((16 * (float)Math.Cos(faceDirection - Math.PI / 2)) + location.Center.X, (16 * (float)Math.Sin(faceDirection - Math.PI / 2)) + location.Center.Y, (float)Math.Cos(faceDirection - Math.PI / 2), (float)Math.Sin(faceDirection - Math.PI / 2), inventory.GetEquippedPrimary().Damage, this);
                }
            }
        }
        */

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

            movement.X = 0;
            movement.Y = 0;

            if(Game1.Instance.worldManager.CurrentWorld.tiles[(int)(location.X / Tile.SIDE_LENGTH)][(int)(location.Y / Tile.SIDE_LENGTH)] is Door)
            {
                Door door = (Game1.Instance.worldManager.CurrentWorld.tiles[(int)(location.X / Tile.SIDE_LENGTH)][(int)(location.Y / Tile.SIDE_LENGTH)] as Door);
                if (!Game1.Instance.worldManager.worlds.ContainsKey(door.World))
                {
                    Game1.Instance.worldManager.worlds.Add(door.World, Game1.Instance.saveManager.LoadWorld(door.World));
                }
                Game1.Instance.worldManager.current = door.World;
                location.X = door.Destination.X * GUI.Tile.SIDE_LENGTH;
                location.Y = door.Destination.Y * GUI.Tile.SIDE_LENGTH;
                Game1.Instance.worldManager.CurrentWorld.manager.AddEntity(this);
            }
        }
    }
}
