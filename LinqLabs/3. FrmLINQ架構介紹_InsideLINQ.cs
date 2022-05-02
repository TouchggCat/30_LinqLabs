using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            //非泛用方法點不到where   arrList.Where(X)
            System.Collections.ArrayList arrList = new System.Collections.ArrayList();
            arrList.Add(3);
            arrList.Add(4);
            arrList.Add(1);
            var q = from n in arrList.Cast<int>()
                    where n > 2
                    select new { N=n };
            this.dataGridView1.DataSource = q.ToList();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet11.Products);
            var q = (from p in this.nwDataSet11.Products
                     orderby p.UnitsInStock descending
                     select p).Take(5);   //取前五筆
            this.dataGridView1.DataSource=q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
    }
}