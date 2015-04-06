using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.Xna.Framework;

namespace TheChicagoProject.Quests.QuestGenerator
{
    //Sean LEvorse
    public partial class QuestBuilder : Form
    {
        string code;
        Quest quest;
        string name;
        string description;
        string objective;
        Vector2 start;
        int reward;
        int cashreward;

        public QuestBuilder()
        {
            //initialize
            //quest = new Quest("", "", "", new Vector2(-1, -1), 0, 0);
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void richTextUpdate_TextChanged(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamReader questStream = null;
            int index = 0;
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                try
                {
                    if((questStream = new StreamReader(path)) != null)
                    {
                        code = questStream.ReadToEnd();
                        //parse the code
                        index = code.IndexOf("class");
                        name = code.Substring(index + 6, code.IndexOf(':', index) - 1 - index + 6);

                        //index = index

                        
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error Loading file: " + exception.Message);
                }
            }
        }

    }
}
