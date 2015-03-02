using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
    class RenderManager
    {
        // Fields


        // The main SpriteBatch taken from Game1 which will
        // be used to execute the draw method, doing it
        // this way will require this class to be
        // constructed in LoadContent (which only happens once)
        private SpriteBatch sb;

        /// <summary>
        /// Constructs RenderManager using a SpriteBatch object which will be used for drawing.
        /// </summary>
        /// <param name="sb">MonoGames SpriteBatch object.</param>
        public RenderManager(SpriteBatch sb)
        {
            this.sb = sb;

        }

        // LOAD TEXTURES
        // This will be called when rendermanager is constructed in LoadContent... (since we only want to load textures once!!!)s
        public void LoadTextures()
        {

        }

        /// <summary>
        /// Draws shit.
        /// </summary>
        public void Draw() {

        }

        // Sprite Drawing
        // This will take loaded content from somewhere 
        

        // GUI Drawing


        // World drawing
        // takes 2x2 array with tile codes and draws their respective tile from 0,0
        public void DrawWorld()
        {
            // 2x2 
        }



    }
}
