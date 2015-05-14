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
        private int[][] getRandomGoals(int baseNum = 7) {
            if (baseNum < 0)
                return null;
            Random random = Game1.Instance.random;
            int[][] list = new int[random.Next(5) + baseNum][];
            for (int k = 0; k < list.Length; k++) {
                int x = random.Next(0, worldWidth);
                int y = random.Next(0, worldHeight);
                while (!isTileValid(x, y)) {
                    x = random.Next(0, worldWidth);
                    y = random.Next(0, worldHeight);
                }
                list[k] = new int[] { x, y };
            }
            return list;
        }

        private bool isTileValid(int x, int y) {
            if (x < 0 || y < 0 || x >= tiles.Length || y >= tiles[0].Length)
                return false;
            if (!tiles[x][y].IsWalkable)
                return false;
            if (x - 1 > 0) {
                if (y - 1 > 0)
                    if (!tiles[x - 1][y - 1].IsWalkable)
                        return false;
                if (!tiles[x - 1][y].IsWalkable)
                    return false;
                if (y + 1 < tiles[0].Length)
                    if (tiles[x - 1][y + 1].IsWalkable)
                        return false;
            }
            if (y - 1 > 0)
                if (!tiles[x][y - 1].IsWalkable)
                    return false;
            if (y + 1 < tiles[0].Length)
                if (!tiles[x][y + 1].IsWalkable)
                    return false;
            if (x + 1 < tiles.Length) {
                if (y - 1 > 0)
                    if (!tiles[x + 1][y - 1].IsWalkable)
                        return false;
                if (!tiles[x + 1][y].IsWalkable)
                    return false;
                if (y + 1 < tiles[0].Length)
                    if (tiles[x + 1][y + 1].IsWalkable)
                        return false;
            }
            return true;
        }

        public void SpawnCivilians() {
            int[][] spawns = this.getRandomGoals(27 - manager.civilianCount);
            if (spawns == null)
                return;
            Console.WriteLine("Spawning : " + spawns.Length + " entities!");
            foreach (int[] locs in spawns) {
                NPC civvie = new NPC(new FloatRectangle(locs[0] * Tile.SIDE_LENGTH, locs[1] * Tile.SIDE_LENGTH, 32, 32), Sprites.spritesDictionary["player"], 4);
                manager.AddEntity(civvie);
            }
        }

        //Updates the world every frame.
        public void tick(GameTime time) {
            // Commenting out for now. I need to move this to a new Thread.
            // There is a reason no game tries spawning entities on the main thread.
            // I have run into that reason. ~Ashwin

            //if (civilianMaps == null) { //Initial Setup of the Civilian Walk maps.
            //    civilianMaps = new DijkstraMap[2];
            //    civilianMaps[0] = new DijkstraMap(this, worldWidth, worldHeight, 0, 0, getRandomGoals());
            //    civilianMaps[1] = new DijkstraMap(this, worldWidth, worldHeight, 0, 0, getRandomGoals());
            //}
            //if (time.ElapsedGameTime.TotalSeconds % 10 <= 3)
            //    SpawnCivilians();

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
