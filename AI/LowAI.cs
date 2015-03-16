using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TheChicagoProject.Entity;

namespace TheChicagoProject.AI
{
    /// <summary>
    /// Gets up close and personal.
    /// Will knife (?) / shoot you from ~1-3 tiles.
    /// Does not care if you get close, but will chase you.
    /// </summary>
    public class LowAI : AI
    {
        public LowAI(LivingEntity entity)
            : base(entity) {

        }

        public override void Update(GameTime time, EntityManager manager) {
            DijkstraMap map = manager.world.playerMap;
            int dist = map.Map[entity.location.X][entity.location.Y];
            Direction furtherDir = findPos(map, -1);
            Direction closerDir = findPos(map, 1);
            if (dist < 4) {
                entity.direction = closerDir;
                entity.Attack(0, entity.inventory.GetEquippedPrimary());
            } else {
                entity.direction = closerDir;
                entity.Move();
            }
        }
    }
}
