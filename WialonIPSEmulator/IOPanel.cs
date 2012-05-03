using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace WialonIPSEmulator
{
    public class IOPanel : Panel
    {
        private uint _value;

        public uint Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
                this.Invalidate();
            }
        }

        public event EventHandler OnChange;

        public IOPanel() : base() {
            this._value = 0;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            var width = this.Width / 32;
            var pos = e.X / width;
            this.Value ^= (uint)(1 << pos);
            if (this.OnChange != null)
                this.OnChange(this, new EventArgs());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var width = this.Width / 32;
            var height = this.Height;
            var g = e.Graphics;
            Pen p1, p0;
            Font f = this.Font;
            Brush sb;
            if (this.Enabled)
            {
                p1 = new Pen(Color.Black);
                p0 = new Pen(Color.Gray);
            } else
            {
                p1 = new Pen(Color.Gray);
                p0 = new Pen(Color.Gray);
            }
            p1.Width = 3;
            p0.Width = 1;
            sb = new SolidBrush(p1.Color);
            
            for (var i = 0; i < 32; i++)
            {
                var x = i * width;
                var r = new Rectangle(x, 0, width-1, height-1);
                if (((this._value >> i) & 0x01) != 0)
                {
                    // 1
                    g.DrawRectangle(p1, r);
                } else
                {
                    // 0
                    g.DrawRectangle(p0, r);
                }
                var s = (i + 1).ToString();
                var sz = g.MeasureString(s, f);
                g.DrawString(s, f, sb, x + ((width / 2) - sz.Width / 2), height / 2 - sz.Height / 2);
            }
        }
    }
}