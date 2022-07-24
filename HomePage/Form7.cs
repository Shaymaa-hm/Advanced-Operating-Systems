using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HomePage
{
    public partial class Form7 : Form
    {
        DataTable table = new DataTable("table");
        DataTable table2 = new DataTable("table");
        int ctp = 0;
        int ctb = 0;
       
        int n, m;
        int[] id=new int[15];
        int[] blockSize = new int[15];
        int[] processSize = new int[15];
        public Form7()
        {
            InitializeComponent();
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46 && ch > -1)
            {
                e.Handled = true;
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46 && ch > -1)
            {
                e.Handled = true;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46 && ch > -1)
            {
                e.Handled = true;
            }
        }
        private void Form7_Load(object sender, EventArgs e)
        {
            //input
            table.Columns.Add("Process ID", Type.GetType("System.Int32"));
            table.Columns.Add("Process Size", Type.GetType("System.Int32"));
            //output
            table.Columns.Add("Assigned Block Number", Type.GetType("System.Int32"));

            dataGridView1.DataSource = table;

            //input
            table2.Columns.Add("Partition ID", Type.GetType("System.Int32"));
            table2.Columns.Add("partition Size", Type.GetType("System.Int32"));

            dataGridView2.DataSource = table2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty)
            {
                n = Int32.Parse(textBox1.Text);
                label8.Text = textBox1.Text;
                m = Int32.Parse(textBox2.Text);
                labelblocks.Text = textBox2.Text;
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
            }
            if (textBox3.Text != string.Empty && textBox5.Text != string.Empty)
            {
                table.Rows.Add(textBox5.Text, textBox3.Text);
                
                id[ctp] = Int32.Parse(textBox5.Text);
                processSize[ctp] = Int32.Parse(textBox3.Text);
                textBox5.Text = string.Empty;
                textBox3.Text = string.Empty;
                ctp++;
            }
            if (textBox4.Text != string.Empty)
            {
                blockSize[ctb] = Int32.Parse(textBox4.Text);
                table2.Rows.Add((ctb+1), textBox4.Text);
                textBox4.Text = string.Empty;
                ctb++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Stores block id of the block
            // allocated to a process
            int[] allocation = new int[n];

            // Initially no block is assigned to
            // any process
            for (int i = 0; i < allocation.Length; i++)
                allocation[i] = -1;

            // pick each process and find suitable
            // blocks according to its size ad
            // assign to it
            for (int i = 0; i < n; i++)
            {

                // Find the best fit block for
                // current process
                int bestIdx = -1;
                for (int j = 0; j < m; j++)
                {
                    if (blockSize[j] >= processSize[i])
                    {
                        if (bestIdx == -1)
                            bestIdx = j;
                        else if (blockSize[bestIdx]
                                       > blockSize[j])
                            bestIdx = j;
                    }
                }

                // If we could find a block for
                // current process
                if (bestIdx != -1)
                {

                    // allocate block j to p[i]
                    // process
                    allocation[i] = bestIdx;

                    // Reduce available memory in
                    // this block.
                    blockSize[bestIdx] -= processSize[i];
                }
            }

            //Console.WriteLine("\nProcess No.\tProcess"
             //                   + " Size\tBlock no.");
            for (int i = 0; i < n; i++)
            {
                if (allocation[i] != -1)
                    dataGridView1.Rows[i].Cells[2].Value = allocation[i] + 1;
                else
                    dataGridView1.Rows[i].Cells[2].Value = 0;
            }
        }
    }
}
