﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using TheChicagoProject.Quests;
using TheChicagoProject.Entity;
using TheChicagoProject.AI;
#endregion
// DEBUG
using TheChicagoProject.GUI;

namespace TheChicagoProject
{
    //Ashwin Ganapathiraju
    //Sean Levorse
    /// <summary>
    /// Manages the worlds (maps) of the game.
    /// </summary>
    public class WorldManager
    {
        public static Player player;
        public Dictionary<String, World> worlds;
        public String current;
        public QuestLog worldQuests;
        public SpawnDaemon spawnDaemon;
        public Thread spawnThread;

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
            
            player.inventory.ActiveWeapon = 0;
            
            if(!worlds.ContainsKey(current))
                worlds.Add(current, Game1.Instance.saveManager.LoadWorld(current));

            worlds["main"].manager.AddEntity(player);
            // DEBUG
            #region debug
#if false

            LivingEntity mugger = new LivingEntity(new FloatRectangle(384, 150, 32, 32), Sprites.spritesDictionary["player"], 10);
            mugger.ai = new MidAI(mugger);
            mugger.inventory.Add(new Item.Weapon(50, 1, 10, "Bam", 100, 0.5));
            mugger.inventory.ActiveWeapon = 0;
            //mugger.interactData = new Entity.Entity.InteractionData(new List<String>() { "I bite my thumb at you, sir!" });
            worlds["main"].manager.AddEntity(mugger);

            //LivingEntity civvie = new LivingEntity(new FloatRectangle(384, 247, 32, 32), Sprites.spritesDictionary["player"], 4);
            //civvie.ai = new CivilianAI(civvie);
            //civvie.interactData = new Entity.Entity.InteractionData(new List<String>() { "pls no." });
            //worlds["main"].manager.AddEntity(civvie);

            worlds["main"].manager.AddEntity(player);

            Quest mugging = SaveManager.ParseQuest("./Content/Quests/Mugging.quest");
            mugging.SetAvailable();
            mugging.worldManager = this;
            player.log.Add(mugging);
            this.worldQuests.Add(mugging);
            mugging.player = player;

            Quest gunman = SaveManager.ParseQuest("./Content/Quests/Crazed Gunman.quest");
            gunman.SetAvailable();
            player.log.Add(gunman);
            this.worldQuests.Add(gunman);

            Quest war = SaveManager.ParseQuest("./Content/Quests/Gang war.quest");
            war.SetAvailable();
            player.log.Add(war);
            this.worldQuests.Add(war);

            Quest sniper = SaveManager.ParseQuest("./Content/Quests/Sniper.quest");
            sniper.SetAvailable();
            player.log.Add(sniper);
            this.worldQuests.Add(sniper);
            /*
            Quest sniper1 = SaveManager.ParseQuest("./Content/Quests/Sniper1.quest");
            sniper1.SetAvailable();
            player.log.Add(sniper1);
            this.worldQuests.Add(sniper1);

            Quest sniper2 = SaveManager.ParseQuest("./Content/Quests/Sniper2.quest");
            sniper2.SetAvailable();
            player.log.Add(sniper2);
            this.worldQuests.Add(sniper2);

            Quest sniper3 = SaveManager.ParseQuest("./Content/Quests/Sniper3.quest");
            sniper3.SetAvailable();
            player.log.Add(sniper3);
            this.worldQuests.Add(sniper3);

            Quest mugging1 = SaveManager.ParseQuest("./Content/Quests/Mugging1.quest");
            mugging1.SetAvailable();
            player.log.Add(mugging1);
            this.worldQuests.Add(mugging1);

            Quest mugging2 = SaveManager.ParseQuest("./Content/Quests/Mugging2.quest");
            mugging2.SetAvailable();
            player.log.Add(mugging2);
            this.worldQuests.Add(mugging2);

            Quest mugging3 = SaveManager.ParseQuest("./Content/Quests/Mugging3.quest");
            mugging3.SetAvailable();
            player.log.Add(mugging3);
            this.worldQuests.Add(mugging3);
            */
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
            spawnDaemon = new SpawnDaemon();
            spawnThread = new Thread(new ThreadStart(spawnDaemon.startDaemon));
        }

        public void Update(GameTime gameTime)
        {
            if (spawnThread.ThreadState == ThreadState.Unstarted)
                spawnThread.Start();
            //update quests
            worldQuests.Update(gameTime);
            player.log.Update(gameTime);

        }

        public void Reset()
        {
            spawnThread.Abort();
            worlds = new Dictionary<String, World>();
            worldQuests = new QuestLog();
            
            //reset the worlds
            foreach(string key in worlds.Keys.ToList())
            {
                worlds[key] = Game1.Instance.saveManager.LoadWorld(key);
            }

            current = "main";

            player = new Player(new FloatRectangle(384, 72, 32, 32), Sprites.spritesDictionary["player"]);

            player.inventory.ActiveWeapon = 0;

            //reset player quest log
            for (int i = 0; i < player.log.Count; i++)
            {
                player.log[i] = SaveManager.ParseQuest(SaveManager.QUEST_DIRECTORY + player.log[i].Name + ".quest");
            }

            if (!worlds.ContainsKey(current))
                worlds.Add(current, Game1.Instance.saveManager.LoadWorld(current));

            worlds["main"].manager.AddEntity(player);
            

            worldQuests = new QuestLog();
            spawnDaemon = new SpawnDaemon();
            spawnThread = new Thread(new ThreadStart(spawnDaemon.startDaemon));
        }
    }
}   
