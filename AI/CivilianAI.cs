using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TheChicagoProject.Entity;
using TheChicagoProject.GUI;

namespace TheChicagoProject.AI
{
    //Ashwin Ganapathiraju
    class CivilianAI : AI
    {
        private int mapToUse; //Which map this AI is currently going for.
        private DijkstraMap flee;
        private int fleeCount;

        public CivilianAI(LivingEntity entity)
            : base(entity) {
            mapToUse = Game1.Instance.random.Next(0, 1);
            entity.color = Color.Green;
        }

        public override void Update(GameTime time, EntityManager manager) {
            DijkstraMap map = manager.world.civilianMaps[mapToUse];
            int x = this.getEntityX();//(int) entity.location.Center.X / Tile.SIDE_LENGTH;
            int y = this.getEntityY();//(int) entity.location.Center.Y / Tile.SIDE_LENGTH;
            if (x < 0 || x >= map.Map.Length || y < 0 || y >= map.Map[0].Length)
                return;
            if (map.Map[x][y] < 2 || !entity.currentWorld.tiles[x][y].IsWalkable)
                entity.markForDelete = true;

            if (entity.collidingEntites.Count > 0 && flee == null) {
                Entity.Entity e = entity.collidingEntites[0];
                int modX = (int) e.location.X / Tile.SIDE_LENGTH - x + 3;
                int modY = (int) e.location.Y / Tile.SIDE_LENGTH - y + 3;
                flee = new DijkstraMap(entity.currentWorld, 6, 6, x - 3, y - 3, 1, new int[] { modX, modY });
                flee = flee.GenerateFleeMap(entity.currentWorld);
                fleeCount = 30;
            }
            if (flee != null) {
                entity.direction = this.findPos(flee, 1);
                fleeCount--;
                if (fleeCount <= 0)
                    flee = null;
            } else
                entity.direction = this.findPos(map, 1);
            entity.Move();
        }
    }
}
