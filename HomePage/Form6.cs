using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HomePage
{
    public partial class Form6 : Form
    {
        DataTable table = new DataTable("table");

        int[] arr = new int[15];
        List<int> values = new List<int>();
        int head;
        int ct = 0;
        int size;
        public Form6()
        {
            InitializeComponent();
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46 && ch > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46 && ch > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46 && ch > -1)
            {
                e.Handled = true;
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Request Queue", Type.GetType("System.Int32"));
            table.Columns.Add("Order of Served Requests", Type.GetType("System.Int32"));
            
            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                table.Rows.Add(textBox2.Text);
                arr[ct] = Int32.Parse(textBox2.Text);
                values.Add(arr[ct]);
                textBox2.Text = string.Empty;
                ct++;
            }
            if (textBox3.Text != string.Empty)
            {
                label8.Text = textBox3.Text;
                head = Int32.Parse(textBox3.Text);
                textBox3.Text = string.Empty;
            }
            if (textBox1.Text != string.Empty)
            {
                size = Int32.Parse(textBox1.Text);
                textBox1.Text = string.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int seek_count = 0;
            int distance, cur_track;

            for (int i = 0; i < size; i++)
            {
                cur_track = arr[i];

                // calculate absolute distance
                distance = Math.Abs(cur_track - head);

                // increase the total count
                seek_count += distance;

                // accessed track is now new head
                head = cur_track;
            }
            label5.Text = seek_count.ToString();
            for (int i = 0; i < size; i++)
            {
                dataGridView1.Rows[i].Cells[1].Value= arr[i];
            }

            diskgraph GRAPH = new diskgraph();
            GRAPH.passingvals(values, size, Int32.Parse(label8.Text));
            GRAPH.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
