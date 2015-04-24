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

            // Name Label
            name = new Label();
            name.Alignment = ControlAlignment.Center;
            name.Text = "Quest Name";
            name.parent = nameRewardContainer;
            nameRewardContainer.Add(name);

            // Description container
            /*
            Container descriptionContainer = new Container();
            descriptionContainer.Size = new Vector2(this.Size.X + 40, this.Size.Y / 2)
            */



            


        }
    }
}
