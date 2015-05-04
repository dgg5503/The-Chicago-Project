using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheChicagoProject.GUI.Forms;
using TheChicagoProject.Quests;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheChicagoProject.GUI
{
    class QuestInfoBarUI : Button
    {
        // Fields
        // Controls to be modified
        private Label questNameLabel;
        private Label questRewardPointLabel;
        private Label questRewardMoneyLabel;
        //private Label questWindConditionLabel;

        // Quest this bar holds.
        private Quest currentQuest; 

        public QuestInfoBarUI(Vector2 size)
        {
            // Root control information
            this.Size = size;

            // Name container
            Container nameContainer = new Container();
            nameContainer.Size = new Vector2(this.Size.X - 40, this.Size.Y / 3);
            nameContainer.Location = new Vector2(0, -this.Size.Y / 5);
            nameContainer.Alignment = ControlAlignment.Center;
            nameContainer.parent = this;
            Add(nameContainer);

            // Name label
            questNameLabel = new Label();
            questNameLabel.Alignment = ControlAlignment.Center;
            questNameLabel.Text = "Quest Name";
            questNameLabel.parent = nameContainer;
            nameContainer.Add(questNameLabel);


            // Quest Info container
            Container questInfoContainer = new Container();
            questInfoContainer.Size = new Vector2(this.Size.X - 40, this.Size.Y / 3);
            questInfoContainer.Location = new Vector2(0, this.Size.Y / 5);
            questInfoContainer.Alignment = ControlAlignment.Center;
            questInfoContainer.parent = this;
            Add(questInfoContainer);


            // money label
            questRewardMoneyLabel = new Label();
            questRewardMoneyLabel.Location = new Vector2(5, 0);
            questRewardMoneyLabel.Alignment = ControlAlignment.Left;
            //questRewardMoneyLabel.Location = new Vector2(0, questInfoContainer.Size.Y / 2);
            questRewardMoneyLabel.Text = "$0000";
            questRewardMoneyLabel.parent = questInfoContainer;
            questInfoContainer.Add(questRewardMoneyLabel);
            
            // quest point label
            questRewardPointLabel = new Label();
            questRewardPointLabel.Location = new Vector2(5, 0);
            questRewardPointLabel.Alignment = ControlAlignment.Right;
            //questRewardPointLabel.Location = new Vector2(0, questInfoContainer.Size.Y / 2);
            questRewardPointLabel.Text = "@0000";
            questRewardPointLabel.parent = questInfoContainer;
            questInfoContainer.Add(questRewardPointLabel);

            /*
            // condition label
            questWindConditionLabel = new Label();
            questWindConditionLabel.Alignment = ControlAlignment.Right;
            questWindConditionLabel.Text = "Win Condition";
            questWindConditionLabel.parent = questInfoContainer;
            questInfoContainer.Add(questWindConditionLabel);
            */
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
            questNameLabel.Text = currentQuest.Name;
            questRewardMoneyLabel.Text = "$" + currentQuest.CashReward + "";
            questRewardPointLabel.Text = "@" + currentQuest.Reward + "";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            questRewardMoneyLabel.Location = new Vector2(questRewardMoneyLabel.Location.X, questRewardMoneyLabel.parent.Size.Y / 2 - questRewardMoneyLabel.Size.Y / 2);
            questRewardPointLabel.Location = new Vector2(questRewardPointLabel.Location.X, questRewardPointLabel.parent.Size.Y / 2 - questRewardPointLabel.Size.Y / 2);
        }
    }
}
