using System;
using System.Collections.Generic;
using System.IO;
using TheChicagoProject.GUI;
using TheChicagoProject.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;


namespace TheChicagoProject
{
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
    /// </summary>
    public class RenderManager
    {
        // Fields

        // The main SpriteBatch taken from Game1 which will
        // be used to execute the draw method, doing it
        // this way will require this class to be
        // constructed in LoadContent (which only happens once)
        private SpriteBatch sb;

        // Main graphicsDevice from Game1, required
        // for loading textures
        private GraphicsDevice g;

        // The game1 class.
        private Game1 mainGame;

        private WorldManager w;

        /// <summary>
        /// Constructs RenderManager using a SpriteBatch object which will be used for drawing.
        /// </summary>
        /// <param name="sb">MonoGames SpriteBatch object.</param>
        /// <param name="g">MonoGames GraphicsDevice object.</param>
        /// <param name="mainGame">Game1 class to interact with other managers.</param>
        public RenderManager(SpriteBatch sb, GraphicsDevice g, Game1 mainGame, WorldManager w)
        {
            this.sb = sb;
            this.g = g;
            this.w = w;
            this.mainGame = mainGame;
            
            // Load all textures once (constructor will only be called once, so will this method)
            LoadTextures();
        }

        // LOAD TEXTURES
        // This will be called when rendermanager is constructed in LoadContent... (since we only want to load textures once!!!)
        public void LoadTextures()
        {
            // Image stream for basic texture loading

            //--------TILES--------
            foreach(KeyValuePair<string, Tile> kvp in Tiles.tilesDictionary)
            {
                using (Stream imageStream = TitleContainer.OpenStream(Tile.Directory + kvp.Value.FileName))
                {
                    //imageStream = 
                    kvp.Value.Texture = Texture2D.FromStream(g, imageStream);
                }
            }
            //--------TILES--------

            //------ENTITIES-------
            // FOREACH THROUGH EACH WORLD!!
            foreach (World z in w.worlds.Values)
            {
                foreach (Entity.Entity e in z.manager.EntityList)
                {
                    using (Stream imageStream = TitleContainer.OpenStream(Sprite.Directory + e.sprite.FileName))
                    {
                        //imageStream = 
                        e.sprite.Texture = Texture2D.FromStream(g, imageStream);
                    }
                }
            }
            //------ENTITIES-------



        }

        public void Update(GameTime gameTime)
        {
            // DO THIS FOR SPRITES AND OTHER MOVING THINGS
        }
        
        /// <summary>
        /// Draws LEGITERALLY EVERYTHING!
        /// </summary>
        public void Draw(GameTime gameTime)
        {
            // DEBUG DRAWING
            //Tiles.tilesDictionary["RoadTar"].Draw(sb, 0, 0);
            //Tiles.tilesDictionary["RoadTar"].Draw(sb, 64 * 100, 0);
            //Tiles.tilesDictionary["RoadLine"].Draw(sb, 256, 0, Color.White);

            // ORDER OF DRAWING:
            // World (own method of drawing)
            DrawWorld();

            // Entities (items, players and what not) (list of entities and their locs (?))
            DrawEntities();

            // GUI (list of GUI elements and their locs (?))
            DrawGUI();

            // High Priority Menus (inventory, pause, etc...) (list of GUI elements and their locs (?))
            // ????
        }

        // Sprite Sheets
        // Loading/parsing/setup for animation.


        // Draws all entities
        public void DrawEntities()
        {
            // Simply draw all entities in the currentWorld.
            foreach (Entity.Entity e in w.CurrentWorld.manager.EntityList)
            {
                e.sprite.Draw(sb, e.location.X, e.location.Y, e.direction);
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
        public void DrawGUI()
        {

        }

        // World drawing
        // @Ashwin
        // What possible parameters would this take in? How are we creating our world?
        // characters / numbers / etc...
        // Perhaps the way in which we parse and construct the world should determine the keys in the dictionary? (?)
        // i.e. maybe not strings but enum/identifying ints/character
        // 
        public void DrawWorld()
        {
            // We are taking in a 2D array of Tile and doing a simple double for loop
            // to draw all the tiles on the screen.
            World w =  mainGame.worldManager.CurrentWorld;
            
            // Off the screen technique
            for (int x = 0; x < w.size; x++)
            {
                for (int y = 0; y < w.size; y++)
                {
                    w.tiles[x][y].Draw(sb, x * Tile.SIDE_LENGTH, y * Tile.SIDE_LENGTH);
                }
            }

        }
    }
}
