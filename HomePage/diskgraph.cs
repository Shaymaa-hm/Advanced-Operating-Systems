using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HomePage
{
    public partial class diskgraph : Form
    {
        List<int> allvals;
        List<PointF> alldots = new List<PointF>();
        List<PointF> allXS = new List<PointF>();
        List<int> orderedvalls;
        PointF Lstart, Lend;
        PointF temp;
        Bitmap off;
        int total,Head;
        bool containshead = true;
        public diskgraph()
        {
            InitializeComponent();
            Load += Diskgraph_Load;
            Paint += Diskgraph_Paint;
        }

        private void Diskgraph_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }

        public void passingvals(List<int> values , int count,int head)
        {
            total = count;
            allvals = new List<int>(values);
            orderedvalls = new List<int>(values);
            if (!orderedvalls.Contains(0))
            { orderedvalls.Add(0); }
            Head = head;
            if (!orderedvalls.Contains(head))
            { orderedvalls.Add(head);
                containshead = false;
            }
            orderedvalls.Sort();
        }
        private void Diskgraph_Load(object sender, EventArgs e)
        {
            Size = new Size(900, 600);
            off = new Bitmap(this.Width, this.Height);
            Lstart.X = 30;
            Lstart.Y = 30;
            Lend.X = this.Width - 35;
            Lend.Y = 30;

            for (int i=0;i< orderedvalls.Count; i++)
            {
                temp.X = ( i *((Lend.X - Lstart.X) / (orderedvalls.Count-1)))+30;
                temp.Y = 30;
                allXS.Add(temp);
            }
            for(int i = 0;i<orderedvalls.Count;i++)
            {
                if (orderedvalls[i] == Head && containshead == false)
                {
                    temp.X = allXS[i].X;
                    temp.Y = 50;
                    alldots.Add(temp);
                }
                if (orderedvalls[i] == Head && containshead == true && Head != allvals[0])
                {
                    temp.X = allXS[i].X;
                    temp.Y = 50;
                    alldots.Add(temp);
                }
            }
            for(int i=0; i<allvals.Count; i++)
            {
                for (int k = 0; k < orderedvalls.Count; k++)
                {
                    if (containshead == false)
                    {
                        if (i == 0)
                        {

                            if (orderedvalls[k] == allvals[i])
                            {
                                temp.X = allXS[k].X;
                                temp.Y = ((i+2) * 40) + (50);
                                alldots.Add(temp);
                            }
                        }
                        if (orderedvalls[k] == allvals[i] && i!=0)
                        {
                            temp.X = allXS[k].X;
                            temp.Y = ((i + 2) * 40) + (50);
                            alldots.Add(temp);
                           // break;
                        }
                    }
                    if (containshead == true)
                    {
                        if (orderedvalls[k] == allvals[i] && containshead == true)
                        {
                            temp.X = allXS[k].X;
                            temp.Y = ((i+1) * 40) + (50);
                            alldots.Add(temp);
                            //break;
                        }
                    }

                }
            }
            DrawDubb(CreateGraphics());
        }

        void DrawScene(Graphics g)
        {
            g.Clear(Color.White);
            g.DrawLine(Pens.Black, Lstart, Lend);
            var font = new Font(FontFamily.GenericSansSerif, 10);
            string str;
            for (int i = 0; i < orderedvalls.Count; i++)
            {
                str = orderedvalls[i].ToString();

                g.DrawString(str, font, Brushes.Black, allXS[i].X - (font.Size * str.Length)/2, allXS[i].Y );
            }

            for (int i=0;i<alldots.Count;i++)
            {
                g.FillEllipse(Brushes.Purple, alldots[i].X-5,alldots[i].Y-5,10,10);
                if (i != alldots.Count - 1)
                {
                    g.DrawLine(Pens.Purple, alldots[i], alldots[i+1]);
                }
            }
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
