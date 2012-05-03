using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WialonIPSEmulator
{
    public partial class SettingsForm : Form
    {
        CSettings _settings;

        public SettingsForm(CSettings settings)
        {
            this._settings = settings;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this._settings.AutoConnect = this.cbAutoConnect.Checked;
            this._settings.SendPingPackets = this.cbSendPing.Checked;

            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.cbAutoConnect.Checked = this._settings.AutoConnect;
            this.cbSendPing.Checked = this._settings.SendPingPackets;
        }
    }
}
