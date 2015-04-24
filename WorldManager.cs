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
using TheChicagoProject.AI;
using System.IO;
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

        public String CurrentWorldString
        {
            get { return current; }
        }

        //Josiah S DeVizia, implemented world importation
        public WorldManager() {
            worlds = new Dictionary<String, World>();
            //TODO: Load/Save worlds.
            current = "main";
            this.mainGame = Game1.Instance;

            player = new Player(new FloatRectangle(384, 72, 32, 32), Sprites.spritesDictionary["player"]);
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "The Screwdriver", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "Gun", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "Knife", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "Test1", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "Test2", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "Test3", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "Test4", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "Test5", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "Test6", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "Test7", 30, 5D));
            player.inventory.ActiveWeapon = 0;

            #region debug

            Stream worldStream = File.OpenRead(".\\Content\\World.txt");
            StreamReader worldReader = new StreamReader(worldStream);

            World tmpWorld = new World(int.Parse(worldReader.ReadLine()), int.Parse(worldReader.ReadLine()));

            string line = worldReader.ReadLine();
            int row = 0;
            while (line != null)
            {
                for (int col = 0; col < line.Length; ++col)
                {
                    switch (line[col])
                    {
                        case '0':
                            tmpWorld.tiles[row][col] = Tiles.tilesDictionary["RoadTar"];
                            break;

                        case '1':
                            tmpWorld.tiles[row][col] = Tiles.tilesDictionary["RoadLine"];
                            break;

                        case '2':
                            tmpWorld.tiles[row][col] = Tiles.tilesDictionary["SideWalkBrick"];
                            break;

                        case '3':
                            tmpWorld.tiles[row][col] = Tiles.tilesDictionary["BuildingEdge"];
                            break;

                        case '4':
                            tmpWorld.tiles[row][col] = Tiles.tilesDictionary["BuildingRoof"];
                            break;

                        case '5':
                            tmpWorld.tiles[row][col] = Tiles.tilesDictionary["Water"];
                            break;
                    }
                }
                row++;
                line = worldReader.ReadLine();
            }

            worlds.Add("main", tmpWorld);

            // DEBUG
            LivingEntity mugger = new LivingEntity(new FloatRectangle(384, 128, 32, 32), Sprites.spritesDictionary["player"], 10);
            mugger.ai = new LowAI(mugger);
            mugger.inventory.Add(new Item.Weapon(10, 1, 1, "Bam", 100, 0.5));
            mugger.inventory.ActiveWeapon = 0;
            worlds["main"].manager.AddEntity(mugger);
            worlds["main"].manager.AddEntity(player);
            #endregion
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
