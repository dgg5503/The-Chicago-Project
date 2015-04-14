using System;
using System.Collections.Generic;
using System.IO;
using TheChicagoProject.GUI;
using TheChicagoProject.GUI.Forms;
using TheChicagoProject.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace TheChicagoProject
{
    // Douglas Gliner

    /*
     * Tile ideas:
     * 
     * (OUT SIDE STUFF)
     * Notes:
     * - Top left, Top right, Bottom left, Bottom right tiles will be designated
     * 
     * 
     * Roads
     *      Line
     *      Storm Drain
     *      Tar w/ trolley track
     *      Tar cracked
     *      Gravel Stone
     *      Tar (blank space w/o a line)
     *      
     * Sidewalk
     *      Concrete
     *      Brick  
     *      
     * Lamp
     *      LampA
     *      LampB
     * 
     * Poles (WIRES)
     *      (JUST ONE POLE)     
     * 
     * Alley
     *  
     * Shrubbery
     *      PlantA
     *      PlantB
     *      PlantC
     * 
     * Tree
     *      Pine
     *      Oak
     *      Mixed Trees
     *      
     * Bridge
     *      Bridge Edge (rail)
     *      Bridge middle
     *      
     * Buildings (VARY BY COLOR)
     *      Edge (defined edge of building)
     *      Roof
     *      Awnings
     *      Antennas
     *      Notes:
     *          - There will be decorative building tops and edges for special buildings
     *          
     * Monument
     *      Base (base of monument)
     *      Decorative (the monument itself)
     *      Obelisk?
     *      
     * 
     * 
     */

    /// <summary>
    /// Handles the drawing. Any and all drawing. Ever.
    /// TO-DO
    /// - on the fly drawing for code generated textures.
    /// </summary>
    public class RenderManager
    {
        // The main SpriteBatch taken from Game1 which will
        // be used to execute the draw method, doing it
        // this way will require this class to be
        // constructed in LoadContent (which only happens once)
        private SpriteBatch spriteBatch;

        // Main graphicsDevice from Game1, required
        // for loading textures
        private GraphicsDevice graphics;

        // The game1 class.
        private Game1 mainGame;

        private WorldManager worldManager;

        private Player player;

        // Width and height for everyones use.
        private static int viewportWidth;
        private static int viewportHeight;

        // Static props so no need to make a render manager...
        public static int ViewportWidth { get { return viewportWidth; } }
        public static int ViewportHeight { get { return viewportHeight; } }


        /// <summary>
        /// Constructs RenderManager using a SpriteBatch object which will be used for drawing.
        /// </summary>
        /// <param name="spriteBatch">MonoGames SpriteBatch object.</param>
        /// <param name="graphics">MonoGames GraphicsDevice object.</param>
        /// <param name="mainGame">Game1 class to interact with other managers.</param>
        public RenderManager(SpriteBatch spriteBatch, GraphicsDevice graphics, Game1 mainGame, WorldManager worldManager)
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
            this.worldManager = worldManager;
            this.mainGame = mainGame;

            viewportHeight = graphics.Viewport.Height;
            viewportWidth = graphics.Viewport.Width;

            // WHAT IF PLAYER CHANGES WORLD (?)
            player = mainGame.worldManager.CurrentWorld.manager.GetPlayer(); 
            
            // Load all textures once (constructor will only be called once, so will this method)
            LoadTextures();
        }

        // LOAD TEXTURES
        // This will be called when rendermanager is constructed in LoadContent... (since we only want to load textures once!!!)
        public void LoadTextures()
        {
            //--------TILES--------
            foreach(KeyValuePair<string, Tile> kvp in Tiles.tilesDictionary)
            {
                using (Stream imageStream = TitleContainer.OpenStream(Tile.Directory + kvp.Value.FileName))
                {
                    kvp.Value.Texture = Texture2D.FromStream(graphics, imageStream);

                }
            }
            //--------TILES--------

            //------SPRITES-------
            foreach (KeyValuePair<string, Sprite> kvp in Sprites.spritesDictionary)
            {
                using (Stream imageStream = TitleContainer.OpenStream(Sprite.Directory + kvp.Value.FileName))
                {
                    kvp.Value.Texture = Texture2D.FromStream(graphics, imageStream);

                }
            }
            //------SPRITES-------

            //--------GUI----------
            foreach(KeyValuePair<string, Control> c in Controls.guiElements)
            {
                c.Value.LoadTextures(graphics);
                c.Value.LoadContent(mainGame.Content);
            }
            //--------GUI----------

        }

        public void Update(GameTime gameTime)
        {
            // DO THIS FOR SPRITES AND OTHER MOVING THINGS
            // if the GUI is not visible, dont update it.
            foreach(KeyValuePair<string, Control> c in Controls.guiElements)
            {
                if(c.Value.IsVisible)
                    c.Value.Update(gameTime);
            }
           
        }
        
        /// <summary>
        /// Draws LEGITERALLY EVERYTHING!
        /// </summary>
        public void Draw(GameTime gameTime)
        {
            // ORDER OF DRAWING:
            // World (own method of drawing)
            if (Game1.state != GameState.Menu)
            {
                // Camera and relative coordinate translation (coordinates are still on the X, Y plane NOT RELATIVE TO THE CAMERA)
                // Camera follows the player
                Vector2 cameraWorldPostion = new Vector2((player.location.X) + (player.location.Width / 2), player.location.Y + (player.location.Height / 2));

                Vector2 screenCenter = new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);

                Vector2 translation = -cameraWorldPostion + screenCenter;
                Matrix cameraMatrix = Matrix.CreateTranslation(translation.X, translation.Y, 0);

                // Custom spriteBatch params
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cameraMatrix);
                DrawWorld();

                // Entities
                DrawEntities();

                #region debug
                // DEBUG DRAWS
                mainGame.collisionManager.Draw(spriteBatch);
                #endregion

                spriteBatch.End();
            }

            // GUI
            spriteBatch.Begin();
            DrawGUI(gameTime);
            spriteBatch.End();

            // All we need are 2 sprite batches for GUI and the world.
            
        }

        // Sprite Sheets
        // Loading/parsing/setup for animation.


        // Draws all entities
        public void DrawEntities()
        {
            // Simply draw all entities in the currentWorld.
            foreach (Entity.Entity e in worldManager.CurrentWorld.manager.EntityList)
            {
                e.sprite.Draw(spriteBatch, e.location.IntX, e.location.IntY, e.direction);
            }
        }


        // GUI
        /*
         * Notes:
         * - Requires mouse?
         * - Dialog type?
         * - Pauses game?
         * 
         * 
         * - Another tool could be an external GUI builder? (no need to hand code stuff) (?)
         */
        public void DrawGUI(GameTime gameTime)
        {
            switch(Game1.state)
            {
                case GameState.Menu:
                    Controls.guiElements["mainMenu"].Draw(spriteBatch, gameTime);
                    Controls.guiElements["testDrag"].Draw(spriteBatch, gameTime);
                    break;

                case GameState.Pause:
                    // Transparent fadeout.
                    Controls.guiElements["pauseMenu"].Draw(spriteBatch, gameTime);
                    
                    break;

                case GameState.Inventory:
                    Controls.guiElements["inventoryMenu"].Draw(spriteBatch, gameTime);
                    break;
                    
                case GameState.FastTravel:
                    break;

                case GameState.Game:
                    // UI (health, current wep, other stuff)
                    break;

                case GameState.QuestLog:
                    break;

                case GameState.Shop:
                    break;

                case GameState.WeaponWheel:

                    //weapons come from holster
                    break;
            }
            
        }

        // World drawing
        public void DrawWorld()
        {
            // We are taking in a 2D array of Tile and doing a simple double for loop
            // to draw all the tiles on the screen.
            World w =  mainGame.worldManager.CurrentWorld;

            // All locations are relative to the XY global axis.
            int excess = 2;

            int xLowBound = ((player.location.IntX / Tile.SIDE_LENGTH)) - ((graphics.Viewport.Width / Tile.SIDE_LENGTH) / 2);
            int yLowBound = ((player.location.IntY / Tile.SIDE_LENGTH)) - ((graphics.Viewport.Height / Tile.SIDE_LENGTH) / 2) - excess;

            if (xLowBound < 0)
                xLowBound = 0;

            if (yLowBound < 0)
                yLowBound = 0;

            int xHighBound = ((player.location.IntX / Tile.SIDE_LENGTH)) + ((graphics.Viewport.Width / Tile.SIDE_LENGTH) / 2) + excess;
            int yHighBound = ((player.location.IntY / Tile.SIDE_LENGTH)) + ((graphics.Viewport.Height / Tile.SIDE_LENGTH) / 2) + excess;

            if (xHighBound > w.tiles.Length)
                xHighBound = w.tiles.Length;

            if (yHighBound > w.tiles[0].Length)
                yHighBound = w.tiles[0].Length;

            for (int x = xLowBound; x < xHighBound; x++)
                for (int y = yLowBound; y < yHighBound; y++)
                {
                    
                    w.tiles[x][y].Draw(spriteBatch, x * Tile.SIDE_LENGTH, y * Tile.SIDE_LENGTH);
                }

        }
    }
}

