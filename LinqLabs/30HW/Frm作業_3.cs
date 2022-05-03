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
            TreeNode node;
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
    }
}
