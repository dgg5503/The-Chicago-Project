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
        public readonly int worldWidth; //Size in tiles, not pixels.
        public readonly int worldHeight;
        public DijkstraMap playerMap;

        public World(Game1 game, int height, int width) {
            tiles = new Tile[height][];
            for (int x = 0; x < width; x++)
                tiles[x] = new Tile[width];
            manager = new EntityManager(game, this);
            this.worldWidth = width;
            this.worldHeight = height;
        }

        //Updates the world every frame.
        public void tick(GameTime time) {
            Player player = manager.GetPlayer();
            int pX = player.location.IntX / Tile.SIDE_LENGTH; //The actual player location.
            int pY = player.location.IntY / Tile.SIDE_LENGTH; //The actual player location.
            int width = manager.mainGame.GraphicsDevice.Viewport.Width / Tile.SIDE_LENGTH + 40;
            int height = manager.mainGame.GraphicsDevice.Viewport.Height / Tile.SIDE_LENGTH + 40;
            if (playerMap == null)
                playerMap = new DijkstraMap(this, width, height, pX - 20, pY - 20, new int[] { 20, 20 });
            int[] pLoc = playerMap.Goals[0]; //The player location for AI's.
            if (pX != pLoc[0] || pY != pLoc[1])
                playerMap = new DijkstraMap(this, width, height, pX - 20, pY - 20, new int[] { 20, 20 });
            manager.Update(time);
        }
    }
}
