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
            player.inventory.Add(new Item.Weapon(600, 1, 3D, "The Screwdriver", 30, 0D) { previewSprite = Sprites.spritesDictionary["gatling_gun_preview"] });
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "Gun", 30, 5D) { previewSprite = Sprites.spritesDictionary["basic_gun_preview"] });
            player.inventory.Add(new Item.Weapon(400, 3, 10D, "Knife", 1, 5D) { previewSprite = Sprites.spritesDictionary["knife_preview"] });
            player.inventory.Add(new Item.Weapon(400, 1, 3D, "Uzi", 30, 100D) { previewSprite = Sprites.spritesDictionary["uzi_gun_preview"] });
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
            mugger.ai = new MidAI(mugger);
            mugger.inventory.Add(new Item.Weapon(50, 1, 10, "Bam", 100, 0.5));
            mugger.inventory.ActiveWeapon = 0;
            mugger.interactData = new Entity.Entity.InteractionData(new List<String>() { "I bite my thumb at you, sir!" });
            worlds["main"].manager.AddEntity(mugger);

            LivingEntity civvie = new LivingEntity(new FloatRectangle(384, 247, 32, 32), Sprites.spritesDictionary["player"], 4);
            civvie.ai = new CivilianAI(civvie);
            worlds["main"].manager.AddEntity(civvie);

            worlds["main"].manager.AddEntity(player);

            Quest mugging = SaveManager.ParseQuest("./Content/Quests/Mugging.quest");
            mugging.SetAvailable();
            mugging.worldManager = this;
            player.log.Add(mugging);
            this.worldQuests.Add(mugging);

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
