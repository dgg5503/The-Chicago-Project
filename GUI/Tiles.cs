using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace TheChicagoProject.GUI
{
    // Douglas Gliner

    public static class Tiles
    {
        // Dictionary of tiles and their file name.
        public readonly static Dictionary<string, Tile> tilesDictionary = new Dictionary<string, Tile>
        {
            {"RoadTar", new Tile(true, "RoadTar.png")},

            {"RoadLine", new Tile(true, "RoadLine.png")},

            {"SideWalkBrick", new Tile(true, "SideWalkBrick.png")},

            {"BuildingEdge", new Tile(false, "BuildingEdge.png")},

            {"BuildingRoof", new Tile(true, "BuildingRoof.png")}
        };


        // TILE FILE FORMAT 
        // IMAGE AND THEN TILE INFO SUCH AS WALKABLE

        // POSSIBLE IDEA BELOW?

        // Generate list of tiles from the Content/tiles folder
        /*
        public static void LoadTiles()
        {
            // WORKING DIRECTIORY STRINGS? (?)
            string tilesDir = "./Content/Tiles";

            // See if content/tiles exists.
            if (!Directory.Exists(tilesDir))
                throw new Exception("Error: Tiles folder not found.");

            // Get all png files in this directory.
            string[] tilePNGs = Directory.GetFiles(tilesDir, "*?.png");

            // Take each tilePNG file name and make a new tile in the dictionary
            // the string that identifies the file in the dicitionary is the name
            // of the tile.
            for(int i = 0; i < tilePNGs.Length; i++)
            {
                tiles.Add(tilePNGs, new Tile()) // HOW TO KNOWW IF WALKABLE???
            }

        }
        */
    }
}
