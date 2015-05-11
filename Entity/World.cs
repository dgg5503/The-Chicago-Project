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
        public DijkstraMap fleeMap;
        public DijkstraMap[] civilianMaps;

        public World(int height, int width) {
            tiles = new Tile[height][];
            for (int x = 0; x < height; x++)
                tiles[x] = new Tile[width];
            manager = new EntityManager(this);
            this.worldWidth = width;
            this.worldHeight = height;
        }

        //Let's get a random list of valid goal points for civvies to walk towards!
        private int[][] getRandomGoals() {
            Random random = Game1.Instance.random;
            int[][] list = new int[random.Next(5) + 4][];
            for (int k = 0; k < list.Length; k++) {
                int x = random.Next(0, worldWidth);
                int y = random.Next(0, worldHeight);
                while (x >= tiles.Length || y >= tiles[0].Length || !tiles[x][y].IsWalkable) {
                    x = random.Next(0, worldWidth);
                    y = random.Next(0, worldHeight);
                }
                list[k] = new int[] { x, y };
            }
            return list;
        }

        //Updates the world every frame.
        public void tick(GameTime time) {
            if (civilianMaps == null) { //Initial Setup of the Civilian Walk maps.
                civilianMaps = new DijkstraMap[2];
                civilianMaps[0] = new DijkstraMap(this, worldWidth, worldHeight, 0, 0, getRandomGoals());
                civilianMaps[1] = new DijkstraMap(this, worldWidth, worldHeight, 0, 0, getRandomGoals());
            }

            Player player = manager.GetPlayer();
            int pX = player.location.IntX / Tile.SIDE_LENGTH; //The actual player location.
            int pY = player.location.IntY / Tile.SIDE_LENGTH; //The actual player location.
            int width = Game1.Instance.GraphicsDevice.Viewport.Width / Tile.SIDE_LENGTH + 40;
            int height = Game1.Instance.GraphicsDevice.Viewport.Height / Tile.SIDE_LENGTH + 40;
            if (playerMap == null) {
                playerMap = new DijkstraMap(this, width, height, pX - 20, pY - 20, new int[] { 20, 20 });
                fleeMap = playerMap.Clone().GenerateFleeMap(this);
            }
            int[] pLoc = playerMap.Goals[0]; //The player location for AI's.
            if (pX != pLoc[0] || pY != pLoc[1]) {
                playerMap = new DijkstraMap(this, width, height, pX - 20, pY - 20, new int[] { 20, 20 });
                fleeMap = playerMap.Clone().GenerateFleeMap(this);
            }

            manager.Update(time);
        }
    }
}
