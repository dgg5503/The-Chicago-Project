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
        public String current;
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
            worldQuests = new QuestLog();
            //TODO: Load/Save worlds.
            current = "main";

            player = new Player(new FloatRectangle(384, 72, 32, 32), Sprites.spritesDictionary["player"]);
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "The Screwdriver", 30, 0D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "Gun", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 3, 10D, "Knife", 1, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "w", 30, 100D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "ww", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "www", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "wwww", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "wwwww", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "wwwwww", 30, 5D));
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "wwwwwww", 30, 5D));
            player.inventory.ActiveWeapon = 0;

            #region debug


            if(!worlds.ContainsKey(current))
                worlds.Add(current, Game1.Instance.saveManager.LoadWorld(current));
            // DEBUG
#if DEBUG
            LivingEntity mugger = new LivingEntity(new FloatRectangle(384, 150, 32, 32), Sprites.spritesDictionary["player"], 10);
            mugger.ai = new LowAI(mugger);
            mugger.inventory.Add(new Item.Weapon(50, 1, 10, "Bam", 1, 0.5));
            mugger.inventory.ActiveWeapon = 0;
            //worlds["main"].manager.AddEntity(mugger);
             
            // need to fix fleemap lag before renabling the above.
            //Uncommenting just to see if I can load quests again - Sean

            worlds["main"].manager.AddEntity(player);

            Quest mugging = SaveManager.ParseQuest("./Content/Quests/Mugging.quest");
            mugging.SetAvailable();
            player.log.Add(mugging);
            this.worldQuests.Add(mugging);
            
            /*
            Quest test = new Quest("Mugging", "Kill the mugger", "You are being attacked", new Vector2(100, 1000), player, this, WinCondition.EnemyDies, 4, 50);
            test.EnemyToKill = mugger;
            test.entitites.Add(mugger);
            test.Status = 1;
            player.log.Add(test);
             */
#endif
            // need to fix fleemap lag before renabling the above.
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
