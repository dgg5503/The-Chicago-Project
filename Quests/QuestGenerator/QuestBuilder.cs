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

    public struct LivingEntityData
    {
        public string name;
        public int x;
        public int y;
        public string sprite;
        public int health;
        public string ai;

        public LivingEntityData(string name, int x, int y, string sprite, int health, string ai)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.sprite = sprite;
            this.health = health;
            this.ai = ai;
        }
    }

    public struct QuestItemData
    {
        public string name;
        public string sprite;

        public QuestItemData(string name, string sprite)
        {
            this.name = name;
            this.sprite = sprite;
        }
    }

    public partial class QuestBuilder : Form
    {
        
        string QuestName;
        string description;
        string objective;
        int x, y;
        int reward;
        int cashreward;
        string condition;

        //new variables
        string enemyToKill;
        string itemToFind;
        string itemToDeliver;
        string recipient;

        List<LivingEntityData> livingEntities;
        List<QuestItemData> items;


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
            this.Hide();
        }

        private void richTextUpdate_TextChanged(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //get the quest
                string path = openFileDialog.FileName;
                Quest openQuest = SaveManager.ParseQuest(path);

                //populate the form
                textBoxName.Text = openQuest.Name;
                textBoxDescription.Text = openQuest.Description;
                textBoxObjective.Text = openQuest.Objective;
                nudCash.Value = openQuest.CashReward;
                nudQPoints.Value = openQuest.Reward;
                nudStartX.Value = (decimal)openQuest.StartPoint.X;
                nudStartY.Value = (decimal)openQuest.StartPoint.Y;
                cmbConditions.Text = Enum.GetName(typeof(WinCondition), openQuest.WinCondition);

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

            livingEntities = new List<LivingEntityData>();
            items = new List<QuestItemData>();
             

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

            //store this information somewhere
            livingEntities.Add(new LivingEntityData(name, x, y, sprite, health, ai));

            if (chkRecipient.Checked)
                recipient = name;
            if (chkTarget.Checked)
                enemyToKill = name;

            //clear the fields
            txtLivingEntityName.Text = "";
            nudLivingEntityStartX.Value = 0;
            nudLivingEntityStartY.Value = 0;
            nudLivingEntityHealth.Value = 0;
            cmbLivingEntityAI.Text = "";
            chkTarget.Checked = false;
            chkRecipient.Checked = false;

        }

        private void butCreatItem_Click(object sender, EventArgs e)
        {
            //get the data
            string name = txtItemName.Text;
            string sprite = cmbItemSprite.Text;

            //store the data
            items.Add(new QuestItemData(name, sprite));

            if (chkGoalItem.Checked)
                itemToFind = name;
            if (chkDelivery.Checked)
                itemToDeliver = name;


            //clear the fields
            txtItemName.Text = "";
            chkDelivery.Checked = false;
            chkGoalItem.Checked = false;
        }

        private void chkGoalItem_CheckedChanged(object sender, EventArgs e)
        {
            chkDelivery.Enabled = !chkGoalItem.Checked;
        }

        private void chkDelivery_CheckedChanged(object sender, EventArgs e)
        {
            chkGoalItem.Enabled = chkDelivery.Checked;
        }

        private void chkTarget_CheckedChanged(object sender, EventArgs e)
        {
            chkRecipient.Enabled = !chkTarget.Checked;
        }

        private void chkRecipient_CheckedChanged(object sender, EventArgs e)
        {
            chkTarget.Enabled = !chkRecipient.Checked;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter output = null;

            try
            {
                //get information
                QuestName = textBoxName.Text;
                QuestName.Replace('"', '\'');
                objective = textBoxObjective.Text;
                objective.Replace('"','\'');
                description = textBoxDescription.Text;
                description.Replace('"', '\'');
                x = (int)nudStartX.Value;
                y = (int)nudStartY.Value;
                cashreward = (int)nudCash.Value;
                reward = (int)nudQPoints.Value;
                condition = cmbConditions.Text;

                //write the information
                output = new StreamWriter(QuestName + ".quest");

                output.WriteLine("Type:\"Quest\"");
                output.WriteLine("Name:\"{0}\"", QuestName);
                output.WriteLine("Objective:\"{0}\"", objective);
                output.WriteLine("Description:\"{0}\"", description);
                output.WriteLine("Start:\n\tX:{0}\n\tY:{1}", x, y);
                output.WriteLine("Reward:\n\tCash:{0}\n\tQ-Points:{1}\n",cashreward, reward);

                //write the living entitities
                foreach(LivingEntityData entityData in livingEntities)
                {
                    output.WriteLine("Living Entity:");
                    output.WriteLine("\tID:" + entityData.name);
                    output.WriteLine("\tStart:\n\t\tX:{0}\n\t\tY:{1}", entityData.x, entityData.y);
                    output.WriteLine("\tTexture:\"{0}\"", entityData.sprite);
                    output.WriteLine("\tHealth:" + entityData.health);
                    output.WriteLine("\tAI:" + entityData.ai);
                    output.WriteLine("End Living Entity\n");
                }

                //write the items
                foreach(QuestItemData itemData in items)
                {
                    output.WriteLine("Item:");
                    output.WriteLine("\tID:" + itemData.name);
                    output.WriteLine("\tTexture:\"{0}\"", itemData.sprite);
                    output.WriteLine("End Item\n");
                }

                //write the condition
                output.WriteLine("Condition:" + condition);
                switch(condition)
                {
                    case "EnemyDies":
                        output.WriteLine("Target:" + enemyToKill);
                        break;
                    case "ObtainItem":
                        output.WriteLine("Target:" + itemToFind);
                        break;
                    case "DeliverItem":
                        output.WriteLine("Target:" + itemToDeliver);
                        output.WriteLine("Target:" + enemyToKill);
                        break;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error saving file:\n" + ex.Message);
            }
            finally
            {
                if (output != null)
                    output.Close();
            }
        }

        private void cmbLivingEntitySprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            picEntity.BackgroundImage = System.Drawing.Image.FromFile("Content\\Sprites\\" + cmbLivingEntitySprite.Text + ".png");
        }

        private void cmbItemSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            picItem.BackgroundImage = System.Drawing.Image.FromFile("Content\\Sprites\\" + cmbItemSprite.Text + ".png");
        }

        private void txtLivingEntityName_TextChanged(object sender, EventArgs e)
        {
            butCreateLivingEntity.Enabled = txtLivingEntityName.Text != "";
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            butCreatItem.Enabled = txtItemName.Text != "";
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear all of the fields
            textBoxName.Text = "";
            textBoxDescription.Text = "";
            textBoxObjective.Text = "";
            cmbConditions.Text = "None";
            nudCash.Value = 0;
            nudQPoints.Value = 0;
            nudStartX.Value = 0;
            nudStartY.Value = 0;
            txtLivingEntityName.Text = "";
            nudLivingEntityStartX.Value = 0;
            nudLivingEntityStartY.Value = 0;
            nudLivingEntityHealth.Value = 0;
            cmbLivingEntityAI.Text = "";
            chkTarget.Checked = false;
            chkRecipient.Checked = false;
            txtItemName.Text = "";
            chkDelivery.Checked = false;
            chkGoalItem.Checked = false;
        }

        private void QuestBuilder_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }



    }
}
