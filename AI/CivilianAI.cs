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
        private int mapToUse; //Which map this AI is currently going for.a

        public CivilianAI(LivingEntity entity)
            : base(entity) {
            mapToUse = Game1.Instance.random.Next(0, 2);
            entity.color = Color.Green;
            //Console.WriteLine(entity.GetType().FullName + " #" + entity.GetHashCode() + " is using map: " + mapToUse + "!");
        }

        public override void Update(GameTime time, EntityManager manager) {
            DijkstraMap map = manager.world.civilianMaps[mapToUse];
            int x = (int) entity.location.Center.X / Tile.SIDE_LENGTH;
            int y = (int) entity.location.Center.Y / Tile.SIDE_LENGTH;
            if (x < 0 || x >= map.Map.Length || y < 0 || y >= map.Map[0].Length)
                return;
            if (!manager.world.tiles[x][y].IsWalkable)
                entity.markforDelete = true;
            if (map.Map[x][y] <= 2) {
                //mapToUse = (mapToUse == 1 ? 0 : 1);
                //Console.WriteLine(entity.GetType().FullName + " #" + entity.GetHashCode() + " is dying now!");
                entity.markforDelete = true;
            }
            entity.direction = this.findPos(map, 1);
            entity.Move();
        }
    }
}
