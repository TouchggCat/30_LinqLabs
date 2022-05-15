using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs
{
    public partial class Frm考試 : Form
    {
        public Frm考試()
        {
            InitializeComponent();

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                          };
        }

        List<Student> students_scores;

        public class Student
        {
            public string Name { get; set; }
            public string Class { get;  set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get;  set; }
            public string Gender { get; set; }
        }
        int studentCount=0;
        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?			
            studentCount++;
            if (studentCount == 1)
            {
                this.chart1.Series.Clear();
                this.chart1.DataSource = null;
                var q = from n in students_scores
                        group n by n.Class into g
                        select new
                        {
                            班級 = g.Key,
                            學員成績數 = g.Count()
                        };
                this.chart1.Series.Add("各班學員成績數");
                this.chart1.DataSource = q.ToList();
                this.chart1.Series[0].XValueMember = "班級";
                this.chart1.Series[0].YValueMembers = "學員成績數";
                this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            }

			

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績						

            // 找出學員 'bbb' 的成績	                          

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	

            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |				
            // 數學不及格 ... 是誰 
            #endregion
            else if (studentCount == 2)
            {
                // 找出 前面三個 的學員所有科目成績		
                this.chart1.Series.Clear();
                this.chart1.DataSource = null;

                //var q2 = (from n in students_scores
                //          orderby n.Chi
                //          select new
                //          {
                //              名字=n.Name,
                //              國文 = n.Chi,
                //              英文 = n.Eng,
                //              數學 = n.Math
                //          }).Take(3);
                var q2 = students_scores.Take(3).Select(m => new
                {
                    名字 = m.Name,
                    國文 = m.Chi,
                    英文 = m.Eng,
                    數學 = m.Math
                });
                this.chart1.DataSource = q2.ToList();
                this.chart1.Series.Add("前3個學生國文成績");
                this.chart1.Series[0].XValueMember = "名字";
                this.chart1.Series[0].YValueMembers = "國文";
                this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                this.chart1.Series.Add("前3個學生英文成績");
                this.chart1.Series[1].XValueMember = "名字";
                this.chart1.Series[1].YValueMembers = "英文";
                this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                this.chart1.Series.Add("前3個學生數學成績");
                this.chart1.Series[2].XValueMember = "名字";
                this.chart1.Series[2].YValueMembers = "數學";
                this.chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            }
            else if (studentCount == 3)
            {
                // 找出 後面兩個 的學員所有科目成績		
                this.chart1.Series.Clear();
                this.chart1.DataSource = null;
                var q2 = students_scores.Skip(students_scores.Count()-2).Take(2)/*.OrderByDescending(n => n.Chi).ThenByDescending(n => n.Eng).ThenByDescending(n => n.Math)*/
                    .Select(m => new
                {
                    名字 = m.Name,
                    國文 = m.Chi,
                    英文 = m.Eng,
                    數學 = m.Math
                });
                this.chart1.DataSource = q2.ToList();
                this.chart1.Series.Add("後2個學生國文成績");
                this.chart1.Series[0].XValueMember = "名字";
                this.chart1.Series[0].YValueMembers = "國文";
                this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                this.chart1.Series.Add("後2個學生英文成績");
                this.chart1.Series[1].XValueMember = "名字";
                this.chart1.Series[1].YValueMembers = "英文";
                this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                this.chart1.Series.Add("後2個學生數學成績");
                this.chart1.Series[2].XValueMember = "名字";
                this.chart1.Series[2].YValueMembers = "數學";
                this.chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            }
        }


        private void button37_Click(object sender, EventArgs e)
        {
            //個人 sum, min, max, avg

            //各科 sum, min, max, avg
        }
        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
        }

        private void button35_Click(object sender, EventArgs e)
        {
            // 統計 :　所有隨機分數出現的次數/比率; sort ascending or descending
            // 63     7.00%
            // 100    6.00%
            // 78     6.00%
            // 89     5.00%
            // 83     5.00%
            // 61     4.00%
            // 64     4.00%
            // 91     4.00%
            // 79     4.00%
            // 84     3.00%
            // 62     3.00%
            // 73     3.00%
            // 74     3.00%
            // 75     3.00%
        }
        NorthwindEntities dbContext = new NorthwindEntities();
        int count = 0;
        private void button34_Click(object sender, EventArgs e)
        {
            // 年度最高銷售金額 年度最低銷售金額
       

            // 每年 總銷售分析 圖
            // 每月 總銷售分析 圖.
            //==============================================
            count++; //按一次切換
            if (count == 1)
            {
                dataGridView1.DataSource = null;
                this.chart1.Series.Clear();
                this.chart1.DataSource = null;
                this.chart1.Series.Add("年銷售額");
                var q = from od in dbContext.Order_Details.AsEnumerable()
                        group od by od.Order.OrderDate.Value.Year into g
                        orderby g.Key 
                        select new
                        {
                            年份 = g.Key,
                            銷售額 = g.Sum(w => /*(double)*/w.UnitPrice * w.Quantity)
                            //select new 可不加(double)
                        };
                dataGridView1.DataSource = q.ToList();
                //this.chart1.Series[0].Name = "年度銷售額";       //這裡不能這樣寫?
                this.chart1.DataSource = q.ToList();           //只能加入一次var q ??
                this.chart1.Series[0].XValueMember = "年份";
                this.chart1.Series[0].YValueMembers = "銷售額";          //跟key的字要相同
                this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            }
            //======================================
            else if (count == 2)
            {
                dataGridView1.DataSource = null;
                this.chart1.Series.Clear();
                this.chart1.DataSource = null;
                this.chart1.Series.Add("月銷售額");
                var qq = from od in dbContext.Order_Details.AsEnumerable()
                         group od by od.Order.OrderDate.Value.Month into g
                         orderby g.Key
                         select new
                         {
                             月份 = g.Key,
                             銷售額 = g.Sum(bbb => bbb.UnitPrice * bbb.Quantity)
                         };
                dataGridView1.DataSource = qq.ToList();
                this.chart1.DataSource = qq.ToList();
                //this.chart1.Name = "每月銷售額";
                this.chart1.Series[0].XValueMember = "月份";
                chart1.Series[0].YValueMembers = "銷售額";
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            }
       
            // 年度最高銷售金額 年度最低銷售金額
            else if (count == 3)
            {
                dataGridView1.DataSource = null;
                this.chart1.Series.Clear();
                this.chart1.DataSource = null;
                this.chart1.Series.Add("年銷售最高");
                this.chart1.Series.Add("年銷售最低");    //加入第二個Series
                var qq = from od in dbContext.Order_Details.AsEnumerable()
                         group od by od.Order.OrderDate.Value.Year into g
                         orderby g.Key
                         select new
                         {
                             年份 = g.Key,
                             最高銷售額 = g.Max(bbb => bbb.UnitPrice * bbb.Quantity),
                             最低銷售額 = g.Min(ccc => ccc.UnitPrice * ccc.Quantity)
                         };
                this.chart1.DataSource = qq.ToList();
                this.chart1.Series[0].XValueMember = "年份";
                this.chart1.Series[0].YValueMembers = "最高銷售額";
                this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                this.chart1.DataSource = qq.ToList();
                this.chart1.Series[1].XValueMember = "年份";
                this.chart1.Series[1].YValueMembers = "最低銷售額";
                this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            }
            //那一年總銷售最好? 那一年總銷售最不好 ?  
            // 那一個月總銷售最好? 那一個月總銷售最不好 ?
            else if (count == 4)
            {
                //this.chart1.DataSource = null;
                //this.chart1.Series.Clear();
                //this.chart1.Series.Add("銷售最差年份");
                //this.chart1.Series.Add("銷售最低年份");
                var q = (from o in dbContext.Order_Details.AsEnumerable()
                         group o by o.Order.OrderDate.Value.Year into g
                         orderby g.Key
                         select new
                         {
                             年份 = g.Key,
                             銷售額 = g.Sum(n => n.UnitPrice * n.Quantity)
                         }).OrderByDescending(n => n.銷售額).Select(m => m.年份).FirstOrDefault();    //.Max(n => n.年份)    最高數字的年份     是要問最高銷售額
        
                MessageBox.Show("銷售最佳年份:" + q.ToString());
            }
            else if (count == 5)
            {
                var q = (from o in dbContext.Order_Details.AsEnumerable()
                         group o by o.Order.OrderDate.Value.Year into g
                         orderby g.Key
                         select new
                         {
                             年份 = g.Key,
                             銷售額 = g.Sum(n => n.UnitPrice * n.Quantity)
                         }).OrderByDescending(n => n.銷售額).Select(m => m.年份).LastOrDefault();   

                MessageBox.Show("銷售最差年份:" + q.ToString());
                count = 0;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
