using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Item;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Douglas Gliner
namespace TheChicagoProject.GUI.Forms
{

    public class DragableControl : Control
    {
        public const int SIDE_LENGTH = 64;

        private Label textLbl;

        private Item.Item item;

        public Item.Item Item { get { return item; } }

        public DragableControl(Item.Item item, float sideLen = 64)
        {
            this.Size = new Vector2(sideLen, sideLen);
            this.Fill = new FillInfo(item.previewSprite.Texture, Color.White);

            this.item = item;

            textLbl = new Label();
            textLbl.Text = item.name;
            textLbl.AutoResize = true;
            textLbl.Alignment = ControlAlignment.Center;
            //textLbl.parent = this;
            Add(textLbl);

            


            Weapon testWep;
            if ((testWep = item as Weapon) != null)
            {
                textLbl.Text += "\n" + testWep.LoadedAmmo + " / " + testWep.Ammo;
            }
        }

        /*
        void DragableControl_Pressed(object sender, EventArgs e)
        {
            if (controlMatrix != null && isPressed == false)
            {
                mouseOrigin = new Vector2(this.CurrentFrameMouseState.Position.X - this.Location.X, this.CurrentFrameMouseState.Position.Y - this.Location.Y);
                isPressed = true;
            }
        }
         
        public override void Update(GameTime gameTime)
        {
            if(controlMatrix != null && isPressed)
            {
                this.Location = new Vector2(this.CurrentFrameMouseState.Position.X - mouseOrigin.X, this.CurrentFrameMouseState.Position.Y - mouseOrigin.Y);

                // If most of box is over a new container, snap to that box
                // else
                // return to last location.
                if (this.CurrentFrameMouseState.LeftButton == ButtonState.Released)
                {
                    for (int x = 0; x < controlMatrix.GetLength(0); x++)
                        for (int y = 0; y < controlMatrix.GetLength(1); y++ )
                        {
                            Vector2 globalLoc = controlMatrix[x, y].GlobalLocation();
                            Vector2 size = controlMatrix[x, y].Size;
                            if(new Rectangle((int)globalLoc.X, (int)globalLoc.Y, (int)Size.X, (int)Size.Y).Contains(new Vector2(CurrentFrameMouseState.Position.X,CurrentFrameMouseState.Position.Y)))
                            {
                                // nothing in that loc, place, else swap.
                                if(controlMatrix[x, y] == null)
                                {
                                    controlMatrix[x, y] = this;
                                    controlMatrix[dragMatrixX, dragMatrixY] = null;

                                    this.dragMatrixX = x;
                                    this.dragMatrixY = y;
                                }
                                else
                                {
                                    DragableControl tmp = controlMatrix[x, y];

                                    controlMatrix[x, y] = this;
                                    controlMatrix[dragMatrixX, dragMatrixY] = tmp;

                                    controlMatrix[dragMatrixX, dragMatrixY].DragMatrixX = dragMatrixX;
                                    controlMatrix[dragMatrixX, dragMatrixY].DragMatrixY = dragMatrixY;

                                    this.dragMatrixX = x;
                                    this.dragMatrixY = y;
                                }
                                
                                 
                            }
                        }
                    isPressed = false;
                }
            }

            base.Update(gameTime);
        }
        */
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Fill, this.GlobalLocation(), Color.White);
            //spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);
            base.Draw(spriteBatch, gameTime);
        }

    }
}
