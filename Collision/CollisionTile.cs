using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Entity;
using TheChicagoProject.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheChicagoProject.Collision
{
    //Douglas Gliner
    public class CollisionTile
    {
        // Grid reference
        private CollisionTile[,] grid;

        // Every tile holds entities.
        private List<Entity.Entity> entities;

        // Tiles sizes.
        public static int SIDE_LENGTH = Tile.SIDE_LENGTH;

        private FloatRectangle loc;

        private bool isWalkable;

        public List<Entity.Entity> EntitiesInTile { get { return entities; } }
        public bool IsWalkable { get { return isWalkable; } set { isWalkable = value; } }

        public FloatRectangle Rectangle { get { return loc; } }

        public float X { get { return loc.X; } }
        public float Y { get { return loc.Y; } }

        public int GridX { get { return (int)(loc.X / SIDE_LENGTH); } } // should always be ints if not wtf did i do.
        public int GridY { get { return (int)(loc.Y / SIDE_LENGTH); } }

        // size in 
        public CollisionTile(int x, int y, CollisionTile[,] grid)
        {
            entities = new List<Entity.Entity>();
            loc = new FloatRectangle(x, y, SIDE_LENGTH, SIDE_LENGTH);
            isWalkable = true;
            this.grid = grid;  
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities">Global list of entities.</param>
        public void DetectEntitiesInThisTile(List<Entity.Entity> globalEntities)
        {            
            for (int i = 0; i < globalEntities.Count; i++)
            {
                if (loc.Contains(new Vector2(globalEntities[i].location.X, globalEntities[i].location.Y)))
                {
                    // Allow entites to know what tile they are at!
                    globalEntities[i].CurrentCollisionTile = this;

                    entities.Add(globalEntities[i]);

                    // Add this entity to 8 surrounding boxes (relative to top left corner)
                    AddAdjacent(globalEntities[i]);
                }
                else
                    continue;
            }
        }

        /// <summary>
        /// Adds the entity e to the 8 adjacent boxes relative to
        /// the entity e's top left corner bounding box.
        /// </summary>
        /// <param name="e">The entity to add to the 8 adjacent bounding boxes.</param>
        public void AddAdjacent(Entity.Entity e)
        {
            // the 8 cases, also checking to make sure that the 8 cases exist!
            int up = this.GridY - 1;
            int down = this.GridY + 1;
            int left = this.GridX - 1;
            int right = this.GridX + 1;

            if (up >= 0)
                grid[GridX,up].AddEntity(e);

            if (down < grid.GetLength(1)) 
                grid[GridX,down].AddEntity(e);

            if (up >= 0 && left >= 0)
                grid[left,up].AddEntity(e);

            if (down < grid.GetLength(1) && left >= 0) 
                grid[left,down].AddEntity(e);

            if (up >= 0 && right < grid.GetLength(0))
                grid[right,up].AddEntity(e);

            if (down < grid.GetLength(1) && right < grid.GetLength(0))
                grid[right,down].AddEntity(e);

            if (left >= 0)
                grid[left,GridY].AddEntity(e);

            if (right < grid.GetLength(0)) 
                grid[right,GridY].AddEntity(e);
        }

        /// <summary>
        /// Gets adjacent non walkable tiles.
        /// </summary>
        /// <param name="x">Grid X location (not pixels)</param>
        /// <param name="y">Grid Y location (not pixels)</param>
        /// <returns>An array of adjacent non walkable tiles.</returns>
        public CollisionTile[] GetAdjacentNonWalkableTiles()
        {
            // list of tiles
            List<CollisionTile> adjNonWalkTiles = new List<CollisionTile>();

            // the 8 cases, also checking to make sure that the 8 cases exist!
            int up = this.GridY - 1;
            int down = this.GridY + 1;
            int left = this.GridX - 1;
            int right = this.GridX + 1;

            if (up >= 0)
                if (!grid[GridX, up].IsWalkable)
                    adjNonWalkTiles.Add(grid[GridX, up]);

            if (down < grid.GetLength(1))
                if (!grid[GridX, down].IsWalkable)
                    adjNonWalkTiles.Add(grid[GridX, down]);

            if (up >= 0 && left >= 0)
                if (!grid[left, up].IsWalkable)
                    adjNonWalkTiles.Add(grid[left, up]);

            if (down < grid.GetLength(1) && left >= 0)
                if (!grid[left, down].IsWalkable)
                    adjNonWalkTiles.Add(grid[left, down]);

            if (up >= 0 && right < grid.GetLength(0))
                if (!grid[right, up].IsWalkable)
                    adjNonWalkTiles.Add(grid[right, up]);

            if (down < grid.GetLength(1) && right < grid.GetLength(0))
                if (!grid[right, down].IsWalkable)
                    adjNonWalkTiles.Add(grid[right, down]);

            if (left >= 0)
                if (!grid[left, GridY].IsWalkable)
                    adjNonWalkTiles.Add(grid[left, GridY]);

            if (right < grid.GetLength(0))
                if (!grid[right, GridY].IsWalkable)
                    adjNonWalkTiles.Add(grid[right, GridY]);

            if (!this.IsWalkable)
                adjNonWalkTiles.Add(this);

            return adjNonWalkTiles.ToArray();
        }

        /// <summary>
        /// Gets all tiles that interesect with a rotated rectangle
        /// </summary>
        /// <param name="rectangle">The rectangle to check for inteserctions with.</param>
        /// <returns>An array of adjacent interesetcing tiles.</returns>
        public CollisionTile[] GetAdjacentTilesFromIntersection(RotatedRectangle rectangle)
        {
            // list of tiles
            List<CollisionTile> adjTiles = new List<CollisionTile>();

            // the 8 cases, also checking to make sure that the 8 cases exist!
            int up = this.GridY - 1;
            int down = this.GridY + 1;
            int left = this.GridX - 1;
            int right = this.GridX + 1;

            if (up >= 0)
                if (rectangle.Intersects(grid[GridX, up].loc))
                    adjTiles.Add(grid[GridX, up]);

            if (down < grid.GetLength(1))
                if (rectangle.Intersects(grid[GridX, down].loc))
                    adjTiles.Add(grid[GridX, down]);

            if (up >= 0 && left >= 0)
                if (rectangle.Intersects(grid[left, up].loc))
                    adjTiles.Add(grid[left, up]);

            if (down < grid.GetLength(1) && left >= 0)
                if (rectangle.Intersects(grid[left, down].loc))
                    adjTiles.Add(grid[left, down]);

            if (up >= 0 && right < grid.GetLength(0))
                if (rectangle.Intersects(grid[right, up].loc))
                    adjTiles.Add(grid[right, up]);

            if (down < grid.GetLength(1) && right < grid.GetLength(0))
                if (rectangle.Intersects(grid[right, down].loc))
                    adjTiles.Add(grid[right, down]);

            if (left >= 0)
                if (rectangle.Intersects(grid[left, GridY].loc))
                    adjTiles.Add(grid[left, GridY]);

            if (right < grid.GetLength(0))
                if (rectangle.Intersects(grid[right, GridY].loc))
                    adjTiles.Add(grid[right, GridY]);

            if (rectangle.Intersects(this.loc))
                adjTiles.Add(this);

            return adjTiles.ToArray();
        }

        public void AddEntity(Entity.Entity e)
        {
            entities.Add(e);
        }

        // CLEAR
        public void Clear()
        {
            entities.Clear();
        }

        #region debug
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.border, loc, Color.Black);
        }
        #endregion

        public override string ToString()
        {
            return String.Format("GridX: {0} | GridY: {1}", GridX, GridY);
        }
    }
}
