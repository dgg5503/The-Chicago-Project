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
            DijkstraMap playerMap = manager.world.playerMap;
            DijkstraMap fleeMap = manager.world.fleeMap;
            // Floats --> Ints (D.G. 4/5/15, 920pm)
            int pX = this.getEntityX() - playerMap.modX;
            int pY = this.getEntityY() - playerMap.modY;
            if (pX < 0 || pY < 0 || pX >= playerMap.Map.Length || pY >= playerMap.Map[0].Length)
                return;
            int dist = playerMap.Map[pX][pY];
            Direction furtherDir = findPos(fleeMap, -1);
            Direction closerDir = findPos(playerMap, 1);
            if (dist < 3) {
                entity.direction = furtherDir;
                entity.Move();
                entity.Attack(0, entity.inventory.GetEquippedPrimary());
            } else {
                entity.direction = closerDir;
                entity.Move();
                if (dist < 8)
                    entity.Attack(0, entity.inventory.GetEquippedPrimary());
            }
        }
    }
}
