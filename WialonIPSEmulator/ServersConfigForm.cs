using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WialonIPSEmulator
{
    public partial class ServersConfigForm : Form
    {
        public ServersConfigForm(MainForm main_form, ServerInfo[] servers)
        {
            InitializeComponent();
            this._main_form = main_form;
            this._servers = new List<ServerInfo>(servers);
        }

        public DialogResult ShowConfig()
        {
            this.UpdateItems();
            this.SelectServer(null);
            this.ShowDialog(this._main_form);
            return this._result;
        }

        void UpdateItems()
        {
            this.lbServers.Items.Clear();
            this.lbServers.Items.AddRange(this._servers.ToArray());
        }

        void SelectServer(ServerInfo server)
        {
            if (server == null)
            {
                this.tbHost.Text = "";
                this.tbHost.Enabled = false;
                this.tbPort.Text = "";
                this.tbPort.Enabled = false;
                this.tbName.Text = "";
                this.tbName.Enabled = false;
                this.btnRemove.Enabled = false;
                this.btnSet.Enabled = false;
            } else
            {
                this.tbHost.Enabled = !server.ReadOnly;
                this.tbPort.Enabled = !server.ReadOnly;
                this.tbName.Enabled = !server.ReadOnly;
                this.btnSet.Enabled = !server.ReadOnly;
                this.btnRemove.Enabled = !server.ReadOnly;
                this.tbHost.Text = server.Host;
                this.tbPort.Text = server.Port.ToString();
                this.tbName.Text = server.Name;
            }
        }

        public ServerInfo[] GetServers()
        {
            return this._servers.ToArray();
        }

        private MainForm _main_form;
        private List<ServerInfo> _servers;
        private bool _adding = false;
        private ServerInfo _selected = null;
        private DialogResult _result = DialogResult.Cancel;

        private void lbServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._adding)
            {
                this.btnAdd.Enabled = true;
                this._adding = false;
            }
            this._selected = this.lbServers.SelectedItem as ServerInfo;
            this.SelectServer(this._selected);
        }

        private class HostFieldEmpty : Exception {}

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                ServerInfo si;
                if (this._adding)
                    si = new ServerInfo("", 0, "", false);
                else
                    si = this._selected;
                si.Host = this.tbHost.Text;
                if (si.Host == "")
                    throw new HostFieldEmpty();
                si.Port = UInt16.Parse(this.tbPort.Text);
                if (si.Port == 0)
                    throw new OverflowException();
                si.Name = this.tbName.Text;
                if (this._adding)
                {
                    this.btnAdd.Enabled = true;
                    this._adding = false;
                    this._servers.Add(si);
                    this._selected = si;
                }
                this.UpdateItems();
                this.SelectServer(si);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid port value");
                this.tbPort.Focus();
            }
            catch (OverflowException)
            {
                MessageBox.Show("Please enter valid port value");
                this.tbPort.Focus();
            }
            catch (HostFieldEmpty)
            {
                MessageBox.Show("Host field can not be empty");
                this.tbHost.Focus();
            }
            catch (Exception exc)
            {
                this._main_form.Log.PostHead("Exception", "Server set, exc: " + exc);
                MessageBox.Show(exc.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this._adding = true;
            this.btnAdd.Enabled = false;
            this.btnSet.Enabled = true;
            this.tbHost.Enabled = true;
            this.tbHost.Text = "";
            this.tbPort.Enabled = true;
            this.tbPort.Text = "";
            this.tbName.Enabled = true;
            this.tbName.Text = "";
            this.tbHost.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this._result = DialogResult.OK;
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this._adding)
            {
                this._adding = false;
                this.btnAdd.Enabled = true;
                this.SelectServer(null);
            } else
            {
                if (this._selected != null)
                {
                    var si = this._selected as ServerInfo;
                    if (!si.ReadOnly)
                    {
                        this._servers.Remove(si);
                        this.UpdateItems();
                        this.SelectServer(null);
                    }
                }
            }
        }
    }
}
