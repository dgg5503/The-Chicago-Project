using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject
{
    /*
     * 
     *     /// <summary>
            /// The different types of tiles.
            /// 
            /// Each tiletype is also a filename!
            /// </summary>
            enum TileType
            {
                RoadTar,
                RoadLine,
                SideWalkBrick,
                // More to come :D!
            }

     */
    static class Tiles
    {
        // make this an arrayList 
        public static Tile ROADTAR = new Tile(true); // Road
        public static Tile BUILDINGMIDDLE = new Tile(false); // Middle of building (not the edge, just for testing as of now)
        
    }
}
