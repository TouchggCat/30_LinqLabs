using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(this.nwDataSet11.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet11.Order_Details);
            this.productsTableAdapter1.Fill(this.nwDataSet11.Products);
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
       
            int positionIndex=this.bindingSource1.Position;
            this.dataGridView2.DataSource = this.nwDataSet11.Orders[positionIndex].GetOrder_DetailsRows();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.nwDataSet11.Orders;
            this.dataGridView2.DataSource = this.nwDataSet11.Order_Details;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q = from o in this.nwDataSet11.Orders
                    //避免空值的例外  (還是有一堆)
                    where  !o.IsShipRegionNull()&&!o.IsShippedDateNull() &&(o.OrderDate.Year == Convert.ToInt32(comboBox1.SelectedItem))
                    select o;
            this.dataGridView1.DataSource = q.ToList();
       
            //===========================
            //TODO 訂單明細
            //this.bindingSource1.DataSource = this.nwDataSet11.Orders;
            //var qq = from od in this.nwDataSet11.Order_Details
            //             //where od.OrderID == 10248
            //         where od.OrderID == (int)this.dataGridView1.CurrentCell.Value /*&& (int)this.dataGridView1.CurrentCell.RowIndex== bindingSource1.Position*/
            //         select od;
            //this.dataGridView2.DataSource = qq.ToList();
            //TODO 提示要使用bindingSource事件 CurrentChanged
        }

        private void Frm作業_1_Load(object sender, EventArgs e)
        {
            var q = (from o in this.nwDataSet11.Orders
                     select o.OrderDate.Year).Distinct();

            //comboBox1.Items.Add(q.ToList());   //錯 Add(物件)
            comboBox1.DataSource = q.ToList();     //List 或 Array

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var qq = from od in this.nwDataSet11.Order_Details
                     where od.OrderID == (int)this.dataGridView1.CurrentCell.Value
                     select od;
            this.dataGridView2.DataSource = qq.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");      //目錄位置
            System.IO.FileInfo[] files = dir.GetFiles();        // 從目前的目錄傳回檔案清單  回傳陣列
            var qqq = from f in files
                      where f.Extension.Contains("log")   //Extension找副檔名
                      select f;
            //this.dataGridView1.DataSource = files;       //此行為顯示所有檔案
            this.dataGridView1.DataSource = qqq.ToList();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            var q = from f in files
                    where f.CreationTime.Year == 2017
                    select f;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] fileInfos = directory.GetFiles();   //GetFiles() 回傳FileInfo陣列
            var q = from f in fileInfos
                    where f.Length >= 100000
                    select f;
            this.dataGridView1.DataSource = q.ToList();


        }
        int count = 0;
        private void button13_Click(object sender, EventArgs e)
        {
            int pageRow = Convert.ToInt32(textBox1.Text);
            //this.nwDataSet1.Products.Take(10);      //相當於SQL語法 Top 10 //跳下一頁用Skip(10)
            //Distinct()
            var q = from p in this.nwDataSet11.Products.Skip(pageRow*count).Take(pageRow)
                    select p;
            this.dataGridView1.DataSource = q.ToList();
            count++;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            int pageRow = Convert.ToInt32(textBox1.Text);
           //第一次按完下一頁count = 1,第二次count = 2(第二頁) ,回前一頁skip為count-2,實際上count--只有-1
            var q = from p in this.nwDataSet11.Products.Skip(pageRow * (count-2)).Take(pageRow)
                    select p;
            this.dataGridView1.DataSource = q.ToList();
            if (count <= 0)
            {
                count = 0;
            }
            else
            {
                count--;
            }

        }
        //CurrentCellChanged，dataGridView的事件方法
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentCell == null)
            {

            }
            else
            {
                var qq = from od in this.nwDataSet11.Order_Details
                         where od.OrderID == (int)this.dataGridView1.CurrentCell.Value
                         select od;
                this.dataGridView2.DataSource = qq.ToList();
            }
        }
    }
}
