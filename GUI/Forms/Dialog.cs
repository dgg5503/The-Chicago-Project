using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheChicagoProject.GUI.Forms
{
    public abstract class Dialog : Control
    {
        protected bool eventsCompleted;

        /// <summary>
        /// Gets whether or not the events for this dialog have completed.
        /// </summary>
        public bool EventsCompleted { get { return eventsCompleted; } }
    }
}
