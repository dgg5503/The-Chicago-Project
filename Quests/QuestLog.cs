using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject.Quests
{
    /// <summary>
    /// Holds a Dictionary of Quest objects and information about them
    /// </summary>
    class QuestLog
    {
        //fields
        private Dictionary<String, Quest> log;

        public QuestLog()
        {
            log = new Dictionary<String, Quest>();
        }

        //adds a new quest to the log
        public void Add(Quest newQuest)
        {
            log[newQuest.Name] = newQuest;
        }

        /// <summary>
        /// Gets the log for the user to access
        /// </summary>
        /// <returns>A list of quests that represents the player's quest log</returns>
        public List<Quest> GetLog()
        {
            return log.Values.ToList();
        }

        /// <summary>
        /// Returns a list of all quests that match the given status
        /// </summary>
        /// <param name="status">The status you are looking for: 0 - Unavailable, 1 - Unstarted, 2 - Started, 3 - Completed</param>
        /// <returns></returns>
        public List<Quest> GetByStatus(int status)
        {
            List<Quest> questsOfStatus = new List<Quest>();
            foreach(Quest quest in log.Values)
            {
                if (quest.Status == status)
                    questsOfStatus.Add(quest);
            }
            return questsOfStatus;
        }
    }
}
