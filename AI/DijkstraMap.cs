using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Entity;
using TheChicagoProject.GUI;

namespace TheChicagoProject.AI
{
    /// <summary>
    /// It's not the mathematical DijkstraMap, but rather an AI construct.
    /// Premise will be explained here at a later date.
    /// http://www.roguebasin.com/index.php?title=The_Incredible_Power_of_Dijkstra_Maps
    /// 
    /// Ashwin Ganapathiraju
    /// 
    /// Code taken from The Catalyst Project (copyright Serakos Coding 2015, All Rights Reserved).
    /// </summary>
    public class DijkstraMap
    {
        private int[][] goals;
        private int[][] map;
        private int mapHeight; //Size of the map
        private int mapWidth; //Size of the map
        public int modX; //Start X for the miniature DMap
        public int modY; //Start Y for the miniature DMap

        /// <summary>
        /// Returns the list of goal-points.
        /// </summary>
        public int[][] Goals {
            get { return goals; }
        }

        /// <summary>
        /// Returns the Dijkstra Map grid for Entities to use.
        /// </summary>
        public int[][] Map {
            get { return map; }
        }

        public DijkstraMap(World world, int width, int height, int startX, int startY, params int[][] goals) {
            this.goals = goals;
            this.mapWidth = width;
            this.mapHeight = height;
            this.modX = startX;
            this.modY = startY;
            this.map = generateMap(world, goals);
            // printMap();
        }

        private DijkstraMap() {

        }

        private int[][] generateMap(World world, params int[][] goals) {
            int[][] grid = new int[mapWidth][];
            //Initalizes the grid with 100
            for (int x = 0; x < grid.Length; x++) {
                grid[x] = new int[mapHeight];
                for (int y = 0; y < grid[x].Length; y++)
                    grid[x][y] = 100;
            }
            //Sets the goal points to 0
            foreach (int[] g in goals) {
                int actualX = g[0];
                int actualY = g[1];
                grid[actualX][actualY] = 0;
                //Console.WriteLine("GOAL: " + actualX + ", " + actualY);
            }

            scan(world, grid);

            for (int x = 0; x < grid.Length; x++)
                for (int y = 0; y < grid[x].Length; y++)
                    if (valid(world, modX + x, modY + y))
                        if (!world.tiles[modX + x][modY + y].IsWalkable)
                            grid[x][y] = 100;

            return grid;
        }

        public int[][] scan(World world, int[][] grid) {
            bool b = false;
            while (!b) {
                b = true;
                for (int x = 0; x < grid.Length; x++) {
                    for (int y = 0; y < grid[x].Length; y++)
                        if (valid(world, modX + x, modY + y)) {
                            if (!world.tiles[modX + x][modY + y].IsWalkable)
                                continue;
                            #region Grid Checking
                            if (x - 1 >= 0) {
                                if (y - 1 >= 0) //top left
                                    if (grid[x - 1][y - 1] > grid[x][y] + 1) {
                                        grid[x - 1][y - 1] = grid[x][y] + 1;
                                        b = false;
                                    }
                                if (grid[x - 1][y] > grid[x][y] + 1) { //top middle
                                    grid[x - 1][y] = grid[x][y] + 1;
                                    b = false;
                                }
                                if (y + 1 < grid[x].Length)
                                    if (grid[x - 1][y + 1] > grid[x][y] + 1) { //top right
                                        grid[x - 1][y + 1] = grid[x][y] + 1;
                                        b = false;
                                    }
                            }
                            if (y - 1 >= 0)
                                if (grid[x][y - 1] > grid[x][y] + 1) { //middle left
                                    grid[x][y - 1] = grid[x][y] + 1;
                                    b = false;
                                }
                            if (y + 1 < grid[x].Length)
                                if (grid[x][y + 1] > grid[x][y] + 1) { //middle right
                                    grid[x][y + 1] = grid[x][y] + 1;
                                    b = false;
                                }

                            if (x + 1 < grid.Length) {
                                if (y - 1 >= 0)
                                    if (grid[x + 1][y - 1] > grid[x][y] + 1) { //bottom left
                                        grid[x + 1][y - 1] = grid[x][y] + 1;
                                        b = false;
                                    }
                                if (grid[x + 1][y] > grid[x][y] + 1) { //bottom middle
                                    grid[x + 1][y] = grid[x][y] + 1;
                                    b = false;
                                }
                                if (y + 1 < grid[x].Length)
                                    if (grid[x + 1][y + 1] > grid[x][y] + 1) { //bottom right
                                        grid[x + 1][y + 1] = grid[x][y] + 1;
                                        b = false;
                                    }
                            }
                            #endregion
                        }
                }
            }
            return grid;
        }

        public bool valid(World world, int x, int y) {
            return x > -1 && y > -1 && x < world.tiles.Length && y < world.tiles.Length;
        }

        public DijkstraMap GenerateFleeMap(World world) {
            for (int x = 0; x < map.Length; x++)
                for (int y = 0; y < map[x].Length; y++)
                    if (valid(world, modX + x, modY + y)) {
                        if (!world.tiles[modX + x][modY + y].IsWalkable)
                            continue;
                        map[x][y] = (int) (map[x][y] * -1.2);
                    }
            map = scan(world, map);
            return this;
        }

        public DijkstraMap Clone() {
            DijkstraMap clone = new DijkstraMap();
            clone.goals = (int[][]) goals.Clone();
            clone.mapWidth = mapWidth;
            clone.mapHeight = mapHeight;
            clone.modX = modX;
            clone.modY = modY;
            clone.map = new int[map.Length][];
            for (int x = 0; x < map.Length; x++) {
                clone.map[x] = new int[map[x].Length];
                for (int y = 0; y < map[x].Length; y++)
                    clone.map[x][y] = map[x][y];
            }
            return clone;
        }

        //Debug

        public void printMap() {
            Console.WriteLine("" + mapWidth + "/" + mapHeight + ": " + modX + ", " + modY + " | " + goals[0][0] + ", " + goals[0][1]);
            for (int x = 0; x < map.Length; x++) {
                for (int y = 0; y < map[x].Length; y++)
                    Console.Write(format(map[x][y]) + " ");
                Console.WriteLine();
            }
        }

        private String format(int num) {
            if (num < 10)
                return "  " + num;
            if (num < 99)
                return " " + num;
            return "" + num;
        }


    }
}
