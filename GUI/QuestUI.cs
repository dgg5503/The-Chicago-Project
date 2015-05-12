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

        // objectives container
        private Container objectivesContainer;

        // Current quest in this questUI
        private Quest currentQuest;

        public bool IsQuestLoaded { get { if(currentQuest == null){return false;} return true;} }
        public Quest LoadedQuest { get { return currentQuest; } }

        public QuestUI(Vector2 size)
        {
            // base
            this.Size = size;
            this.Alignment = ControlAlignment.Center;

            // Name and Reward container
            Container nameRewardContainer = new Container();
            nameRewardContainer.Size = new Vector2(this.Size.X, this.Size.Y / 10);
            //nameRewardContainer.parent = this;
            Add(nameRewardContainer);

            // Reward Info Container
            Container rewardInfoContainer = new Container();
            rewardInfoContainer.Size = new Vector2(nameRewardContainer.Size.X / 2, nameRewardContainer.Size.Y);
            rewardInfoContainer.Alignment = ControlAlignment.Right;
            //rewardInfoContainer.parent = nameRewardContainer;
            nameRewardContainer.Add(rewardInfoContainer);

            // Cash reward container
            Container cashRewardContainer = new Container();
            cashRewardContainer.Size = new Vector2(rewardInfoContainer.Size.X / 2, nameRewardContainer.Size.Y);
            cashRewardContainer.Alignment = ControlAlignment.Right;
            //cashRewardContainer.parent = rewardInfoContainer;
            rewardInfoContainer.Add(cashRewardContainer);

            // Cash label
            cashReward = new Label();
            cashReward.Alignment = ControlAlignment.Center;
            cashReward.Text = "$000";
            //cashReward.parent = cashRewardContainer;
            cashRewardContainer.Add(cashReward);

            // Quest point reward container
            Container questPointContainer = new Container();
            questPointContainer.Size = new Vector2(rewardInfoContainer.Size.X / 2, nameRewardContainer.Size.Y);
            questPointContainer.Alignment = ControlAlignment.Left;
            //questPointContainer.parent = rewardInfoContainer;
            rewardInfoContainer.Add(questPointContainer);

            // Quest point label
            pointReward = new Label();
            pointReward.Alignment = ControlAlignment.Center;
            pointReward.Text = "@000";
            //pointReward.parent = questPointContainer;

            questPointContainer.Add(pointReward);

            // Name label container
            Container nameLabelContainer = new Container();
            nameLabelContainer.Size = new Vector2(nameRewardContainer.Size.X / 2, nameRewardContainer.Size.Y);
            nameLabelContainer.Alignment = ControlAlignment.Left;
            //nameLabelContainer.parent = nameRewardContainer;
            nameRewardContainer.Add(nameLabelContainer);

            // Name Label
            name = new Label();
            name.Alignment = ControlAlignment.Center;
            name.Text = "Quest Name";
            //name.parent = nameLabelContainer;
            nameLabelContainer.Add(name);

            // DescriptionObjective container
            Container descObjectiveContainer = new Container();
            descObjectiveContainer.Size = new Vector2(this.Size.X, this.Size.Y - nameRewardContainer.Size.Y);
            descObjectiveContainer.Location = new Vector2(0, nameRewardContainer.Size.Y);
            descObjectiveContainer.Alignment = ControlAlignment.Left;
            //descObjectiveContainer.parent = this;
            Add(descObjectiveContainer);

            // Description container
            Container descriptionContainer = new Container();
            descriptionContainer.Size = new Vector2((this.Size.X / 8) * 5, this.Size.Y - nameRewardContainer.Size.Y);
            descriptionContainer.Alignment = ControlAlignment.Left;
            //descriptionContainer.parent = descObjectiveContainer;
            descObjectiveContainer.Add(descriptionContainer);

            // Description label
            description = new Label();
            description.Alignment = ControlAlignment.Left;
            //description.Text = "Description\nDescription Description Description Description Description Description Description Description Description Description Description Description\nDescription Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description ";
            description.WordWrap = true;
            //description.parent = descriptionContainer;
            descriptionContainer.Add(description);

            // --- THERE COULD BE MULTIPLE OF THESE (stack them or side by side?) ---
            
            // Objective container
            objectivesContainer = new Container();
            objectivesContainer.Size = new Vector2((this.Size.X / 8) * 3, this.Size.Y - nameRewardContainer.Size.Y);
            objectivesContainer.Alignment = ControlAlignment.Right;
            //objectivesContainer.parent = descObjectiveContainer;
            descObjectiveContainer.Add(objectivesContainer);
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
            cashReward.Text = "$" + currentQuest.CashReward + "";
            pointReward.Text = "@" + currentQuest.Reward + "";

            // load objectives
            ObjectivesUI tmpObjective = null;
            switch(quest.WinCondition)
            {
                case WinCondition.AllEnemiesDead:
                    // enemies to kill? (not sure about this one)
                    // foreach for #

                    // tmp dict.
                    /*
                    Dictionary<string, Entity.LivingEntity> objectiveEntities = new Dictionary<string, Entity.LivingEntity>();
                    foreach(Entity.Entity ent in quest.entitites)
                    {
                        Console.WriteLine(ent.GetType());
                    }
                    */

                    // Currently no way to get entity names or group them by types (no mugger class etc...) (?)

                    tmpObjective = new ObjectivesUI(objectivesContainer.Size);
                    tmpObjective.Load("Kill this enemy", "mugger", quest.EnemyToKill.sprite.Texture);
                    //tmpObjective.parent = objectivesContainer;
                    objectivesContainer.Add(tmpObjective);
                    break;

                case WinCondition.DeliverItem:
                    // deliver some item to some recipient
                    tmpObjective = new ObjectivesUI(new Vector2(objectivesContainer.Size.X, objectivesContainer.Size.Y / 2));
                    tmpObjective.Load("Deliver this", quest.Delivery.name, quest.Delivery.previewSprite.Texture);
                    //tmpObjective.parent = objectivesContainer;
                    objectivesContainer.Add(tmpObjective);

                    tmpObjective.Load("Recipient", "RECIP", quest.Recipient.sprite.Texture);
                    tmpObjective.Location = new Vector2(0, objectivesContainer.Size.Y / 2);
                    //tmpObjective.parent = objectivesContainer;
                    objectivesContainer.Add(tmpObjective);
                    break;

                case WinCondition.EnemyDies:
                    // kill some enemy
                    tmpObjective = new ObjectivesUI(objectivesContainer.Size);
                    tmpObjective.Load("Kill this enemy", "mugger", quest.EnemyToKill.sprite.Texture);
                    //tmpObjective.parent = objectivesContainer;
                    objectivesContainer.Add(tmpObjective);
                    break;

                case WinCondition.ObtainItem:
                    // find this
                    tmpObjective = new ObjectivesUI(objectivesContainer.Size);
                    tmpObjective.Load("Find this item", quest.FindThis.name, quest.FindThis.previewSprite.Texture);
                    //tmpObjective.parent = objectivesContainer;
                    objectivesContainer.Add(tmpObjective);
                    break;
            }
            
            
            
        }

        public void Close()
        {
            name.Text = "Quest Name";
            description.Text = String.Empty;
            cashReward.Text = "$000";
            pointReward.Text = "@000";

            currentQuest = null;
        }


    }
}
