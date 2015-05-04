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
        // Controls to be modified
        private Label questLogHeaderLabel;
        private Container questBarsContainer;

        // List of quest bars for each quest...
        private List<QuestInfoBarUI> questBars;
        private QuestLog questLog;
        

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
            headerContainer.Size = new Vector2(this.Size.X, 20);
            headerContainer.Alignment = ControlAlignment.Left;
            headerContainer.parent = this;
            Add(headerContainer);

            // Header
            questLogHeaderLabel = new Label();
            questLogHeaderLabel.Alignment = ControlAlignment.Center;
            questLogHeaderLabel.Text = "QUEST LOG";
            questLogHeaderLabel.Color = Color.Black;
            questLogHeaderLabel.parent = headerContainer;
            Add(questLogHeaderLabel);

            // Quest bar container
            questBarsContainer = new Container();
            questBarsContainer.Alignment = ControlAlignment.Left;
            questBarsContainer.Size = new Vector2(this.Size.X / 2, this.Size.Y - headerContainer.Size.Y);
            questBarsContainer.Location = new Vector2(0, headerContainer.Size.Y);
            questBarsContainer.parent = this;
            Add(questBarsContainer);

            // Forward backward button container

            // quest information container

            // quest buttons container.



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
                infoBar.Location = new Vector2(questBarsContainer.Size.X / 2 - infoBar.Size.X / 2, (this.Size.Y * count) + 10);
                infoBar.Alignment = ControlAlignment.Left;
                infoBar.parent = questBarsContainer;
                infoBar.Click += infoBar_Click;
                questBarsContainer.Add(infoBar);
                infoBar.Load(quest);

                questBars.Add(infoBar);
                count++;
            }
            questLog = log; 
        }

        // on click, take quest info from bar and pass to a quest ui.
        private void infoBar_Click(object sender, EventArgs e)
        {
            QuestInfoBarUI infoBar = sender as QuestInfoBarUI;
            if(infoBar != null)
            {
                // create quest UI
                // load the quest
                // load visuals (how...)
            }
        }

        
    }
}
