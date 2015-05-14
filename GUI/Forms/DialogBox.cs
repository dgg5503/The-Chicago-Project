using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

//Douglas Gliner
namespace TheChicagoProject.GUI.Forms
{
    // TO-DO:
    // - Fix clicking buttons through buttons!!

    public enum DialogBoxType
    {
        YesNo,
        OkCancel,
        Ok
    }

    public class DialogBox : Dialog
    {
        private DialogBoxType type;

        public event EventHandler yesEvent; // also ok
        public event EventHandler noEvent; // also cancel

        public DialogBox(Vector2 size, DialogBoxType type, string header, string description, Vector2? location = null)
        {
            this.Size = size;
            this.type = type;

            eventsCompleted = false;

            if (location != null)
                this.Location = (Vector2)location;

            // Header container
            Container headerContainer = new Container();
            headerContainer.Size = new Vector2(this.Size.X, this.Size.Y * .13f);
            Add(headerContainer);

            // Header lbl
            Label headerLabel = new Label();
            headerLabel.Alignment = ControlAlignment.Center;
            headerLabel.Text = header;
            headerContainer.Add(headerLabel);

            // Close dialog button.
            Button closeButton = new Button();
            closeButton.Alignment = ControlAlignment.Right;
            closeButton.Size = new Vector2(this.Size.Y * .10f, this.Size.Y * .10f);
            closeButton.Click += closeButton_Click;
            closeButton.Text = "X";
            headerContainer.Add(closeButton);

            // Fill (description)
            Container fillContainer = new Container();
            fillContainer.Fill = new FillInfo(Color.DarkGray);
            fillContainer.Border = null;
            fillContainer.Alignment = ControlAlignment.Center;
            fillContainer.Size = new Vector2(this.Size.X * .90f, this.Size.Y * .72f);
            //fillContainer.Location = new Vector2(0, headerContainer.Size.Y);
            Add(fillContainer);

            // Fill description 
            Label dialogDescriptionLabel = new Label();
            dialogDescriptionLabel.WordWrap = true;
            dialogDescriptionLabel.Text = description;
            dialogDescriptionLabel.Alignment = ControlAlignment.Center;
            fillContainer.Add(dialogDescriptionLabel);

            // Button container
            Container buttonsContainer = new Container();
            buttonsContainer.Size = new Vector2(this.Size.X, this.Size.Y * .15f);
            buttonsContainer.Location = new Vector2(0, headerContainer.Size.Y + fillContainer.Size.Y);
            Add(buttonsContainer);

            // Buttons
            Button firstActionButton;
            Button secondActionButton;
            switch(type)
            {
                case DialogBoxType.Ok:
                    firstActionButton = new Button();
                    firstActionButton.Text = "OK";
                    firstActionButton.Alignment = ControlAlignment.Center;
                    firstActionButton.Size = new Vector2(buttonsContainer.Size.X * .50f, buttonsContainer.Size.Y * .80f);
                    firstActionButton.Click += firstActionButton_Click;
                    buttonsContainer.Add(firstActionButton);
                    break;

                case DialogBoxType.OkCancel:
                    firstActionButton = new Button();
                    firstActionButton.Text = "OK";
                    firstActionButton.Alignment = ControlAlignment.Center;
                    firstActionButton.Location = new Vector2(-buttonsContainer.Size.X * .25f, 0);
                    firstActionButton.Size = new Vector2(buttonsContainer.Size.X * .25f, buttonsContainer.Size.Y * .80f);
                    firstActionButton.Click += firstActionButton_Click;
                    buttonsContainer.Add(firstActionButton);

                    secondActionButton = new Button();
                    secondActionButton.Text = "Cancel";
                    secondActionButton.Alignment = ControlAlignment.Center;
                    secondActionButton.Location = new Vector2(buttonsContainer.Size.X * .25f, 0);
                    secondActionButton.Size = new Vector2(buttonsContainer.Size.X * .25f, buttonsContainer.Size.Y * .80f);
                    secondActionButton.Click += secondActionButton_Click;
                    buttonsContainer.Add(secondActionButton);
                    break;

                case DialogBoxType.YesNo:
                    firstActionButton = new Button();
                    firstActionButton.Text = "Yes";
                    firstActionButton.Alignment = ControlAlignment.Center;
                    firstActionButton.Location = new Vector2(-buttonsContainer.Size.X * .25f, 0);
                    firstActionButton.Size = new Vector2(buttonsContainer.Size.X * .25f, buttonsContainer.Size.Y * .80f);
                    firstActionButton.Click += firstActionButton_Click;
                    buttonsContainer.Add(firstActionButton);

                    secondActionButton = new Button();
                    secondActionButton.Text = "No";
                    secondActionButton.Alignment = ControlAlignment.Center;
                    secondActionButton.Location = new Vector2(buttonsContainer.Size.X * .25f, 0);
                    secondActionButton.Size = new Vector2(buttonsContainer.Size.X * .25f, buttonsContainer.Size.Y * .80f);
                    secondActionButton.Click += secondActionButton_Click;
                    buttonsContainer.Add(secondActionButton);
                    break;
            }

        }

        void secondActionButton_Click(object sender, EventArgs e)
        {
            // run no events then close
            if (noEvent != null)
                noEvent(this, EventArgs.Empty);

            eventsCompleted = true;
        }

        void firstActionButton_Click(object sender, EventArgs e)
        {
            // run yes events then close
            // run events then close
            if (yesEvent != null)
                yesEvent(this, EventArgs.Empty);

            eventsCompleted = true;
        }

        void closeButton_Click(object sender, EventArgs e)
        {
            // run events then close
            if (noEvent != null)
                noEvent(this, EventArgs.Empty);

            eventsCompleted = true;
        }
    }
}
