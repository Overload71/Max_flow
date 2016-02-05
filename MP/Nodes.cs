using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP
{
    class Nodes
    {
        public Graphics formGraphics;
        int p = 0;
        public Point[] points = new Point[10]; //Nodes points
        public void set_nodes(int x,int y)
         {
             points[p].X = x;
             points[p].Y = y;
         }
        public Nodes(Panel panel1)
        {
            formGraphics = panel1.CreateGraphics();
            formGraphics.DrawRectangle(new Pen(Color.Blue, 5), panel1.ClientRectangle);
        }

        public void draw_sol(int A,int B,int s)
        {
            //SolidBrush br = new SolidBrush(Color.Black);
            //formGraphics2.FillEllipse(br, new Rectangle(points[A], new Size(40, 40)));
            //formGraphics2.DrawString(A.ToString(), new Font("Arial", 18), Brushes.White, new Point(points[A].X + 10, points[A].Y + 5));
            //formGraphics2.FillEllipse(br, new Rectangle(points[B], new Size(40, 40)));
            //formGraphics2.DrawString(B.ToString(), new Font("Arial", 18), Brushes.White, new Point(points[B].X + 10, points[B].Y + 5));
            //formGraphics2.DrawString(s.ToString(), new Font("Arial", 10), Brushes.Black, new Point((points[A].X + points[B].X) / 2, (points[A].Y + points[B].Y) / 2));
            //formGraphics2.DrawLine(new Pen(Color.Orange, 3), points_edge[A], points_edge[B]);
        }

        public void draw()
        {
          //for (int i = 0; i < Node_number; i++)
          //  {
                SolidBrush br = new SolidBrush(Color.Black);
                formGraphics.FillEllipse(br, new Rectangle(points[p], new Size(50, 50)));
                formGraphics.DrawString(p.ToString(), new Font("Arial", 24), Brushes.White, new Point(points[p].X + 10, points[p].Y + 5));
            //}
           // formGraphics.Dispose();
            p++;
        }

        public void Draw_edge(int A,int B,string s)
        {
           
            double DeltaX, DeltaY, r = 25;
            double h = (Convert.ToDouble(points[B].Y - points[A].Y) / Convert.ToDouble(points[B].X - points[A].X));
            DeltaX = r / (h + 1);
            DeltaY = ((points[B].Y - points[A].Y) * DeltaX) / (points[B].X - points[A].X);
            Point pA = new Point();
            Point pB = new Point();
            pA.X = points[A].X  +25;
            pA.Y = points[A].Y + 25;
            pB.X = points[B].X + 25;
            pB.Y = points[B].Y + 25;
            Pen pen = new Pen(Color.Orange,10);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            formGraphics.DrawLine(pen,pA,pB);
            formGraphics.DrawString(s, new Font("Arial", 18), Brushes.Black, new Point((points[A].X + points[B].X) / 2, (points[A].Y + points[B].Y) / 2));
        }
    }
}
