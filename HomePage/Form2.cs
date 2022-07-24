//#include <bits/stdc++.h>;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HomePage
{
    public partial class Form2 : Form
    {
        DataTable tablehidden = new DataTable("table");
        int no_of_frames, no_of_pages, flag1, flag2, flag3, i, j, k, pos, max, faults = 0;
        int[] frames = new int[15];
        int[] temp = new int[15];
        List<int> pages = new List<int>();
        public Form2()
        {
            InitializeComponent();
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46&& e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {


 
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty && textBox2.Text != string.Empty)
            {
                no_of_frames = Int32.Parse(textBox1.Text);
                for (int i = 0; i < no_of_frames; i++)
                {
                    tablehidden.Columns.Add("Frame" + i , Type.GetType("System.Int32"));
                }
                string numbers = textBox2.Text;
                string[] items = numbers.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for(int i =0;i<items.Length;i++)
                {
                    pages.Add(Int32.Parse(items[i]));
                }
                no_of_pages = pages.Count;
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;

            }
            else
            {
                MessageBox.Show("Please enter all fields!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        
        }

        public void docalculations()
        {
            //filling first frames with -1
            for (i = 0; i < no_of_frames; ++i)
            {
                frames[i] = -1;
            }

            for (i = 0; i < no_of_pages; ++i)
            {
                //resetflags
                flag1 = flag2 = 0;

                for (j = 0; j < no_of_frames; ++j)
                {
                    //check if number allready exists
                    if (frames[j] == pages[i])
                    {
                        flag1 = flag2 = 1;
                        break;
                    }
                }

                if (flag1 == 0)
                {
                    //when i find an empty frame replace it with the number and increase the miss counter
                    for (j = 0; j < no_of_frames; ++j)
                    {
                        if (frames[j] == -1)
                        {
                            faults++;
                            frames[j] = pages[i];
                            flag2 = 1;
                            break;
                        }
                    }
                }

                if (flag2 == 0)
                {
                    //in case there were no empty frames
                    flag3 = 0;

                    for (j = 0; j < no_of_frames; ++j)
                    {
                        temp[j] = -1;//create temporary new page

                        for (k = i + 1; k < no_of_pages; ++k)
                        { //check "future pages"
                            if (frames[j] == pages[k])
                            {
                                temp[j] = k;
                                //put position of hit in temp array
                                break;
                            }
                        }
                    }

                    for (j = 0; j < no_of_frames; ++j)
                    {
                        if (temp[j] == -1)
                        {
                            pos = j;
                            flag3 = 1;
                            break;
                        }
                    }

                    if (flag3 == 0)
                    {
                        max = temp[0];
                        pos = 0;

                        for (j = 1; j < no_of_frames; ++j)
                        {
                            if (temp[j] > max)
                            {
                                max = temp[j];
                                pos = j;
                            }
                        }
                    }
                    frames[pos] = pages[i];
                    faults++;
                }
                if(flag1!=1||flag2!=1)
                {
                    DataRow newRow = tablehidden.NewRow();
                    for (j = 0; j < no_of_frames; ++j)
                    {
                        newRow[j] = frames[j];
                    }
                    tablehidden.Rows.Add(newRow);

                }

            }

            DataTable showntable = new DataTable();

            for (int i = 0; i < tablehidden.Rows.Count; i++)
                showntable.Columns.Add("page fault "+(i+1));

            for (int i = 0; i < tablehidden.Columns.Count; i++)
            {
                DataRow newRow = showntable.NewRow();

                newRow[0] = tablehidden.Columns[i].Caption;
                for (int j = 0; j < tablehidden.Rows.Count; j++)
                    newRow[j] = tablehidden.Rows[j][i];
                showntable.Rows.Add(newRow);
            }
            dataGridView1.DataSource = showntable;
            //total misses
            label13.Text = faults.ToString();

            int hits = no_of_pages - faults;
            label7.Text = hits.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            docalculations();

        }

    }
}

