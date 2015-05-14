//Josiah S DeVizia
//Douglas Gliner (collision reaction in Update)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.GUI;
using TheChicagoProject.GUI.Forms;
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

        public Vector2 lastLocation;

        public Direction direction;

        public float faceDirection;

        public Sprite sprite;

        public Vector2 movement;

        public World currentWorld;

        public string Name;

        // The current collision tile the entity is in...
        public CollisionTile CurrentCollisionTile;

        // Applied collision vector
        public Vector2 collisionReactionVector;

        //Interaction Data for Quests / Action-Interact
        public InteractionData interactData;

        public Color color;

        //Remove this entity if true!
        public bool markForDelete;

        public List<Entity> collidingEntites;

        /// <summary>
        /// The constructor for the base entity.
        /// </summary>
        /// <param name="fileName">The texture filename for the entity.</param>
        /// <param name="location">The height, width, and X and Y location.</param>
        public Entity(FloatRectangle location, Sprite sprite) {
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
            //sprite = new Sprite(location.IntHeight, location.IntWidth, 0, fileName);
            this.sprite = sprite;
            collisionReactionVector = Vector2.Zero;
            movement = Vector2.Zero;
            color = Color.White;
        }

        /// <summary>
        /// The constructor for the base entity.
        /// </summary>
        /// <param name="sprite">Sprite object.</param>
        public Entity(Sprite sprite) {
            this.sprite = sprite;
            movement = Vector2.Zero;
        }

        /// <summary>
        /// Updates the Game Time and the Entity Manager of the Entity
        /// </summary>
        /// <param name="time">The game Time</param>
        /// <param name="manager">The EntityManager that links to the player</param>
        public virtual void Update(GameTime time, EntityManager manager) {
            // ------- COLLISION TEST ------
            collidingEntites.Clear();

            location.X += movement.X;
            location.Y += movement.Y;

            // ------ EDGE OF SCREEN TEST ------
            /*
             * TO-DO:
             * - Include all edges of the world
             *      - What if we need to hit the edge of a world to load the next one???
             */
            if (location.X < 0) {
                location.X = 0;
            }

            if (location.Y < 0) {
                location.Y = 0;
            }


            // world loading flips width and height...
            if (location.X + location.Width > (currentWorld.worldHeight * Tile.SIDE_LENGTH)) {
                location.X = (currentWorld.worldHeight * Tile.SIDE_LENGTH) - location.Width;
            }

            if (location.Y + location.Height > (currentWorld.worldWidth * Tile.SIDE_LENGTH)) {
                location.Y = (currentWorld.worldWidth * Tile.SIDE_LENGTH) - location.Height;
            }


            // ------ EDGE OF SCREEN TEST ------

            if (this.CurrentCollisionTile == null)
                return;

            // ------ TILE COLLISION TEST ------
            CollisionTile[] adjNonWalkableTiles = this.CurrentCollisionTile.GetAdjacentNonWalkableTiles();

            // Time saver...
            if (adjNonWalkableTiles.Length != 0) {
                // if this intersects with a non walkable tile, react
                // Find rectangles entity is intersecting with...
                List<CollisionTile> intersectingTiles = new List<CollisionTile>();
                foreach (CollisionTile colTile in adjNonWalkableTiles) {
                    if (this.location.Intersects(colTile.Rectangle)) {
                        intersectingTiles.Add(colTile);
                    }
                }


                // Find rectangles in rows, if none do the next thing
                List<CollisionTile> verticalRects = new List<CollisionTile>();
                foreach (CollisionTile colTileIntersectingVertSearch in intersectingTiles) {
                    int GridYToLookFor = colTileIntersectingVertSearch.GridY;
                    foreach (CollisionTile colTileIntersectingVertDoubleSearch in intersectingTiles) {
                        if (GridYToLookFor + 1 == colTileIntersectingVertDoubleSearch.GridY || GridYToLookFor - 1 == colTileIntersectingVertDoubleSearch.GridY)
                            verticalRects.Add(colTileIntersectingVertDoubleSearch);
                    }
                }

                if (verticalRects.Count == 0) {
                    foreach (CollisionTile colTileIntersecting in intersectingTiles)
                        CollisionReaction(colTileIntersecting.Rectangle);
                } else {
                    // do X reaction instead of Y...
                    foreach (CollisionTile colTileIntersecting in verticalRects)
                        CollisionReaction(colTileIntersecting.Rectangle, 1);

                    foreach (CollisionTile colTileIntersecting in intersectingTiles) // needed??
                        CollisionReaction(colTileIntersecting.Rectangle);
                }
            }
            // ------ TILE COLLISION TEST ------

            if (this.CurrentCollisionTile.EntitiesInTile.Count == 1)
                return;

            // ------- ENTITY COLLISION TEST ------
            foreach (Entity e in this.CurrentCollisionTile.EntitiesInTile.Where(e => !(e.Equals(this)))) {
                bool isColliding;
                FloatRectangle toCheck = e.location;
                this.location.Intersects(ref toCheck, out isColliding);

                /*
                 * Sometimes entity can very slightly clip through tops and sides of objects. (possibly converting from ints to floats and vice versa) - Fixed with floatrectangle :)!
                 * Handle corner case - still iffy on this one, good enough measures have been added.
                 * Fast object handling - NOT IMPLEMENTED, should consider optimizing current code before even considering this!
                 * 
                 */
                if (isColliding) {
                    collidingEntites.Add(e);
                    CollisionReaction(toCheck);

                }

            }
            // ------- COLLISION TEST ------
        }

        private void CollisionReaction(FloatRectangle toCheck, int cornerCollisionCase = 0) {
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
            float xScalarFinal = (projXThisWidth.Length() + projXToTestWidth.Length()) - projXCenters.Length();
            float yScalarFinal = (projYThisHeight.Length() + projYToTestHeight.Length()) - projYCenters.Length();

            // DEBUG
            //Console.WriteLine("FLOAT X SCALAR: {0:0.00}", (projXThisWidth.Length() + projXToTestWidth.Length()) - projXCenters.Length());
            //Console.WriteLine("FLOAT Y SCALAR: {0:0.00}", (projYThisHeight.Length() + projYToTestHeight.Length()) - projYCenters.Length());

            if (xScalarFinal > yScalarFinal) {
                // DEBUG
                //Console.WriteLine("INT Y SCALAR: {0}", yScalarFinal * Math.Sign(DeltaY));
                collisionReactionVector = new Vector2(0, yScalarFinal * Math.Sign(movement.Y));
                //location.Y -= yScalarFinal * Math.Sign(movement.Y);
            } else {
                if (xScalarFinal == yScalarFinal) // corner collision
                {
                    float diff = 0;
                    switch (cornerCollisionCase) {
                        // y dummy reaction
                        case 0:
                            if (this.location.Center.Y < toCheck.Center.Y) {
                                // on top
                                diff = (this.location.Y + this.location.Height) - toCheck.Location.Y;
                                collisionReactionVector = new Vector2(0, diff);
                                //location.Y -= diff;
                                //Console.WriteLine("GOING UP");

                            } else {
                                // on bottom
                                diff = (toCheck.Location.Y + toCheck.Height) - this.location.Y;
                                collisionReactionVector = new Vector2(0, diff * -1);
                                //location.Y += diff;
                                //Console.WriteLine("GOING DOWN");
                            }
                            break;

                        // x dummy reaction
                        case 1:
                            if (this.location.Center.X < toCheck.Center.X) {
                                // on top
                                diff = (this.location.X + this.location.Width) - toCheck.Location.X;
                                collisionReactionVector = new Vector2(diff, 0);
                                //location.X -= diff;
                                //Console.WriteLine("GOING LEFT");

                            } else {
                                // on bottom
                                diff = (toCheck.Location.X + toCheck.Width) - this.location.X;
                                collisionReactionVector = new Vector2(diff * -1, 0);
                                //location.X += diff;
                                //Console.WriteLine("GOING RIGHT");
                            }
                            break;
                    }

                    //Console.WriteLine("CORNER {0}", new Random().NextDouble());
                } else {
                    //Console.WriteLine("INT X SCALAR: {0}", xScalarFinal * Math.Sign(DeltaY));
                    collisionReactionVector = new Vector2(xScalarFinal * Math.Sign(movement.X), 0);
                    //location.X -= xScalarFinal * Math.Sign(movement.X);
                }
            }

            location.Location -= collisionReactionVector;


        }

        public bool IsCollidingWith(Entity entity)
        {
            if (collidingEntites.Contains(entity))
                return true;
            return false;
        }

        /// <summary>
        /// Moves the Entity
        /// </summary>
        public virtual void Move() {
            throw new NotImplementedException();
        }

        public virtual bool Action(LivingEntity interactor, String interaction) {
            if (markForDelete || interactData == null || interactData.Dialogue[interaction] == null)
                return false;
            Response r = interactData.Dialogue[interaction];
            //Send the message to the Render Manager here.
            switch (r.Action) {
                case ResponseAction.StartQuest:
                    //How do we start quests?
                    break;
                case ResponseAction.GiveItem:
                    interactor.inventory.Add((Item.Item) r.Data);
                    break;
                case ResponseAction.TakeItem:
                    if (interactor.inventory.EntityInventory.Contains((Item.Item) r.Data))
                        interactor.inventory.EntityInventory.Remove(((Item.Item) r.Data));
                    break;
                case ResponseAction.Heal:
                    interactor.health = Math.Min(interactor.health + (int) r.Data, interactor.maxHealth);
                    break;
                default:
                    break;
            }

            return true;
        }

        //Ashwin Ganapathiraju
        public class InteractionData
        {
            private Dictionary<String, Response> dialogues;

            public Dictionary<String, Response> Dialogue {
                get { return dialogues; }
            }

            public InteractionData(Dictionary<String, Response> dialogue) {
                this.dialogues = dialogue;
            }
        }

        public class Response
        {
            private String text;
            private ResponseAction action;
            private Object data;

            public String Text {
                get { return text; }
            }

            public ResponseAction Action {
                get { return action; }
            }

            public Object Data {
                get { return data; }
            }
            /// <summary>
            /// A response to being interacted with.
            /// </summary>
            /// <param name="text">What the entity in question says.</param>
            /// <param name="action">The action they subsequently take, if anything.</param>
            /// <param name="data">Any associated data with said Action.</param>
            public Response(String text, ResponseAction action = ResponseAction.None, Object data = null) {
                this.text = text;
                this.action = action;
                this.data = data;
            }
        }


        public enum ResponseAction
        {
            /// <summary>
            /// Starts a Quest. Data should be the quest name.
            /// </summary>
            StartQuest,
            /// <summary>
            /// Gives the interactor an Item. Data should be the item.
            /// </summary>
            GiveItem,
            /// <summary>
            /// Takes an item from the interactor. Data should be the item.
            /// </summary>
            TakeItem,
            /// <summary>
            /// Heals the interactor by some amount. Data should be the amount.
            /// </summary>
            Heal,
            /// <summary>
            /// No additional actions apart from speech. Data is ignored.
            /// </summary>
            None
        }
    }
}
