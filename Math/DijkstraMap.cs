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
                for (int x = 0; x < grid.Length; x++) {
                    for (int y = 0; y < grid[x].Length; y++) {
                        //TODO: Check whether the tile is walkable, when Doug is done with his work.
                        #region Grid Checking
                        if (x - 1 >= 0) {
                            if(y - 1 >= 0) //top left
                                if (grid[x - 1][y - 1] > grid[x][y] + 1) {
                                    grid[x - 1][y - 1] = grid[x][y] + 1;
                                    b = false;
                                }
                            if (grid[x - 1][y] > grid[x][y] + 1) { //top middle
                                grid[x - 1][y] = grid[x][y] + 1;
                                b = false;
                            }
                            if(y + 1 < grid.Length)
                                if (grid[x - 1][y + 1] > grid[x][y] + 1) { //top right
                                    grid[x - 1][y + 1] = grid[x][y] + 1;
                                    b = false;
                                }
                        }
                        if (y - 1 >= 0) {
                            if (grid[x][y - 1] > grid[x][y] + 1) { //middle left
                                grid[x][y - 1] = grid[x][y] + 1;
                                b = false;
                            }
                            if(x + 1 < grid.Length)
                                if (grid[x + 1][y - 1] > grid[x][y] + 1) { //middle right
                                    grid[x + 1][y - 1] = grid[x][y] + 1;
                                    b = false;
                                }
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
                            if(y + 1 < grid.Length)
                                if (grid[x + 1][y + 1] > grid[x][y] + 1) { //bottom right
                                    grid[x + 1][y + 1] = grid[x][y] + 1;
                                    b = false;
                                }
                        }
                        #endregion
                    }
                }
            }
            for (int x = 0; x < grid.Length; x++)
                for (int y = 0; y < grid[x].Length; y++)
                    if (grid.Length == 0/*Doug allows me to check walkableness!*/)
                        grid[x][y] = 100;

            return grid;
        }
    }
}
