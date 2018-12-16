using Distribution.Send.PerSend.DialogItems;
using Distribution.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Distribution
{
    public partial class Form1 : Form
    {
        FormIndividualPrice f1 = null;
        IndividualPriceView individualPriceView = null;
        public bool isSaved = false;
        public int savedCount = 0;
        public List<int> priceList = null;



        public Form1()
        {
            InitializeComponent();
            this.individualPriceView = new IndividualPriceView(0,null,this);
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //int temp = 0;

            //if(int.TryParse(this.textBox1.Text.Trim(), out temp) && temp > 0)
            //{
            //    f1 = new FormIndividualPrice(temp, true);
            //    f1.ShowDialog(this);
            //}
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            int number = 0;
            if (int.TryParse(this.textBox1.Text.Trim(), out number) && number>0)
            {
                if(isSaved = true && savedCount == number)
                {
                    individualPriceView = new IndividualPriceView(Convert.ToInt32(this.textBox1.Text.Trim()), this.priceList,this);
                }
                else
                {
                    individualPriceView = new IndividualPriceView(Convert.ToInt32(this.textBox1.Text.Trim()),null,this);
                }
                individualPriceView.ShowDialog(this);
            }
        }

    }
}
