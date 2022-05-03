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
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();
           
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n1, n2;  //交換 by ref
            n1 = 100;
            n2 = 200;
            MessageBox.Show(n1 + "," + n2);
            Swap(ref n1,ref n2);
            MessageBox.Show(n1 + "," + n2);
            //======================
            string s1 = "aaaa";
            string s2 = "bbbb";
            MessageBox.Show(s1 + "," + s2);
            Swap(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);
        }
        //許多Swap =>方法的OverLoad 多載
        private void Swap(ref int a, ref int b)
        {
            int x = a;
            a = b;
            //int y = b;
            b= x;
            //a = y;
        }
        void Swap (ref string a , ref string b)
        {
            string s = a;
            a = b;
            b = s;
        }
        void Swap(ref Point n1, ref Point n2)
        {
            Point temp = n2;
            n2 = n1;
            n1 = temp;
        }
        private void SwapObject(ref object o1, ref object o2)
        {
            object obj = o2;
            o2 = o1;
            o1 = obj;
        }
        private void button5_Click(object sender, EventArgs e)
            //C#2.0 使用的anytype, 用Object ,但是麻煩在要轉型且耗效能
        {
            //int n1 = 100;
            //int n2 = 200;
            //MessageBox.Show(n1 + "," + n2);
            //SwapObject(ref n1, ref n2);   //TODO
            //MessageBox.Show(n1 + "," + n2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n1, n2;  //交換 by ref
            n1 = 100;
            n2 = 200;
            MessageBox.Show(n1 + "," + n2);
            //SwapAnyType<int>(ref n1, ref n2);   //有型別推斷 <int>可以不用
            SwapAnyType(ref n1, ref n2);
            MessageBox.Show(n1 + "," + n2);
            //======================
            string s1 = "aaaa";
            string s2 = "bbbb";
            MessageBox.Show(s1 + "," + s2);
            SwapAnyType(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);
        }

        private void SwapAnyType<T>(ref T a, ref T b)
            //傳任意型別的方法,  T為型別參數
        {

            T xxx = a;
            a = b;
            b = xxx;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //C#1.0 具名方法
            //this.buttonX.Click += ButtonX_Click;
            this.buttonX.Click += new EventHandler(aaa);
            this.buttonX.Click += bbb;   //syntax sugar
            //==============================
            //C#2.0 匿名方法  //如果只有限定buttonX本身使用則可用匿名方法(沒別的地方要用)
            this.buttonX.Click += delegate (object sender1, EventArgs e1)   //sender,e 會重複,要改
                                                       {
                                                           MessageBox.Show("匿名方法2.0");
                                                       };
            //===================================
            //C#3.0  匿名方法 lambda運算式    =>   goes to
            //this.buttonX.Click += (參數) => { 敘述};
            this.buttonX.Click += (object sender1, EventArgs e1) =>
           {
               MessageBox.Show("匿名方法3.0");
           };
        }

        private void bbb(object sender, EventArgs e)
        {
            MessageBox.Show("bbb啦");
        }

        private void aaa(object sender, EventArgs e)
        {
            MessageBox.Show("aaa啦");
        }

        private void ButtonX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("你按到ButtonX惹");
        }

        //Step 1: create delegate型別
        //Step 2: create delegate Object (new ...)
        //Step 3: invoke / call method
        delegate bool Mydelegate(int n);
        private void button9_Click(object sender, EventArgs e)
        {
            bool result = Test(6);
            MessageBox.Show("Result =  " + result);
            //===================================
            //建立委派 指向Test方法， 非call方法Test( )
            //Mydelegate delegateObj = Test;   //這樣寫也可以
          Mydelegate delegateObj = new Mydelegate(Test);
            result = delegateObj(4);    //call method(OOO)      小括號call方法
            MessageBox.Show("delegate Result = " + result);
            //===================================
            delegateObj = Test1;   //syntax sugar
            result = delegateObj(3);
            MessageBox.Show("delegate IsEven Result = " + result);
            //================================
           //C#2.0   匿名方法  小括號(參數)   大括號{敘述}
            delegateObj = delegate (int n)
            {
                return n > 5;
            };
            result = delegateObj(6);
            MessageBox.Show("C#2.0_Result =  " + result);
            //====================================
            //C#3.0 lambda運算式    =>   goes to
            //Lambda 運算式是建立委派最簡單的方法 (參數型別也沒寫 / return 也沒寫 => 非常高階的抽象)
            delegateObj =  n => n > 5;
            
            result = delegateObj(2);
            MessageBox.Show("C#3.0_Result =  " + result);
        }
        List<int> MyWhere(int[] nums, Mydelegate delegateObj) //Mydelegate delegateObj  委派當參數傳
        {
            //是否要把int陣列加入List要看傳進來的委派(true/false)   
            List<int> list = new List<int> ();
            foreach(int n in nums)
            {
               if(delegateObj(n))  //呼叫方法n>5  或n%2==0等等
                {
                    list.Add(n);
                }
            }
            return list;
        }
        bool Test(int n)
        {
            //if (n>5)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return n > 5;
        }
        bool Test1(int n) //, int x)
        {
            return n % 2 == 0;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> Large_list = MyWhere(nums, Test1);  //具名方法
            foreach(int n in Large_list)
            {
                this.listBox1.Items.Add(n);
            }
            //===================匿名方法
            List<int> list1 = MyWhere(nums, n=>n>5);  //return int n>5的方法(也就是匿名方法) //參數要是bool
            List<int> OddList = MyWhere(nums, n => n %2==1);
            List<int> EvenList = MyWhere(nums, n => n %2==0);
            this.listBox1.Items.Add("==============");
            foreach (int n in OddList)
            {
                this.listBox1.Items.Add(n);
            }
            foreach(int n in EvenList)
            {
                this.listBox2.Items.Add(n);
            }
        }

        IEnumerable<int> MyIterator(int[] nums, Mydelegate delegateObj)
        {
            foreach (int n in nums)
            {
                if (delegateObj(n)) //call方法
                {
                    yield return n;
                }
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> q = MyIterator(nums,n => n > 5);
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }

        }
        //Linq三步驟
        //Step 1: define Data Source
        //Step 2: define query
        //Step 3: execute query
        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //var q = from n in nums
            //        where n > 5
            //        select n;
            IEnumerable<int> q = nums.Where   /*<int>*/  (n => n > 5);
            foreach(int n in q)
            {
                this.listBox1.Items.Add(n);
            }
            //============================
            string[] words = { "aaa","bbbbbb","ccccccccc" };
            //var q = from n in nums
            //        where n > 5
            //        select n;
            var q1 = words.Where(w =>w.Length>3);
            foreach (string w in q1)
            {
                this.listBox2.Items.Add(w);
            }
            this.dataGridView1.DataSource = q1.ToList();       //q1為Length
            //=========================
            this.productsTableAdapter1.Fill(nwDataSet11.Products);
            //var q2 = from p in this.nwDataSet11.Products
            //         where p.UnitPrice > 30
            //         select p;
            var q2 = this.nwDataSet11.Products.Where(p => p.UnitPrice > 30);
            this.dataGridView2.DataSource=q2.ToList();
        }

        private void button45_Click(object sender, EventArgs e)
        {
            //var 懶得寫(x) 知道型別則盡量別用
            //========================
            //var 用在 型別難寫
            //var for 匿名型別
            var n = 100;
            var s = "aaaa";
            MessageBox.Show(s.ToUpper());
            //s.Length =
            var p = new Point(100, 350);
            MessageBox.Show(p.X + "," + p.Y);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            MyPoint pt1 = new MyPoint();  //constructor 建構子
            pt1.P1 = 100;  //set
            int w = pt1.P1; //get
            MessageBox.Show(pt1.P1.ToString());   //get
            //===================
            List<MyPoint> list = new List<MyPoint>();
            list.Add(new MyPoint());
            list.Add(new MyPoint(100));
            list.Add(new MyPoint(100,300));
            list.Add(new MyPoint("AAAAAA"));
      
            //{ }  大括號物件初始化
            list.Add(new MyPoint { P1 = 1, P2 = 1, Field1 = "aaaa", Field2 = "aaaa" });
            list.Add(new MyPoint { P1 = 99 });
            list.Add(new MyPoint { P1 = 13, P2 = 33});
            this.dataGridView1.DataSource = list;
            //{ }  大括號集合初始化
            List<MyPoint> list2 = new List<MyPoint>
            {
                new MyPoint{ P1 = 1, P2 = 1, Field1 = "xxx"},   //逗號分隔
                new MyPoint{P1=11,P2 =1},
                  new MyPoint{P1=111,P2 =2}
            };
            this.dataGridView2.DataSource = list2;
        }
        public class MyPoint
        {
            private int m_P1;
            public int P1
            {
                get
                {   //Logic.........
                    return m_P1;
                }
                set
                {   //Logic.......
                    m_P1 = value+100;
                    //用一個類別變數m_P1存P1
                }
            }  
            //類別變數有被實作，但繫結不上DataGridView，要寫成屬性才可以繫結
            //!!!可以想成有屬性才有欄位名稱
            public string Field1 = "xxx", Field2 = "yyyy";
            //準備許多建構子方法(很累)
            public MyPoint()
            {

            }
            public MyPoint(int p1)
            {
                this.P1 =p1;   //this為此類別的物件
            }
            public MyPoint(int p1,int p2)
            {
                this.P1 = p1;
                this.P2 = p2;
            }
            public MyPoint(string field1)
            {

            }
            public int P2 { get; set; }   //自動實作屬性
        }

        private void button43_Click(object sender, EventArgs e)
        {
            var x = new { P1 = 99, P2 = 77, P3 = 666 };  //匿名型別物件，型別不知用var
            var y = new { P1 = 69, P2 = 59, P3 = 555 };
            var z= new { userName = "hhh",passWord= "aaa" };
            this.listBox1.Items.Add(x.GetType());
            this.listBox1.Items.Add(y.GetType());   //x,y同一個type 但不知是三小
            this.listBox1.Items.Add(z.GetType());
            //============================
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };   //來源物件
            //var q = from n in nums  //型別不知 只能用var
            //        where n > 5
            //        select new { N = n, Squar = n * n, Cube = n * n * n };  //new一個匿名型別 //select投射來源物件到 目標物件
            var q = nums.Where(n => n > 5).Select(n => new { N = n, Squar = n * n, Cube = n * n * n });  //new一個匿名型別(投射)
            this.dataGridView1.DataSource=q.ToList();
            //==================================
            this.productsTableAdapter1.Fill(this.nwDataSet11.Products);  //TODO 重點
            //var q2 = from p in this.nwDataSet11.Products
            //         where p.UnitPrice > 30
            //         select new     //new一個匿名型別，屬性名自訂
            //         { 
            //             ID = p.ProductID, 
            //             產品名稱 = p.ProductName, 
            //             p.UnitPrice,    //屬性名不寫會推斷
            //             p.UnitsInStock, 
            //             TotalPrice=$"{(p.UnitPrice * p.UnitsInStock):C2}"   //無法推斷，一定要自己寫(價錢要格式化)
            //         };
            //this.dataGridView2.DataSource = q2.ToList();
            var q3 = nwDataSet11.Products.Where(p => p.UnitPrice > 30).
                Select(p => new
                {
                    ID = p.ProductID,
                    產品名稱 = p.ProductName,
                    p.UnitPrice,    //屬性名不寫會推斷
                    p.UnitsInStock,
                    TotalPrice = $"{p.UnitPrice * p.UnitsInStock:C2}"
                });
            this.dataGridView2.DataSource = q3.ToList();
        }

        private void button32_Click(object sender, EventArgs e)
        {
             string s1 = "abcd";
            //字串多一個WordCount方法，首先會想到用繼承，子類別(衍生類別derived)有父類別功能(屬性方法事件)，可以修正Override，父類別要用virtual
            //但是String為Sealed類別，無法被別人繼承
            int n = s1.WordCount();  //擴充方法   
            MessageBox.Show("WordCount = " + n);
            string s2 = "1234567890";
            n = s2.WordCount();
            MessageBox.Show("WordCount = " + n);
            //=========================
            char ch = s2.Chars(3);
            MessageBox.Show("Char =" + ch); 

        }
    }
    public static class MyStringExtend   //不能是巢狀類別(要在namespace)
    {
        public static int WordCount(this string s)  //this = s1/s2       //一定要加this
        {
            return s.Length;
        }
        public static char Chars(this string s, int index)
        {
            return s[index];
        }
    }
}
