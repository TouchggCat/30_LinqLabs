using LinqLabs;
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
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");      //目錄位置
            System.IO.FileInfo[] files = dir.GetFiles();
            //var q = from n in files select n;
            var q = from n in files
                    orderby n.Length descending //此時排序Treenodes也會排好
                    group n by MyLengthKey(Convert.ToInt32(n.Length)) into g
                    select new
                    {
                        MyFile = g.Key,
                        MyCount = g.Count(),
                        MyGroup = g         //g為了呈現該群底下的items
                    };
            this.dataGridView1.DataSource = q.ToList();
            //==================================
            this.treeView1.Nodes.Clear(); 
            foreach (var group in q)
            {
                string keyText = $"{group.MyFile} ({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(group.MyFile,keyText);
                foreach (var item in group.MyGroup)       //分好的群每個item
                {
                    node.Nodes.Add(item.ToString());
                }
            }

        }

        private string MyLengthKey(int n)
        {
            if (n < 1000)
            {
                return "小";
            }
            else if (n < 10000)
            {
                return "中";
            }
            else
            {
                return "大";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");      //目錄位置
            System.IO.FileInfo[] files = dir.GetFiles();
            var q = from n in files
                    orderby n.CreationTime.Year descending
                    group n by n.CreationTime.Year into g   //年很好分群，直接用treenode
                    select new
                    {
                        MyYear = g.Key,
                        MyYearCount = g.Count(),
                        MyGroup= g    //g為了呈現該群底下的items
                    };
            this.dataGridView1.DataSource = q.ToList();
            //==========================
            treeView1.Nodes.Clear();
            foreach(var group in q)
            {
                TreeNode node = this.treeView1.Nodes.Add(group.MyYear.ToString());
                foreach(var item in group.MyGroup)  //每個群下面的檔案
                {
                    node.Nodes.Add(item.ToString());
                }
            }
        }
        int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        private void button4_Click(object sender, EventArgs e)
        {
            int a = 0, b = 0, c = 0;
            List<int> Mylist1 = MyWhere(nums,Test1);
            List<MyPoint> treelist = new List<MyPoint>();
            List<MyPoint> list = new List<MyPoint>();
            foreach (int n in Mylist1)
            {
                treelist.Add(new MyPoint("小", n));
                a++;
            }
            list.Add(new MyPoint("小", a));
            List<int> Mylist2 = MyWhere(nums, Test2);
            foreach (int n in Mylist2)
            {
                treelist.Add(new MyPoint("中", n));
                b++;
            }
            list.Add(new MyPoint("中", b));
            List<int> Mylist3 = MyWhere(nums, Test3);
            foreach (int n in Mylist3)
            {
                //list.Add(new MyPoint("大", n));
                treelist.Add(new MyPoint("大", n));
                c++;
            }
            list.Add(new MyPoint("大", c));
            this.dataGridView1.DataSource = list;
            this.dataGridView2.DataSource = treelist;
            //===========================
            this.treeView1.Nodes.Clear();
            TreeNode node;   //TODO   無法呈現 要用3個list嗎
            foreach (var g in list)
            {
                node = this.treeView1.Nodes.Add(g.大小);
                foreach (var ggg in treelist)
                {
                    node.Nodes.Add(ggg.陣列元素.ToString());
                }
            }

        }

        delegate bool Mydelegate(int n);
        List<int> MyWhere(int [] nums,Mydelegate mydeleObj)
        {
            //是否要把int陣列加入List要看傳進來的委派(true/false)   
            List<int> list = new List<int>();
            foreach (int n in nums)
            {
                if (mydeleObj(n))  //呼叫方法n>5  或n%2==0等等
                {
                    list.Add(n);
                } 
            }
            return list;
        }
        bool Test1(int n)
        {
            //if (n>5)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return n <= 5;
        }
        bool Test2(int n)
        {
            return n > 5&&n <= 10;
        }
        bool Test3(int n)
        {
            return n > 10 && n <= 15;
        }
        public class MyPoint
        {
            public string 大小 { get; set; }   //自動實作屬性
            public int 陣列元素 { get; set; }
            public MyPoint(string p1, int p2)
            {
                this.大小 = p1;
                this.陣列元素 = p2;
            }
        }
        //================================================
        NorthwindEntities dbContext = new NorthwindEntities();
        private void button8_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            var q = from p in dbContext.Products.AsEnumerable()  //!!必須加入AsEnumerable()  方法傳回為可列舉值
                        //where p.UnitPrice != null
                        orderby p.UnitPrice
                    group p by PriceKey(p.UnitPrice) into g  //TODO 方法一直出錯
                    select new
                    {
                        MyPrice = g.Key,
                        Count = g.Count(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            this.treeView1.Nodes.Clear();
            foreach(var group in q)
            {
                TreeNode node = this.treeView1.Nodes.Add($"{group.MyPrice.ToString()}  ({group.Count})");
                foreach(var item in group.MyGroup)
                {
                    node.Nodes.Add($"{item.ProductID}_{item.ProductName}");
                }

            }

        }
        private object PriceKey(decimal? n)
        {
            if (n < 10) 
            { 
                return "低價位"; 
            }
            else if (n < 40)
            {
                return "中價位";
                    }
            else 
            { 
                return "高價位"; 
            }
        }

        //=========================================
        private void button15_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            var q = from y in this.dbContext.Orders
                    group y by y.OrderDate.Value.Year into g
                    select new
                    {
                        年 = g.Key,
                        Count = g.Count(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                TreeNode node = this.treeView1.Nodes.Add($"{group.年.ToString()} ({group.Count})");
                foreach (var item in group.MyGroup)  //每個群下面的檔案
                {
                    node.Nodes.Add(item.CustomerID.ToString() +"  ,  " + item.OrderDate);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            var q = from y in this.dbContext.Orders
                    group y by new { y.OrderDate.Value.Year, y.OrderDate.Value.Month } into g
                    orderby g.Key
                    //new 一個新個grouping方式
                    select new
                    {
                        MyYear = g.Key.Year,    //從key下面導出年 月
                        MyMonth = g.Key.Month,
                        Count = g.Count(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();
            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                TreeNode node = this.treeView1.Nodes.Add($"{group.MyYear.ToString()}/{group.MyMonth}({group.Count})");
                foreach (var item in group.MyGroup)  //每個群下面的檔案
                {
                    node.Nodes.Add(item.OrderDate.Value.Month.ToString() + "  ,  " + item.OrderID);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var q = (from od in dbContext.Order_Details
                         //select od.UnitPrice * od.Quantity * (1 - od.Discount)).Sum();   //TODO 計算Discount要怎麼轉型?
                     select (double)od.UnitPrice * od.Quantity * (1 - od.Discount)).Sum();    //轉型寫在最前面

            MessageBox.Show("總銷售額 = " +q);
            //dataGridView1.DataSource = q.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q = (from n in dbContext.Order_Details
                     group n by n.Order.EmployeeID into g
                     orderby g.Sum(n => n.UnitPrice * n.Quantity) descending
                     select new
                    {
                        ID=g.Key,
                        SaleAmount=(g.Sum(n=>(double)n.UnitPrice*n.Quantity*(1-n.Discount)))
                    }).Take(5);
            dataGridView1.DataSource = q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var q = (from c in dbContext.Categories
                     from n in c.Products                 //這邊要用導覽屬性
                     orderby n.UnitPrice descending
                     where n.UnitPrice != null
                    //group c by c.CategoryName into g
                    select new
                    {
                      c.CategoryName,
                        n.ProductName,
                        n.UnitPrice
                        //Price = g.OrderByDescending(n=>n.UnitPrice),
                    }).Take(5);
            dataGridView1.DataSource = q.ToList();
        }
        
        private void button7_Click(object sender, EventArgs e)
        {
            //var q = from n in dbContext.Products
            //        where n.UnitPrice >= 300
            //        select n;
            //dataGridView1.DataSource = q.ToList();
            //if (q.Count() == 0)
            //{
            //    MessageBox.Show("大於300數量=" + q.Count());
            //}
            //else
            //{
            //    MessageBox.Show("大於300數量 =" + q.Count());
            //}
            //bool result = db.Products.Select(p => p.UnitPrice).Any(p => p > 300);
            bool result = dbContext.Products.Any(p => p.UnitPrice > 300);
            MessageBox.Show(result ? "有大於300" : "沒有大於300");

        }
    }
}
