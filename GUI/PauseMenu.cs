using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.GUI.Forms;

namespace TheChicagoProject.GUI
{
    //Douglas Gliner
    public class PauseMenu : Control
    {
        // Header of the menu
        private Label lblHeader;

        // Resume button
        private Button resumeButton;

        // Save button
        private Button saveButton;

        public PauseMenu()
        {

            this.Location = new Vector2(100, 100);
            this.Size = new Vector2(200, 300);

            resumeButton = new Button();
            resumeButton.Text = "Resume";
            resumeButton.Click += resumeButton_Click;
            resumeButton.Size = new Vector2(55, 20);
            resumeButton.Location = new Vector2((this.Size.X / 2) - (resumeButton.Size.X / 2), (this.Size.Y / 2) + 20);
            resumeButton.parent = this;
            Add(resumeButton);

            saveButton = new Button();
            saveButton.Text = "Save";
            saveButton.Click += saveButton_Click;
            saveButton.Size = new Vector2(55, 20);
            saveButton.Location = new Vector2((this.Size.X / 2) - (saveButton.Size.X / 2), (this.Size.Y / 2) + 60);
            saveButton.parent = this;
            Add(saveButton);

            lblHeader = new Label();
            lblHeader.Text = "Paused!";
            lblHeader.AutoResize = true;
            lblHeader.Location = new Vector2(0, -130);
            lblHeader.Alignment = ControlAlignment.Center;
            lblHeader.parent = this;
            Add(lblHeader);
        }

        void saveButton_Click(object sender, EventArgs e)
        {
            // where does this come from? it has to be static!!!
            Game1.Instance.saveManager.Save();
        }

        void resumeButton_Click(object sender, EventArgs e)
        {
            Game1.state = GameState.Game;
        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Fill, this.GlobalLocation(), Color.White);
            //spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            
            base.Draw(spriteBatch, gameTime);
        }
    }
}
