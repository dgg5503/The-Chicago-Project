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
        private Button pageForwardButton;
        private Button pageBackwardButton;
        private Container questBarsContainer;
        //private Container questUIContainer;
        private Container questActionsContainer;

        // List of quest bars for each quest...
        private QuestLog questLog;

        // "pages"
        private List<QuestInfoBarUI> questInfoBars;
        private int currentIndex;

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
            // Quest pages
            questInfoBars = new List<QuestInfoBarUI>();
            currentIndex = 0;

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
            questBarsContainer.Size = new Vector2(this.Size.X / 2, (this.Size.Y - headerContainer.Size.Y) * .85f);
            questBarsContainer.Location = new Vector2(0, headerContainer.Size.Y);
            questBarsContainer.parent = this;
            Add(questBarsContainer);

            // Forward backward button container

            // Quest UI to display more info on the quest.
            questUI = new QuestUI(new Vector2(this.Size.X / 2, (this.Size.Y - headerContainer.Size.Y) * .85f));
            //questUI.Size = new Vector2(this.Size.X / 2, (this.Size.Y - headerContainer.Size.Y) * .80f);
            questUI.Alignment = ControlAlignment.Right;
            questUI.Location = new Vector2(0, headerContainer.Size.Y);
            questUI.parent = this;
            Add(questUI);

            // quest buttons container.
            questActionsContainer = new Container();
            questActionsContainer.Alignment = ControlAlignment.Right;
            questActionsContainer.Size = new Vector2(this.Size.X / 2, (this.Size.Y - headerContainer.Size.Y) * .15f);
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

            // page button container
            Container pageButtonContainer = new Container();
            pageButtonContainer.Alignment = ControlAlignment.Left;
            pageButtonContainer.Size = new Vector2(this.Size.X / 2, (this.Size.Y - headerContainer.Size.Y) * .15f);
            pageButtonContainer.Location = new Vector2(0, questUI.Size.Y + headerContainer.Size.Y);
            pageButtonContainer.parent = this;
            Add(pageButtonContainer);

            // forward button
            pageForwardButton = new Button();
            pageForwardButton.Alignment = ControlAlignment.Center;
            pageForwardButton.Size = new Vector2(100, 50);
            pageForwardButton.Location = new Vector2(60, 0);
            pageForwardButton.IsActive = false;
            pageForwardButton.Text = "Forward >";
            pageForwardButton.Click += pageForwardButton_Click;
            pageForwardButton.parent = pageButtonContainer;
            pageButtonContainer.Add(pageForwardButton);

            // back button
            pageBackwardButton = new Button();
            pageBackwardButton.Alignment = ControlAlignment.Center;
            pageBackwardButton.Size = new Vector2(100, 50);
            pageBackwardButton.Location = new Vector2(-60, 0);
            pageBackwardButton.IsActive = false;
            pageBackwardButton.Text = "< Backward";
            pageBackwardButton.Click += pageBackwardButton_Click;
            pageBackwardButton.parent = pageButtonContainer;
            pageButtonContainer.Add(pageBackwardButton);
        }

        void pageBackwardButton_Click(object sender, EventArgs e)
        {
            // hide last that were on and toggle last four.
            questBarsContainer.Clear();

            int newMax = currentIndex;

            if (newMax - 4 <= 0)
            {
                newMax = questInfoBars.Count;
                currentIndex = questInfoBars.Count - 3;
            }
            else
                newMax -= 4;

            // isvisible????
            int lastFoundIndex = currentIndex;
            for (int i = currentIndex; i < newMax && i < questInfoBars.Count; i++)
            {
                questBarsContainer.Add(questInfoBars[i]);
                lastFoundIndex = i;
            }
            currentIndex = newMax + 1;

            /*
            int newMax = currentIndex;

            if (newMax - 4 < 0)
                newMax = questInfoBars.Count - newMax;
            else
                newMax -= 4;

            // isvisible????
            int lastFoundIndex = currentIndex;
            for (int i = currentIndex; i < newMax && i < questInfoBars.Count; i++)
            {
                questBarsContainer.Add(questInfoBars[i]);
                lastFoundIndex = i;
            }
            currentIndex = lastFoundIndex;
             * */
        }

        void pageForwardButton_Click(object sender, EventArgs e)
        {
            // hide last that were on and toggle next four.
            questBarsContainer.Clear();

            //currentIndex += 4;
            int newMax = currentIndex;

            if (newMax == questInfoBars.Count)
            {
                newMax = 4;
                currentIndex = 0;
            }
            else
            {
                if (newMax + 4 > questInfoBars.Count)
                    newMax += questInfoBars.Count - newMax;
            }

            // isvisible????
            int lastFoundIndex = currentIndex;
            for (int i = currentIndex; i < newMax && i < questInfoBars.Count; i++)
            {
                questBarsContainer.Add(questInfoBars[i]);
                lastFoundIndex = i;
            }
            currentIndex = lastFoundIndex + 1;
            
        }

        void startQuestButton_Click(object sender, EventArgs e)
        {
            if(questUI.LoadedQuest != null && questUI.LoadedQuest.Status == 1)
            {
                

                questUI.LoadedQuest.StartQuest();

                Game1.state = GameState.Game;
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

            if(questInfoBars.Count > 4)
            {
                pageBackwardButton.IsActive = true;
                pageForwardButton.IsActive = true;
            }

            base.Update(gameTime);
            
            questLogHeaderLabel.Location = new Vector2(questLogHeaderLabel.parent.Size.X / 2 - questLogHeaderLabel.Size.X / 2, questLogHeaderLabel.Location.Y);
        }

        public void Load(QuestLog log)
        {
            
            int count = 0;
            foreach(Quest quest in log)
            {
                if (count == 4)
                    count = 0;

                QuestInfoBarUI infoBar = new QuestInfoBarUI(new Vector2(200, 80));
                infoBar.Location = new Vector2(questBarsContainer.Size.X / 2 - infoBar.Size.X / 2, infoBar.Size.Y * count);
                infoBar.Alignment = ControlAlignment.Left;
                infoBar.parent = questBarsContainer;
                infoBar.Click += infoBar_Click;
                infoBar.Load(quest);
                infoBar.LoadVisuals();

                if (questInfoBars.Count < 4)
                {
                    questBarsContainer.Add(infoBar);
                    currentIndex = 4;
                }

                questInfoBars.Add(infoBar);
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
