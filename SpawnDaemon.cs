using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TheChicagoProject.Entity;
using TheChicagoProject.AI;
using TheChicagoProject.GUI;

namespace TheChicagoProject
{
    //Ashwin Ganapathiraju
    public class SpawnDaemon
    {
        private Dictionary<World, Queue<Object[]>> worldPools;
        private bool setToClear; //Resort the queue based on what world we're in.

        public void startDaemon() {
            worldPools = new Dictionary<World, Queue<Object[]>>();
            while (Game1.state == GameState.Game) {
                World world = Game1.Instance.worldManager.CurrentWorld;
                if (!worldPools.ContainsKey(world))
                    worldPools.Add(world, new Queue<Object[]>());


                Queue<Object[]> pool = worldPools[world];
                initalizeMaps(world);
                SpawnCivilians(world);
                if (pool.Count > 0) {
                    Object[] obj = pool.Dequeue();
                    LivingEntity e = (LivingEntity) obj[0];
                    int[] loc = null;
                    if (obj[1] == null)
                        loc = getRandomGoals(world, 1)[0];
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
        private void initalizeMaps(World world) {
            if (world.civilianMaps != null)
                return;
            Console.WriteLine("Initalizing map for world #" + world.GetHashCode() + " with goals at: ");
            int[][] goals = new int[world.doors.Length][];
            for (int x = 0; x < goals.Length; x++) {
                goals[x] = new int[] { (int) world.doors[x].X, (int) world.doors[x].Y };
                Console.WriteLine("X: " + goals[x][0] + "\tY: " + goals[x][1]);
                if (goals[x][0] > world.worldWidth || goals[x][1] > world.worldHeight)
                    goals[x] = null;
            }
            world.civilianMaps = new DijkstraMap[1];
            world.civilianMaps[0] = new DijkstraMap(world, world.worldWidth, world.worldHeight, 0, 0, 1, goals);
            for (int x = 0; x < 27; x++) {
                NPC civvie = new NPC(new FloatRectangle(), Sprites.spritesDictionary["player"], 4);
                civvie.ai = new CivilianAI(civvie);
                if (!worldPools.ContainsKey(world))
                    worldPools.Add(world, new Queue<Object[]>());
                worldPools[world].Enqueue(new Object[] { civvie, null});
            }
        }

        //Let's get a random list of valid goal points for civvies to walk towards!
        private int[][] getRandomGoals(World world, int baseNum = 7, bool keepTrying = false) {
            if (baseNum < 0)
                return null;
            Random random = Game1.Instance.random;
            int[][] list = new int[baseNum][];
            for (int k = 0; k < list.Length; k++) {
                int x = random.Next(0, world.worldWidth);
                int y = random.Next(0, world.worldHeight);
                int i = 0;
                while (!isTileValid(world, x, y) && (keepTrying || i < 500)) {
                    x = random.Next(0, world.worldWidth);
                    y = random.Next(0, world.worldHeight);
                    i++;
                }
                if (keepTrying || i < 500)
                    list[k] = new int[] { x, y };
                else
                    list[k] = new int[] { -1, -1 };
            }
            return list;
        }

        private bool isTileValid(World world, int x, int y) {
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

        private void SpawnCivilians(World world) {
            if (!Game1.Instance.worldManager.CurrentWorld.canRespawn)
                return;
            if (world.manager.civilianCount >= 27)
                return;
            Console.WriteLine("Respawning Civilians: " + (27 - world.manager.civilianCount) + " for world: " + world.GetHashCode());
            for (int x = 0; x < 27 - world.manager.civilianCount; x++) {
                NPC civvie = new NPC(new FloatRectangle(), Sprites.spritesDictionary["player"], 4);
                civvie.ai = new CivilianAI(civvie);
                if (!worldPools.ContainsKey(world))
                    worldPools.Add(world, new Queue<Object[]>());
                worldPools[world].Enqueue(new Object[] { civvie, null});
            }
        }

        public void ClearSpawning() {
            setToClear = true;
        }
    }
}
