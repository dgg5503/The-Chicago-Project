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
    class DragableMatrixV2 : Control
    {
        // Matrix handling
        private List<DragableContainer> containers;
        private int maxSlots;
        private float sideLength;

        // Mouse handling
        private Vector2 mouseOrigin;
        private DragableControl currentDragableControl;
        private DragableControl hoveringDragableControl;

        public DragableMatrixV2(Vector2 size, int maxSlots)
        {
            // make sure valid w/h for number of max slots
            if (size.X * size.Y < maxSlots)
                throw new Exception("More max slots then allowed by size of control!");

            this.Size = size;
            
            containers = new List<DragableContainer>();
            this.maxSlots = maxSlots;

            // setup containers relative to loc.
            SetupContainers();

            mouseOrigin = new Vector2(-1, -1);
            currentDragableControl = null;
        }

        /// <summary>
        /// Sets up containers relative to this matrix position. Will create square containers with sidelength appropriate for maxSlots.
        /// </summary>
        private void SetupContainers()
        {
            // find optimal container size when max number of slots are in this matrix

            // http://stackoverflow.com/questions/868997/max-square-size-for-unknown-number-inside-rectangle
            // come up with an initial guess
            double aspect = (double)Size.Y / Size.X;
            double xf = Math.Sqrt(maxSlots / aspect);
            double yf = xf * aspect;
            int tx = (int)Math.Max(1.0, Math.Floor(xf));
            int ty = (int)Math.Max(1.0, Math.Floor(yf));
            int x_size = (int)Math.Floor((double)Size.X / tx);
            int y_size = (int)Math.Floor((double)Size.Y / ty);
            int tileSize = Math.Min(x_size, y_size);

            // test our guess:
            tx = (int)Math.Floor((double)Size.X / tileSize);
            ty = (int)Math.Floor((double)Size.Y / tileSize);
            if (tx * ty < maxSlots) // we guessed too high
            {
                if (((tx + 1) * ty < maxSlots) && (tx * (ty + 1) < maxSlots))
                {
                    // case 2: the upper bound is correct
                    //         compute the tileSize that will
                    //         result in (x+1)*(y+1) tiles
                    x_size = (int)Math.Floor((double)Size.X / (tx + 1));
                    y_size = (int)Math.Floor((double)Size.Y / (ty + 1));
                    tileSize = Math.Min(x_size, y_size);
                }
                else
                {
                    // case 3: solve an equation to determine
                    //         the final x and y dimensions
                    //         and then compute the tileSize
                    //         that results in those dimensions
                    int test_x = (int)Math.Ceiling((double)maxSlots / ty);
                    int test_y = (int)Math.Ceiling((double)maxSlots / tx);
                    x_size = (int)Math.Min(Math.Floor((double)Size.X / test_x), Math.Floor((double)Size.Y / ty));
                    y_size = (int)Math.Min(Math.Floor((double)Size.X / tx), Math.Floor((double)Size.Y / test_y));
                    tileSize = Math.Max(x_size, y_size);
                }
            }

            sideLength = tileSize;
            
            
            // TO-DO: center the controls or provide divider, they will probably be off a little due to floats.
            //Math.Max(Size.X % tileSize, Size.Y % tileSize);
            float dividerX = 0;
            float dividerY = 0;
           
            dividerX = (Size.X % tileSize) / ((float)Math.Floor(Size.X / tileSize) - 1);
            dividerY = (Size.Y % tileSize) / ((float)Math.Floor(Size.Y / tileSize) - 1);

            // add the containers via for
            int count = 0;
            for (float y = 0; y + sideLength <= Size.Y && count < maxSlots; y += (sideLength + dividerY))
                for (float x = 0; x + sideLength <= Size.X && count < maxSlots; x += (sideLength + dividerX))
                {
                    DragableContainer tmpContainer = new DragableContainer(new Vector2(sideLength, sideLength));
                    tmpContainer.Location = new Vector2(x, y);
                    tmpContainer.HoverRelease += tmpContainer_HoverRelease;
                    tmpContainer.Pressed += tmpContainer_Pressed;
                    tmpContainer.parent = this;
                    Add(tmpContainer);
                    containers.Add(tmpContainer);
                    count++;
                }
            
        }

        void tmpContainer_Pressed(object sender, EventArgs e)
        {
            DragableContainer clickedOn = sender as DragableContainer;

            if (currentDragableControl == null && clickedOn.ControlContained != null)
            {
                currentDragableControl = clickedOn.ControlContained;
                clickedOn.Clear();
                mouseOrigin = new Vector2(this.CurrentFrameMouseState.Position.X - currentDragableControl.Location.X, this.CurrentFrameMouseState.Position.Y - currentDragableControl.Location.Y);
            }
        }

        void tmpContainer_HoverRelease(object sender, EventArgs e)
        {
            Console.WriteLine("hover relase");
            if (currentDragableControl != null)
            {
                DragableContainer hoveringOver = sender as DragableContainer;

                DragableControl lastControl = hoveringOver.SetDragableControl(currentDragableControl);
                if (lastControl != null)
                    Console.WriteLine("replace stuff");
            }
        }
        
        /// <summary>
        /// Adds a control to the matrix. Will be added to the first empty container.
        /// </summary>
        /// <param name="control">The control to add.</param>
        public void AddToMatrix(Item.Item item)
        {
            // find first open container and set.
            foreach(DragableContainer container in containers)
            {
                if(container.ControlContained == null)
                {
                    DragableControl tmpDragable = new DragableControl(item, sideLength);
                    //tmpDragable.Pressed += tmpDragable_Pressed;
                    //tmpDragable.Hover += tmpDragable_Hover;
                    container.SetDragableControl(tmpDragable);
                    break;
                }
            }

            // TO-DO: prompt to drop something.
        }
        /*
        void tmpDragable_Hover(object sender, EventArgs e)
        {
            // We only care about what object we're hovering over if we have an object in our hand
            if (currentDragableControl != null)
            {
                DragableControl hoveringOver = sender as DragableControl;

                if (hoveringOver != null && hoveringOver != currentDragableControl)
                    hoveringDragableControl = hoveringOver;
            }
        }*/
        /*
        void tmpDragable_Pressed(object sender, EventArgs e)
        {
            DragableControl clickedOn = sender as DragableControl;

            if (currentDragableControl == null && clickedOn != null)
            {
                currentDragableControl = clickedOn;
                mouseOrigin = new Vector2(this.CurrentFrameMouseState.Position.X - currentDragableControl.Location.X, this.CurrentFrameMouseState.Position.Y - currentDragableControl.Location.Y);
            }
        }
        */
        public override void Update(GameTime gameTime)
        {
            
            if (currentDragableControl != null)
            {
                currentDragableControl.Location = new Vector2(this.CurrentFrameMouseState.Position.X - mouseOrigin.X, this.CurrentFrameMouseState.Position.Y - mouseOrigin.Y);
                if (this.CurrentFrameMouseState.LeftButton == ButtonState.Released)
                {
                    // if hovering over no slot, return the control back to original location. (COULD BE A BLANK SPACE (?))
                    if (hoveringDragableControl == null)
                    {
                        //currentDragableControl.Location = 
                        currentDragableControl = null;
                    }
                    else
                    {
                        // else swap spaces
                        // changes spaces in dic AND matrix.
                        /*
                        Vector2 tmpLoc = controlDictionary[currentDragableControl];
                        currentDragableControl.Location = controlDictionary[hoveringDragableControl];
                        hoveringDragableControl.Location = tmpLoc;

                        controlDictionary[currentDragableControl] = currentDragableControl.Location;
                        controlDictionary[hoveringDragableControl] = hoveringDragableControl.Location;
                        */
                        currentDragableControl = null;
                    }

                }
            }

            // updated every frame so this is fine.
            hoveringDragableControl = null;
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Border, this.GlobalLocation(), Color.White);

            base.Draw(spriteBatch, gameTime);

            // hack for overlap drawing.
            if (currentDragableControl != null)
                currentDragableControl.Draw(spriteBatch, gameTime);
        }
    }
}
