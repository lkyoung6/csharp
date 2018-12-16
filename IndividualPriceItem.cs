using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Distribution
{
    public partial class IndividualPriceItem : UserControl
    {
        public IndividualPriceItem()
        {
            InitializeComponent();
        }

        public int Price { get; set; }
        public int Volumn { get; set; }
        public int Weight { get; set; }

        public delegate void CheckBoxHandler(object sender, EventArgs e);
        public delegate void TxtPriceHandler(object sender, EventArgs e);
        public event CheckBoxHandler StateChangeEvent;
        public event TxtPriceHandler ItemTxtPriceChangeEvent;

    

        private void chkPriceItem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEachItem.Checked)
            {
                this.txtPrice.Enabled = false;
                this.Price = 0;
                this.txtPrice.Text = string.Empty;
            }
            else if (chkEachItem.Checked == false)
            {
                this.txtPrice.Enabled = true;
                this.Price = 0;
                this.txtPrice.Text = string.Empty;
                
            }
            StateChangeEvent(sender, e);
            
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            int price;
            bool parsed = Int32.TryParse(txtPrice.Text, out price);
            if(!parsed)
            {
                price = 0;
            }
            this.Price = price;
            ItemTxtPriceChangeEvent(sender, e);


        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || txtPrice.Text.Length < 1 && e.KeyChar == '0')
            {
                e.Handled = true;
            }
        }
    }
}
