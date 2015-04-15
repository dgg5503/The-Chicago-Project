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
    /// Gets up close and personal.
    /// Will knife (?) / shoot you from ~1-3 tiles.
    /// Does not care if you get close, but will chase you.
    /// 
    /// Ashwin Ganapathiraju
    /// </summary>
    public class LowAI : AI
    {
        public LowAI(LivingEntity entity)
            : base(entity) {

        }

        public override void Update(GameTime time, EntityManager manager) {
            DijkstraMap map = manager.world.playerMap;
            // Float --> Int
            int pX = (int) (entity.location.Center.X / Tile.SIDE_LENGTH) - map.modX;
            int pY = (int) (entity.location.Center.Y / Tile.SIDE_LENGTH) - map.modY;
            if (pX < 0 || pY < 0 || pX >= map.Map.Length || pY >= map.Map[0].Length)
                return;
            int dist = map.Map[pX][pY];
            Direction furtherDir = findPos(map, -1);
            Direction closerDir = findPos(map, 1);
            entity.direction = closerDir;
            if (dist < 4) {
                entity.Attack(0, entity.inventory.GetEquippedPrimary());
            } else {
                entity.Move();
            }
        }
    }
}
