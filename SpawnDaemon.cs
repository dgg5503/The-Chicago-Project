﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TheChicagoProject.Entity;
using TheChicagoProject.AI;
using TheChicagoProject.GUI;

namespace TheChicagoProject
{
    public class SpawnDaemon
    {
        private World world;
        private Queue<Object[]> pool;

        public void startDaemon() {
            pool = new Queue<Object[]>();
            while (Game1.state == GameState.Game) {
                world = Game1.Instance.worldManager.CurrentWorld;
                initalizeMaps();
                SpawnCivilians();
                if (pool.Peek() != null) {
                    Object[] obj = pool.Dequeue();
                    LivingEntity e = (LivingEntity) obj[0];
                    int[] loc = null;
                    if (obj[1] == null)
                        loc = getRandomGoals(1)[0];
                    else
                        loc = (int[]) obj[1];
                    if (loc[0] == -1 && loc[1] == -1) {
                        Console.WriteLine("Failed to spawn Entity: " + e);
                        continue;
                    }
                    e.location = new FloatRectangle(loc[0] * Tile.SIDE_LENGTH, loc[1] * Tile.SIDE_LENGTH, 32, 32);
                    world.manager.AddEntity(e);
                }
                Thread.Sleep(1000);
            }
        }
        private void initalizeMaps() {
            if (world.civilianMaps != null)
                return;
            world.civilianMaps = new DijkstraMap[2];
            world.civilianMaps[0] = new DijkstraMap(world, world.worldWidth, world.worldHeight, 0, 0, getRandomGoals());
            world.civilianMaps[1] = new DijkstraMap(world, world.worldWidth, world.worldHeight, 0, 0, getRandomGoals());
        }

        //Let's get a random list of valid goal points for civvies to walk towards!
        private int[][] getRandomGoals(int baseNum = 7) {
            if (baseNum < 0)
                return null;
            Random random = Game1.Instance.random;
            int[][] list = new int[baseNum][];
            for (int k = 0; k < list.Length; k++) {
                int x = random.Next(0, world.worldWidth);
                int y = random.Next(0, world.worldHeight);
                int i = 0;
                while (!isTileValid(x, y) && i < 500) {
                    x = random.Next(0, world.worldWidth);
                    y = random.Next(0, world.worldHeight);
                    i++;
                }
                if (i < 500)
                    list[k] = new int[] { x, y };
                else
                    list[k] = new int[] { -1, -1 };
            }
            return list;
        }

        private bool isTileValid(int x, int y) {
            if (x < 0 || y < 0 || x >= world.tiles.Length || y >= world.tiles[0].Length)
                return false;
            if (!world.tiles[x][y].IsWalkable)
                return false;
            if (x - 1 > 0) {
                //if (y - 1 > 0)
                //    if (!world.tiles[x - 1][y - 1].IsWalkable)
                //        return false;
                if (!world.tiles[x - 1][y].IsWalkable)
                    return false;
                //if (y + 1 < world.tiles[0].Length)
                //    if (!world.tiles[x - 1][y + 1].IsWalkable)
                //        return false;
            }
            if (y - 1 > 0)
                if (!world.tiles[x][y - 1].IsWalkable)
                    return false;
            if (y + 1 < world.tiles[0].Length)
                if (!world.tiles[x][y + 1].IsWalkable)
                    return false;
            if (x + 1 < world.tiles.Length) {
                //if (y - 1 > 0)
                //    if (!world.tiles[x + 1][y - 1].IsWalkable)
                //        return false;
                if (!world.tiles[x + 1][y].IsWalkable)
                    return false;
                //if (y + 1 < world.tiles[0].Length)
                //    if (!world.tiles[x + 1][y + 1].IsWalkable)
                //        return false;
            }
            return true;
        }

        public void SpawnCivilians() {
            if (world.manager.civilianCount >= 27)
                return;
            for (int x = 0; x < 27 - world.manager.civilianCount; x++) {
                NPC civvie = new NPC(new FloatRectangle(), Sprites.spritesDictionary["player"], 4);
                civvie.ai = new CivilianAI(civvie);
                pool.Enqueue(new Object[] { civvie, null });
            }
        }
    }
}
