#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using TheChicagoProject.Quests;
#endregion
// DEBUG
using TheChicagoProject.GUI;

namespace TheChicagoProject
{

    /// <summary>
    /// Manages the worlds (maps) of the game.
    /// 
    /// Ashwin Ganapathiraju
    /// </summary>
    public class WorldManager
    {
        public static Player player;
        public Dictionary<String, World> worlds;
        private String current;
        protected Game1 mainGame;
        public QuestLog worldQuests;

        public World CurrentWorld {
            get { return worlds[current]; }
        }

        public WorldManager(Game1 game) {
            worlds = new Dictionary<String, World>();
            //TODO: Load/Save worlds.
            current = "main";
            this.mainGame = game;

            player = new Player(new Microsoft.Xna.Framework.Rectangle(256, 256, 32, 32), "player.png");
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "The Screwdriver", 30, 5D));
            player.inventory.ActiveWeapon = 0;

            // DEBUG
            World tmpWorld = new World(game, 100);
            Random RNG = new Random();
            for(int x = 0; x < tmpWorld.size; x++)
                for (int y = 0; y < tmpWorld.size; y++)
                {
                    // get random tile from dict.
                    tmpWorld.tiles[x][y] = Tiles.tilesDictionary.Values.ToArray()[RNG.Next(0, Tiles.tilesDictionary.Count)];
                }

            worlds["main"] = tmpWorld;

            //Add player to the world
            worlds["main"].manager.AddEntity(player);
            worldQuests = new QuestLog();
        }

        public void Update(GameTime gameTime)
        {
            //udate quests
            worldQuests.Update(gameTime);
            player.log.Update(gameTime);
        }
    }
}
