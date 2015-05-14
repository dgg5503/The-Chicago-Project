using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TheChicagoProject.Quests;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using TheChicagoProject.Entity;
using TheChicagoProject.GUI;

namespace TheChicagoProject
{
    //Sean Levorse
    /// <summary>
    /// Anything and everything to do with file handling,
    /// aka reading or writing to the hard disk, goes here.
    /// </summary>
    public class SaveManager
    {
        public const string QUEST_DIRECTORY = "./Content/Quests/";
        public static string storylineFolder;
        public string saveLoc;
        protected Game1 MainGame;

        //Constructor
        public SaveManager()
        {
            MainGame = Game1.Instance;
            saveLoc = "./Content/SaveFiles/save.save";
        }

        /// <summary>
        /// loads the game
        /// </summary>
        public void Load()
        {
#if !DEBUG
            if (!MainGame.worldManager.worlds.ContainsKey("main"))
            {
                World mainworld = LoadWorld("main");
                MainGame.worldManager.worlds.Add("main", mainworld);
            }
#endif
            LoadSave();
        }

        #region load world
        /// <summary>
        /// loads the world from a given path
        /// </summary>
        /// <param name="worldPath">The path to the world</param>
        public World LoadWorld(String worldPath) {
            
            Stream worldStream = File.OpenRead("./Content/" + worldPath + ".txt");
            StreamReader worldReader = new StreamReader(worldStream);

            World tmpWorld = new World(int.Parse(worldReader.ReadLine()), int.Parse(worldReader.ReadLine()));

            GUI.Door[] doors = new GUI.Door[int.Parse(worldReader.ReadLine())];

            for(int x = 0; x < doors.Length; ++x)
            {
                doors[x] = new Door(worldReader.ReadLine(), int.Parse(worldReader.ReadLine()), int.Parse(worldReader.ReadLine()));
            }

            Vector2[] doorLocs = new Vector2[doors.Length];

            int y = 0;
            string line = worldReader.ReadLine();
            int row = 0;
            int doorCntr = 0;
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

                        case '6':
                            doorLocs[y] = new Vector2(row, col);
                            y++;
                            tmpWorld.tiles[row][col] = doors[doorCntr];
                            doorCntr++;
                            break;

                        case '7':
                            tmpWorld.tiles[row][col] = Tiles.tilesDictionary["Debris"];
                            break;
                    }
                }
                row++;
                line = worldReader.ReadLine();
            }
            tmpWorld.doors = doorLocs;
            //MainGame.worldManager.worlds.Add(worldPath, tmpWorld);
            return tmpWorld;
        }
        #endregion

        #region .save File
        /// <summary>
        /// saves the game
        /// </summary>
        public void Save()
        {
            // Create a stream, then a writer
            Stream outStream = null;
            BinaryWriter output = null;
            try
            {
                if(!Directory.Exists("./Content/SaveFiles"))
                {
                    Directory.CreateDirectory("./Content/SaveFiles");
                }
                //initialize them
                outStream = File.OpenWrite(saveLoc);
                output = new BinaryWriter(outStream);

                //Get the player
                Entity.Player player = MainGame.worldManager.CurrentWorld.manager.GetPlayer();

                //get the current world
                string world = MainGame.worldManager.CurrentWorldString;
               
                //get the player's stats
                int playerX = player.location.IntX;
                int playerY = player.location.IntY;
                int playerHealth = player.health;
                int pCash = player.Cash;
                int pQuestPoints = player.QuestPoints;

                //get the quest statuses
                QuestLog log = player.log;
                object[,] questStatuses = new object[log.Count, 2];
                for(int i = 0; i < log.Count; i++)
                {
                    questStatuses[i, 0] = log[i].Name;
                    questStatuses[i, 1] = log[i].Status;
                }

                //get the items in the inventory
                List<string> items = new List<string>();
                Item.Inventory inventory = player.inventory;
                foreach(Item.Item item in inventory)
                {
                    items.Add(item.name);
                }

                //store all of the data in the file
                output.Write(world);
                output.Write(playerX);
                output.Write(playerY);
                output.Write(playerHealth);
                output.Write(pCash);
                output.Write(pQuestPoints);
                output.Write((Int32)log.Count);
                Console.WriteLine(log.Count);
                for(int i = 0; i < log.Count; i++)
                {
                    output.Write((string)questStatuses[i, 0]);
                    output.Write((int)questStatuses[i, 1]);
                }
                output.Write(items.Count);
                foreach(Item.Item item in inventory)
                {
                    if(SaveItem(item))
                        output.Write(item.name);
                }
                output.Write(inventory.ActiveWeapon);

            }
            catch(Exception e)
            {
                Console.WriteLine("Error while saving the game: " + e.Message);
                Console.WriteLine("Stack: \n\t" + e.StackTrace);
            }
            finally
            {
                if (output != null)
                {
                    // Done writing
                    output.Close();
                }
            }
            
        }

        /// <summary>
        /// loads a save file
        /// </summary>
        private void LoadSave()
        {
            Console.WriteLine(saveLoc);
            Stream inStream = null;
            BinaryReader input = null;

            try
            {
                //open the file for reading
                inStream = File.OpenRead(saveLoc);
                input = new BinaryReader(inStream);

                //red the file
                string world = input.ReadString();
                Console.WriteLine("World: " + world);
                int pX = input.ReadInt32();
                int pY = input.ReadInt32();
                Console.WriteLine("X, Y: " + pX + ", " + pY);
                int pHealth = input.ReadInt32();
                Console.WriteLine("Health: " + pHealth);
                int pCash = input.ReadInt32();
                Console.WriteLine("Cash: " + pCash);
                int pQuestPoints = input.ReadInt32();
                Console.WriteLine("QuestPoints: " + pQuestPoints);

                //read the quests
                int numQuests = input.ReadInt32();
                Console.WriteLine("Num quests: " + numQuests);
                object[,] quests = new object[numQuests,2];
                for(int i = 0; i < numQuests; i++)
                {
                    quests[i, 0] = input.ReadString();
                    Console.WriteLine("\tName: " + quests[i, 0]);
                    quests[i, 1] = input.ReadInt32();
                    Console.WriteLine("\tStatus: " + quests[i, 1]);
                }

                //read the inventory
                int numItems = input.ReadInt32();
                Console.WriteLine("Num Items: " + numItems);
                string[] items = new string[numItems];
                for(int i = 0 ; i < numItems; i++)
                {
                    items[i] = input.ReadString();
                    Console.WriteLine("\t" + items[i]);
                }
                
                int activeItem = input.ReadInt32();
                Console.WriteLine("Active Weapon: " + activeItem);

                //make the world
                if (MainGame.worldManager.worlds.ContainsKey(world))
                {
                    MainGame.worldManager.worlds[world] = LoadWorld(world);
                }
                else
                {
                    MainGame.worldManager.worlds.Add(world, LoadWorld(world));
                }

                //set the player in the world
                Player player = new Player(
                    new FloatRectangle(pX, pY, 32, 32),
                    Sprites.spritesDictionary["player"]
                    );
                player.Cash = pCash;
                player.health = pHealth;
                player.QuestPoints = pQuestPoints;

                
                //load all of the quests in the quest file
                LoadQuests(MainGame.worldManager.worldQuests);

                //load the quest status
                string quest;
                QuestLog log = MainGame.worldManager.worldQuests;
                QuestLog pLog = player.log;
                for(int i = 0; i < numQuests; i++)
                {
                    quest = (string)quests[i,0];
                    if (log.ContainsQuest(quest))
                    {
                        pLog[quest] = log[quest];
                        pLog[quest].Status = (int)quests[i, 1];
                    }
                }
                
                //load the items
                Item.Inventory inventory = player.inventory;
                Item.Item newItem;
                for(int i = 0; i < numItems; i++)
                {
                    string name = items[i];
                    //newItem.image = Sprites.spritesDictionary[newItem.name].Texture;
                    //figure out the save path
                    int startname = saveLoc.LastIndexOf('/') + 1;
                    int endname = saveLoc.LastIndexOf('.');
                    string filename = saveLoc.Substring(startname, endname - startname);
                    string directory = saveLoc.Substring(0, startname) + "/" + filename;
                    newItem = LoadItem(directory + "/" + name + ".item");
                    inventory.Add(newItem);
                }
                inventory.ActiveWeapon = activeItem;
                
                //add the player to the world
                MainGame.worldManager.CurrentWorld.manager.AddEntity(player);

                //add the player to the quests
                foreach(Quest loopQuest in MainGame.worldManager.worldQuests)
                {
                    loopQuest.player = player;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error reading file: " + e.Message);
                Console.WriteLine("Stack: \n\t" + e.StackTrace);
            }
            finally
            {
                if (input != null)
                    input.Close();
            }
            
        }

        #endregion

        #region Quests

        /// <summary>
        /// Loads the quests in the quest folder
        /// </summary>
        /// <param name="log">the directory of the quests</param>
        public void LoadQuests(QuestLog log)
        {
            string[] files = Directory.GetFiles(QUEST_DIRECTORY);
            Quest loaded;
            //loop through all of the files in the directory
            foreach(string path in files)
            {
                //try and parse the quest from each file
                loaded = ParseQuest(path);

                //if the parse was successfull, add it to the log
                if (loaded != null)
                {
                    log.Add(loaded);
                }
            }
        }

        /// <summary>
        /// Takes a path to a quest file and parses it to a quest object
        /// </summary>
        /// <param name="filename">the file path</param>
        /// <returns>A quest</returns>
        public static Quest ParseQuest(string filename)
        {
            Quest quest = null;
            Console.WriteLine("Quest file name: \n\t" + filename);
            using (StreamReader input = new StreamReader(filename))
            {

                string data = input.ReadToEnd();
                if (data.Substring(5, 12).Contains("Storyline"))
                {
                    /*
                    //load individual quests
                    int index, end;
                    index = data.IndexOf("Folder") + 8;
                    end = data.IndexOf('"', index);
                    string folder = data.Substring(index, end - index);
                    storylineFolder = QUEST_DIRECTORY + folder;

                    Storyline newStoryline;


                    string[] files = Directory.GetFiles(storylineFolder);
                    Quest loaded;
                    
                    //loop through all of the files in the directory
                    foreach (string path in files)
                    {
                        //try and parse the quest from each file
                        loaded = ParseQuest(path);

                        //if the parse was successfull, add it to the log
                        if (loaded != null)
                        {
                            newStoryline.Add(loaded);
                        }
                    }
                     */
                }
                else
                {
                    int index, end;
                    string attribute;
                    //get name
                    index = data.IndexOf("Name:") + 6;
                    end = data.IndexOf('"', index);
                    string name = data.Substring(index, end - index);

                    //get the description
                    index = data.IndexOf("Description:") + 13;
                    end = data.IndexOf('"', index);
                    string description = data.Substring(index, end - index);
                    
                    //get objective
                    index = data.IndexOf("Objective:") + 11;
                    end = data.IndexOf('"', index);
                    string objective = data.Substring(index, end - index);

                    //get reward
                    index = data.IndexOf("Reward:");
                    index = data.IndexOf("Cash:", index) + 5;
                    end = data.IndexOf("\n", index);
                    attribute = data.Substring(index, end - index);
                    int cash;
                    if (!int.TryParse(attribute, out cash))
                        cash = 0;
                    index = data.IndexOf("Q-Points:", index) + 9;
                    end = data.IndexOf("\n", index);
                    attribute = data.Substring(index, end - index);
                    int qPoints;
                    if (!int.TryParse(attribute, out qPoints))
                        qPoints = 0;
                    
                    //get start point
                    index = data.IndexOf("Start:");
                    index = data.IndexOf("X:", index) + 2;
                    end = data.IndexOf("\n", index);
                    attribute = data.Substring(index, end - index);
                    int X;
                    if (!int.TryParse(attribute, out X))
                        X = 10;
                    index = data.IndexOf("Y:", index) + 2;
                    end = data.IndexOf("\r", index);
                    attribute = data.Substring(index, end - index);
                    int Y;
                    if (!int.TryParse(attribute, out Y))
                        Y = 10;
                    Vector2 start = new Vector2(X, Y);

                    //get the win condition
                    index = data.IndexOf("Condition:") + 10;
                    end = data.IndexOf("\r", index);
                    attribute = data.Substring(index, end - index);
                    WinCondition condition; //win state
                    switch (attribute)
                    {
                        case "EnemyDies":
                            condition = WinCondition.EnemyDies;
                            break;   
                        case "ObtainItem":
                            condition = WinCondition.ObtainItem;
                            break;
                        case "DeliverItem":
                            condition = WinCondition.DeliverItem;
                            break;
                        case "AllEnemiesDead":
                        default:
                            condition = WinCondition.AllEnemiesDead;
                            break;
                    }

                    //get the id of the entity or item that satisfies the quest condition
                    string target = "";
                    string recipient = "";
                    switch (condition)
                    {
                        case WinCondition.EnemyDies:
                        case WinCondition.ObtainItem:
                            //get the name of the entity
                            index = data.IndexOf("Target:") + 7;
                            string test = data.Substring(index);
                            end = data.IndexOf('\n', index) - 1;
                            target = data.Substring(index, end - index);
                            break;
                        case WinCondition.DeliverItem:
                            //get the target item
                            index = data.IndexOf("Target:") + 7;
                            target = data.Substring(index);

                            //get destination
                            index = data.IndexOf("Recipient:") + 10;
                            end = data.IndexOf('\n');
                            recipient = data.Substring(index, index - end);
                            break;
                    }
#region Read Enemies
                    //read all of the living entities
                    Dictionary<string, Entity.LivingEntity> livingEntities = new Dictionary<string, Entity.LivingEntity>();
                    int endEntity = data.Length;
                    FloatRectangle EntityRect;
                    AI.AI ai;
                    //string test = data.Substring(index, 10);
                    index = 1;
                    while((index = data.IndexOf("Living Entity:", index)) > 0)
                    {
                        ai = null;
                        endEntity = data.IndexOf("End Living Enity", index);
                        
                        //get the id
                        index = data.IndexOf("ID:", index) + 3;
                        end = data.IndexOf('\r', index);
                        string id = data.Substring(index, end - index);

                        //get the start position
                        //get start point
                        index = data.IndexOf("Start:", index);
                        index = data.IndexOf("X:", index) + 2;
                        end = data.IndexOf("\n", index);
                        attribute = data.Substring(index, end - index);
                        int sX;
                        if (!int.TryParse(attribute, out sX))
                            sX = 10;
                        index = data.IndexOf("Y:", index) + 2;
                        end = data.IndexOf("\r", index);
                        attribute = data.Substring(index, end - index);
                        int sY;
                        if (!int.TryParse(attribute, out sY))
                            sY = 10;

                        //get texture
                        index = data.IndexOf("Texture:", index) + 9;
                        end = data.IndexOf('"', index);
                        string texturePath = data.Substring(index, end - index);
                        GUI.Sprite sprite = GUI.Sprites.spritesDictionary[texturePath];

                        //create rectangle
                        EntityRect = new FloatRectangle(sX, sY, sprite.Width, sprite.Height);

                        //get the health
                        index = data.IndexOf("Health:", index) + 7;
                        end = data.IndexOf('\n', index);
                        attribute = data.Substring(index, end - index);
                        int health;
                        if(!int.TryParse(attribute, out health))
                        {
                            health = 0;
                        }

                        livingEntities[id] = new Entity.LivingEntity(EntityRect, sprite, health, null);
                        livingEntities[id].Name = id;

                        //get the ai
                        index = data.IndexOf("AI:", index) + 3;
                        end = data.IndexOf('\n', index) - 1;
                        if (index >= 0)
                        {
                            attribute = data.Substring(index, end - index);
                            
                            switch (attribute)
                            {
                                case "Low":
                                    ai = new AI.LowAI(livingEntities[id]);
                                    break;
                                case "Mid":
                                    ai = new AI.HighAI(livingEntities[id]);
                                    break;
                                case "High":
                                    ai = new AI.HighAI(livingEntities[id]);
                                    break;
                                default:
                                    ai = null;
                                    break;
                            }

                            livingEntities[id].ai = ai;
                            livingEntities[id].inventory.Add(new Item.Weapon(50, 1, 10, "Bam", 100, 0.5));
                            livingEntities[id].inventory.ActiveWeapon = 0;

                        }

                        
                        
                    }

#endregion

#region Read Items
                    Dictionary<string, QuestItem> items = new Dictionary<string,QuestItem>();
                    index = 0;
                    int endItem = data.Length;
                    while((index = data.IndexOf("Item:", index)) > 0)
                    {
                        endItem = data.IndexOf("End Item", index);

                        //get the id
                        index = data.IndexOf("ID:", index) + 3;
                        end = data.IndexOf('\n', index);
                        string id = data.Substring(index, end - index);

                        //get the texture
                        index = data.IndexOf("Texture:", index) + 9;
                        end = data.IndexOf('"', index);
                        string texturePath = data.Substring(index, end - index);
                        GUI.Sprite sprite = GUI.Sprites.spritesDictionary[texturePath];

                        //create item
                        items[id] = new QuestItem(id);
                        items[id].Item = new Item.Item();
                        items[id].Item.previewSprite = sprite;

                        
                    }
#endregion

                    //Create the quest
                    quest = new Quest(name, objective, description, start, null, condition, qPoints, cash);

                    //add the entities to the quests entitiy list
                    foreach (Entity.LivingEntity entity in livingEntities.Values.ToList())
                    {
                        quest.entitites.Add(entity);
                    }
                    
                    //items too
                    /* Quest needs to be updated
                    foreach(Item.Item item in items.Values.ToArray())
                    {
                        quest.
                    }
                     */

                    switch (condition)
                    {
                        case WinCondition.EnemyDies:
                            quest.EnemyToKill = livingEntities[target];
                            break;
                        case WinCondition.AllEnemiesDead:
                            break;
                        case WinCondition.ObtainItem:
                            quest.FindThis = items[target].Item;
                            break;
                        case WinCondition.DeliverItem:
                            quest.Delivery = items[target].Item;
                            quest.Recipient = livingEntities[recipient];
                            break;
                        default:
                            break;
                    }
                }

            }
            return quest;
        }

        #endregion

        #region Items

        /// <summary>
        /// loads an item based on the item file
        /// </summary>
        /// <param name="path">the filepath of the item</param>
        /// <returns>The item</returns>
        public Item.Item LoadItem(string path)
        {
            Item.Item newItem = null;
            using(Stream inStream = File.OpenRead(path))
            {
                using(BinaryReader input = new BinaryReader(inStream))
                {
                    string type = input.ReadString();
                    if(type.ToUpper() == "WEAPON")
                    {
                        Item.Weapon newWeapon;
                        int rof = input.ReadInt32();
                        int damage = input.ReadInt32();    
                        double reload = input.ReadDouble();
                        int clip = input.ReadInt32();      
                        double spread = input.ReadDouble();
                        int loadedAmmo = input.ReadInt32();
                        int ammo = input.ReadInt32();      
                        string name = input.ReadString();  
                        string sprite = input.ReadString();
                        newWeapon = new Item.Weapon(rof, damage, reload, name, clip, spread);

                        Console.WriteLine("ROF:\t" + rof);
                        Console.WriteLine("Damage:\t" + damage);
                        Console.WriteLine("Reload:\t" + reload);
                        Console.WriteLine("Clip:\t" + clip);
                        Console.WriteLine("Spread:\t" + spread);
                        Console.WriteLine("Loaded Ammo:\t" + loadedAmmo);
                        Console.WriteLine("Ammo:\t" + ammo);
                        Console.WriteLine("Name:\t" + name);
                        Console.WriteLine("Sprite:\t" + sprite);



                        newWeapon.Ammo = ammo;
                        newWeapon.LoadedAmmo = loadedAmmo;

                        if (Sprites.spritesDictionary.ContainsKey(sprite))
                            newWeapon.previewSprite = Sprites.spritesDictionary[sprite];
                        else
                            newWeapon.previewSprite = Sprites.spritesDictionary["NULL"];

                        return newWeapon;
                    }
                    else
                    {
                        newItem = new Item.Item();
                        string name = input.ReadString();      
                        string sprite = input.ReadString();    
                    }
                }
            }
            return newItem;
        }

        /// <summary>
        /// writes to a file the details behind an item
        /// </summary>
        /// <param name="item">the item to save</param>
        /// <returns>if the save was successful</returns>
        public bool SaveItem(Item.Item item)
        {
            // Create a stream, then a writer
            Stream outStream = null;
            BinaryWriter output = null;
            bool successful;
            try
            {
                //figure out the save path
                int startname = saveLoc.LastIndexOf('/') + 1;
                int endname = saveLoc.LastIndexOf('.');
                string name = saveLoc.Substring(startname, endname - startname);
                string directory = saveLoc.Substring(0, startname) + "/" + name;

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                //initialize the binary writer
                outStream = File.OpenWrite(directory+ item.name + ".item");
                output = new BinaryWriter(outStream);

                //write data
                if(item is Item.Weapon)
                {
                    output.Write("Weapon");
                    Item.Weapon weapon = (Item.Weapon)item;
                    output.Write(weapon.rateOfFire);
                    output.Write(weapon.Damage);
                    output.Write(weapon.ReloadTime);
                    output.Write(weapon.maxClip);
                    output.Write(weapon.spread);
                    output.Write(weapon.LoadedAmmo);
                    output.Write(weapon.Ammo);
                }
                else
                {
                    output.Write("Item");
                }

                output.Write(item.name);

                //get the key of the texture
                Dictionary<String, Sprite> sDict = Sprites.spritesDictionary;
                string key = "NULL";
                foreach (string spriteKey in sDict.Keys.ToList())
                {
                    if (sDict[spriteKey] == item.previewSprite)
                    {
                        key = spriteKey;
                    }
                }
                output.Write(key);

                successful = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving item: " + e.Message);
                successful = false;
            }
            finally
            {
                if (output != null)
                    output.Close();
            }

            return successful;
        }

        #endregion
    }
}
