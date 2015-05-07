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
    /// <summary>
    /// A place holder for a dragable control. These can be placed anywhere but must be assigned to a specific dictionary!
    /// </summary>
    class DragableContainer : Control
    {
        // Fields
        // Name of control
        private string name;
        private static HashSet<string> namesInUse = new HashSet<string>(); // every control can only have 1 unique name.

        // ID information
        private int myID;
        private static int id = 0;

        // Dragable control to hold in this container
        private DragableControl controlInContainer;

        // Properties
        /// <summary>
        /// Sets or gets the name of this container (no two containers can have the same name!)
        /// </summary>
        public string Name { 
            get 
            { 
                return name; 
            }

            set
            {
                if (namesInUse.Contains(value))
                    throw new Exception("WARNING: CONFLICTING CONTAINER NAMES!");

                if (!string.IsNullOrWhiteSpace(value))
                    namesInUse.Add(value);

                name = value;
            }
        }
        /// <summary>
        /// Returns the ID of this dragable container.
        /// </summary>
        public int ID { get { return myID; } }
        /// <summary>
        /// Gets the control contained in this dragbale container.
        /// </summary>
        public DragableControl ControlContained
        { 
            get 
            { 
                return controlInContainer; 
            } 
        }

        public DragableContainer(Vector2 size, DragableControl controlInContainer = null, string name = null)
        {
            this.Size = size;

            if (namesInUse.Contains(name))
                throw new Exception("WARNING: CONFLICTING CONTAINER NAMES!");

            if(!string.IsNullOrWhiteSpace(name))
                namesInUse.Add(name);

            this.name = name;

            this.controlInContainer = controlInContainer;

            SetDragableControl(controlInContainer);

            myID = id++;
        }

        /// <summary>
        /// Sets the dragable control to be contained.
        /// </summary>
        /// <param name="controlToSet">The new control to be contained in this container.</param>
        /// <returns>The old control contained.</returns>
        public DragableControl SetDragableControl(DragableControl controlToSet)
        {
            //Console.WriteLine("OLD: {1} NEW: {0}", controlToSet.Item.name, this.controlInContainer.Item.name);
            Console.WriteLine("KK {0}", this.ID);
            // if given nothing, dont set anything.
            if (controlToSet == null)
                return null;

            // should this even EVER happen?
           
            if (controlToSet == controlInContainer)
            {
                Console.WriteLine("Dude same");
                controlToSet.Location = Vector2.Zero;
                return null;
                //return controlToSet;
            }

            DragableControl controlToReplace = controlInContainer;
            
            // Clear this container.
            // Clear the continar this came from.

            // this assumes the parent is the dragable container!
            if (controlToSet.parent != null)
                controlToSet.parent.Clear();
            Clear();
            
            // All controls should be centered in their container...
            controlInContainer = controlToSet;
            controlInContainer.Location = Vector2.Zero;
            controlInContainer.Alignment = ControlAlignment.Center;
            controlInContainer.parent = this;
            Add(controlInContainer);

            if(controlToReplace != null)
                controlToReplace.parent = null;

            return controlToReplace;

            // parent for controlToReplace is still this.
            // controlToReplace is no longer in this container.
        }

        public override void Clear()
        {
            controlInContainer = null;
            base.Clear();
        }


        


    }
}
