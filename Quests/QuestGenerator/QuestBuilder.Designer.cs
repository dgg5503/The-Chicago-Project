namespace TheChicagoProject.Quests.QuestGenerator
{
    partial class QuestBuilder
    {
        //Sean Levorse
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.treeEntities = new System.Windows.Forms.TreeView();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxObjective = new System.Windows.Forms.TextBox();
            this.nudCash = new System.Windows.Forms.NumericUpDown();
            this.nudQPoints = new System.Windows.Forms.NumericUpDown();
            this.nudStartX = new System.Windows.Forms.NumericUpDown();
            this.nudStartY = new System.Windows.Forms.NumericUpDown();
            this.cmbConditions = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.butCreateLivingEntity = new System.Windows.Forms.Button();
            this.panLivingEntitiy = new System.Windows.Forms.Panel();
            this.chkRecipient = new System.Windows.Forms.CheckBox();
            this.chkTarget = new System.Windows.Forms.CheckBox();
            this.cmbLivingEntityAI = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.nudLivingEntityHealth = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbLivingEntitySprite = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.nudLivingEntityStartY = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nudLivingEntityStartX = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLivingEntityName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panItem = new System.Windows.Forms.Panel();
            this.chkDelivery = new System.Windows.Forms.CheckBox();
            this.chkGoalItem = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.cmbItemSprite = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.butCreatItem = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartY)).BeginInit();
            this.panLivingEntitiy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLivingEntityHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLivingEntityStartY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLivingEntityStartX)).BeginInit();
            this.panItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // treeQuests
            // 
            this.treeQuests.Location = new System.Drawing.Point(12, 29);
            this.treeQuests.Name = "treeQuests";
            this.treeQuests.Size = new System.Drawing.Size(158, 333);
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
            this.menuStrip1.Size = new System.Drawing.Size(1193, 28);
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
            this.newToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.newToolStripMenuItem.Text = "&New Quest";
            // 
            // newStorylineToolStripMenuItem
            // 
            this.newStorylineToolStripMenuItem.Name = "newStorylineToolStripMenuItem";
            this.newStorylineToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.newStorylineToolStripMenuItem.Text = "New Story&line";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(167, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
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
            this.label1.Location = new System.Drawing.Point(176, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(252, 29);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(176, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Objective(s):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(452, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 25);
            this.label5.TabIndex = 3;
            this.label5.Text = "Reward: $";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(693, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 25);
            this.label6.TabIndex = 3;
            this.label6.Text = "Quest Points:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(913, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 25);
            this.label7.TabIndex = 3;
            this.label7.Text = "Start: X                   Y";
            // 
            // treeEntities
            // 
            this.treeEntities.Location = new System.Drawing.Point(12, 368);
            this.treeEntities.Name = "treeEntities";
            this.treeEntities.Size = new System.Drawing.Size(158, 497);
            this.treeEntities.TabIndex = 1;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "OpenQuest.cs";
            this.openFileDialog.Filter = "C# files|*.cs|All Files|*.*";
            this.openFileDialog.InitialDirectory = "\\Quests";
            this.openFileDialog.Title = "Open Quest";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDescription.Location = new System.Drawing.Point(176, 106);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(1005, 107);
            this.textBoxDescription.TabIndex = 6;
            // 
            // textBoxObjective
            // 
            this.textBoxObjective.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxObjective.Location = new System.Drawing.Point(176, 255);
            this.textBoxObjective.Multiline = true;
            this.textBoxObjective.Name = "textBoxObjective";
            this.textBoxObjective.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxObjective.Size = new System.Drawing.Size(1005, 107);
            this.textBoxObjective.TabIndex = 6;
            // 
            // nudCash
            // 
            this.nudCash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudCash.Location = new System.Drawing.Point(558, 29);
            this.nudCash.Name = "nudCash";
            this.nudCash.Size = new System.Drawing.Size(106, 30);
            this.nudCash.TabIndex = 7;
            // 
            // nudQPoints
            // 
            this.nudQPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudQPoints.Location = new System.Drawing.Point(819, 29);
            this.nudQPoints.Name = "nudQPoints";
            this.nudQPoints.Size = new System.Drawing.Size(84, 30);
            this.nudQPoints.TabIndex = 8;
            // 
            // nudStartX
            // 
            this.nudStartX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudStartX.Location = new System.Drawing.Point(994, 29);
            this.nudStartX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudStartX.Name = "nudStartX";
            this.nudStartX.Size = new System.Drawing.Size(80, 30);
            this.nudStartX.TabIndex = 9;
            // 
            // nudStartY
            // 
            this.nudStartY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudStartY.Location = new System.Drawing.Point(1105, 29);
            this.nudStartY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudStartY.Name = "nudStartY";
            this.nudStartY.Size = new System.Drawing.Size(76, 30);
            this.nudStartY.TabIndex = 9;
            this.nudStartY.ValueChanged += new System.EventHandler(this.nudStartY_ValueChanged);
            // 
            // cmbConditions
            // 
            this.cmbConditions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbConditions.FormattingEnabled = true;
            this.cmbConditions.Location = new System.Drawing.Point(183, 396);
            this.cmbConditions.Name = "cmbConditions";
            this.cmbConditions.Size = new System.Drawing.Size(222, 33);
            this.cmbConditions.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(178, 368);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Condition for completion:";
            // 
            // butCreateLivingEntity
            // 
            this.butCreateLivingEntity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butCreateLivingEntity.Location = new System.Drawing.Point(183, 435);
            this.butCreateLivingEntity.Name = "butCreateLivingEntity";
            this.butCreateLivingEntity.Size = new System.Drawing.Size(263, 44);
            this.butCreateLivingEntity.TabIndex = 11;
            this.butCreateLivingEntity.Text = "Create Living Entity";
            this.butCreateLivingEntity.UseVisualStyleBackColor = true;
            this.butCreateLivingEntity.Click += new System.EventHandler(this.butCreateLivingEntity_Click);
            // 
            // panLivingEntitiy
            // 
            this.panLivingEntitiy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panLivingEntitiy.Controls.Add(this.chkRecipient);
            this.panLivingEntitiy.Controls.Add(this.chkTarget);
            this.panLivingEntitiy.Controls.Add(this.cmbLivingEntityAI);
            this.panLivingEntitiy.Controls.Add(this.label13);
            this.panLivingEntitiy.Controls.Add(this.nudLivingEntityHealth);
            this.panLivingEntitiy.Controls.Add(this.label12);
            this.panLivingEntitiy.Controls.Add(this.pictureBox1);
            this.panLivingEntitiy.Controls.Add(this.cmbLivingEntitySprite);
            this.panLivingEntitiy.Controls.Add(this.label11);
            this.panLivingEntitiy.Controls.Add(this.nudLivingEntityStartY);
            this.panLivingEntitiy.Controls.Add(this.label10);
            this.panLivingEntitiy.Controls.Add(this.nudLivingEntityStartX);
            this.panLivingEntitiy.Controls.Add(this.label9);
            this.panLivingEntitiy.Controls.Add(this.txtLivingEntityName);
            this.panLivingEntitiy.Controls.Add(this.label8);
            this.panLivingEntitiy.Location = new System.Drawing.Point(183, 483);
            this.panLivingEntitiy.Name = "panLivingEntitiy";
            this.panLivingEntitiy.Size = new System.Drawing.Size(998, 150);
            this.panLivingEntitiy.TabIndex = 12;
            // 
            // chkRecipient
            // 
            this.chkRecipient.AutoSize = true;
            this.chkRecipient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRecipient.Location = new System.Drawing.Point(21, 117);
            this.chkRecipient.Name = "chkRecipient";
            this.chkRecipient.Size = new System.Drawing.Size(446, 28);
            this.chkRecipient.TabIndex = 14;
            this.chkRecipient.Text = "This is the living entity that will recieve the delivery";
            this.chkRecipient.UseVisualStyleBackColor = true;
            this.chkRecipient.CheckedChanged += new System.EventHandler(this.chkRecipient_CheckedChanged);
            // 
            // chkTarget
            // 
            this.chkTarget.AutoSize = true;
            this.chkTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTarget.Location = new System.Drawing.Point(21, 84);
            this.chkTarget.Name = "chkTarget";
            this.chkTarget.Size = new System.Drawing.Size(334, 28);
            this.chkTarget.TabIndex = 13;
            this.chkTarget.Text = "This is the enemy that must be killed";
            this.chkTarget.UseVisualStyleBackColor = true;
            this.chkTarget.CheckedChanged += new System.EventHandler(this.chkTarget_CheckedChanged);
            // 
            // cmbLivingEntityAI
            // 
            this.cmbLivingEntityAI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLivingEntityAI.FormattingEnabled = true;
            this.cmbLivingEntityAI.Items.AddRange(new object[] {
            "None",
            "Low",
            "Mid",
            "High"});
            this.cmbLivingEntityAI.Location = new System.Drawing.Point(395, 48);
            this.cmbLivingEntityAI.Name = "cmbLivingEntityAI";
            this.cmbLivingEntityAI.Size = new System.Drawing.Size(121, 30);
            this.cmbLivingEntityAI.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(280, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 24);
            this.label13.TabIndex = 11;
            this.label13.Text = "AI(Optional)";
            // 
            // nudLivingEntityHealth
            // 
            this.nudLivingEntityHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudLivingEntityHealth.Location = new System.Drawing.Point(154, 50);
            this.nudLivingEntityHealth.Name = "nudLivingEntityHealth";
            this.nudLivingEntityHealth.Size = new System.Drawing.Size(120, 28);
            this.nudLivingEntityHealth.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(17, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(131, 24);
            this.label12.TabIndex = 9;
            this.label12.Text = "Starting Health";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(856, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // cmbLivingEntitySprite
            // 
            this.cmbLivingEntitySprite.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLivingEntitySprite.FormattingEnabled = true;
            this.cmbLivingEntitySprite.Location = new System.Drawing.Point(660, 14);
            this.cmbLivingEntitySprite.Name = "cmbLivingEntitySprite";
            this.cmbLivingEntitySprite.Size = new System.Drawing.Size(190, 30);
            this.cmbLivingEntitySprite.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(596, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 24);
            this.label11.TabIndex = 6;
            this.label11.Text = "Sprite";
            // 
            // nudLivingEntityStartY
            // 
            this.nudLivingEntityStartY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudLivingEntityStartY.Location = new System.Drawing.Point(478, 12);
            this.nudLivingEntityStartY.Name = "nudLivingEntityStartY";
            this.nudLivingEntityStartY.Size = new System.Drawing.Size(97, 28);
            this.nudLivingEntityStartY.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(449, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 24);
            this.label10.TabIndex = 4;
            this.label10.Text = "Y";
            // 
            // nudLivingEntityStartX
            // 
            this.nudLivingEntityStartX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudLivingEntityStartX.Location = new System.Drawing.Point(346, 13);
            this.nudLivingEntityStartX.Name = "nudLivingEntityStartX";
            this.nudLivingEntityStartX.Size = new System.Drawing.Size(97, 28);
            this.nudLivingEntityStartX.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(270, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 24);
            this.label9.TabIndex = 2;
            this.label9.Text = "Start: X";
            // 
            // txtLivingEntityName
            // 
            this.txtLivingEntityName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLivingEntityName.Location = new System.Drawing.Point(85, 11);
            this.txtLivingEntityName.Name = "txtLivingEntityName";
            this.txtLivingEntityName.Size = new System.Drawing.Size(178, 28);
            this.txtLivingEntityName.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(17, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 24);
            this.label8.TabIndex = 0;
            this.label8.Text = "Name";
            // 
            // panItem
            // 
            this.panItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panItem.Controls.Add(this.chkDelivery);
            this.panItem.Controls.Add(this.chkGoalItem);
            this.panItem.Controls.Add(this.pictureBox2);
            this.panItem.Controls.Add(this.cmbItemSprite);
            this.panItem.Controls.Add(this.label15);
            this.panItem.Controls.Add(this.txtItemName);
            this.panItem.Controls.Add(this.label14);
            this.panItem.Location = new System.Drawing.Point(184, 692);
            this.panItem.Name = "panItem";
            this.panItem.Size = new System.Drawing.Size(985, 173);
            this.panItem.TabIndex = 13;
            // 
            // chkDelivery
            // 
            this.chkDelivery.AutoSize = true;
            this.chkDelivery.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDelivery.Location = new System.Drawing.Point(20, 92);
            this.chkDelivery.Name = "chkDelivery";
            this.chkDelivery.Size = new System.Drawing.Size(263, 28);
            this.chkDelivery.TabIndex = 6;
            this.chkDelivery.Text = "This item must be delivered";
            this.chkDelivery.UseVisualStyleBackColor = true;
            this.chkDelivery.CheckedChanged += new System.EventHandler(this.chkDelivery_CheckedChanged);
            // 
            // chkGoalItem
            // 
            this.chkGoalItem.AutoSize = true;
            this.chkGoalItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGoalItem.Location = new System.Drawing.Point(20, 57);
            this.chkGoalItem.Name = "chkGoalItem";
            this.chkGoalItem.Size = new System.Drawing.Size(418, 28);
            this.chkGoalItem.TabIndex = 5;
            this.chkGoalItem.Text = "This item must be found to complete the quest";
            this.chkGoalItem.UseVisualStyleBackColor = true;
            this.chkGoalItem.CheckedChanged += new System.EventHandler(this.chkGoalItem_CheckedChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Location = new System.Drawing.Point(454, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(128, 128);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // cmbItemSprite
            // 
            this.cmbItemSprite.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbItemSprite.FormattingEnabled = true;
            this.cmbItemSprite.Location = new System.Drawing.Point(269, 16);
            this.cmbItemSprite.Name = "cmbItemSprite";
            this.cmbItemSprite.Size = new System.Drawing.Size(175, 30);
            this.cmbItemSprite.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(206, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 24);
            this.label15.TabIndex = 2;
            this.label15.Text = "Sprite";
            // 
            // txtItemName
            // 
            this.txtItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemName.Location = new System.Drawing.Point(86, 13);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(100, 28);
            this.txtItemName.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(18, 11);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 24);
            this.label14.TabIndex = 0;
            this.label14.Text = "Name";
            // 
            // butCreatItem
            // 
            this.butCreatItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butCreatItem.Location = new System.Drawing.Point(185, 642);
            this.butCreatItem.Name = "butCreatItem";
            this.butCreatItem.Size = new System.Drawing.Size(263, 44);
            this.butCreatItem.TabIndex = 11;
            this.butCreatItem.Text = "Create Quest Item";
            this.butCreatItem.UseVisualStyleBackColor = true;
            // 
            // QuestBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 877);
            this.Controls.Add(this.panItem);
            this.Controls.Add(this.panLivingEntitiy);
            this.Controls.Add(this.butCreatItem);
            this.Controls.Add(this.butCreateLivingEntity);
            this.Controls.Add(this.cmbConditions);
            this.Controls.Add(this.nudStartY);
            this.Controls.Add(this.nudStartX);
            this.Controls.Add(this.nudQPoints);
            this.Controls.Add(this.nudCash);
            this.Controls.Add(this.textBoxObjective);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeEntities);
            this.Controls.Add(this.treeQuests);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "QuestBuilder";
            this.Text = "QuestBuilder";
            this.Load += new System.EventHandler(this.QuestBuilder_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartY)).EndInit();
            this.panLivingEntitiy.ResumeLayout(false);
            this.panLivingEntitiy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLivingEntityHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLivingEntityStartY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLivingEntityStartX)).EndInit();
            this.panItem.ResumeLayout(false);
            this.panItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TreeView treeEntities;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxObjective;
        private System.Windows.Forms.NumericUpDown nudCash;
        private System.Windows.Forms.NumericUpDown nudQPoints;
        private System.Windows.Forms.NumericUpDown nudStartX;
        private System.Windows.Forms.NumericUpDown nudStartY;
        private System.Windows.Forms.ComboBox cmbConditions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butCreateLivingEntity;
        private System.Windows.Forms.Panel panLivingEntitiy;
        private System.Windows.Forms.ComboBox cmbLivingEntitySprite;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudLivingEntityStartY;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudLivingEntityStartX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLivingEntityName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbLivingEntityAI;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudLivingEntityHealth;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panItem;
        private System.Windows.Forms.Button butCreatItem;
        private System.Windows.Forms.ComboBox cmbItemSprite;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox chkTarget;
        private System.Windows.Forms.CheckBox chkGoalItem;
        private System.Windows.Forms.CheckBox chkRecipient;
        private System.Windows.Forms.CheckBox chkDelivery;
    }
}