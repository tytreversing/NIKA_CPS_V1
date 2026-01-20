namespace NIKA_CPS_V1
{
    partial class Grouplist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Grouplist));
            this.lbUsedContacts = new System.Windows.Forms.ListBox();
            this.lCounter = new System.Windows.Forms.Label();
            this.lbAvailableContacts = new System.Windows.Forms.ListBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bAdd = new System.Windows.Forms.Button();
            this.bRemove = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.bUp = new System.Windows.Forms.Button();
            this.bDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbUsedContacts
            // 
            this.lbUsedContacts.FormattingEnabled = true;
            this.lbUsedContacts.ItemHeight = 15;
            this.lbUsedContacts.Location = new System.Drawing.Point(24, 30);
            this.lbUsedContacts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lbUsedContacts.Name = "lbUsedContacts";
            this.lbUsedContacts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbUsedContacts.Size = new System.Drawing.Size(307, 469);
            this.lbUsedContacts.TabIndex = 0;
            // 
            // lCounter
            // 
            this.lCounter.AutoSize = true;
            this.lCounter.Location = new System.Drawing.Point(21, 9);
            this.lCounter.Name = "lCounter";
            this.lCounter.Size = new System.Drawing.Size(41, 15);
            this.lCounter.TabIndex = 1;
            this.lCounter.Text = "label1";
            // 
            // lbAvailableContacts
            // 
            this.lbAvailableContacts.FormattingEnabled = true;
            this.lbAvailableContacts.ItemHeight = 15;
            this.lbAvailableContacts.Location = new System.Drawing.Point(600, 31);
            this.lbAvailableContacts.MultiColumn = true;
            this.lbAvailableContacts.Name = "lbAvailableContacts";
            this.lbAvailableContacts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAvailableContacts.Size = new System.Drawing.Size(471, 469);
            this.lbAvailableContacts.TabIndex = 2;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(355, 31);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(214, 23);
            this.tbName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(597, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Доступные группы:";
            // 
            // bAdd
            // 
            this.bAdd.Location = new System.Drawing.Point(382, 71);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(152, 30);
            this.bAdd.TabIndex = 5;
            this.bAdd.Text = "<- Добавить";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bRemove
            // 
            this.bRemove.Location = new System.Drawing.Point(382, 117);
            this.bRemove.Name = "bRemove";
            this.bRemove.Size = new System.Drawing.Size(152, 30);
            this.bRemove.TabIndex = 6;
            this.bRemove.Text = "Удалить ->";
            this.bRemove.UseVisualStyleBackColor = true;
            this.bRemove.Click += new System.EventHandler(this.bRemove_Click);
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(374, 456);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(152, 30);
            this.bOK.TabIndex = 7;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bUp
            // 
            this.bUp.Location = new System.Drawing.Point(346, 219);
            this.bUp.Name = "bUp";
            this.bUp.Size = new System.Drawing.Size(152, 30);
            this.bUp.TabIndex = 8;
            this.bUp.Text = "Вверх";
            this.bUp.UseVisualStyleBackColor = true;
            this.bUp.Click += new System.EventHandler(this.bUp_Click);
            // 
            // bDown
            // 
            this.bDown.Location = new System.Drawing.Point(346, 267);
            this.bDown.Name = "bDown";
            this.bDown.Size = new System.Drawing.Size(152, 30);
            this.bDown.TabIndex = 9;
            this.bDown.Text = "Вниз";
            this.bDown.UseVisualStyleBackColor = true;
            this.bDown.Click += new System.EventHandler(this.bDown_Click);
            // 
            // Grouplist
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1103, 519);
            this.Controls.Add(this.bDown);
            this.Controls.Add(this.bUp);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.bRemove);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lbAvailableContacts);
            this.Controls.Add(this.lCounter);
            this.Controls.Add(this.lbUsedContacts);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Grouplist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Списки групп приема";
            this.Load += new System.EventHandler(this.Grouplist_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbUsedContacts;
        private System.Windows.Forms.Label lCounter;
        private System.Windows.Forms.ListBox lbAvailableContacts;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Button bRemove;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bUp;
        private System.Windows.Forms.Button bDown;
    }
}