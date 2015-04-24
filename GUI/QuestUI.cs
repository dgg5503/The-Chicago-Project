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

            // Name Label
            name = new Label();
            name.Alignment = ControlAlignment.Left;
            name.Text = "Quest Name";
            name.parent = nameRewardContainer;
            nameRewardContainer.Add(name);


            


        }
    }
}
