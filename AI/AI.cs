using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TheChicagoProject.Entity;
using TheChicagoProject.GUI;

namespace TheChicagoProject.AI
{
    /// <summary>
    /// The base AI class.
    /// Extensions of this are needed.
    /// Why? Because AI's have different "wants".
    /// Every Living Entity has an AI. (lies, what about player?)
    /// 
    /// Ashwin Ganapathiraju
    /// </summary>
    public abstract class AI
    {
        protected LivingEntity entity;

        public AI(LivingEntity entity) {
            this.entity = entity;
        }
        public abstract void Update(GameTime time, EntityManager manager);

        /// <summary>
        /// If the entity has mostly moved into the other tile,
        /// return the other tile. Otherwise, assume you're here.
        /// </summary>
        /// <returns>the entity's 'location' X.</returns>
        protected int getEntityX() {
            float lX = entity.lastLocation.X;
            float nX = movementHack(true, (int) (entity.location.Center.X / Tile.SIDE_LENGTH));
            double d = Math.Abs(lX - nX);
            //System.Diagnostics.Debug.WriteLine("lX: " + lX + "\tnX: " + nX + "\td: " + d);
            if (d > 0.75) {
                entity.lastLocation.X = nX;
                return (int) nX;
            }
            return (int) lX;
        }
        /// <summary>
        /// If the entity has mostly moved into the other tile,
        /// return the other tile. Otherwise, assume you're here.
        /// </summary>
        /// <returns>the entity's 'location' Y.</returns>
        protected int getEntityY() {
            float lY = entity.lastLocation.Y;
            float nY = movementHack(false, (int) (entity.location.Center.Y / Tile.SIDE_LENGTH));
            double d = Math.Abs(lY - nY);
            //System.Diagnostics.Debug.WriteLine("lY: " + lY + "\tnY: " + nY + "\td: " + d);
            if (d > 0.75) {
                entity.lastLocation.Y = nY;
                return (int) nY;
            }
            return (int) lY;
        }

        /// <summary>
        /// Does some silly value modding to try and get movement working nice w/ corners.
        /// </summary>
        /// <param name="xy">Is it X, or Y? True = X, False = Y.</param>
        /// <param name="val">the value to mod.</param>
        /// <returns></returns>
        private float movementHack(bool xy, float val) {
            switch (entity.direction) {
                case Direction.Up:
                    if (!xy)
                        return val + 0.75f;
                    break;
                case Direction.Down:
                    if (!xy)
                        return val - 0.75f;
                    break;
                case Direction.Left:
                    if (xy)
                        return val + 0.75f;
                    break;
                case Direction.Right:
                    if (xy)
                        return val - 0.75f;
                    break;
            }
            return val;
        }

        /// <summary>
        /// Finds a position around the entity.
        /// </summary>
        /// <param name="map">The map to search.</param>
        /// <param name="hl">-1 (further) to 1 (closer)</param>
        /// <returns></returns>
        protected Direction findPos(DijkstraMap map, int hl) {
            int ex = getEntityX()*map.scale - map.modX;
            int ey = getEntityY()*map.scale - map.modY;
            if (ex < 0 || ey < 0 || ex >= map.Map.Length || ey >= map.Map[ex].Length)
                return entity.direction;

            if (ey - 1 > -1)
                if (map.Map[ex][ey - 1] == map.Map[ex][ey] - hl) //up
                    return Direction.Up;
            if (ey + 1 < map.Map[ex].Length)
                if (map.Map[ex][ey + 1] == map.Map[ex][ey] - hl) //down
                    return Direction.Down;
            if (ex - 1 > -1)
                if (map.Map[ex - 1][ey] == map.Map[ex][ey] - hl) //left
                    return Direction.Left;
            if (ex + 1 < map.Map.Length)
                if (map.Map[ex + 1][ey] == map.Map[ex][ey] - hl) //right
                    return Direction.Right;


            if (ex - 1 > -1) {
                if (ey - 1 > -1)
                    if (map.Map[ex - 1][ey - 1] == map.Map[ex][ey] - hl) //upleft
                        return Direction.UpLeft;
                if (ey + 1 < map.Map[ex].Length)
                    if (map.Map[ex - 1][ey + 1] == map.Map[ex][ey] - hl) //downleft
                        return Direction.DownLeft;
            }

            if (ex + 1 < map.Map.Length) {
                if (ey - 1 > -1)
                    if (map.Map[ex + 1][ey - 1] == map.Map[ex][ey] - hl) //upright
                        return Direction.UpRight;
                if (ey + 1 < map.Map[ex].Length)
                    if (map.Map[ex + 1][ey + 1] == map.Map[ex][ey] - hl) //downright
                        return Direction.DownRight;
            }

            return entity.direction;
        }

        public Direction findMinPos(List<DijkstraMap> maps) {
            int[] dirs = new int[8];
            foreach (DijkstraMap map in maps) {
                int ex = getEntityX()*map.scale - map.modX;
                int ey = getEntityY()*map.scale - map.modY;

                if (ey - 1 > -1)
                    dirs[0] += map.Map[ex][ey - 1]; //up
                if (ey + 1 < map.Map[ex].Length)
                    dirs[1] += map.Map[ex][ey + 1]; //down
                if (ex - 1 > -1)
                    dirs[2] += map.Map[ex - 1][ey]; //left
                if (ex + 1 < map.Map.Length)
                    dirs[3] += map.Map[ex + 1][ey]; //right


                if (ex - 1 > -1) {
                    if (ey - 1 > -1)
                        dirs[4] += map.Map[ex - 1][ey - 1]; //upleft
                    if (ey + 1 < map.Map[ex].Length)
                        dirs[5] += map.Map[ex - 1][ey + 1]; //downleft
                }

                if (ex + 1 < map.Map.Length) {
                    if (ey - 1 > -1)
                        dirs[6] += map.Map[ex + 1][ey - 1]; //upright
                    if (ey + 1 < map.Map[ex].Length)
                        dirs[7] += map.Map[ex + 1][ey + 1]; //downright
                }
            }
            Direction[] directs = new Direction[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right, Direction.UpLeft, Direction.DownLeft, Direction.UpRight, Direction.DownRight };
            int min = Int32.MaxValue, minIndex = 0;
            for (int x = 0; x < dirs.Length; x++) {
                if (dirs[x] < min) {
                    min = dirs[x];
                    minIndex = x;
                }
                //Console.Write(dirs[x] + "-" + x + ", ");
            }
            //Console.WriteLine();
            return directs[minIndex];
        }
    }
}
