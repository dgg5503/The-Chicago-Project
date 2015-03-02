using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject.Math
{
    /// <summary>
    /// It's not the mathematical DijkstraMap, but rather an AI construct.
    /// Premise will be explained here at a later date.
    /// </summary>
    class DijkstraMap
    {
        private static int[][] generateMap(int mapSize, params int[][] goals) {
            int[][] grid = new int[mapSize][];
            //Initalizes the grid with 100
            for (int x = 0; x < grid.Length; x++) {
                grid[x] = new int[mapSize];
                for (int y = 0; y < grid.Length; y++)
                    grid[x][y] = 100;
            }
            //Sets the goal points to 0
            foreach (int[] g in goals) {
                grid[g[0]][g[1]] = 0;
            }

            bool b = false;
            while (!b) {
                b = true;
                
            }

            return grid;
        }
    }
}
