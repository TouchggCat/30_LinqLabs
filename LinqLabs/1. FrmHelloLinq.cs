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
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();
            this.productsTableAdapter1.Fill(this.nwDataSet11.Products);
            this.ordersTableAdapter1.Fill(this.nwDataSet11.Orders);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //foreach sugar
            foreach(int n in nums)
            {
                this.listBox1.Items.Add(n);
            }
            //=================
            this.listBox1.Items.Add("=====================");
            System.Collections.IEnumerator en = nums.GetEnumerator();
            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 33 };
            foreach (int n in list)
            {
                this.listBox1.Items.Add(n);
            }
            //=================
            List<int>.Enumerator en = list.GetEnumerator();
            while(en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();

            //Step 1: define Data Source
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //Setp2: Define Query
            //define query  (IEnumerable<int> q 是一個  Iterator 物件), 如陣列集合一般 (陣列集合也是一個  Iterator 物件)
            //迭代器（iterator）

            //IEnumerable<int> q -  公開支援指定型別集合上簡單反覆運算的列舉值。
 
            IEnumerable<int> q = from n in nums            //var q = from n in nums
                                     //where n >= 5
                                     //where n <= 8
                                     //where n % 2 == 0
                                 where (n >= 3 && n <= 8) && (n % 2 == 0)
                                 select n;
            //Step 3: Execute Query
            //execute query(執行 iterator - 逐一查看集合的item)
            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,12 };
            //var q = from n in nums
            IEnumerable<int> q = from n in nums  //此時定義q(未執行)
                                 where IsEven(n)
                                 select n;
            //where n >= 5
            //where n <= 8
            //where n % 2 == 0

            foreach (int n in q)   //這時才執行
            {
                listBox1.Items.Add(n);
            }
        }

        bool IsEven(int n)
        {
            //if(n%2==0)
            //  {
            //      return true;
            //  }
            //else
            //  {
            //      return false;
            //  }
            return n % 2 == 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            //var q = from n in nums
            IEnumerable<Point> q = from n in nums  //<Point> 型別為Point
                                                            where n > 5
                                                            select new Point(n, n * n);   //new Point 物件
                  
            foreach (Point pt in q)   //這時才執行
            {
                listBox1.Items.Add(pt.X + " , "+pt.Y);
            }
            //=======================
            List<Point> list = q.ToList();      //foreach(...item...in q ....)     list.Add(item)......return list;
            this.dataGridView1.DataSource = list;
            //this.dataGridView1.DataSource = q.ToList();
            ////==========================
            this.chart1.DataSource = list;
            this.chart1.Series[0].XValueMember = "X"; //"X"  =>>Point物件的屬性   Series[0] 第0個series(藍條)
            this.chart1.Series[0].YValueMembers = "Y";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] words = { "aaa", "Apple", "pineApple", "xxxapple" };
            IEnumerable<string> q = from w in words
                                                            where w.ToLower().Contains("apple") && w.Length>5  //不分大小寫(全轉成小寫)
                                                            select w;
            foreach(string s in q)
            {
                this.listBox1.Items.Add(s);
            }
            //===========================
            this.dataGridView1.DataSource = listBox1.Items;   //字串的屬性是length 
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //this.dataGridView1.DataSource = this.nwDataSet11.Products;
            /*var*/ IEnumerable<global :: LinqLabs.NWDataSet1.ProductsRow> q = from p in this.nwDataSet11.Products
                                                                                                                                                   where ! p.IsUnitPriceNull() && p.UnitPrice > 30 && p.UnitPrice < 50 && p.ProductName.StartsWith("M")
                                                                                                                                                   select p;
            this.dataGridView1.DataSource = q.ToList();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            var q = from o in this.nwDataSet11.Orders
                    where o.OrderDate.Year ==1997 && o.OrderDate.Month <4 //(第一季)
                    orderby o.OrderDate descending
                    select o;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5 };
            var q = from ooo in this.nwDataSet11.Orders
                    select ooo;
        }

        private void FrmHelloLinq_Load(object sender, EventArgs e)
        {

        }
    }
}
