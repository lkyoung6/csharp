using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Distribution.View
{
    public partial class IndividualPriceView : Form
    {
        private int number;
        List<IndividualPriceItem> individualPriceItemList;

        public List<int> priceList= new List<int>();

        public bool isSave = false;
        public int SavedDataCount = 0;

        private const int ItemHeight = 28;
        private Form1 form1;

        
        public int TotalPrice
        {
            get
            {
                int totalPrice = 0;
                foreach (var price in priceList)
                {
                    totalPrice += price;
                }
                return totalPrice;
            }
        }

        

        public IndividualPriceView(int number, List<int> priceList, Form1 form1) 
        {
            InitializeComponent();
            this.number = number;
            this.priceList = priceList;
            this.form1 = form1;
            if (priceList == null)
            {
                this.priceList = new List<int>();
            }

            
            this.individualPriceItemList = new List<IndividualPriceItem>();

            if (priceList == null)
            {
                for (int i = 0; i < number; i++)
                {
                    IndividualPriceItem priceItem = new IndividualPriceItem();

                    priceItem.Location = new Point(0, i * ItemHeight);
                    priceItem.StateChangeEvent += chkAll_CheckedChanged;
                    priceItem.ItemTxtPriceChangeEvent += IndividualPriceItemChanged; 
                    individualPriceItemList.Add(priceItem);
                    this.priceList.Add(0);
                    pnlItems.Controls.Add(individualPriceItemList[i]);
                }
            }
            else
            {
                for (int i = 0; i < number; i++)
                {
                    IndividualPriceItem priceItem = new IndividualPriceItem();
                    individualPriceItemList.Add(priceItem);
                    priceItem.Location = new Point(0, i * ItemHeight);
                    priceItem.StateChangeEvent += chkAll_CheckedChanged;
                    priceItem.ItemTxtPriceChangeEvent += IndividualPriceItemChanged; 
                    individualPriceItemList[i].Price = priceList[i];
                    priceItem.ItemTxtPriceChangeEvent += CaculateTotalPrice;
                    priceItem.txtPrice.Text = individualPriceItemList[i].Price.ToString();
                    pnlItems.Controls.Add(individualPriceItemList[i]);
                }
            }
           
        }

        private void IndividualPriceItemChanged(object sender, EventArgs e)
        {
            UpdatePriceList();
            UpdateTxtTotalPrice();
        }

        private void UpdatePriceList()
        {
            int index = 0;
            foreach (var priceitem in individualPriceItemList)
            {
                priceList[index ]= priceitem.Price;
                index++;
            }
        }

        private void CaculateTotalPrice(object sender, EventArgs e)
        {
            UpdateTxtTotalPrice();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
       {
            if ((!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || txtPrice.Text.Length < 1 && e.KeyChar == '0')
            {
                e.Handled = true;
            }

       }

        private void IndividualPriceView_Shown(object sender, EventArgs e)
        {
            txtPrice.Select();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;

            if(chk.Name == "chkAll")
            {
                if (chkAll.CheckState == CheckState.Unchecked)
                {
                    for (int i = 0; i < individualPriceItemList.Count; i++)
                    {
                        individualPriceItemList[i].chkEachItem.Checked = false;
                        individualPriceItemList[i].txtPrice.Enabled = true;
                    }
                }
                else if (chkAll.CheckState == CheckState.Checked)
                {
                    for (int i = 0; i < individualPriceItemList.Count; i++)
                    {
                        individualPriceItemList[i].chkEachItem.Checked = true;
                        individualPriceItemList[i].txtPrice.Enabled = false;
                    }
                }
            }
            else if (chk.Name == "chkEachItem")
            {
                ChangeChkAllState();
                UpdateTxtTotalPrice();
            }
        }

        private void UpdateTxtTotalPrice()
        {
            txtTotalPrice.Text = TotalPrice.ToString();
        }

        private void ChangeChkAllState()
        {
            int count = 0;

            for (int i = 0; i < individualPriceItemList.Count; i++)
            {
                if(individualPriceItemList[i].chkEachItem.Checked)
                {
                    count++;
                }
            }
            if (count == individualPriceItemList.Count)
            {
                chkAll.CheckState = CheckState.Checked;
            }
            else if(count == 0)
            {
                chkAll.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkAll.CheckState = CheckState.Indeterminate;
            }
        }

        private void SaveCurrentState()
        {
            form1.isSaved = true;
            form1.savedCount = priceList.Count;
            form1.priceList = priceList;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInitialization_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < individualPriceItemList.Count; i++)
            {
                individualPriceItemList[i].txtPrice.Text = string.Empty;
            }
            txtPrice.Text = string.Empty;
            txtTotalPrice.Text = string.Empty;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            bool emptyTxtExist = CheckEmptyTxt();

            if(!emptyTxtExist)
            {
                UpdatePriceList();
                SaveCurrentState();
                this.Close();
            }
        }

        private bool CheckEmptyTxt()
        {
            List<int> indexList = new List<int>();

            for (int i = 0; i < priceList.Count; i++)
            {
                if (priceList[i] == 0)
                {
                    indexList.Add(i);
                }
            }

            string errorIndexString = string.Empty;

            if (indexList.Count == 1)
            {
                errorIndexString = (indexList[0] + 1).ToString();
            }
            else
            {
                for (int i = 0; i < indexList.Count; i++)
                {
                    errorIndexString += (indexList[i] + 1).ToString() + ", ";
                }
            }

            if (errorIndexString != string.Empty)
            {
                MessageBox.Show(string.Format("물건 가격 {0} 번째가 비어 있습니다.", errorIndexString));
                return true;
            }
            return false;

        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void btnApplyAll_Click(object sender, EventArgs e)
        {
            if (txtPrice.Text != string.Empty)
            {
                if (individualPriceItemList.Count == 1)
                {
                    individualPriceItemList[0].txtPrice.Text = txtPrice.Text;
                    SaveCurrentState();

                    this.Close();
                }
                else
                {
                    for (int i = 0; i < individualPriceItemList.Count; i++)
                    {
                        individualPriceItemList[i].txtPrice.Text = txtPrice.Text;
                    }
                }

            }
            else
            {
                MessageBox.Show("일괄적용 금액이 비어 있습니다.");
            }
        }
    }
}
