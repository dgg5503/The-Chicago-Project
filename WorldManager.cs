using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Entity;

// DEBUG
using TheChicagoProject.GUI;

namespace TheChicagoProject
{

    /// <summary>
    /// Manages the worlds (maps) of the game.
    /// </summary>
    public class WorldManager
    {
        public static Player player;
        public Dictionary<String, World> worlds;
        private String current;
        protected Game1 mainGame;

        public World CurrentWorld {
            get { return worlds[current]; }
        }

        public WorldManager(Game1 game) {
            worlds = new Dictionary<String, World>();
            //TODO: Load/Save worlds.
            current = "main";
            this.mainGame = game;

            // DEBUG
            World tmpWorld = new World(game, 10);
            Random RNG = new Random();
            for(int x = 0; x < tmpWorld.size; x++)
                for (int y = 0; y < tmpWorld.size; y++)
                {
                    // get random tile from dict.
                    tmpWorld.tiles[x][y] = Tiles.tilesDictionary.Values.ToArray()[RNG.Next(0, Tiles.tilesDictionary.Count)];
                }

            worlds["main"] = tmpWorld;
        }
    }
}
