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
    //Sean Levorse
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

        //new variables
        string enemyToKill;
        string itemToFind;
        string itemToDeliver;
        string recipient;


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

        private void nudStartY_ValueChanged(object sender, EventArgs e)
        {

        }

        private void QuestBuilder_Load(object sender, EventArgs e)
        {
            cmbConditions.DataSource = Enum.GetNames(typeof(Quests.WinCondition));
            List<string> keys = new List<string>();
            foreach (string key in GUI.Sprites.spritesDictionary.Keys)
                keys.Add(key);
            cmbLivingEntitySprite.DataSource = keys;
            cmbItemSprite.DataSource = keys;

        }

        private void butCreateLivingEntity_Click(object sender, EventArgs e)
        {
            //get each of the fields to add to the living entity
            string name = txtLivingEntityName.Text;
            int x = (int)nudLivingEntityStartX.Value;
            int y = (int)nudLivingEntityStartY.Value;

            string sprite = cmbLivingEntitySprite.Text;
            int health = (int)nudLivingEntityHealth.Value;
            string ai = cmbLivingEntityAI.Text;
            if (ai == "None")
                ai = "";

            if (chkRecipient.Checked)
                recipient = name;
            if (chkTarget.Checked)
                enemyToKill = name;

        }

    }
}
