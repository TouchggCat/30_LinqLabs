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

        private void button1_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            listBox1.Items.Add("Sum = " + nums.Sum());
            listBox1.Items.Add("Max = " + nums.Max());
            listBox1.Items.Add("Min = " + nums.Min());
            listBox1.Items.Add("Average = " + nums.Average());
            listBox1.Items.Add("Count = " + nums.Count());
            //==================================
            this.productsTableAdapter1.Fill(nwDataSet11.Products);
            this.listBox1.Items.Add("Sum UnitsInStock = " + this.nwDataSet11.Products.Sum(p => p.UnitsInStock));
            this.listBox1.Items.Add("Max UnitsInStock = " + this.nwDataSet11.Products.Max(p => p.UnitsInStock));
            this.listBox1.Items.Add("Min UnitsInStock = " + this.nwDataSet11.Products.Min(p => p.UnitsInStock));
            this.listBox1.Items.Add("Average UnitsInStock = " + this.nwDataSet11.Products.Average(p => p.UnitsInStock));
        }
    }
}