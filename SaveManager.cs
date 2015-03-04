using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TheChicagoProject.Quests;

namespace TheChicagoProject
{
    /// <summary>
    /// Anything and everything to do with file handling,
    /// aka reading or writing to the hard disk, goes here.
    /// </summary>
    class SaveManager
    {
        public const string QUEST_DIRECTORY = "Quests/";

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

    }
}
