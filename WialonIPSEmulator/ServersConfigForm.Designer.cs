namespace WialonIPSEmulator
{
    partial class ServersConfigForm
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
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbHost = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lbServers = new System.Windows.Forms.ListBox();
            this.gbSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.btnSet);
            this.gbSettings.Controls.Add(this.btnRemove);
            this.gbSettings.Controls.Add(this.btnAdd);
            this.gbSettings.Controls.Add(this.label3);
            this.gbSettings.Controls.Add(this.tbName);
            this.gbSettings.Controls.Add(this.label2);
            this.gbSettings.Controls.Add(this.tbPort);
            this.gbSettings.Controls.Add(this.label1);
            this.gbSettings.Controls.Add(this.tbHost);
            this.gbSettings.Controls.Add(this.btnSave);
            this.gbSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbSettings.Location = new System.Drawing.Point(0, 191);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(444, 141);
            this.gbSettings.TabIndex = 1;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Server information";
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.Location = new System.Drawing.Point(363, 76);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 17;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(363, 50);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 16;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(363, 24);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Name:";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(109, 76);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(248, 20);
            this.tbName.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Port:";
            // 
            // tbPort
            // 
            this.tbPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPort.Location = new System.Drawing.Point(109, 50);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(248, 20);
            this.tbPort.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Host (IP address):";
            // 
            // tbHost
            // 
            this.tbHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHost.Location = new System.Drawing.Point(109, 24);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(248, 20);
            this.tbHost.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(92, 106);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(260, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save servers and close";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lbServers
            // 
            this.lbServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbServers.FormattingEnabled = true;
            this.lbServers.Location = new System.Drawing.Point(0, 0);
            this.lbServers.Name = "lbServers";
            this.lbServers.ScrollAlwaysVisible = true;
            this.lbServers.Size = new System.Drawing.Size(444, 186);
            this.lbServers.TabIndex = 2;
            this.lbServers.SelectedIndexChanged += new System.EventHandler(this.lbServers_SelectedIndexChanged);
            // 
            // ServersConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 332);
            this.Controls.Add(this.lbServers);
            this.Controls.Add(this.gbSettings);
            this.MinimumSize = new System.Drawing.Size(460, 370);
            this.Name = "ServersConfigForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure servers";
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lbServers;
    }
}