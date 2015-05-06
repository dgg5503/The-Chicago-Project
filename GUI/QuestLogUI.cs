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
        private QuestUI questUI;
        private Button startQuestButton;
        private Button stopQuestButton;
        private Container questBarsContainer;
        //private Container questUIContainer;
        private Container questActionsContainer;

        // List of quest bars for each quest...
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

            // Exit quest log
            Button closeButton = new Button();
            closeButton.Size = new Vector2(15, 15);
            closeButton.Location = new Vector2(5, (headerContainer.Size.Y / 2) - 7.5f);
            closeButton.Alignment = ControlAlignment.Right;
            closeButton.Text = "X";
            closeButton.Click += closeButton_Click;
            closeButton.parent = headerContainer;
            headerContainer.Add(closeButton);

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

            // Quest UI to display more info on the quest.
            questUI = new QuestUI(new Vector2(this.Size.X / 2, (this.Size.Y - headerContainer.Size.Y) * .80f));
            //questUI.Size = new Vector2(this.Size.X / 2, (this.Size.Y - headerContainer.Size.Y) * .80f);
            questUI.Alignment = ControlAlignment.Right;
            questUI.Location = new Vector2(0, headerContainer.Size.Y);
            questUI.parent = this;
            Add(questUI);

            // quest buttons container.
            questActionsContainer = new Container();
            questActionsContainer.Alignment = ControlAlignment.Right;
            questActionsContainer.Size = new Vector2(this.Size.X / 2, (this.Size.Y - headerContainer.Size.Y) * .20f);
            questActionsContainer.Location = new Vector2(0, questUI.Size.Y + headerContainer.Size.Y);
            questActionsContainer.parent = this;
            Add(questActionsContainer);
            
            // the buttons...
            startQuestButton = new Button();
            startQuestButton.Alignment = ControlAlignment.Center;
            startQuestButton.Size = new Vector2(100, 50);
            startQuestButton.Location = new Vector2(-60, 0);
            startQuestButton.IsActive = false;
            startQuestButton.Text = "Start Quest";
            startQuestButton.Click += startQuestButton_Click;
            startQuestButton.parent = questActionsContainer;
            questActionsContainer.Add(startQuestButton);

            stopQuestButton = new Button();
            stopQuestButton.Alignment = ControlAlignment.Center;
            stopQuestButton.Size = new Vector2(100, 50);
            stopQuestButton.Location = new Vector2(60, 0);
            stopQuestButton.IsActive = false;
            stopQuestButton.Text = "Stop Quest";
            stopQuestButton.Click += stopQuestButton_Click;
            stopQuestButton.parent = questActionsContainer;
            questActionsContainer.Add(stopQuestButton);
        }

        void startQuestButton_Click(object sender, EventArgs e)
        {
            if(questUI.LoadedQuest != null && questUI.LoadedQuest.Status == 1)
            {
                // quest start code here.
                questUI.LoadedQuest.StartQuest();
            }
        }

        void stopQuestButton_Click(object sender, EventArgs e)
        {
            if (questUI.LoadedQuest != null && questUI.LoadedQuest.Status == 2)
            {
                // quest stop code here (NO STOP FUNCTION FOR QUESTS (?) )
                questUI.LoadedQuest.SetAvailable();
            }
        }

        void closeButton_Click(object sender, EventArgs e)
        {
            Game1.state = GameState.Game;
        }

        public override void Update(GameTime gameTime)
        {
            if (questUI.LoadedQuest != null)
            {
                if (questUI.LoadedQuest.Status == 1)
                    startQuestButton.IsActive = true;
                else
                    startQuestButton.IsActive = false;

                if (questUI.LoadedQuest.Status == 2)
                    stopQuestButton.IsActive = true;
                else
                    stopQuestButton.IsActive = false;
            }

            base.Update(gameTime);
            
            questLogHeaderLabel.Location = new Vector2(questLogHeaderLabel.parent.Size.X / 2 - questLogHeaderLabel.Size.X / 2, questLogHeaderLabel.Location.Y);
        }

        public void Load(QuestLog log)
        {
            int count = 0;
            foreach(Quest quest in log)
            {
                QuestInfoBarUI infoBar = new QuestInfoBarUI(new Vector2(200, 80));
                infoBar.Location = new Vector2(questBarsContainer.Size.X / 2 - infoBar.Size.X / 2, infoBar.Size.Y * count + 5);
                infoBar.Alignment = ControlAlignment.Left;
                infoBar.parent = questBarsContainer;
                infoBar.Click += infoBar_Click;
                questBarsContainer.Add(infoBar);
                infoBar.Load(quest);

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
                // make sure the current quest isnt already loaded!
                if (questUI.LoadedQuest != infoBar.LoadedQuest)
                {
                    // load the quest
                    questUI.Load(infoBar.LoadedQuest);

                    // load visuals
                    questUI.LoadVisuals(Game1.Instance.Content, Game1.Instance.GraphicsDevice);

                    // show actions buttons based on quest info.
                    // start stop quest buttons.
                    
                    
                }
            }
        }

        
    }
}
