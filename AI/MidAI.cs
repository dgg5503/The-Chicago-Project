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
    /// Keeps his distance.
    /// Will attempt to shoot you / grenade you.
    /// Stays within 5-7 tiles, and moves away if you get close.
    /// 
    /// Ashwin Ganapathiraju
    /// </summary>
    public class MidAI : AI
    {
        public MidAI(LivingEntity entity)
            : base(entity) {

        }

        public override void Update(GameTime time, EntityManager manager) {
            DijkstraMap map = manager.world.playerMap;
            // Floats --> Ints (D.G. 4/5/15, 920pm)
            int pX = entity.location.IntX / Tile.SIDE_LENGTH - map.modX;
            int pY = entity.location.IntY / Tile.SIDE_LENGTH - map.modY;
            if (pX < 0 || pY < 0 || pX >= map.Map.Length || pY >= map.Map[0].Length)
                return;
            int dist = map.Map[pX][pY];
            Direction furtherDir = findPos(map, -1);
            Direction closerDir = findPos(map, 1);
            if (dist < 3) {
                entity.direction = furtherDir;
                entity.Move();
            } else if (dist < 8) {
                entity.direction = closerDir;
                if (time.ElapsedGameTime.Milliseconds % 2 == 0)
                    entity.Attack(0, entity.inventory.GetEquippedPrimary());
                else
                    entity.Attack(1);
            } else {
                entity.direction = closerDir;
                entity.Move();
            }
        }
    }
}
