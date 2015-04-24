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


            if(!worlds.ContainsKey(current))
                worlds.Add(current, Game1.Instance.saveManager.LoadWorld(current));
#if DEBUG
            // DEBUG
            LivingEntity mugger = new LivingEntity(new FloatRectangle(384, 150, 32, 32), Sprites.spritesDictionary["player"], 10);
            mugger.ai = new LowAI(mugger);
            mugger.inventory.Add(new Item.Weapon(50, 1, 10, "Bam", 1, 0.5));
            mugger.inventory.ActiveWeapon = 0;
            worlds["main"].manager.AddEntity(mugger);
            worlds["main"].manager.AddEntity(player);

            //player.log.Add(SaveManager.ParseQuest("\\Content\\Quests\\Mugging.quest")); 
            Quest test = new Quest("Mugging", "Kill the mugger", "You are being attacked", new Vector2(100, 1000), player, this, WinCondition.EnemyDies, 4, 0);
            test.EnemyToKill = mugger;
#endif
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
