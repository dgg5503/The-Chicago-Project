//Josiah DeVizia

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Item;
using TheChicagoProject.AI;
using TheChicagoProject.GUI;
using TheChicagoProject.GUI.Particles;
using TheChicagoProject.GUI.Forms;
using TheChicagoProject.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheChicagoProject.Entity
{
    public class LivingEntity : Entity
    {
        public Inventory inventory;

        public int health;

        public int maxHealth;

        protected GameTime time;

        public double lastShot;

        private static Random rand = new Random();

        public AI.AI ai;

        private int cash;

        private float interactRange;

        private ProgressBar healthBar;

        /// <summary>
        /// Gets or sets the amount of cash the player has
        /// </summary>
        public int Cash {
            get { return cash; }
            set {
                if (value >= 0)
                    cash = value;
            }
        }


        public double LastShot {
            get { return lastShot; }
        }

        /// <summary>
        /// Creates a new Living Entity
        /// </summary>
        /// <param name="rect">The rectangle that represents the location and width and height of the entity</param>
        /// <param name="fileName">the location of the sprite for this entity</param>
        public LivingEntity(FloatRectangle rect, Sprite sprite, int health, AI.AI ai = null, int cash = 0)
            : base(rect, sprite) {
            inventory = new Inventory();
            time = new GameTime();
            lastShot = 60000D;

            interactRange = 32;
            // the below depend on texture, this should not be needed ever but because of the player texture it is...
            //this.interactBoundsOffsetY = interactBoundsOffsetY;
            //this.interactBoundsOffsetX = interactBoundsOffsetX;

            this.cash = cash;
            this.health = health;
            this.ai = ai;

            maxHealth = health;

            color = Color.Red;

            healthBar = new ProgressBar(new Vector2(60, 20));
            healthBar.MaxValue = maxHealth;
            healthBar.CurrentValue = health;
            healthBar.IncludeText = Name;
            healthBar.LoadVisuals(Game1.Instance.Content, Game1.Instance.GraphicsDevice);
            controls.Add(healthBar);
            
        }

        /// <summary>
        /// Updates the Game Time and Entity Manager of the Living Entity
        /// </summary>
        /// <param name="time">The GameTime</param>
        /// <param name="manager">The Entity Manager that links to the living entity</param>
        public override void Update(GameTime time, EntityManager manager) {
            base.Update(time, manager);

            if (this.health <= 0) {
                this.markForDelete = true;
                return;
            }

            if (ai != null)
                ai.Update(time, manager);

            this.time = time;
            lastShot += time.ElapsedGameTime.TotalMilliseconds;
            if (inventory.GetEquippedPrimary() != null && lastShot >= inventory.GetEquippedPrimary().ReloadTime * 1000D && inventory.GetEquippedPrimary().Reloading) {
                inventory.GetEquippedPrimary().Reload();
                inventory.GetEquippedPrimary().Reloading = false;
            }

            
            healthBar.MaxValue = maxHealth;
            healthBar.CurrentValue = health;
            healthBar.IncludeText = Name;
            healthBar.Location = new Vector2(this.location.Location.X - (healthBar.Size.X / 2) + (this.location.Width / 2), this.location.Location.Y - healthBar.Size.Y);
            healthBar.Update(time);
             
        }

        /// <summary>
        /// Draws the base sprite and a health bar!
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
            //healthBar.Draw(spriteBatch, gameTime);
        }

        /*
        public override void DrawUI(SpriteBatch spriteBatch, GameTime gameTime)
        {
            healthBar.Draw(spriteBatch, gameTime);
        }*/

        /// <summary>
        /// The Living Entity attacks
        /// </summary>
        /// <param name="type">0 is primary fire, 1 is secondary</param>
        /// <param name="weapon">The weapon with which they are attacking</param>
        public virtual void Attack(int type, Weapon weapon = null) {
            if (type == 0) {
                if (weapon.LoadedAmmo > 0 && lastShot > (60000D / (weapon.rateOfFire)) && !weapon.Reloading) {
                    lastShot = 0D;
                    double trajectory = faceDirection + ((rand.NextDouble() * Math.PI / 2) - (Math.PI / 4)) * (weapon.spread / 100); // or we could make the cone the weapon spread amount (-weapon.spread to weapon.spread)
                    // this is just using the weapon.spread as a multiplier for a max range being -pi/2 to pi/2 relative to face direction

                    // need to get FRONT face, location will not work since that is not rotated, only the sprite is.
                    // we must utilize facedirection to calculate where on the outside of the sprite the shooting location should be set then apply an offset for weaponry...
                    //Game1.Instance.worldManager.CurrentWorld.manager.FireBullet(location.X, location.Y, (float)System.Math.Cos(trajectory), (float)System.Math.Sin(trajectory), inventory.GetEquippedPrimary().Damage, this);
                    // Douglas Gliner
                    Game1.Instance.worldManager.CurrentWorld.manager.FireBullet(((int) (sprite.Texture.Width / 2) * (float) Math.Cos(faceDirection - Math.PI / 4)) + location.Center.X,
                        ((int) (sprite.Texture.Height / 2) * (float) Math.Sin(faceDirection - Math.PI / 4)) + location.Center.Y,
                        (float) Math.Cos(trajectory - Math.PI / 2), 
                        (float) Math.Sin(trajectory - Math.PI / 2),
                        inventory.GetEquippedPrimary().Damage, 
                        this);
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

        // spending too much time on making this versatile, this can only be 32 units away from player.
        public void Interact() {
            // ray cast
            RotatedRectangle rayCast = new RotatedRectangle(new FloatRectangle((location.Center.X + (((sprite.Texture.Width) * (float)Math.Cos(faceDirection - Math.PI / 2))) - (sprite.Texture.Width / 2)), (location.Center.Y + (((sprite.Texture.Height) * (float)Math.Sin(faceDirection - Math.PI / 2))) - (interactRange / 2)), sprite.Texture.Width, interactRange), faceDirection);

            //FloatRectangle rectangleToRotate = new FloatRectangle(location.X + 5 + (sprite.Texture.Width * (float)Math.Cos(faceDirection - Math.PI / 2)), location.Y + 5 + (sprite.Texture.Height * (float)Math.Sin(faceDirection - Math.PI / 2)), sprite.Texture.Width, interactRange);

            //FloatRectangle rectangleToRotate = new FloatRectangle(location.Center.X - sprite.Texture.Width / 2, location.Center.Y - interactRange / 2, sprite.Texture.Width, interactRange);
            //RotatedRectangle rayCast = new RotatedRectangle(rectangleToRotate, faceDirection);
            

            //Console.WriteLine("Radius: {0}", Vector2.Distance(location.Location, rayCast.UpperLeftCorner()));
            //Console.WriteLine("X: {0}, Y: {1} Theta: {2}", rayCast.X - location.X, rayCast.Y - location.Y, faceDirection);
            //Console.Write("ThisX: {0}, ThisY: {1}", location.X, locat)
            // tiles to do a check for entities
            CollisionTile[] intersectingTiles = CurrentCollisionTile.GetAdjacentTilesFromIntersection(rayCast);
             
            // foreach through each tile, foreach through each ent list and find ent closest to this ent.
            // starts with distance from edge midpoint closest to player to top left corner.
            // the closer items are to player, that item will be acted upon.
            //float shortestDistance = (float)Math.Sqrt((rayCast.Height * rayCast.Height) + ((rayCast.Width * rayCast.Width) / 4));

            // start with longest distance possible which in this case is from center of player to a corner of raycast
            float shortestDistance = Vector2.Distance(this.location.Center, rayCast.UpperLeftCorner());
            Entity entityToInteract = null;

            foreach (CollisionTile tile in intersectingTiles) {
                // only look at entities that interset with raycast bounds!!
                foreach (Entity ent in tile.EntitiesInTile.Where(ent => rayCast.Intersects(ent.location))) {
                    if (ent == this)
                        continue;

                    // want to make sure ATLEAST more than the center is inside the check box!!
                    float tmpDist = Vector2.Distance(ent.location.Center, this.location.Center);
                    if (tmpDist < shortestDistance && ent.interactData != null) {
                        entityToInteract = ent;
                        shortestDistance = tmpDist;
                    }
                }
            }

            // debug
            

            // if entity is null, return
            if (entityToInteract == null)
                return;

            //We need to fix this.
            entityToInteract.Action(this, "");
            
            // do interact method here... (events or method?)
            Game1.Instance.renderManager.EmitParticle(new RectangleOutline(new RotatedRectangle(entityToInteract.location, 0), Color.Purple, 1));
            Game1.Instance.renderManager.EmitParticle(new RectangleOutline(rayCast, Color.White, 1));
            foreach (CollisionTile t in intersectingTiles)
                Game1.Instance.renderManager.EmitParticle(new RectangleOutline(new RotatedRectangle(t.Rectangle, 0), Color.Red, 1));
        }

        /// <summary>
        /// Moves the Living Entity
        /// </summary>
        public override void Move() {
            float move = 1.5f;
            switch (direction) {
                case Direction.Down:
                    movement = new Vector2(0, move);
                    //      this.faceDirection = 2 * (float) Math.PI / 2f;
                    break;
                case Direction.Up:
                    movement = new Vector2(0, -move);
                    //       this.faceDirection = 0 * (float) Math.PI / 2f;
                    break;
                case Direction.Left:
                    movement = new Vector2(-move, 0);
                    //       this.faceDirection = 3 * (float) Math.PI / 2f;
                    break;
                case Direction.Right:
                    movement = new Vector2(move, 0);
                    //      this.faceDirection = 1 * (float) Math.PI / 2f;
                    break;

                case Direction.DownLeft:
                    movement = new Vector2(-move, move);
                    //       this.faceDirection = 5 * (float) Math.PI / 4f;
                    break;
                case Direction.DownRight:
                    movement = new Vector2(move, move);
                    //        this.faceDirection = 3 * (float) Math.PI / 4f;
                    break;
                case Direction.UpLeft:
                    movement = new Vector2(-move, -move);
                    //         this.faceDirection = 7 * (float) Math.PI / 4f;
                    break;
                case Direction.UpRight:
                    movement = new Vector2(move, -move);
                    //  this.faceDirection = 1 * (float) Math.PI / 4f;
                    break;
            }
            Player player = Game1.Instance.worldManager.CurrentWorld.manager.GetPlayer();
            float actual = (float) Math.Atan2(this.location.Center.Y - player.location.Center.Y, this.location.Center.X - player.location.Center.X);
            actual -= (float) Math.PI / 2f;
            this.faceDirection = actual;
        }

        
    }
}
