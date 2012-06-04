namespace WialonIPSEmulator
{
    partial class SettingsForm
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
            this.cbSendPing = new System.Windows.Forms.CheckBox();
            this.cbAutoConnect = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbSendPing
            // 
            this.cbSendPing.AutoSize = true;
            this.cbSendPing.Location = new System.Drawing.Point(10, 35);
            this.cbSendPing.Name = "cbSendPing";
            this.cbSendPing.Size = new System.Drawing.Size(169, 17);
            this.cbSendPing.TabIndex = 1;
            this.cbSendPing.Text = "Periodically send ping packets";
            this.cbSendPing.UseVisualStyleBackColor = true;
            // 
            // cbAutoConnect
            // 
            this.cbAutoConnect.AutoSize = true;
            this.cbAutoConnect.Location = new System.Drawing.Point(11, 12);
            this.cbAutoConnect.Name = "cbAutoConnect";
            this.cbAutoConnect.Size = new System.Drawing.Size(168, 17);
            this.cbAutoConnect.TabIndex = 0;
            this.cbAutoConnect.Text = "Auto connect at emulator start";
            this.cbAutoConnect.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(185, 84);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(128, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 119);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbAutoConnect);
            this.Controls.Add(this.cbSendPing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbSendPing;
        private System.Windows.Forms.CheckBox cbAutoConnect;
        private System.Windows.Forms.Button btnOk;
    }
}