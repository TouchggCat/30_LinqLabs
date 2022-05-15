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

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();
            dbContext.Database.Log = Console.Write;   //觀察SQL指令(在輸出視窗)
        }
        NorthwindEntities dbContext = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            var q = from p in dbContext.Products
                    where p.UnitPrice > 30
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
            this.dataGridView1.DataSource = dbContext.Categories.First().Products.ToList();
            MessageBox.Show(dbContext.Products.First().Category.CategoryName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource=this.dbContext.Sales_by_Year(new DateTime(1997, 1, 1), DateTime.Now).ToList();
            
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products.AsEnumerable()   //AsEnumerable()加入此方法就可翻譯格式化$,且group by 裡的方法為可列舉值
                    orderby p.UnitsInStock descending, p.ProductID    //關注UnisInStock 數量112重複，再來次要用ID排序
                    select new  //new一個匿名型別
                    {
                        p.ProductID,
                        p.ProductName,
                        p.UnitPrice,
                        p.UnitsInStock,
                        TotalPrice = $"{p.UnitPrice * p.UnitsInStock:C2}"   //無法直接用$格式化:C2
                    };
            this.dataGridView1.DataSource = q.ToList();
            //===================================
            var q2 = dbContext.Products.OrderByDescending(p => p.UnitsInStock).ThenBy(p => p.ProductID);
            this.dataGridView2.DataSource = q2.ToList();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products
                    select new
                    {
                        p.CategoryID,
                        p.Category.CategoryName,    //!!導覽屬性
                        p.ProductName,
                        p.UnitPrice
                    };
            this.dataGridView3.DataSource = q.ToList();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //inner Join (o.o)
            var q = from c in this.dbContext.Categories
                    from p in c.Products    //這邊要用導覽屬性
                    select new
                    {
                        c.CategoryID,
                        c.CategoryName,
                        p.ProductName,
                        p.UnitPrice
                    };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products
                    group p by p.Category.CategoryName into g
                    select new
                    {
                        CategoryName=g.Key, 
                        AveragePrice=g.Average(p=>p.UnitPrice)
                    };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //bool? b;    //代表b允許空值
            //b = true;
            //b = false;
            //b = null;   //也就是可以這樣寫
            var q = from o in this.dbContext.Orders
                    group o by o.OrderDate.Value.Year into g   //OrderDate為 DateTime? <<允許空值
                    select new
                    {
                        Year = g.Key,
                        Count = g.Count()
                    };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            Product prod = new Product { ProductName = DateTime.Now.ToLongTimeString(), Discontinued = true };
            this.dbContext.Products.Add(prod);
            this.dbContext.SaveChanges();   //查看資料表
            this.Read_RefreshDataGridView();
        }
        void Read_RefreshDataGridView()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = this.dbContext.Products.ToList();

        }

        private void button56_Click(object sender, EventArgs e)
        {
            //update
            var product = (from p in this.dbContext.Products
                           where p.ProductName.Contains("Test")
                           select p).FirstOrDefault();

            if (product == null) return; //exit method

            product.ProductName = "Test" + product.ProductName;

            this.dbContext.SaveChanges();

            this.Read_RefreshDataGridView();
        }

        private void button53_Click(object sender, EventArgs e)
        {
            //delete one product
            var product = (from p in this.dbContext.Products
                           where p.ProductName.Contains("Test")
                           select p).FirstOrDefault();

            if (product == null) return;

            this.dbContext.Products.Remove(product);
            this.dbContext.SaveChanges();

            this.Read_RefreshDataGridView();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            var q2 = from c in this.dbContext.Categories
                     join p in this.dbContext.Products
                     on c.CategoryID equals p.CategoryID
                     select new { c.CategoryID, c.CategoryName, p.ProductName, p.UnitPrice };

            this.dataGridView2.DataSource = q2.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
    }
}
