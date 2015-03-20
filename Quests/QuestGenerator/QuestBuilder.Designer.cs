namespace TheChicagoProject.Quests.QuestGenerator
{
    partial class QuestBuilder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextUpdate = new System.Windows.Forms.RichTextBox();
            this.treeQuests = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newStorylineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.livingEntityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Description = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxReward = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPoints = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxStartX = new System.Windows.Forms.TextBox();
            this.textBoxStartY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.richTextCompleted = new System.Windows.Forms.RichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.treeEntities = new System.Windows.Forms.TreeView();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextUpdate
            // 
            this.richTextUpdate.AcceptsTab = true;
            this.richTextUpdate.Location = new System.Drawing.Point(176, 302);
            this.richTextUpdate.Name = "richTextUpdate";
            this.richTextUpdate.Size = new System.Drawing.Size(898, 179);
            this.richTextUpdate.TabIndex = 0;
            this.richTextUpdate.Text = "";
            this.richTextUpdate.TextChanged += new System.EventHandler(this.richTextUpdate_TextChanged);
            // 
            // treeQuests
            // 
            this.treeQuests.Location = new System.Drawing.Point(12, 368);
            this.treeQuests.Name = "treeQuests";
            this.treeQuests.Size = new System.Drawing.Size(158, 390);
            this.treeQuests.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1147, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.newStorylineToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.optionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.newToolStripMenuItem.Text = "&New Quest";
            // 
            // newStorylineToolStripMenuItem
            // 
            this.newStorylineToolStripMenuItem.Name = "newStorylineToolStripMenuItem";
            this.newStorylineToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.newStorylineToolStripMenuItem.Text = "New Story&line";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(172, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.questItemToolStripMenuItem,
            this.livingEntityToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(132, 24);
            this.addToolStripMenuItem.Text = "&Add";
            // 
            // questItemToolStripMenuItem
            // 
            this.questItemToolStripMenuItem.Name = "questItemToolStripMenuItem";
            this.questItemToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.questItemToolStripMenuItem.Text = "Quest &Item";
            // 
            // livingEntityToolStripMenuItem
            // 
            this.livingEntityToolStripMenuItem.Name = "livingEntityToolStripMenuItem";
            this.livingEntityToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.livingEntityToolStripMenuItem.Text = "Li&ving Entity";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(132, 24);
            this.removeToolStripMenuItem.Text = "&Remove";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(176, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(252, 28);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(194, 30);
            this.textBoxName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(176, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Description:";
            // 
            // Description
            // 
            this.Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Description.Location = new System.Drawing.Point(297, 78);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(777, 30);
            this.Description.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(176, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Objective(s):";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(176, 155);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(898, 107);
            this.textBox1.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(452, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 25);
            this.label5.TabIndex = 3;
            this.label5.Text = "Reward: $";
            // 
            // textBoxReward
            // 
            this.textBoxReward.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxReward.Location = new System.Drawing.Point(548, 25);
            this.textBoxReward.Name = "textBoxReward";
            this.textBoxReward.Size = new System.Drawing.Size(80, 30);
            this.textBoxReward.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(634, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 25);
            this.label6.TabIndex = 3;
            this.label6.Text = "Quest Points:";
            // 
            // textBoxPoints
            // 
            this.textBoxPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPoints.Location = new System.Drawing.Point(764, 25);
            this.textBoxPoints.Name = "textBoxPoints";
            this.textBoxPoints.Size = new System.Drawing.Size(80, 30);
            this.textBoxPoints.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(850, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(166, 25);
            this.label7.TabIndex = 3;
            this.label7.Text = "Start: X               Y";
            // 
            // textBoxStartX
            // 
            this.textBoxStartX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStartX.Location = new System.Drawing.Point(924, 25);
            this.textBoxStartX.Name = "textBoxStartX";
            this.textBoxStartX.Size = new System.Drawing.Size(63, 30);
            this.textBoxStartX.TabIndex = 4;
            // 
            // textBoxStartY
            // 
            this.textBoxStartY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStartY.Location = new System.Drawing.Point(1011, 25);
            this.textBoxStartY.Name = "textBoxStartY";
            this.textBoxStartY.Size = new System.Drawing.Size(63, 30);
            this.textBoxStartY.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(176, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(268, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "public override void Update(){";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(178, 484);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 25);
            this.label8.TabIndex = 3;
            this.label8.Text = "}";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(176, 518);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(300, 25);
            this.label9.TabIndex = 3;
            this.label9.Text = "public override void Completed(){";
            // 
            // richTextCompleted
            // 
            this.richTextCompleted.AcceptsTab = true;
            this.richTextCompleted.Location = new System.Drawing.Point(176, 547);
            this.richTextCompleted.Name = "richTextCompleted";
            this.richTextCompleted.Size = new System.Drawing.Size(898, 179);
            this.richTextCompleted.TabIndex = 0;
            this.richTextCompleted.Text = "";
            this.richTextCompleted.TextChanged += new System.EventHandler(this.richTextUpdate_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(176, 729);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 25);
            this.label10.TabIndex = 3;
            this.label10.Text = "}";
            // 
            // treeEntities
            // 
            this.treeEntities.Location = new System.Drawing.Point(12, 31);
            this.treeEntities.Name = "treeEntities";
            this.treeEntities.Size = new System.Drawing.Size(158, 331);
            this.treeEntities.TabIndex = 1;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "OpenQuest.cs";
            this.openFileDialog.Filter = "C# files|*.cs|All Files|*.*";
            this.openFileDialog.InitialDirectory = "\\Quests";
            this.openFileDialog.Title = "Open Quest";
            // 
            // QuestBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 767);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Description);
            this.Controls.Add(this.textBoxStartY);
            this.Controls.Add(this.textBoxStartX);
            this.Controls.Add(this.textBoxPoints);
            this.Controls.Add(this.textBoxReward);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeEntities);
            this.Controls.Add(this.treeQuests);
            this.Controls.Add(this.richTextCompleted);
            this.Controls.Add(this.richTextUpdate);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "QuestBuilder";
            this.Text = "QuestBuilder";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextUpdate;
        private System.Windows.Forms.TreeView treeQuests;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newStorylineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem questItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem livingEntityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Description;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxReward;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxPoints;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxStartX;
        private System.Windows.Forms.TextBox textBoxStartY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox richTextCompleted;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TreeView treeEntities;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}