using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Douglas Gliner
namespace TheChicagoProject.GUI.Forms
{
    
    class ProgressBar : Control
    {
        // Fields
        // Graphics
        private Color progressColor;
        private Label text;
        private bool includeText;

        // Progress bar stuff
        private double maxValue;
        private double currentValue;
        private Bar bar;

        /// <summary>
        /// Gets or sets the progress bars progress color.
        /// </summary>
        public Color ProgressColor { get { return progressColor; } set { progressColor = value; } }
        /// <summary>
        /// Gets or sets whether or not the current over max is displayed in the center of the bar.
        /// </summary>
        public bool IncludeText { get { return includeText; } set { includeText = value;  } }
        /// <summary>
        /// Gets or sets the max value for this progress bar.
        /// </summary>
        public double MaxValue { get { return maxValue; } set { if (maxValue < currentValue) { maxValue = currentValue - maxValue; } else { maxValue = value; } } }
        /// <summary>
        /// Gets or sets the current value for this progress bar.
        /// </summary>
        public double CurrentValue { get { return currentValue; } set { if (currentValue > maxValue) { currentValue = maxValue; } else { currentValue = value; } } }

        public ProgressBar(Vector2 size)
        {
            includeText = false;
            progressColor = Color.Green;
            maxValue = 100;
            currentValue = 50;

            this.Size = size;

            bar = new Bar();
            bar.Size = size;
            //bar.parent = this;
            Add(bar);

            text = new Label();
            text.Text = String.Empty;
            text.AutoResize = true;
            text.Alignment = ControlAlignment.Center;
            //text.parent = this;
            Add(text);
        }

        public override void Update(GameTime gameTime)
        {
            if (includeText)
                text.Text = "" + currentValue + " / " + maxValue + "";
            else
                text.Text = "";

            bar.ProgressColor = progressColor;
            bar.Scale = new Vector2(((float)currentValue * Size.X) / (float)maxValue, Size.Y);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }

    }
}
