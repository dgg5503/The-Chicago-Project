using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TheChicagoProject.Entity;

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
            int dist = map.Map[entity.location.X][entity.location.Y];
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
