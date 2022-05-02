using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            this.productPhotoTableAdapter1.Fill(nwDataSet11.ProductPhoto);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = nwDataSet11.ProductPhoto;

        }

        private void Frm作業_2_Load(object sender, EventArgs e)
        {
            var q = nwDataSet11.ProductPhoto.OrderBy(n => n.ModifiedDate.Year).Select(n => n.ModifiedDate.Year).Distinct();
            //var q = (from n in nwDataSet11.ProductPhoto
            //         select n.ModifiedDate.Year).Distinct();

            //this.comboBox3.Items.Add(q.ToList());       Add=>物件  q.ToList =>集合
            this.comboBox3.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var q = nwDataSet11.ProductPhoto.
                Where(n => n.ModifiedDate.Date >= this.dateTimePicker1.Value && n.ModifiedDate.Date <= this.dateTimePicker2.Value).
                Select(n => n);
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var q = nwDataSet11.ProductPhoto.
                Where(n => n.ModifiedDate.Year == Convert.ToInt32(this.comboBox3.SelectedItem)).
                Select(n => n);
            dataGridView1.DataSource = q.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int x = comboBox2.SelectedIndex;
            int sStart = 1 + 3 * x;
            int sEnd = 3 + 3 * x;

            var q = nwDataSet11.ProductPhoto.
                Where(n => n.ModifiedDate.Month>=sStart&&n.ModifiedDate.Month<=sEnd).
                OrderBy(n=>n.ModifiedDate.Year).
                Select(n => n);
            dataGridView1.DataSource = q.ToList();
        }

        //CurrentCellChanged，dataGridView的事件方法
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null)
            {
                //什麼都不做
            }
            else
            {
                byte[] bytes = (byte[])this.dataGridView1.CurrentRow.Cells[3].Value;   //第三個欄位
                MemoryStream ms = new MemoryStream(bytes);
                pictureBox1.Image = Image.FromStream(ms);
            }
        }
    }
}
