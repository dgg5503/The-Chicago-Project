//Josiah S DeVizia

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.GUI;
using TheChicagoProject.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheChicagoProject.Entity
{
    /// <summary>
    /// The base entity class.
    /// </summary>
    public abstract class Entity
    {
        public FloatRectangle location;

        public Direction direction;

        public Sprite sprite;

        public Vector2 movement;

        // The current tile the entity is in...
        public CollisionTile Tile;

        /// <summary>
        /// The constructor for the base entity.
        /// </summary>
        /// <param name="fileName">The texture filename for the entity.</param>
        /// <param name="location">The height, width, and X and Y location.</param>
        public Entity(FloatRectangle location, string fileName)
        {
            this.location = location;

            /*
             * We would need a custom file format in order to take in a sprite
             * and properly parse it for animation and what not.
             * 
             * AS OF NOW, all entities have a "simple" sprite which is
             * basically a static image (Can be rotated within the draw params.) (IWGTI)
             * 
             * 
             * TO RESIZE A SPRITE, EITHER DO SPRITE.RESIZE OR INSTANTIATE VIA
             * THE HEIGHT AND WIDTH FROM LOCATION RECTANGLE?????
             */
            sprite = new Sprite(location.IntHeight, location.IntWidth, 0, fileName);

            movement = Vector2.Zero;
        }

        /// <summary>
        /// The constructor for the base entity.
        /// </summary>
        /// <param name="sprite">Sprite object.</param>
        public Entity(Sprite sprite)
        {
            this.sprite = sprite;
            movement = Vector2.Zero;
        }

        /// <summary>
        /// Updates the Game Time and the Entity Manager of the Entity
        /// </summary>
        /// <param name="time">The game Time</param>
        /// <param name="manager">The EntityManager that links to the player</param>
        public virtual void Update(GameTime time, EntityManager manager)
        {
            // ------- COLLISION TEST ------
            double delta = time.ElapsedGameTime.TotalSeconds;

            // Calc vector from point before to new point based on deltaX and deltaY
            FloatRectangle lastLoc = this.location;

            location.X += movement.X;
            location.Y += movement.Y;

            // ------ EDGE OF SCREEN TEST ------
            if (location.X < 0)
            {
                location.X = 0;
            }

            if (location.Y < 0)
            {
                location.Y = 0;
            }
            // ------ EDGE OF SCREEN TEST ------

            if (this.Tile == null)
                return;

            // ------ TILE COLLISION TEST ------
            // if this intersects with a non walkable tile, react
            foreach(CollisionTile colTile in this.Tile.GetAdjacentNonWalkableTiles())
            {
                if(this.location.Intersects(colTile.Rectangle))
                {
                    CollisionReaction(colTile.Rectangle);
                    break;
                }
            }
            // ------ TILE COLLISION TEST ------

            if (this.Tile.EntitiesInTile.Count == 1)
                return;

            // ------- ENTITY COLLISION TEST ------
            foreach (Entity e in this.Tile.EntitiesInTile.Where(e => !(e.Equals(this))))
            {
                bool isColliding;
                FloatRectangle toCheck = e.location;
                this.location.Intersects(ref toCheck, out isColliding);

                /*
                 * Sometimes entity can very slightly clip through tops and sides of objects. (possibly converting from ints to floats and vice versa)
                 *      - Need to handle both the 1 key case and 2 key case.
                 *  Handle corner case
                 * 
                 *  Odd width and length objects will result in bad rounding.
                 * 
                 *  Fast objects probably wont work.
                 *      - Multisampling or sweep collision?
                 *      - do a speed less than the largest object.
                 *      - Definitly sweep for bullets!
                 * 
                 */
                if (isColliding)
                {
                    CollisionReaction(toCheck);
                }
            }
            // ------- COLLISION TEST ------

            //if(!this.Tile.IsWalkable && )

        }

        private void CollisionReaction(FloatRectangle toCheck)
        {
            // http://www.metanetsoftware.com/technique/tutorialA.html#section0
            // pretty much learned this in a class im taking right now but im
            // too dumb to realize the application :*(...
            Vector2 xAxis = new Vector2(1.0f, 0); // these would have to change for rotating objects...
            Vector2 yAxis = new Vector2(0, 1.0f); // these would have to change for rotating objects...

            float xThisCenter = this.location.Center.X;
            float yThisCenter = this.location.Center.Y;

            //float xToTestCenter = e.location.Center.X;
            //float yToTestCenter = e.location.Center.Y;
            float xToTestCenter = toCheck.Center.X;
            float yToTestCenter = toCheck.Center.Y;

            Vector2 centers = new Vector2(xThisCenter - xToTestCenter, yThisCenter - yToTestCenter);

            Vector2 thisHalfWidth = new Vector2(this.location.Width / 2, 0);
            Vector2 thisHalfHeight = new Vector2(0, this.location.Height / 2);

            Vector2 toTestHalfWidth = new Vector2(toCheck.Width / 2, 0);
            Vector2 toTestHalfHeight = new Vector2(0, toCheck.Height / 2);

            // x axis collision check
            Vector2 projXCenters = Utils.Project(centers, xAxis);

            Vector2 projXThisWidth = Utils.Project(thisHalfWidth, xAxis);

            Vector2 projXToTestWidth = Utils.Project(toTestHalfWidth, xAxis);

            // y axis collision check
            Vector2 projYCenters = Utils.Project(centers, yAxis);

            Vector2 projYThisHeight = Utils.Project(thisHalfHeight, yAxis);

            Vector2 projYToTestHeight = Utils.Project(toTestHalfHeight, yAxis);

            // Collision Times
            //http://gamedev.stackexchange.com/questions/17502/how-to-deal-with-corner-collisions-in-2d
            // check for which dir to go in opp direction of...
            //Vector2 collisionVector = (projXThisWidth + projXToTestWidth) - projXCenters;
            float xScalarFinal = (projXThisWidth.Length() + projXToTestWidth.Length()) - projXCenters.Length();
            float yScalarFinal = (projYThisHeight.Length() + projYToTestHeight.Length()) - projYCenters.Length();

            // DEBUG
            //Console.WriteLine("FLOAT X SCALAR: {0:0.00}", (projXThisWidth.Length() + projXToTestWidth.Length()) - projXCenters.Length());
            //Console.WriteLine("FLOAT Y SCALAR: {0:0.00}", (projYThisHeight.Length() + projYToTestHeight.Length()) - projYCenters.Length());

            if (xScalarFinal > yScalarFinal)
            {
                // DEBUG
                //Console.WriteLine("INT Y SCALAR: {0}", yScalarFinal * Math.Sign(DeltaY));
                location.Y -= yScalarFinal * Math.Sign(movement.Y);

            }
            else
            {


                if (xScalarFinal == yScalarFinal) // corner collision
                {

                    float diff = 0;
                    if (this.location.Center.Y < toCheck.Center.Y)
                    {
                        // on top
                        diff = (this.location.Y + this.location.Height) - toCheck.Location.Y;
                        location.Y -= diff;
                        Console.WriteLine("GOING UP");

                    }
                    else
                    {
                        // on bottom
                        diff = (toCheck.Location.Y + toCheck.Height) - this.location.Y;
                        location.Y += diff;
                        Console.WriteLine("GOING DOWN");
                    }
                    Console.WriteLine("CORNER {0}", new Random().NextDouble());
                }
                else
                {
                    //Console.WriteLine("INT X SCALAR: {0}", xScalarFinal * Math.Sign(DeltaY));
                    location.X -= xScalarFinal * Math.Sign(movement.X);
                }
            }

        }

        /// <summary>
        /// Moves the Entity
        /// </summary>
        public virtual void Move()
        {
            throw new NotImplementedException();
        }
    }
}
