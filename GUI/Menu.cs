using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.GUI.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;


namespace TheChicagoProject.GUI
{
    // Douglas Gliner

    // Each GUI overlay is its own Form (think of Windows forms, but the desktop being the game and the forms being these menus)

    class Menu : Control
    {
        // Header of the menu
        private Label header;

        // Buttons on the menu
        private Button buttons;


        public Menu()
        {

            this.Location = new Vector2(100, 100);
            this.Size = new Vector2(200, 300);
            
            InitializeForms();
        }

        public void InitializeForms()
        {
            buttons = new Button();
            buttons.Text = "START";
            buttons.Click += buttons_Click;
            buttons.Location = new Vector2(this.Size.X / 2, (this.Size.Y / 2) + 20);
            buttons.Size = new Vector2(55, 20);
            buttons.parent = this;
            Add(buttons);

            //Quest builder
            buttons = new Button();
            buttons.Text = "Quest Builder";
            buttons.Click += OpenTool;
            buttons.Location = new Vector2(this.Size.X / 2, (this.Size.Y / 2) + 120);
            buttons.Size = new Vector2(95, 20);
            buttons.parent = this;
            Add(buttons);

            header = new Label();
            header.Text = "MAIN MENU";
            header.Font = null;
            header.Location = new Vector2(this.Size.X / 2 - 45, 10);
            header.Size = new Vector2(50, 10);
            header.parent = this;
            Add(header);
        }

        void buttons_Click(object sender, EventArgs e)
        {
            Game1.state = GameState.Game;
        }

        //Sean Levorse
        void OpenTool(object sender, EventArgs e)
        {
            
                Quests.QuestGenerator.QuestBuilder tool = new Quests.QuestGenerator.QuestBuilder();
                System.Windows.Forms.ApplicationContext appContext = new System.Windows.Forms.ApplicationContext();
                appContext.MainForm = tool;
                System.Windows.Forms.Application.Run(appContext);
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }


    }
}
