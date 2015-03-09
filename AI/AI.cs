using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TheChicagoProject.Entity;
using TheChicagoProject.Math;

namespace TheChicagoProject.AI
{
    /// <summary>
    /// The base AI class.
    /// Extensions of this are needed.
    /// Why? Because AI's have different "wants".
    /// Every Living Entity has an AI. (lies, what about player?)
    /// </summary>
    public abstract class AI
    {
        protected LivingEntity entity;

        public AI(LivingEntity entity) {
            this.entity = entity;
        }
        public abstract void Update(GameTime time, EntityManager manager);

        /// <summary>
        /// Finds a position around the entity.
        /// </summary>
        /// <param name="map">The map to search.</param>
        /// <param name="hl">-1 (further) to 1 (closer)</param>
        /// <returns></returns>
        protected Direction findPos(DijkstraMap map, int hl) {
            int ex = entity.location.X;
            int ey = entity.location.Y;
            if (map.Map[ex - 1][ey] == map.Map[ex][ey] - hl) //left
                return Direction.Left;
            if (map.Map[ex + 1][ey] == map.Map[ex][ey] - hl) //right
                return Direction.Right;
            if (map.Map[ex][ey - 1] == map.Map[ex][ey] - hl) //up
                return Direction.Up;
            if (map.Map[ex][ey + 1] == map.Map[ex][ey] - hl) //down
                return Direction.Down;
            if (map.Map[ex - 1][ey - 1] == map.Map[ex][ey] - hl) //upleft
                return Direction.UpLeft;
            if (map.Map[ex - 1][ey + 1] == map.Map[ex][ey] - hl) //downleft
                return Direction.DownLeft;
            if (map.Map[ex + 1][ey - 1] == map.Map[ex][ey] - hl) //upright
                return Direction.UpRight;
            if (map.Map[ex + 1][ey + 1] == map.Map[ex][ey] - hl) //downright
                return Direction.DownRight;
            return entity.direction;
        }
    }
}
