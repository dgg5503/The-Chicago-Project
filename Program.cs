#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace TheChicagoProject
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Game1.Instance)
                Game1.Instance.Run();
        }
    }
#endif
}
