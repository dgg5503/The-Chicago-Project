using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Collision;
using TheChicagoProject.Entity;
using TheChicagoProject.GUI;
#region debug
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace TheChicagoProject
{
    //Douglas Gliner
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
        
        public CollisionManager()
        {
            this.mainGame = Game1.Instance;

            grid = new CollisionTile[mainGame.worldManager.CurrentWorld.manager.world.worldHeight, mainGame.worldManager.CurrentWorld.manager.world.worldWidth];

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
            World w = mainGame.worldManager.CurrentWorld;
            Player player = mainGame.worldManager.CurrentWorld.manager.GetPlayer();
            
            // this calculation is being done twice.....
            int excess = 3;

            int xLowBound = ((player.location.IntX / Tile.SIDE_LENGTH)) - ((mainGame.GraphicsDevice.Viewport.Width / Tile.SIDE_LENGTH) / 2);
            int yLowBound = ((player.location.IntY / Tile.SIDE_LENGTH)) - ((mainGame.GraphicsDevice.Viewport.Height / Tile.SIDE_LENGTH) / 2) - excess;

            if (xLowBound < 0)
                xLowBound = 0;

            if (yLowBound < 0)
                yLowBound = 0;

            int xHighBound = ((player.location.IntX / Tile.SIDE_LENGTH)) + ((mainGame.GraphicsDevice.Viewport.Width / Tile.SIDE_LENGTH) / 2) + excess;
            int yHighBound = ((player.location.IntY / Tile.SIDE_LENGTH)) + ((mainGame.GraphicsDevice.Viewport.Height / Tile.SIDE_LENGTH) / 2) + excess;

            if (xHighBound > grid.GetLength(0)) // greater than world or collision world?
                xHighBound = grid.GetLength(0);

            if (yHighBound > grid.GetLength(1)) // greater than world or collision world?
                yHighBound = grid.GetLength(1);

            // clear then check for objs in tile
            // only look for grids on the screen....
            // clear all entities, not just ones on the screen....
            for (int x = 0; x < grid.GetLength(0); x++)
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y].Clear();
                }

            for (int x = xLowBound; x < xHighBound && x < w.tiles.Length; x++)
                for (int y = yLowBound; y < yHighBound && y < w.tiles[0].Length; y++)
                {
                    // Get entities from current world.
                    if (!w.tiles[x][y].IsWalkable)
                        grid[x, y].IsWalkable = false;
                    
                    grid[x, y].DetectEntitiesInThisTile(mainGame.worldManager.CurrentWorld.manager.EntityList);
                }
        }

        #region debug
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y].Draw(spriteBatch);
                }
        }
        #endregion


        public CollisionTile[,] GetCollisionGrid()
        {
            return grid;
        }
    }
}
