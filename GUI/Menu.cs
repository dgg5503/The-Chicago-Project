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


    // Make sure to extend Control from .GUI.Forms!
    class Menu : Control
    {
        // Place your controls here like buttons or labels
        //---- CONTROLS ----

        // Header of the menu
        private Label lblHeader;

        // Buttons on the menu
        private Button startButton;
        private Button questBuilderButton;

        //---- CONTROLS ----

        /*
         * - In the constructor you must set atleast a size or else crash (textures are loaded on the fly since they are generated)
         * - Call the function Initialize forms to modify and init your controls.
         * 
         */
        public Menu()
        {

            this.Location = new Vector2(100, 100);
            this.Size = new Vector2(200, 300);

            /*
             * - Init blank constructor of control.
             *      - Init properties (size and control dependent properties are required like text for the label)
             *      - LOCATIONS ARE RELATIVE TO THE PARENTS TOP LEFT CORNER
             *          - Example, this menu class is located at 100, 100 and its top left corner will draw at 100,100
             *          - If a button is given a location of 20, 20 then on the global coord axis (relative to viewport), it will be located at 120, 120 NOT 20, 20
             *      - After initing properties, set its parent to "this" class.
             *      - Add it to this controls control list.
             */

            startButton = new Button();
            startButton.Text = "START";
            startButton.Click += buttons_Click;
            startButton.Size = new Vector2(55, 20);
            startButton.Alignment = ControlAlignment.Center;
            startButton.Location = new Vector2(0, 20);
            startButton.parent = this;
            Add(startButton);

            //Quest builder
            questBuilderButton = new Button();
            questBuilderButton.Size = new Vector2(95, 20);
            questBuilderButton.Alignment = ControlAlignment.Center;
            questBuilderButton.Location = new Vector2(0, 120);
            questBuilderButton.Text = "Quest Builder";
            questBuilderButton.Click += OpenTool;
            //questBuilderButton.Location = new Vector2((this.Size.X / 2) - (questBuilderButton.Size.X / 2), (this.Size.Y / 2) + 120);
            questBuilderButton.parent = this;
            Add(questBuilderButton);

            lblHeader = new Label();
            lblHeader.Text = "MAIN MENU";
            lblHeader.AutoResize = true;
            lblHeader.Location = new Vector2(0, -130);
            lblHeader.Alignment = ControlAlignment.Center;
            lblHeader.parent = this;
            Add(lblHeader);
        }

        /*
         * - As you can tell, clicking is automatically handled in the control class.
         */
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


        /*
         * - To draw border or fill simply do sb.Draw(border or fill, and then this.GlobalLocation(), color.white)
         */
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }


    }
}
