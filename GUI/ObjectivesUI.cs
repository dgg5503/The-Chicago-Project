using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TheChicagoProject.Quests;
using TheChicagoProject.GUI;
using TheChicagoProject.GUI.Forms;

namespace TheChicagoProject.GUI
{
    class ObjectivesUI : Control
    {
        // Controls to be modified
        private Container objectiveImageContainer;
        private Label objectiveNameLabel;
        private Label winConditionLabel;

        // Enemy to kill
        public ObjectivesUI(Vector2 size)
        {
            // same size as parent or predefined size (?)
            this.Size = size;

            // top half container
            Container topHalfContainer = new Container();
            topHalfContainer.Size = new Vector2(this.Size.X, this.Size.Y / 2);
            topHalfContainer.Alignment = ControlAlignment.Left;
            //topHalfContainer.parent = this;
            Add(topHalfContainer);

            // win condition label
            winConditionLabel = new Label();
            winConditionLabel.Location = new Vector2(0, -45);
            winConditionLabel.Text = "Win condition";
            winConditionLabel.Alignment = ControlAlignment.Center;
            //winConditionLabel.parent = topHalfContainer;
            topHalfContainer.Add(winConditionLabel);

            // Image container
            Container actualImageContainer = new Container();
            actualImageContainer.Size = new Vector2(64, 64);
            actualImageContainer.Alignment = ControlAlignment.Center;
            //actualImageContainer.parent = topHalfContainer;
            topHalfContainer.Add(actualImageContainer);

            // The actual image
            objectiveImageContainer = new Container();
            objectiveImageContainer.Size = new Vector2(1, 1);
            objectiveImageContainer.Border = null;
            objectiveImageContainer.Alignment = ControlAlignment.Center;
            //objectiveImageContainer.parent = actualImageContainer;
            actualImageContainer.Add(objectiveImageContainer);

            // info container
            Container nameConditionContainer = new Container();
            nameConditionContainer.Size = new Vector2(this.Size.X, this.Size.Y / 2);
            nameConditionContainer.Location = new Vector2(0, this.Size.Y / 2);
            nameConditionContainer.Alignment = ControlAlignment.Left;
            //nameConditionContainer.parent = this;
            Add(nameConditionContainer);

            // objective name label
            objectiveNameLabel = new Label();
            objectiveNameLabel.Text = "NAME";
            objectiveNameLabel.Alignment = ControlAlignment.Center;
            objectiveNameLabel.WordWrap = true;
            //objectiveNameLabel.parent = nameConditionContainer;
            nameConditionContainer.Add(objectiveNameLabel);
            
        }
        
        // load correct stuff
        public void Load(string winCondition, string objective, Texture2D image)
        {
            objectiveNameLabel.Text = winCondition;
            winConditionLabel.Text = objective;
            
            if (image != null)
            {
                objectiveImageContainer.Size = new Vector2(image.Width, image.Height);
                //objectiveImageContainer.ControlSizeChange(objectiveImageContainer.Size); // possibly un needed work being done here
                objectiveImageContainer.Fill = new FillInfo(image, Color.White);
            }
        }

    }
}
