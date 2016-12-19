namespace MapCreator2D
{
    partial class ControlWindow
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

                _tile.Dispose();
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
            this.rdNone = new System.Windows.Forms.RadioButton();
            this.rdWall = new System.Windows.Forms.RadioButton();
            this.rdDoor = new System.Windows.Forms.RadioButton();
            this.rdGrid = new System.Windows.Forms.RadioButton();
            this.rdDestroyable = new System.Windows.Forms.RadioButton();
            this.rdKey = new System.Windows.Forms.RadioButton();
            this.rdPliers = new System.Windows.Forms.RadioButton();
            this.rdPickaxe = new System.Windows.Forms.RadioButton();
            this.rdLevelUp = new System.Windows.Forms.RadioButton();
            this.rdLevelDown = new System.Windows.Forms.RadioButton();
            this.lblPosition = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.rdSpawn = new System.Windows.Forms.RadioButton();
            this.btnNew = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.rdMessage = new System.Windows.Forms.RadioButton();
            this.gbMessage = new System.Windows.Forms.GroupBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSetMessage = new System.Windows.Forms.Button();
            this.gbID = new System.Windows.Forms.GroupBox();
            this.selectorID = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetID = new System.Windows.Forms.Button();
            this.rdInfo = new System.Windows.Forms.RadioButton();
            this.rdSwitch = new System.Windows.Forms.RadioButton();
            this.rdHalfBlock = new System.Windows.Forms.RadioButton();
            this.gbMessage.SuspendLayout();
            this.gbID.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectorID)).BeginInit();
            this.SuspendLayout();
            // 
            // rdNone
            // 
            this.rdNone.AutoSize = true;
            this.rdNone.Location = new System.Drawing.Point(12, 12);
            this.rdNone.Name = "rdNone";
            this.rdNone.Size = new System.Drawing.Size(76, 17);
            this.rdNone.TabIndex = 0;
            this.rdNone.TabStop = true;
            this.rdNone.Text = "Kein Block";
            this.rdNone.UseVisualStyleBackColor = true;
            this.rdNone.CheckedChanged += new System.EventHandler(this.CheckChange);
            // 
            // rdWall
            // 
            this.rdWall.AutoSize = true;
            this.rdWall.Location = new System.Drawing.Point(12, 35);
            this.rdWall.Name = "rdWall";
            this.rdWall.Size = new System.Drawing.Size(100, 17);
            this.rdWall.TabIndex = 1;
            this.rdWall.TabStop = true;
            this.rdWall.Text = "Einfacher Block";
            this.rdWall.UseVisualStyleBackColor = true;
            this.rdWall.CheckedChanged += new System.EventHandler(this.CheckChange);
            // 
            // rdDoor
            // 
            this.rdDoor.AutoSize = true;
            this.rdDoor.Location = new System.Drawing.Point(12, 104);
            this.rdDoor.Name = "rdDoor";
            this.rdDoor.Size = new System.Drawing.Size(41, 17);
            this.rdDoor.TabIndex = 2;
            this.rdDoor.TabStop = true;
            this.rdDoor.Text = "Tür";
            this.rdDoor.UseVisualStyleBackColor = true;
            this.rdDoor.CheckedChanged += new System.EventHandler(this.CheckChange);
            // 
            // rdGrid
            // 
            this.rdGrid.AutoSize = true;
            this.rdGrid.Location = new System.Drawing.Point(12, 81);
            this.rdGrid.Name = "rdGrid";
            this.rdGrid.Size = new System.Drawing.Size(50, 17);
            this.rdGrid.TabIndex = 3;
            this.rdGrid.TabStop = true;
            this.rdGrid.Text = "Gitter";
            this.rdGrid.UseVisualStyleBackColor = true;
            this.rdGrid.CheckedChanged += new System.EventHandler(this.CheckChange);
            // 
            // rdDestroyable
            // 
            this.rdDestroyable.AutoSize = true;
            this.rdDestroyable.Location = new System.Drawing.Point(12, 58);
            this.rdDestroyable.Name = "rdDestroyable";
            this.rdDestroyable.Size = new System.Drawing.Size(100, 17);
            this.rdDestroyable.TabIndex = 4;
            this.rdDestroyable.TabStop = true;
            this.rdDestroyable.Text = "Brüchiger Block";
            this.rdDestroyable.UseVisualStyleBackColor = true;
            this.rdDestroyable.CheckedChanged += new System.EventHandler(this.CheckChange);
            // 
            // rdKey
            // 
            this.rdKey.AutoSize = true;
            this.rdKey.Location = new System.Drawing.Point(143, 12);
            this.rdKey.Name = "rdKey";
            this.rdKey.Size = new System.Drawing.Size(70, 17);
            this.rdKey.TabIndex = 9;
            this.rdKey.TabStop = true;
            this.rdKey.Text = "Schlüssel";
            this.rdKey.UseVisualStyleBackColor = true;
            this.rdKey.CheckedChanged += new System.EventHandler(this.CheckChange);
            // 
            // rdPliers
            // 
            this.rdPliers.AutoSize = true;
            this.rdPliers.Location = new System.Drawing.Point(143, 35);
            this.rdPliers.Name = "rdPliers";
            this.rdPliers.Size = new System.Drawing.Size(56, 17);
            this.rdPliers.TabIndex = 8;
            this.rdPliers.TabStop = true;
            this.rdPliers.Text = "Zange";
            this.rdPliers.UseVisualStyleBackColor = true;
            this.rdPliers.CheckedChanged += new System.EventHandler(this.CheckChange);
            // 
            // rdPickaxe
            // 
            this.rdPickaxe.AutoSize = true;
            this.rdPickaxe.Location = new System.Drawing.Point(143, 58);
            this.rdPickaxe.Name = "rdPickaxe";
            this.rdPickaxe.Size = new System.Drawing.Size(78, 17);
            this.rdPickaxe.TabIndex = 7;
            this.rdPickaxe.TabStop = true;
            this.rdPickaxe.Text = "Spitzhacke";
            this.rdPickaxe.UseVisualStyleBackColor = true;
            this.rdPickaxe.CheckedChanged += new System.EventHandler(this.CheckChange);
            // 
            // rdLevelUp
            // 
            this.rdLevelUp.AutoSize = true;
            this.rdLevelUp.Location = new System.Drawing.Point(143, 104);
            this.rdLevelUp.Name = "rdLevelUp";
            this.rdLevelUp.Size = new System.Drawing.Size(103, 17);
            this.rdLevelUp.TabIndex = 6;
            this.rdLevelUp.TabStop = true;
            this.rdLevelUp.Text = "Level aufsteigen";
            this.rdLevelUp.UseVisualStyleBackColor = true;
            this.rdLevelUp.CheckedChanged += new System.EventHandler(this.CheckChange);
            // 
            // rdLevelDown
            // 
            this.rdLevelDown.AutoSize = true;
            this.rdLevelDown.Location = new System.Drawing.Point(143, 127);
            this.rdLevelDown.Name = "rdLevelDown";
            this.rdLevelDown.Size = new System.Drawing.Size(100, 17);
            this.rdLevelDown.TabIndex = 5;
            this.rdLevelDown.TabStop = true;
            this.rdLevelDown.Text = "Level absteigen";
            this.rdLevelDown.UseVisualStyleBackColor = true;
            this.rdLevelDown.CheckedChanged += new System.EventHandler(this.CheckChange);
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(216, 206);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(51, 13);
            this.lblPosition.TabIndex = 10;
            this.lblPosition.Text = "X: 0; Y: 0";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(145, 222);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(11, 222);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(128, 23);
            this.btnLoad.TabIndex = 12;
            this.btnLoad.Text = "Laden";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // rdSpawn
            // 
            this.rdSpawn.AutoSize = true;
            this.rdSpawn.Location = new System.Drawing.Point(12, 127);
            this.rdSpawn.Name = "rdSpawn";
            this.rdSpawn.Size = new System.Drawing.Size(85, 17);
            this.rdSpawn.TabIndex = 13;
            this.rdSpawn.TabStop = true;
            this.rdSpawn.Text = "Spawnpunkt";
            this.rdSpawn.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(145, 251);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(128, 23);
            this.btnNew.TabIndex = 14;
            this.btnNew.Text = "Neu";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(11, 206);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(46, 13);
            this.lblName.TabIndex = 15;
            this.lblName.Text = "Name: /";
            // 
            // rdMessage
            // 
            this.rdMessage.AutoSize = true;
            this.rdMessage.Location = new System.Drawing.Point(143, 81);
            this.rdMessage.Name = "rdMessage";
            this.rdMessage.Size = new System.Drawing.Size(49, 17);
            this.rdMessage.TabIndex = 16;
            this.rdMessage.TabStop = true;
            this.rdMessage.Text = "Notiz";
            this.rdMessage.UseVisualStyleBackColor = true;
            this.rdMessage.CheckedChanged += new System.EventHandler(this.CheckChange);
            // 
            // gbMessage
            // 
            this.gbMessage.Controls.Add(this.txtMessage);
            this.gbMessage.Controls.Add(this.btnSetMessage);
            this.gbMessage.Location = new System.Drawing.Point(11, 293);
            this.gbMessage.Name = "gbMessage";
            this.gbMessage.Size = new System.Drawing.Size(262, 78);
            this.gbMessage.TabIndex = 17;
            this.gbMessage.TabStop = false;
            this.gbMessage.Text = "Eigenschaften der Notiz";
            this.gbMessage.Visible = false;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(6, 19);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(250, 20);
            this.txtMessage.TabIndex = 1;
            // 
            // btnSetMessage
            // 
            this.btnSetMessage.Location = new System.Drawing.Point(181, 45);
            this.btnSetMessage.Name = "btnSetMessage";
            this.btnSetMessage.Size = new System.Drawing.Size(75, 23);
            this.btnSetMessage.TabIndex = 0;
            this.btnSetMessage.Text = "Speichern";
            this.btnSetMessage.UseVisualStyleBackColor = true;
            this.btnSetMessage.Click += new System.EventHandler(this.btnSetMessage_Click);
            // 
            // gbID
            // 
            this.gbID.Controls.Add(this.selectorID);
            this.gbID.Controls.Add(this.label1);
            this.gbID.Controls.Add(this.btnSetID);
            this.gbID.Location = new System.Drawing.Point(11, 377);
            this.gbID.Name = "gbID";
            this.gbID.Size = new System.Drawing.Size(262, 101);
            this.gbID.TabIndex = 18;
            this.gbID.TabStop = false;
            this.gbID.Text = "ID";
            this.gbID.Visible = false;
            // 
            // selectorID
            // 
            this.selectorID.Location = new System.Drawing.Point(6, 47);
            this.selectorID.Name = "selectorID";
            this.selectorID.Size = new System.Drawing.Size(250, 20);
            this.selectorID.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Eine Tür kann nur mit einem Schlüssel geöffnet\r\nwerden wenn dieser die gleiche ID" +
    " hat.";
            // 
            // btnSetID
            // 
            this.btnSetID.Location = new System.Drawing.Point(181, 71);
            this.btnSetID.Name = "btnSetID";
            this.btnSetID.Size = new System.Drawing.Size(75, 23);
            this.btnSetID.TabIndex = 0;
            this.btnSetID.Text = "Speichern";
            this.btnSetID.UseVisualStyleBackColor = true;
            this.btnSetID.Click += new System.EventHandler(this.btnSetID_Click);
            // 
            // rdInfo
            // 
            this.rdInfo.AutoSize = true;
            this.rdInfo.Location = new System.Drawing.Point(143, 173);
            this.rdInfo.Name = "rdInfo";
            this.rdInfo.Size = new System.Drawing.Size(82, 17);
            this.rdInfo.TabIndex = 19;
            this.rdInfo.TabStop = true;
            this.rdInfo.Text = "Info abrufen";
            this.rdInfo.UseVisualStyleBackColor = true;
            // 
            // rdSwitch
            // 
            this.rdSwitch.AutoSize = true;
            this.rdSwitch.Location = new System.Drawing.Point(12, 150);
            this.rdSwitch.Name = "rdSwitch";
            this.rdSwitch.Size = new System.Drawing.Size(64, 17);
            this.rdSwitch.TabIndex = 20;
            this.rdSwitch.TabStop = true;
            this.rdSwitch.Text = "Schalter";
            this.rdSwitch.UseVisualStyleBackColor = true;
            // 
            // rdHalfBlock
            // 
            this.rdHalfBlock.AutoSize = true;
            this.rdHalfBlock.Location = new System.Drawing.Point(143, 150);
            this.rdHalfBlock.Name = "rdHalfBlock";
            this.rdHalfBlock.Size = new System.Drawing.Size(86, 17);
            this.rdHalfBlock.TabIndex = 21;
            this.rdHalfBlock.TabStop = true;
            this.rdHalfBlock.Text = "Halber Block";
            this.rdHalfBlock.UseVisualStyleBackColor = true;
            // 
            // ControlWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 561);
            this.ControlBox = false;
            this.Controls.Add(this.rdHalfBlock);
            this.Controls.Add(this.rdSwitch);
            this.Controls.Add(this.rdInfo);
            this.Controls.Add(this.gbID);
            this.Controls.Add(this.gbMessage);
            this.Controls.Add(this.rdMessage);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.rdSpawn);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.rdKey);
            this.Controls.Add(this.rdPliers);
            this.Controls.Add(this.rdPickaxe);
            this.Controls.Add(this.rdLevelUp);
            this.Controls.Add(this.rdLevelDown);
            this.Controls.Add(this.rdDestroyable);
            this.Controls.Add(this.rdGrid);
            this.Controls.Add(this.rdDoor);
            this.Controls.Add(this.rdWall);
            this.Controls.Add(this.rdNone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ControlWindow";
            this.Text = "MapCreator2D";
            this.gbMessage.ResumeLayout(false);
            this.gbMessage.PerformLayout();
            this.gbID.ResumeLayout(false);
            this.gbID.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectorID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdNone;
        private System.Windows.Forms.RadioButton rdWall;
        private System.Windows.Forms.RadioButton rdDoor;
        private System.Windows.Forms.RadioButton rdGrid;
        private System.Windows.Forms.RadioButton rdDestroyable;
        private System.Windows.Forms.RadioButton rdKey;
        private System.Windows.Forms.RadioButton rdPliers;
        private System.Windows.Forms.RadioButton rdPickaxe;
        private System.Windows.Forms.RadioButton rdLevelUp;
        private System.Windows.Forms.RadioButton rdLevelDown;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.RadioButton rdSpawn;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.RadioButton rdMessage;
        private System.Windows.Forms.GroupBox gbMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSetMessage;
        private System.Windows.Forms.GroupBox gbID;
        private System.Windows.Forms.Button btnSetID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown selectorID;
        private System.Windows.Forms.RadioButton rdInfo;
        private System.Windows.Forms.RadioButton rdSwitch;
        private System.Windows.Forms.RadioButton rdHalfBlock;
    }
}