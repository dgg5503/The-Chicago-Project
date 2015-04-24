using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TheChicagoProject.Quests;
using TheChicagoProject.GUI;
using TheChicagoProject.GUI.Forms;

namespace TheChicagoProject.GUI
{
    class QuestUI : Control
    {
        // Controls to be modified via load and close.
        private Label name;
        private Label cashReward;
        private Label pointReward;
        private Label description;

        private Container objectiveImage;
        private Label objectiveName;

        // Only if wincondition delivery
        private Container objective2Image;
        private Label objective2Name;

        // Current quest in this questUI
        private Quest currentQuest;

        public bool IsQuestLoaded { get { if(currentQuest == null){return false;} return true;} }

        public QuestUI()
        {
            // base
            this.Size = new Vector2(400, 400);
            this.Alignment = ControlAlignment.Center;

            // Name and Reward container
            Container nameRewardContainer = new Container();
            nameRewardContainer.Size = new Vector2(this.Size.X, this.Size.Y / 10);
            nameRewardContainer.parent = this;
            this.Add(nameRewardContainer);

            // Reward Info Container
            Container rewardInfoContainer = new Container();
            rewardInfoContainer.Size = new Vector2(nameRewardContainer.Size.X / 2, nameRewardContainer.Size.Y);
            rewardInfoContainer.Alignment = ControlAlignment.Right;
            rewardInfoContainer.parent = nameRewardContainer;
            nameRewardContainer.Add(rewardInfoContainer);

            // Cash reward container
            Container cashRewardContainer = new Container();
            cashRewardContainer.Size = new Vector2(rewardInfoContainer.Size.X / 2, nameRewardContainer.Size.Y);
            cashRewardContainer.Alignment = ControlAlignment.Right;
            cashRewardContainer.parent = rewardInfoContainer;
            rewardInfoContainer.Add(cashRewardContainer);

            // Cash label
            cashReward = new Label();
            cashReward.Alignment = ControlAlignment.Center;
            cashReward.Text = "$000";
            cashReward.parent = cashRewardContainer;
            cashRewardContainer.Add(cashReward);

            // Quest point reward container
            Container questPointContainer = new Container();
            questPointContainer.Size = new Vector2(rewardInfoContainer.Size.X / 2, nameRewardContainer.Size.Y);
            questPointContainer.Alignment = ControlAlignment.Left;
            questPointContainer.parent = rewardInfoContainer;
            rewardInfoContainer.Add(questPointContainer);

            // Quest point label
            pointReward = new Label();
            pointReward.Alignment = ControlAlignment.Center;
            pointReward.Text = "@000";
            pointReward.parent = questPointContainer;

            questPointContainer.Add(pointReward);

            // Name label container
            Container nameLabelContainer = new Container();
            nameLabelContainer.Size = new Vector2(nameRewardContainer.Size.X / 2, nameRewardContainer.Size.Y);
            nameLabelContainer.Alignment = ControlAlignment.Left;
            nameLabelContainer.parent = nameRewardContainer;
            nameRewardContainer.Add(nameLabelContainer);

            // Name Label
            name = new Label();
            name.Alignment = ControlAlignment.Center;
            name.Text = "Quest Name";
            name.parent = nameLabelContainer;
            nameLabelContainer.Add(name);

            // DescriptionObjective container
            Container descObjectiveContainer = new Container();
            descObjectiveContainer.Size = new Vector2(this.Size.X, this.Size.Y - nameRewardContainer.Size.Y);
            descObjectiveContainer.Location = new Vector2(0, nameRewardContainer.Size.Y);
            descObjectiveContainer.Alignment = ControlAlignment.Left;
            descObjectiveContainer.parent = this;
            Add(descObjectiveContainer);

            // Description container
            Container descriptionContainer = new Container();
            descriptionContainer.Size = new Vector2((this.Size.X / 8) * 5, this.Size.Y - nameRewardContainer.Size.Y);
            descriptionContainer.Alignment = ControlAlignment.Left;
            descriptionContainer.parent = descObjectiveContainer;
            descObjectiveContainer.Add(descriptionContainer);

            // Description label
            description = new Label();
            description.Alignment = ControlAlignment.Left;
            description.Text = "Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description ";
            description.WordWrap = true;
            description.parent = descriptionContainer;
            descriptionContainer.Add(description);

            // --- THERE COULD BE MULTIPLE OF THESE (stack them or side by side?) ---
            
            // Objective container
            Container objectivesContainer = new Container();
            objectivesContainer.Size = new Vector2((this.Size.X / 8) * 3, this.Size.Y - nameRewardContainer.Size.Y);
            objectivesContainer.Alignment = ControlAlignment.Right;
            objectivesContainer.parent = descObjectiveContainer;
            descObjectiveContainer.Add(objectivesContainer);

            // Objective image

            // Objective name container

            // Objective name

            // --- THERE COULD BE MULTIPLE OF THESE ---
        }

        /// <summary>
        /// Load the quest you want to view.
        /// </summary>
        /// <param name="quest"></param>
        public void Load(Quest quest)
        {
            // load the quest
            currentQuest = quest;

            // load text
            name.Text = currentQuest.Name;
            description.Text = currentQuest.Description;
            cashReward.Text = "" + currentQuest.CashReward + "";
            pointReward.Text = "" + currentQuest.Reward + "";

            // load objectives
            
        }

        public void Close()
        {
            currentQuest = null;
        }


    }
}
