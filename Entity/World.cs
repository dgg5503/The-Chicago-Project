using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using TheChicagoProject.GUI;
using TheChicagoProject.Math;

namespace TheChicagoProject.Entity
{
    class World
    {
        public EntityManager manager;
        public Tile[][] tiles;
        public readonly int size;
        protected DijkstraMap playerMap;

        public World(int size) {
            tiles = new Tile[size][];
            for (int x = 0; x < size; x++)
                tiles[x] = new Tile[size];
            manager = new EntityManager();
            this.size = size;
        }

        //Updates the world every frame.
        public void tick() {
            Player player = manager.GetPlayer();
            int[] pLoc = playerMap.Goals[0]; //The player location for AI's.
            int pX = (int) player.location.X; //The actual player location.
            int pY = (int) player.location.Y; //The actual player location.
            if (pX != pLoc[0] || pY != pLoc[1])
                playerMap = new DijkstraMap(this, new int[] { pX, pY});
        }
    }
}
