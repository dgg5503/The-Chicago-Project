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
using Microsoft.Xna.Framework.GamerServices;

namespace TheChicagoProject
{
    /// <summary>
    /// Anything and everything to do with file handling,
    /// aka reading or writing to the hard disk, goes here.
    /// </summary>
    class SaveManager
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// saves the game
        /// </summary>
        public void Save()
        {
            throw new NotImplementedException();
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
                using(StreamReader input = new StreamReader(path))
                {
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

                    //create new quest
                    Quest loaded = new Quest(
                        name,
                        objective,
                        description,
                        startPos,
                        reward,
                        cashReward
                        );
                    log.Add(loaded);
                }
            }
        }
    }
}
