using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.GUI.Forms;
using TheChicagoProject.Entity;
using TheChicagoProject.Quests;

namespace TheChicagoProject.GUI
{
    class QuestLogUI : Control
    {
        // List of quest bars for each quest...
        private List<QuestInfoBarUI> questBars;
        private QuestLog questLog;
        private Label questLogHeaderLabel;

        public bool IsQuestLogLoaded
        {
            get
            {
                if (questLog == null)
                    return false;
                return true;
            }
        }

        public QuestLogUI()
        {
            // this info...
            this.Size = new Vector2(600, 400);
            this.Alignment = ControlAlignment.Center;

            // Header container
            Container headerContainer = new Container();
            headerContainer.Size = new Vector2(this.Size.X, this.Size.Y / 8);
            headerContainer.Alignment = ControlAlignment.Center;
            headerContainer.parent = this;
            Add(headerContainer);

            // Header
            questLogHeaderLabel = new Label();
            questLogHeaderLabel.Alignment = ControlAlignment.Left;
            questLogHeaderLabel.Text = "QUEST LOG";
            questLogHeaderLabel.Color = Color.Black;
            questLogHeaderLabel.parent = this;
            Add(questLogHeaderLabel);


            // test info bar.
            questBars = new List<QuestInfoBarUI>();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            questLogHeaderLabel.Location = new Vector2(questLogHeaderLabel.parent.Size.X / 2 - questLogHeaderLabel.Size.X / 2, questLogHeaderLabel.Location.Y);
        }

        public void Load(QuestLog log)
        {
            int count = 0;
            foreach(Quest quest in log)
            {
                QuestInfoBarUI infoBar = new QuestInfoBarUI(new Vector2(200, 80));
                infoBar.Location = new Vector2(10, this.Size.Y * count);
                infoBar.Alignment = ControlAlignment.Left;
                infoBar.parent = this;
                Add(infoBar);
                infoBar.Load(quest);
                questBars.Add(infoBar);
                count++;
            }
            questLog = log; 
        }
    }
}
