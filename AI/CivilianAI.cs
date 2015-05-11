using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TheChicagoProject.Entity;
using TheChicagoProject.GUI;

namespace TheChicagoProject.AI
{
    class CivilianAI : AI
    {
        private int mapToUse; //Which map this AI is currently going for.

        public CivilianAI(LivingEntity entity)
            : base(entity) {
            mapToUse = Game1.Instance.random.Next(0, 2);
        }

        public override void Update(GameTime time, EntityManager manager) {
            DijkstraMap map = manager.world.civilianMaps[mapToUse];
            int x = this.getEntityX();
            int y = this.getEntityY();
            if (map.Map[x][y] <= 2)
                mapToUse = (mapToUse == 1 ? 0 : 1);
            entity.direction = this.findPos(map, 1);
            entity.Move();
        }
    }
}
