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
            int pX = entity.location.IntX / Tile.SIDE_LENGTH;
            int pY = entity.location.IntY / Tile.SIDE_LENGTH;
            int dist = map.Map[pX][pY];
            Direction furtherDir = findPos(map, -1);
            Direction closerDir = findPos(map, 1);
            entity.direction = closerDir;
            Console.WriteLine(closerDir + " " + dist);
            if (dist < 4) {
                entity.Attack(0, entity.inventory.GetEquippedPrimary());
            } else {
                entity.Move();
            }
        }
    }
}
