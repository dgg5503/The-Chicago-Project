//Josiah DeVizia

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Item;
using TheChicagoProject.AI;
using TheChicagoProject.GUI;
using Microsoft.Xna.Framework;

namespace TheChicagoProject.Entity
{
    public class LivingEntity : Entity
    {
        public Inventory inventory;

        public int health;

        protected GameTime time;

        protected double lastShot;

        private static Random rand = new Random();

        public AI.AI ai;

        /// <summary>
        /// Creates a new Living Entity
        /// </summary>
        /// <param name="rect">The rectangle that represents the location and width and height of the entity</param>
        /// <param name="fileName">the location of the sprite for this entity</param>
        public LivingEntity(FloatRectangle rect, Sprite sprite, int health, AI.AI ai = null)
            : base(rect, sprite) {
            inventory = new Inventory();
            time = new GameTime();
            lastShot = 60000D;
            this.health = health;
            this.ai = ai;
        }

        /// <summary>
        /// Updates the Game Time and Entity Manager of the Living Entity
        /// </summary>
        /// <param name="time">The GameTime</param>
        /// <param name="manager">The Entity Manager that links to the living entity</param>
        public override void Update(GameTime time, EntityManager manager) {
            base.Update(time, manager);

            if (ai != null)
                ai.Update(time, manager);

            this.time = time;
            lastShot += time.ElapsedGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// The Living Entity attacks
        /// </summary>
        /// <param name="type">0 is primary fire, 1 is secondary</param>
        /// <param name="weapon">The weapon with which they are attacking</param>
        public virtual void Attack(int type, Weapon weapon = null) {
            if (type == 0) {
                if (weapon.LoadedAmmo > 0 && lastShot > (60000D / (weapon.rateOfFire))) {
                    lastShot = 0D;
                    double trajectory = faceDirection + ((rand.NextDouble() - .5) * 2) * weapon.accuracy; // replace weapon.accuracy with 0 to see perfect alignment.

                    // need to get FRONT face, location will not work since that is not rotated, only the sprite is.
                    // we must utilize facedirection to calculate where on the outside of the sprite the shooting location should be set then apply an offset for weaponry...
                    //Game1.Instance.worldManager.CurrentWorld.manager.FireBullet(location.X, location.Y, (float)System.Math.Cos(trajectory), (float)System.Math.Sin(trajectory), inventory.GetEquippedPrimary().Damage, this);
                    // Douglas Gliner
                    Game1.Instance.worldManager.CurrentWorld.manager.FireBullet(((int)(sprite.Texture.Width / 2) * (float)Math.Cos(faceDirection - Math.PI / 2)) + location.Center.X, ((int)(sprite.Texture.Height / 2) * (float)Math.Sin(faceDirection - Math.PI / 2)) + location.Center.Y, (float)Math.Cos(trajectory - Math.PI / 2), (float)Math.Sin(trajectory - Math.PI / 2), inventory.GetEquippedPrimary().Damage, this);
                    weapon.LoadedAmmo--;
                }
            }
        }

        /// <summary>
        /// Moves the Living Entity
        /// </summary>
        public override void Move() {
            float move = 1.5f;
            switch (direction) {
                case Direction.Down:
                    movement = new Vector2(0, move);
                    break;
                case Direction.DownLeft:
                    movement = new Vector2(-move, move);
                    break;
                case Direction.DownRight:
                    movement = new Vector2(move, move);
                    break;
                case Direction.Left:
                    movement = new Vector2(-move, 0);
                    break;
                case Direction.Right:
                    movement = new Vector2(move, 0);
                    break;
                case Direction.Up:
                    movement = new Vector2(0, -move);
                    break;
                case Direction.UpLeft:
                    movement = new Vector2(-move, -move);
                    break;
                case Direction.UpRight:
                    movement = new Vector2(move, -move);
                    break;
            }
        }
    }
}
