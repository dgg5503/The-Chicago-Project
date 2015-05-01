using System;
using System.Collections.Generic;
using System.IO;
using TheChicagoProject.GUI;
using TheChicagoProject.GUI.Forms;
using TheChicagoProject.GUI.Particles;
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

        // DEBUG QUEST
        private QuestUI tempQuest;
        

        // Particle list
        private List<Particle> particles;

        // Width and height for everyones use.
        private static int viewportWidth;
        private static int viewportHeight;

        private static int viewportDeltaWidth;
        private static int viewportDeltaHeight;

        // pixel for global use.
        private static Texture2D pixel;

        // Static props so no need to make a render manager...
        public static int ViewportWidth { get { return viewportWidth; } }
        public static int ViewportHeight { get { return viewportHeight; } }

        public static int ViewportDeltaWidth { get { return viewportDeltaWidth; } }
        public static int ViewportDeltaHeight { get { return viewportDeltaHeight; } }

        public static Texture2D Pixel { get { return pixel; } }


        /// <summary>
        /// Constructs RenderManager using a SpriteBatch object which will be used for drawing.
        /// </summary>
        /// <param name="spriteBatch">MonoGames SpriteBatch object.</param>
        /// <param name="graphics">MonoGames GraphicsDevice object.</param>
        /// <param name="mainGame">Game1 class to interact with other managers.</param>
        public RenderManager(SpriteBatch spriteBatch, GraphicsDevice graphics, WorldManager worldManager)
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
            this.worldManager = worldManager;
            this.mainGame = Game1.Instance;

            particles = new List<Particle>();

            viewportHeight = graphics.Viewport.Height;
            viewportWidth = graphics.Viewport.Width;

            viewportDeltaWidth = 0;
            viewportDeltaHeight = 0;

            mainGame.Window.ClientSizeChanged += Window_ClientSizeChanged;

            // WHAT IF PLAYER CHANGES WORLD (?)
            player = mainGame.worldManager.CurrentWorld.manager.GetPlayer(); 
            
            // Load all textures once (constructor will only be called once, so will this method)
            LoadTextures();
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            viewportDeltaWidth = graphics.Viewport.Width - viewportWidth;
            viewportDeltaHeight = graphics.Viewport.Height - viewportHeight;

            if (viewportDeltaHeight == 0 && viewportDeltaWidth == 0)
                return;
            

            viewportHeight = graphics.Viewport.Height;
            viewportWidth = graphics.Viewport.Width;

            Console.WriteLine("{0}/{1}", viewportDeltaWidth, viewportDeltaHeight);

            //Controls.guiElements["inventoryMenu"].ScreenSizeChange();

            
            foreach (Control c in Controls.guiElements.Values)
                c.ScreenSizeChange();

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
            // technically this shouldnt be here but casting is very slow so doing a cast
            // then checking is living entity is set in this element in update would slow
            // the game by a little.

            // its placed above the loading of textures and content so text aligns properly.
            (Controls.guiElements["livingEntityInfoUI"] as LivingEntityInfoUI).LivingEntity = player;

            foreach(KeyValuePair<string, Control> c in Controls.guiElements)
                c.Value.LoadVisuals(mainGame.Content, graphics);

            pixel = new Texture2D(graphics, 1, 1);
            pixel.GenColorTexture(1, 1, Color.White);
            //--------GUI----------
            
            

        }

        public void Update(GameTime gameTime)
        {
            // Switch statement needed for on the fly texture loading
            // OTF texture loading will only be done for game generated textures, they will not be loaded from files!
            // Things that are loaded from texture files are found in spriteDicitionary.
            // (one exception is Font files, this will be fixed later on)
            switch (Game1.state)
            {
                case GameState.Menu:
                    //Controls.guiElements["mainMenu"].Update(gameTime);
                    break;

                case GameState.Pause:
                    //Controls.guiElements["pauseMenu"].Update(gameTime);
                    break;

                    // START FROM HERE

                case GameState.Inventory:
                    InventoryMenu inventoryMenu = Controls.guiElements["inventoryMenu"] as InventoryMenu;
                    if (!inventoryMenu.IsInventoryLoaded)
                    {
                        inventoryMenu.Load(player.inventory);
                        inventoryMenu.LoadVisuals(mainGame.Content, graphics);
                        inventoryMenu.Update(gameTime);
                    }

                    //Controls.guiElements["inventoryMenu"].Update(gameTime);
                    break;

                case GameState.FastTravel:
                    break;

                case GameState.Game:
                    // casting takes a lot of time, a way to check if user changed weapon??
                    // UI (health, current wep, other stuff)
                    Controls.guiElements["livingEntityInfoUI"].Update(gameTime);

                    if (player.inventory.ActiveWeapon != -1)
                        (Controls.guiElements["weaponInfoUI"] as WeaponInfoUI).Item = player.inventory.EntityInventory[player.inventory.ActiveWeapon];
                    else
                        (Controls.guiElements["weaponInfoUI"] as WeaponInfoUI).Item = null;

                    // PARTICLES
                    for (int i = 0; i < particles.Count; i++ )
                    {
                        if (particles[i].CurrentTime <= 0) // possible problems (?)
                            particles.Remove(particles[i]);
                        else
                            particles[i].Update(gameTime);
                    }
                        

                    //Controls.guiElements["weaponInfoUI"].Update(gameTime);
                    //Controls.guiElements["livingEntityInfoUI"].Update(gameTime);
                    break;

                case GameState.QuestLog:
                    if(tempQuest == null)
                    {
                        tempQuest = new QuestUI();
                        tempQuest.Load(player.log.GetByStatus(1)[0]);
                        tempQuest.LoadVisuals(mainGame.Content, graphics);
                        tempQuest.Update(gameTime);
                    }
                    break;

                case GameState.Shop:
                    break;

                case GameState.WeaponWheel:
                    WeaponWheelUI weaponWheelUI = Controls.guiElements["weaponWheel"] as WeaponWheelUI;
                    if (!weaponWheelUI.IsInventoryLoaded)
                    {
                        weaponWheelUI.Load(player.inventory);
                        weaponWheelUI.LoadVisuals(mainGame.Content, graphics);
                        weaponWheelUI.Update(gameTime);
                    }
                    //Controls.guiElements["weaponWheel"].Update(gameTime);
                    //weapons come from holster
                    break;
            }

            // DO THIS FOR SPRITES AND OTHER MOVING THINGS
            // if the GUI is not visible, dont update it.
            
            

            foreach(KeyValuePair<string, Control> c in Controls.guiElements)
                if(c.Value.IsVisible)
                    c.Value.Update(gameTime);

           
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

                #region debug
                // DEBUG DRAWS
                //mainGame.collisionManager.Draw(spriteBatch);
                #endregion

                // Entities
                DrawEntities(gameTime);
                DrawParticles();
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
        public void DrawEntities(GameTime gameTime)
        {
            // Simply draw all entities in the currentWorld.
            

            foreach (Entity.Entity e in worldManager.CurrentWorld.manager.EntityList)
            {
                e.sprite.Draw(spriteBatch, e.location.IntX, e.location.IntY, e.faceDirection);
            }

            // the above used e.Direction
            // ASHWIN please set e.FaceDirection AND e.Direction in your AI to the same thing if you
            // want basic rotation on AI.
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
                    break;

                case GameState.Pause:
                    // Transparent fadeout.
                    Controls.guiElements["pauseMenu"].Draw(spriteBatch, gameTime);
                    break;

                case GameState.Inventory:
                    Controls.guiElements["inventoryMenu"].Draw(spriteBatch, gameTime);
                    break;

                // if we get to it.
                case GameState.FastTravel:
                    break;

                case GameState.Game:
                    Controls.guiElements["weaponInfoUI"].Draw(spriteBatch, gameTime);
                    Controls.guiElements["livingEntityInfoUI"].Draw(spriteBatch, gameTime);
                    break;

                // if we get to it.
                case GameState.QuestLog:
                    if (tempQuest != null)
                        tempQuest.Draw(spriteBatch, gameTime);
                    break;

                // if we get to it.
                case GameState.Shop:
                    break;

                // if we get to it
                case GameState.WeaponWheel:
                    Controls.guiElements["weaponWheel"].Draw(spriteBatch, gameTime);
                    break;
            }
            
        }

        public void DrawParticles()
        {
            foreach (Particle particle in particles)
                particle.Draw(spriteBatch);
        }

        public void EmitParticle(Particle particle)
        {
            particles.Add(particle);
        }

        // World drawing
        public void DrawWorld()
        {
            // We are taking in a 2D array of Tile and doing a simple double for loop
            // to draw all the tiles on the screen.
            World w =  mainGame.worldManager.CurrentWorld;

            // All locations are relative to the XY global axis.
            int excess = 3;

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

