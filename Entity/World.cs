using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.GUI;
using TheChicagoProject.AI;
using Microsoft.Xna.Framework;

namespace TheChicagoProject.Entity
{
    // Ashwin Ganapathiraju
    public class World
    {
        public EntityManager manager;
        public Tile[][] tiles;
        public readonly int size; //Size in tiles, not pixels.
        public DijkstraMap playerMap;

        public World(Game1 game, int size) {
            tiles = new Tile[size][];
            for (int x = 0; x < size; x++)
                tiles[x] = new Tile[size];
            manager = new EntityManager(game, this);
            this.size = size;
        }

        //Updates the world every frame.
        public void tick(GameTime time) {
            Player player = manager.GetPlayer();
            int pX = player.location.X / Tile.SIDE_LENGTH; //The actual player location.
            int pY = player.location.Y / Tile.SIDE_LENGTH; //The actual player location.
            if (playerMap == null)
                playerMap = new DijkstraMap(this, new int[] { pX, pY });
            int[] pLoc = playerMap.Goals[0]; //The player location for AI's.
            if (pX != pLoc[0] || pY != pLoc[1])
                playerMap = new DijkstraMap(this, new int[] { pX, pY});
            manager.Update(time);
        }
    }
}
