using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheChicagoProject.GUI;
using TheChicagoProject.GUI.Forms;


namespace TheChicagoProject.GUI.Forms
{
    public class NPCTalkUI : Dialog
    {
        // Controls to modify.
        private Label nameOfNPCLabel;
        private Label textToSpeakLabel;
        private Label proceedingLabel;
        private Container textContainer;

        int pages = 1;


        public NPCTalkUI(Vector2 size, string name, string text, Vector2? location = null)
        {
            this.Size = size;
            if (location != null)
                this.Location = (Vector2)location;
            else
                Alignment = ControlAlignment.Center;

            // Double border for niceness...
            Container doubleBorderContainer = new Container();
            doubleBorderContainer.Size = new Vector2(this.Size.X * .95f, this.Size.Y * .95f);
            doubleBorderContainer.Alignment = ControlAlignment.Center;
            Add(doubleBorderContainer);

            // Header container
            Container headerContainer = new Container();
            headerContainer.Size = new Vector2(doubleBorderContainer.Size.X, doubleBorderContainer.Size.Y * .1f);
            headerContainer.Alignment = ControlAlignment.Left;
            headerContainer.Border = null;
            doubleBorderContainer.Add(headerContainer);

            // Name of thing talking to you label
            nameOfNPCLabel = new Label();
            nameOfNPCLabel.Text = name;
            nameOfNPCLabel.Scale = .9f;
            nameOfNPCLabel.Alignment = ControlAlignment.Center;
            headerContainer.Add(nameOfNPCLabel);

            // Text container
            textContainer = new Container();
            textContainer.Size = new Vector2(doubleBorderContainer.Size.X, doubleBorderContainer.Size.Y * .8f);
            textContainer.Location = new Vector2(0, doubleBorderContainer.Size.Y * .1f);
            textContainer.Alignment = ControlAlignment.Left;
            doubleBorderContainer.Add(textContainer);

            // Text to speak!
            textToSpeakLabel = new Label();
            textToSpeakLabel.WordWrap = true;
            textToSpeakLabel.Text = text;
            textToSpeakLabel.Alignment = ControlAlignment.Center;
            textContainer.Add(textToSpeakLabel);

            // How to proceed container
            Container stuffToDoNextContainer = new Container();
            stuffToDoNextContainer.Size = new Vector2(doubleBorderContainer.Size.X, doubleBorderContainer.Size.Y * .1f);
            stuffToDoNextContainer.Location = new Vector2(0, doubleBorderContainer.Size.Y * .9f);
            stuffToDoNextContainer.Border = null;
            stuffToDoNextContainer.Alignment = ControlAlignment.Left;
            doubleBorderContainer.Add(stuffToDoNextContainer);

            // How to proceed label.
            proceedingLabel = new Label();
            proceedingLabel.Alignment = ControlAlignment.Center;
            proceedingLabel.Scale = .75f;
            proceedingLabel.Text = "Space to continue...";
            stuffToDoNextContainer.Add(proceedingLabel);

            KeyClicked += NPCTalkUI_KeyClicked;
        }

        void NPCTalkUI_KeyClicked(object sender, KeysEventArgs e)
        {

            foreach (Keys key in e.Keys)
                Console.WriteLine(key);

            if(e.Keys.Contains(Keys.Space))
            {
                // next page or close.
                pages--;
                if (pages == 0)
                    eventsCompleted = true;
            }
        }

        protected override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            // check text size and calculate pages!
            if(textToSpeakLabel.Size.X > textContainer.Size.X || textToSpeakLabel.Size.Y > textContainer.Size.Y)
            {

            }

        }


    }
}
