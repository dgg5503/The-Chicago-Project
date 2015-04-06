using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Collision;

namespace TheChicagoProject
{
    /// <summary>
    /// This class will hold the update method for placing entities in the
    /// current world in collision tiles. 
    /// </summary>
    public class CollisionManager
    {
        // Game1 for getting other managers
        private Game1 mainGame;        

        // Grid for coll detection...
        private CollisionTile[,] grid;
        
        public CollisionManager(Game1 mainGame)
        {
            this.mainGame = mainGame;

            grid = new CollisionTile[mainGame.worldManager.CurrentWorld.manager.world.size, mainGame.worldManager.CurrentWorld.manager.world.size];

            // Setup tile grid.
            for (int x = 0; x < grid.GetLength(0); x++)
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = new CollisionTile(x * CollisionTile.SIDE_LENGTH, y * CollisionTile.SIDE_LENGTH, grid);
                }
        }

        
        /*
         * TO-DO:
         * - Update collision grid when loading a new world of different size... (?)
         */

        public void Update()
        {
            // clear then check for objs in tile
            for (int x = 0; x < grid.GetLength(0); x++)
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y].Clear();
                }

            for (int x = 0; x < grid.GetLength(0); x++)
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    // Get entities from current world.
                    grid[x, y].DetectEntitiesInThisTile(mainGame.worldManager.CurrentWorld.manager.EntityList);
                }
        }

        public CollisionTile[,] GetCollisionGrid()
        {
            return grid;
        }
    }
}
