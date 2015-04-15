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

namespace TheChicagoProject
{
    //Sean Levorse
    /// <summary>
    /// Anything and everything to do with file handling,
    /// aka reading or writing to the hard disk, goes here.
    /// </summary>
    public class SaveManager
    {
        public const string QUEST_DIRECTORY = "Quests/";
        protected Game1 MainGame;

        //Constructor
        public SaveManager(Game1 mainGame)
        {
            MainGame = mainGame;
        }

        /// <summary>
        /// loads the game
        /// </summary>
        public void Load()
        {
            LoadQuests(MainGame.worldManager.CurrentWorld.manager.quests);
            LoadWorld("main");
        }

        private void LoadWorld(String worldPath) {
            //using(BinaryReader reader = new BinaryReader())
        }

        /// <summary>
        /// saves the game
        /// </summary>
        public void Save()
        {
           
        }

        /// <summary>
        /// Saves all of the quests in the quest log
        /// </summary>
        /// <param name="log">The player's quest log</param>
        public void SaveQuests(QuestLog log)
        {
            //Loops through the quest log and save each quest
            foreach(Quest quest in log.GetLog())
            {
                string filePath = QUEST_DIRECTORY + quest.Name + ".quest";
                SaveQuest(quest, filePath);
            }
   
            /*****************************************************
             *                                                   *
             *           Display Completion Message?             *
             *                                                   *
             *****************************************************/
        }

        /// <summary>
        /// Saves a specific quest
        /// </summary>
        /// <param name="quest">The Quest</param>
        /// <param name="path">Where the quest should be stored</param>
        protected void SaveQuest(Quest quest, string path)
        {
            using (StreamWriter output = new StreamWriter(path))
            {
                output.WriteLine(quest.Name);
                output.WriteLine(quest.Description);
                output.WriteLine(quest.Objective);
                output.WriteLine(quest.StartPoint.X + ", " + quest.StartPoint.Y);
                output.WriteLine(quest.Status);
                output.WriteLine(quest.Reward);
                output.WriteLine(quest.CashReward);
            }
        }

        /// <summary>
        /// Loads the quests in the quest folder
        /// </summary>
        /// <param name="log">the directory of the quests</param>
        public void LoadQuests(QuestLog log)
        {
            string[] files = Directory.GetFiles(QUEST_DIRECTORY);
            foreach(string path in files)
            {
                
            }
        }

        public Quest ParseQuest(string filename)
        {
            Quest quest = null;
            using (StreamReader input = new StreamReader(filename))
            {
                /*
                //read input
                string name = input.ReadLine();
                string description = input.ReadLine();
                string objective = input.ReadLine();
                string position = input.ReadLine();
                int status = int.Parse(input.ReadLine());
                int reward = int.Parse(input.ReadLine());
                int cashReward = int.Parse(input.ReadLine());

                //parse position
                int X = int.Parse(position.Substring(0, position.Length - position.IndexOf(',')));
                int Y = int.Parse(position.Substring(position.IndexOf(',') + 2));
                Vector2 startPos = new Vector2(X, Y);

                */
                string data = input.ReadToEnd();
                if (data.Substring(5, 10).Contains("Storyline"))
                {
                    //load individual quests
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
                    index = data.IndexOf("Cash Reward:", index) + 12;
                    end = data.IndexOf("\n", index);
                    attribute = data.Substring(index, end - index);
                    int cash;
                    if (!int.TryParse(attribute, out cash))
                        cash = 0;
                    index = data.IndexOf("Quest Reward:", index) + 13;
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
                    end = data.IndexOf("\n", index);
                    attribute = data.Substring(index, end - index);
                    int Y;
                    if (!int.TryParse(attribute, out Y))
                        Y = 10;
                    Vector2 start = new Vector2(X, Y);

                    //get the win condition
                    index = data.IndexOf("Condition:", index) + 10;
                    end = data.IndexOf("\n", index);

                }

            }
            return quest;
        }
    }
}
