using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace TheChicagoProject.GUI.Forms
{
    // Douglas Gliner
    class Button : Control
    {
        // Label on button
        private Label text;

        // Text on button
        // Hover information...
        private BorderInfo hoverBorder;
        private FillInfo hoverFill;

        private BorderInfo pressedBorder;
        private FillInfo pressedFill;

        private BorderInfo defaultBorder;
        private FillInfo defaultFill;

        
        /// <summary>
        /// Gets or sets the text for this label.
        /// </summary>
        public string Text { get { return text.Text; } set { text.Text = value; } }

        public BorderInfo HoverBorder { get { return hoverBorder; } set { hoverBorder = value; } }
        public FillInfo HoverFill { get { return hoverFill; } set { hoverFill = value; } }
        public BorderInfo PressedBorder { get { return pressedBorder; } set { pressedBorder = value; } }
        public FillInfo PressedFill { get { return pressedFill; } set { pressedFill = value; } }
        public BorderInfo DefaultBorder { get { return defaultBorder; } set { defaultBorder = value; } }
        public FillInfo DefaultFill { get { return defaultFill; } set { defaultFill = value; } }

        public Button()
        {
            this.Hover += Button_Hover;
            this.Pressed += Button_Pressed;

            defaultBorder = new BorderInfo(this.Border.Value.width, Color.Black);
            defaultFill = new FillInfo(Color.Gray);

            hoverBorder = new BorderInfo(this.Border.Value.width, Color.White);
            pressedBorder = new BorderInfo(this.Border.Value.width, Color.LightGray);

            hoverFill = new FillInfo(Color.Gray);
            pressedFill = new FillInfo(Color.LightGray);

            text = new Label();
            text.Text = String.Empty;
            text.AutoResize = true;
            text.Alignment = ControlAlignment.Center;
            text.parent = this;
            Add(text);
        }

        void Button_Pressed(object sender, EventArgs e)
        {
            this.Border = pressedBorder;
            this.Fill = pressedFill;
        }

        void Button_Hover(object sender, EventArgs e)
        {
            this.Border = hoverBorder;
            this.Fill = hoverFill;
        }

        public override void Update(GameTime gameTime)
        {
            this.Border = defaultBorder;
            this.Fill = defaultFill;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
