using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheChicagoProject.GUI.Forms;
using TheChicagoProject.Entity;

namespace TheChicagoProject.GUI
{
    //Douglas Gliner
    class LivingEntityInfoUI : Control
    {
        // Important GUI controls that will need to be updated
        private Label healthLbl;
        private Label moneyLbl;

        // Entity to track
        private LivingEntity livingEntity;

        /// <summary>
        /// Get or set the entity which is being tracked by this GUI element
        /// </summary>
        public LivingEntity LivingEntity
        {
            get
            {
                return livingEntity;
            }

            set
            {
                if (value != null)
                {
                    //moneyLbl.Text = "" + value + "";
                    healthLbl.Text = "" + value.health + "";
                }
                else
                {
                    //moneyLbl.Text = "";
                    healthLbl.Text = "";
                }
                livingEntity = value;
            }
        }

        public LivingEntityInfoUI()
        {
            // Properties for this class
            this.Alignment = ControlAlignment.Right;
            this.Size = new Vector2(180, 80);
            this.Location = new Vector2(0, RenderManager.ViewportHeight - 100);

            // Holds health label and current health label
            Container healthInfoContainer = new Container();
            healthInfoContainer.Size = new Vector2(this.Size.X - 20, 30);
            healthInfoContainer.Location = new Vector2(0, -15);
            healthInfoContainer.Alignment = ControlAlignment.Center;

            Container healthTxtLblContainer = new Container();
            healthTxtLblContainer.Size = new Vector2(50, 30);
            healthTxtLblContainer.Alignment = ControlAlignment.Left;
            healthTxtLblContainer.parent = healthInfoContainer;
            healthInfoContainer.Add(healthTxtLblContainer);

            Label healthTxtLbl = new Label();
            healthTxtLbl.Size = new Vector2(1, 1);
            healthTxtLbl.AutoResize = true;
            healthTxtLbl.Text = "Health:";
            healthTxtLbl.TextAlignment = TextAlignment.Center;
            healthTxtLbl.parent = healthTxtLblContainer;
            healthTxtLblContainer.Add(healthTxtLbl);

            Container healthLblContainer = new Container();
            healthLblContainer.Size = new Vector2(80, 30);
            healthLblContainer.Alignment = ControlAlignment.Right;
            healthLblContainer.parent = healthInfoContainer;
            healthInfoContainer.Add(healthLblContainer);

            healthLbl = new Label();
            healthLbl.Size = new Vector2(1, 1);
            healthLbl.AutoResize = true;
            healthLbl.Text = "woah";
            healthLbl.TextAlignment = TextAlignment.Center;
            healthLbl.parent = healthLblContainer;
            healthLblContainer.Add(healthLbl);

            healthInfoContainer.parent = this;
            Add(healthInfoContainer);

            // Holds money label and current money label
            Container moneyInfoContainer = new Container();
            moneyInfoContainer.Location = new Vector2(0, 15);
            moneyInfoContainer.Size = new Vector2(this.Size.X - 20, 30);
            moneyInfoContainer.Alignment = ControlAlignment.Center;

            Container moneyTxtLblContainer = new Container();
            moneyTxtLblContainer.Size = new Vector2(50, 30);
            moneyTxtLblContainer.Alignment = ControlAlignment.Left;
            moneyTxtLblContainer.parent = moneyInfoContainer;
            moneyInfoContainer.Add(moneyTxtLblContainer);

            Label moneyTxtLbl = new Label();
            moneyTxtLbl.Size = new Vector2(1, 1);
            moneyTxtLbl.AutoResize = true;
            moneyTxtLbl.Text = "Money:";
            moneyTxtLbl.TextAlignment = TextAlignment.Center;
            moneyTxtLbl.parent = moneyTxtLblContainer;
            moneyTxtLblContainer.Add(moneyTxtLbl);

            Container moneyLblContainer = new Container();
            moneyLblContainer.Size = new Vector2(80, 30);
            moneyLblContainer.Alignment = ControlAlignment.Right;
            moneyLblContainer.parent = moneyInfoContainer;
            moneyInfoContainer.Add(moneyLblContainer);

            moneyLbl = new Label();
            moneyLbl.Size = new Vector2(1, 1);
            moneyLbl.AutoResize = true;
            moneyLbl.Text = "$00000000";
            moneyLbl.TextAlignment = TextAlignment.Center;
            moneyLbl.parent = moneyLblContainer;
            moneyLblContainer.Add(moneyLbl);

            moneyInfoContainer.parent = this;
            Add(moneyInfoContainer);

        }

        public override void Update(GameTime gameTime)
        {
            if (livingEntity != null)
            {
                healthLbl.Text = "" + livingEntity.health + "";
                //moneyLbl.Text = "" + livingEntity.money + "";
            }
            else
            {
                healthLbl.Text = "";
                moneyLbl.Text = "";
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
