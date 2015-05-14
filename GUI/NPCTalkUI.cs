using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheChicagoProject.GUI;
using TheChicagoProject.GUI.Forms;


namespace TheChicagoProject.GUI
{
    public class NPCTalkUI : Control
    {
        // Controls to modify.
        private Label nameOfNPCLabel;
        private Label textToSpeakLabel;

        public NPCTalkUI(Vector2 size, string name, string text)
        {
            this.Size = size;

            // Double border for niceness...
            Container textContainer = new Container();
            textContainer.Size = new Vector2(this.Size.X * .95f, this.Size.Y * .95f);
            textContainer.Alignment = ControlAlignment.Center;
            Add(textContainer);

            // Name of thing talking to you label
            nameOfNPCLabel = new Label();
            nameOfNPCLabel.Text = "Name of NPC";
            nameOfNPCLabel.Alignment = ControlAlignment.CenterX;
            nameOfNPCLabel.Location = new Vector2(0, 10);
            textContainer.Add(nameOfNPCLabel);

            // Text to speak!
            textToSpeakLabel = new Label();
            textToSpeakLabel.WordWrap = true;
            textToSpeakLabel.Text = "Text here.";
            textToSpeakLabel.Alignment = ControlAlignment.Center;
            textContainer.Add(textToSpeakLabel);
        }

        protected override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            // check text size and calculate pages!

        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }


    }
}
