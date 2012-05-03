using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WialonIPSEmulator
{
    public partial class DevicesConfigForm : Form
    {
        public DevicesConfigForm(MainForm main_form, DeviceInfo[] devices)
        {
            InitializeComponent();
            this._main_form = main_form;
            this._devices = new List<DeviceInfo>(devices);
        }

        public DialogResult ShowConfig()
        {
            this.UpdateItems();
            this.SelectDevice(null);
            this.ShowDialog(this._main_form);
            return this._result;
        }

        void UpdateItems()
        {
            this.lbDevices.Items.Clear();
            this.lbDevices.Items.AddRange(this._devices.ToArray());
        }

        void SelectDevice(DeviceInfo device)
        {
            if (device == null)
            {
                this.tbID.Text = "";
                this.tbID.Enabled = false;
                this.tbPassword.Text = "";
                this.tbPassword.Enabled = false;
                this.tbName.Text = "";
                this.tbName.Enabled = false;
                this.btnRemove.Enabled = false;
                this.btnSet.Enabled = false;
            } else
            {
                this.tbID.Enabled = true;
                this.tbPassword.Enabled = true;
                this.tbName.Enabled = true;
                this.btnSet.Enabled = true;
                this.btnRemove.Enabled = true;
                this.tbID.Text = device.ID;
                this.tbPassword.Text = device.Password;
                this.tbName.Text = device.Name;
            }
        }

        public DeviceInfo[] GetDevices()
        {
            return this._devices.ToArray();
        }

        private MainForm _main_form;
        private List<DeviceInfo> _devices;
        private bool _adding = false;
        private DeviceInfo _selected = null;
        private DialogResult _result = DialogResult.Cancel;

        private void lbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._adding)
            {
                this.btnAdd.Enabled = true;
                this._adding = false;
            }
            this._selected = this.lbDevices.SelectedItem as DeviceInfo;
            this.SelectDevice(this._selected);
        }

        private class IDFieldEmpty : Exception { }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                DeviceInfo di;
                if (this._adding)
                    di = new DeviceInfo();
                else
                    di = this._selected;
                di.ID = this.tbID.Text;
                if (di.ID == "")
                    throw new IDFieldEmpty();
                di.Password = this.tbPassword.Text;
                di.Name = this.tbName.Text;
                if (this._adding)
                {
                    this.btnAdd.Enabled = true;
                    this._adding = false;
                    this._devices.Add(di);
                    this._selected = di;
                }
                this.UpdateItems();
                this.SelectDevice(di);
            }
            catch (IDFieldEmpty)
            {
                MessageBox.Show("ID field can not be empty");
                this.tbID.Focus();
            }
            catch (Exception exc)
            {
                this._main_form.Log.PostHead("Exception", "Device set, exc: " + exc);
                MessageBox.Show(exc.Message);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this._adding = true;
            this.btnAdd.Enabled = false;
            this.btnSet.Enabled = true;
            this.tbID.Enabled = true;
            this.tbID.Text = "";
            this.tbPassword.Enabled = true;
            this.tbPassword.Text = "";
            this.tbName.Enabled = true;
            this.tbName.Text = "";
            this.tbID.Focus();
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
                this.SelectDevice(null);
            } else
            {
                if (this._selected != null)
                {
                    var di = this._selected as DeviceInfo;
                    this._devices.Remove(di);
                    this.UpdateItems();
                    this.SelectDevice(null);
                }
            }
        }
    }
}
