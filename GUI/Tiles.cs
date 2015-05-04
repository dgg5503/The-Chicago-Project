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
        public readonly static Dictionary<string, Tile> tilesDictionary = new Dictionary<string, Tile>();

        public readonly static Tile[] tilesList = new Tile[256];
        private static int open = 0;

        static Tiles() {
            AddTile("RoadTar", new Tile(true, "RoadTar.png")); // 0
            AddTile("RoadLine", new Tile(true, "RoadLine.png")); // 1
            AddTile("SideWalkBrick", new Tile(true, "SideWalkBrick.png")); // 2
            AddTile("BuildingEdge", new Tile(false, "BuildingEdge.png")); // 3
            AddTile("BuildingRoof", new Tile(false, "BuildingRoof.png")); // 4
            AddTile("Water", new Tile(false, "Water.png")); //5
            AddTile("Door", new Tile(true, "door.png"));    //6
        }

        private static void AddTile(String mapName, Tile tile) {
            Console.WriteLine("[TileLoader] Mapping " + mapName + " to " + open);
            tilesDictionary.Add(mapName, tile);
            tilesList[open++] = tile;
        }

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
