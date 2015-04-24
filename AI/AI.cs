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
        private int getEntityX() {
            float lX = entity.lastLocation.X;
            float nX = entity.location.Center.X / Tile.SIDE_LENGTH;
            double d = Math.Abs(lX - nX);
            if (d > 0.5) {
                entity.lastLocation.X = nX;
                return (int) nX;
            }
            return (int)lX;
        }
        /// <summary>
        /// If the entity has mostly moved into the other tile,
        /// return the other tile. Otherwise, assume you're here.
        /// </summary>
        /// <returns>the entity's 'location' Y.</returns>
        private int getEntityY() {
            float lY = entity.lastLocation.Y;
            float nY = entity.location.Center.Y / Tile.SIDE_LENGTH;
            double d = Math.Abs(lY - nY);
            if (d > 0.5) {
                entity.lastLocation.Y = nY;
                return (int) nY;
            }
            return (int) lY;
        }

        /// <summary>
        /// Finds a position around the entity.
        /// </summary>
        /// <param name="map">The map to search.</param>
        /// <param name="hl">-1 (further) to 1 (closer)</param>
        /// <returns></returns>
        protected Direction findPos(DijkstraMap map, int hl) {
            int ex = getEntityX() - map.modX;
            int ey = getEntityY() - map.modY;

            if (ex - 1 > -1)
                if (map.Map[ex - 1][ey] == map.Map[ex][ey] - hl) //left
                    return Direction.Left;
            if (ex + 1 < map.Map.Length)
                if (map.Map[ex + 1][ey] == map.Map[ex][ey] - hl) //right
                    return Direction.Right;
            if (ey - 1 > -1)
                if (map.Map[ex][ey - 1] == map.Map[ex][ey] - hl) //up
                    return Direction.Up;
            if (ey + 1 < map.Map[ex].Length)
                if (map.Map[ex][ey + 1] == map.Map[ex][ey] - hl) //down
                    return Direction.Down;


            if (ex - 1 > -1) {
                if (map.Map[ex - 1][ey - 1] == map.Map[ex][ey] - hl) //upleft
                    return Direction.UpLeft;
                if (ey + 1 < map.Map[ex].Length)
                    if (map.Map[ex - 1][ey + 1] == map.Map[ex][ey] - hl) //downleft
                        return Direction.DownLeft;
            }

            if (ex + 1 < map.Map.Length) {
                if (map.Map[ex + 1][ey - 1] == map.Map[ex][ey] - hl) //upright
                    return Direction.UpRight;
                if (ey + 1 < map.Map[ex].Length)
                    if (map.Map[ex + 1][ey + 1] == map.Map[ex][ey] - hl) //downright
                        return Direction.DownRight;
            }

            return entity.direction;
        }
    }
}
