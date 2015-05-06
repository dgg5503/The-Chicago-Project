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
        private Dictionary<DragableControl, Vector2> controlDictionary;

        // Mouse handling
        private Vector2 mouseOrigin;
        private DragableControl currentDragableControl;
        private DragableControl hoveringDragableControl;

        public DragableMatrixV2(Vector2 size)
        {
            this.Size = size;

            controlDictionary = new Dictionary<DragableControl, Vector2>();

            mouseOrigin = new Vector2(-1, -1);
            currentDragableControl = null;
        }

        public void AddContainerToMatrix(Container container)
        {

        }

        
        /// <summary>
        /// Adds a control to the matrix. Location will be relative to this matrix's location.
        /// </summary>
        /// <param name="control">The control to add.</param>
        public void AddToMatrix(DragableControl control)
        {
            control.Pressed += control_Pressed;

            control.Hover += control_Hover;

            control.parent = this;

            controlDictionary.Add(control, control.Location);

            Add(control);
        }

        void control_Hover(object sender, EventArgs e)
        {
            // We only care about what object we're hovering over if we have an object in our hand
            if (currentDragableControl != null)
            {
                DragableControl hoveringOver = sender as DragableControl;

                if (hoveringOver != null && hoveringOver != currentDragableControl)
                    hoveringDragableControl = hoveringOver;
            }
        }


        private void control_Pressed(object sender, EventArgs e)
        {
            DragableControl clickedOn = sender as DragableControl;

            if (currentDragableControl == null && clickedOn != null)
            {
                currentDragableControl = clickedOn;
                mouseOrigin = new Vector2(this.CurrentFrameMouseState.Position.X - currentDragableControl.Location.X, this.CurrentFrameMouseState.Position.Y - currentDragableControl.Location.Y);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (controlDictionary != null && currentDragableControl != null)
            {
                currentDragableControl.Location = new Vector2(this.CurrentFrameMouseState.Position.X - mouseOrigin.X, this.CurrentFrameMouseState.Position.Y - mouseOrigin.Y);
                if (this.CurrentFrameMouseState.LeftButton == ButtonState.Released)
                {
                    // if hovering over no slot, return the control back to original location. (COULD BE A BLANK SPACE (?))
                    if (hoveringDragableControl == null)
                    {
                        currentDragableControl.Location = controlDictionary[currentDragableControl];
                        currentDragableControl = null;
                    }
                    else
                    {
                        // else swap spaces
                        // changes spaces in dic AND matrix.

                        Vector2 tmpLoc = controlDictionary[currentDragableControl];
                        currentDragableControl.Location = controlDictionary[hoveringDragableControl];
                        hoveringDragableControl.Location = tmpLoc;

                        controlDictionary[currentDragableControl] = currentDragableControl.Location;
                        controlDictionary[hoveringDragableControl] = hoveringDragableControl.Location;

                        currentDragableControl = null;
                    }

                }
            }

            // updated every frame so this is fine.
            hoveringDragableControl = null;

            base.Update(gameTime);
        }
    }
}
