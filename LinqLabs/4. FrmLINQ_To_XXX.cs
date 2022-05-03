using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,13 };
            IEnumerable　<IGrouping <string,int>> q = from n in nums //<outTkey,OOO>要改成string
                                                  group n by n % 2==0? "偶數":"奇數";   //分成兩個可列舉的群
            //dataGridView只能看到key的屬性 (1,0)
            this.dataGridView1.DataSource = q.ToList();
            //=========================
            treeView1.Nodes.Clear();
            foreach(var group in q )
            {
                TreeNode node = this.treeView1.Nodes.Add(group.Key.ToString());
                foreach(var item in group)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            //=========================
            foreach (var group in q)
            {
                string s = $"{group.Key} ({group.Count()})"; //用s取代group.Key.ToString()
                ListViewGroup lvg = this.listView1.Groups.Add(group.Key.ToString(),s);  //(string Dey,string headText) 
                foreach (var item in group)
                {
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13 };
            var q = from n in nums //<outkey,>要改成string  
                                                    group n by n % 2 == 0 ? "偶數" : "奇數" into g //暫存到g變數，便於統計該群
                    select new {                                                 //型別不知，要記得用var q
                                                        Mykey = g.Key,
                                                        MyCount = g.Count(),
                                                        MyMax = g.Max(),
                                                        MyMin = g.Min(),
                                                        MyAvg = g.Average(),
                                                        MyGroup =g         //g為了呈現該群底下的items
                                                    };  
            //dataGridView只能看到key的屬性 (1,0)
            this.dataGridView1.DataSource = q.ToList();
            //==============================
            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.Mykey} ({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(group.Mykey.ToString(),s);
                foreach (var item in group.MyGroup) //每個群下面的檔案
                {
                    node.Nodes.Add(item.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13 };
            var q = from n in nums //<outkey,>要改成string  
                    group n by MyFuckingkey(n) into g //!!用方法分群   //暫存到g變數，統計該群
                    select new
                    {                                                 //型別不知，要記得用var q
                        Mykey = g.Key,
                        MyCount = g.Count(),
                        MyMax = g.Max(),
                        MyMin = g.Min(),
                        MyAvg = g.Average(),
                        MyGroup = g
                    };
            //dataGridView只能看到key的屬性 (1,0)
            this.dataGridView1.DataSource = q.ToList();
            //==============================
            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.Mykey} ({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(group.Mykey.ToString(), s);
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            //==============================
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].XValueMember = "MyKey";
            this.chart1.Series[1].YValueMembers = "MyAvg";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

        }

        private string MyFuckingkey(int n )   //回傳字串,參數int
        {
            if(n<5)
            {
                return "小";
            }
            else if(n<10)
            {
                return "中";
            }
            else
            {
                return "大";
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");      //目錄位置
            System.IO.FileInfo[] files = dir.GetFiles();        // 從目前的目錄傳回檔案清單  回傳陣列
            this.dataGridView1.DataSource = files;
            var q = from n in files
                    group n by n.Extension into g
                    orderby g.Count() descending
                    select new
                    {
                        MyKey = g.Key,
                        MyCount = g.Count()                  
                    };
            this.dataGridView2.DataSource =q.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //每年訂單有幾筆
            this.ordersTableAdapter1.Fill(nwDataSet11.Orders);
            //var q = from n in nwDataSet11.Orders
            //        group n by n.OrderDate.Year into o
            //        select new
            //        {
            //            MyKey = o.Key,
            //            MyCount = o.Count()
            //        };
            //TODO  寫成lambda
            var q = nwDataSet11.Orders.GroupBy(n => n.OrderDate.Year, 
                                                                                             (key, g) => new { MyKey = key, MyCount = g.Count() });
            this.dataGridView2.DataSource = q.ToList();
            //=================只算1997年
            int count  = (nwDataSet11.Orders.Where(n => n.OrderDate.Year == 1997)).Count();
            MessageBox.Show("count = " + count);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");      //目錄位置
            System.IO.FileInfo[] files = dir.GetFiles();        // 從目前的目錄傳回檔案清單  回傳陣列
            int count = (from f in files
                         let s = f.Extension
                     where /*f.Extension*/ s == ".exe"
                     select f).Count();
            MessageBox.Show("count = " + count);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = "This is a book. This is a pen.  this is An apple.";
            char [] chars = { ' ', ',', '.', '?' };    //separator
            string[] words = s.Split(chars,StringSplitOptions.RemoveEmptyEntries);  //刪除空白統計
            var q = from w in words
                    group w by w.ToUpper() into g  //不分大小寫
                    select new {MyKey = g.Key , MyCount = g.Count() };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int[] nums1 = { 1,2,3,5,11,2};
            int[] nums2 = { 1, 3, 11, 77, 88, 99 };
            IEnumerable<int> q;
            q = nums1.Intersect(nums2);
            q = nums1.Union(nums2);
            q = nums1.Distinct();
            //=========================
            //切割運算子 Take / Skip 
            q = nums1.Take(2);
            //=========================
            //數量詞作業 Any / All / Contains
            bool result;
            result = nums1.Any(n => n > 3);
            result = nums1.All(n => n > 100);
            //=================
            //元素運算子
            int n1 = nums1.First();
            n1 = nums1.Last();
            //n1 = nums1.ElementAt(13);   //OutOfRange
            n1 = nums1.ElementAtOrDefault(13);    //避免超出範圍 索引超出則回傳預設值0
            //=========================
            //產生作業 Generation
            //var q1 = Enumerable.Range(1, 100);   //q1為可列舉的int，沒有屬性
              var q1 = Enumerable.Range(1, 100).Select( n=> new { n });
            this.dataGridView1.DataSource = q1.ToList();
            var q2 = Enumerable.Repeat(87, 100).Select(n=>new { N = n });
            this.dataGridView2.DataSource = q2.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(nwDataSet11.Products);
            var q = from p in nwDataSet11.Products
                    group p by p.CategoryID into g
                    select new
                    {
                        CategoryIDDD = g.Key,
                        MyAvg=g.Average(p=> p.UnitPrice),
                    };
            this.dataGridView1.DataSource = q.ToList();
            //=============================
            //太T-SQL
            this.categoriesTableAdapter1.Fill(nwDataSet11.Categories);
            var q2 = from c in nwDataSet11.Categories
                     join p in this.nwDataSet11.Products
                    on c.CategoryID equals p.CategoryID
                     group p by c.CategoryName into g
                     select new
                     {
                         CategoryNAME = g.Key,
                         MyAvg = g.Average(p => p.UnitPrice)
                     };
            this.dataGridView2.DataSource = q2.ToList();
                        
        }
        
    }
}
