//Josiah DeVizia

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Item;
using TheChicagoProject.AI;
using TheChicagoProject.GUI;
using TheChicagoProject.GUI.Particles;
using TheChicagoProject.Collision;
using Microsoft.Xna.Framework;

namespace TheChicagoProject.Entity
{
    public class LivingEntity : Entity
    {
        public Inventory inventory;

        public int health;

        public readonly int maxHealth;

        protected GameTime time;

        protected double lastShot;

        private static Random rand = new Random();

        public AI.AI ai;

        private int cash;

        private float interactRange;

        private float interactBoundsOffsetY;

        private float interactBoundsOffsetX;

        /// <summary>
        /// Gets or sets the amount of cash the player has
        /// </summary>
        public int Cash
        {
            get { return cash; }
            set
            {
                if (value < 0)
                    cash = value;
            }
        }


        public double LastShot
        {
            get { return lastShot; }
        }

        /// <summary>
        /// Creates a new Living Entity
        /// </summary>
        /// <param name="rect">The rectangle that represents the location and width and height of the entity</param>
        /// <param name="fileName">the location of the sprite for this entity</param>
        public LivingEntity(FloatRectangle rect, Sprite sprite, int health, AI.AI ai = null, int cash = 0, int interactRange = 20, int interactBoundsOffsetY = 5, int interactBoundsOffsetX = 0)
            : base(rect, sprite) {
            inventory = new Inventory();
            time = new GameTime();
            lastShot = 60000D;

            this.interactRange = interactRange;
            // the below depend on texture, this should not be needed ever but because of the player texture it is...
            this.interactBoundsOffsetY = interactBoundsOffsetY;
            this.interactBoundsOffsetX = interactBoundsOffsetX;

            this.cash = cash;
            this.health = health;
            this.ai = ai;

            maxHealth = health;
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
            if(inventory.GetEquippedPrimary() != null && lastShot >= inventory.GetEquippedPrimary().ReloadTime * 1000D)
            {
                inventory.GetEquippedPrimary().Reloading = false;
            }
        }

        /// <summary>
        /// The Living Entity attacks
        /// </summary>
        /// <param name="type">0 is primary fire, 1 is secondary</param>
        /// <param name="weapon">The weapon with which they are attacking</param>
        public virtual void Attack(int type, Weapon weapon = null) {
            if (type == 0) {
                if (weapon.LoadedAmmo > 0 && lastShot > (60000D / (weapon.rateOfFire)) && !weapon.Reloading)
                {
                    lastShot = 0D;
                    double trajectory = faceDirection + ((rand.NextDouble() * Math.PI / 2) - (Math.PI / 4)) * (weapon.spread / 100); // or we could make the cone the weapon spread amount (-weapon.spread to weapon.spread)
                                                                                                                                     // this is just using the weapon.spread as a multiplier for a max range being -pi/2 to pi/2 relative to face direction
                   
                    // need to get FRONT face, location will not work since that is not rotated, only the sprite is.
                    // we must utilize facedirection to calculate where on the outside of the sprite the shooting location should be set then apply an offset for weaponry...
                    //Game1.Instance.worldManager.CurrentWorld.manager.FireBullet(location.X, location.Y, (float)System.Math.Cos(trajectory), (float)System.Math.Sin(trajectory), inventory.GetEquippedPrimary().Damage, this);
                    // Douglas Gliner
                    Game1.Instance.worldManager.CurrentWorld.manager.FireBullet(((int)(sprite.Texture.Width / 2) * (float)Math.Cos(faceDirection - Math.PI / 2)) + location.Center.X, ((int)(sprite.Texture.Height / 2) * (float)Math.Sin(faceDirection - Math.PI / 2)) + location.Center.Y, (float)Math.Cos(trajectory - Math.PI / 2), (float)Math.Sin(trajectory - Math.PI / 2), inventory.GetEquippedPrimary().Damage, this);
                    weapon.LoadedAmmo--;
                }
            }
        }

        /*
         * To-do
         * - throw a rectangle in the direction entity is facing (faceDirection)
         * - check to see if this invis rectangle or the player intersects with anything
         *      - use collision manager to get surrounding tiles from where this entity is.
         *      - speedup: only check tiles that are in the same direction as the player (math here . 3.)
         * - once another ENTITY is found, call its action method (pass this entity as a param.) (NOT SURE ABOUT THIS...)
         *      - closest to the player or first to find?
         */

        public void Interact()
        {
            // ray cast
            RotatedRectangle rayCast = new RotatedRectangle(new FloatRectangle(location.X + interactBoundsOffsetX + ((int)(sprite.Texture.Width / 2) * (float)Math.Cos(faceDirection - Math.PI / 2)), location.Y + interactBoundsOffsetY + ((int)(sprite.Texture.Height / 2) * (float)Math.Sin(faceDirection - Math.PI / 2)), sprite.Texture.Height, interactRange), faceDirection);
            
            // tiles to do a check for entities
            CollisionTile[] intersectingTiles = CurrentCollisionTile.GetAdjacentTilesFromIntersection(rayCast);
            
            // foreach through each tile, foreach through each ent list and find ent closest to this ent.
            float shortestDistance = (float)Math.Sqrt((interactRange * interactRange) + ((rayCast.Width * rayCast.Width) / 4));

            Entity entityToInteract = null;

            foreach (CollisionTile tile in intersectingTiles)
            {
               // only look at entities that interset with raycast bounds!!
               foreach(Entity ent in tile.EntitiesInTile.Where(ent => rayCast.Intersects(ent.location)))
               {
                   if (ent == this)
                       continue;

                   float tmpDist = Vector2.Distance(ent.location.Center, this.location.Center);
                   if (tmpDist < shortestDistance)
                   {
                       entityToInteract = ent;
                       shortestDistance = tmpDist;
                   }
               }
            }

            // debug
            Game1.Instance.renderManager.EmitParticle(new RectangleOutline(rayCast, Color.White, 1));
            foreach (CollisionTile t in intersectingTiles)
                Game1.Instance.renderManager.EmitParticle(new RectangleOutline(new RotatedRectangle(t.Rectangle, 0), Color.Red, 1));

            // if entity is null, return
            if (entityToInteract == null)
                return;

            // do interact method here... (events or method?)
            Game1.Instance.renderManager.EmitParticle(new RectangleOutline(new RotatedRectangle(entityToInteract.location, 0), Color.Purple, 1));
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
