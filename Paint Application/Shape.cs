using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint_Application
{
    // ================================================================
    // ENUM
    // ================================================================
    public enum DrawTool
    {
        Select,
        Freehand,
        Line,
        Rectangle,
        FilledRectangle,
        Ellipse,
        FilledEllipse,
        Circle,
        FilledCircle
    }

    // ================================================================
    // BASE CLASS
    // ================================================================
    public abstract class Shape
    {
        public Pen Pen { get; set; }
        public Brush Brush { get; set; }
        public bool IsSelected { get; set; }

        protected Shape(Pen pen, Brush brush)
        {
            Pen = (Pen)pen.Clone();
            Brush = (brush != null) ? (Brush)brush.Clone() : null;
        }

        public abstract void Draw(Graphics g);
        public abstract bool HitTest(Point p);
        public abstract void Move(int dx, int dy);
        public abstract Rectangle GetBounds();

        protected void DrawSelection(Graphics g)
        {
            if (!IsSelected) return;
            Rectangle r = GetBounds();
            r.Inflate(4, 4);
            using (Pen selPen = new Pen(Color.DodgerBlue, 1.5f))
            {
                selPen.DashStyle = DashStyle.Dash;
                g.DrawRectangle(selPen, r);
            }
        }
    }

    // ================================================================
    // LINE
    // ================================================================
    public class LineShape : Shape
    {
        public Point P1, P2;

        public LineShape(Point p1, Point p2, Pen pen) : base(pen, null)
        {
            P1 = p1;
            P2 = p2;
        }

        public override void Draw(Graphics g)
        {
            g.DrawLine(Pen, P1, P2);
            DrawSelection(g);
        }

        public override bool HitTest(Point p)
        {
            float dx = P2.X - P1.X;
            float dy = P2.Y - P1.Y;
            float len2 = dx * dx + dy * dy;
            if (len2 == 0) return false;
            float t = ((p.X - P1.X) * dx + (p.Y - P1.Y) * dy) / len2;
            t = Math.Max(0, Math.Min(1, t));
            float px = P1.X + t * dx;
            float py = P1.Y + t * dy;
            float dist = (float)Math.Sqrt((p.X - px) * (p.X - px) + (p.Y - py) * (p.Y - py));
            return dist < Math.Max(Pen.Width / 2f + 3, 5);
        }

        public override void Move(int dx, int dy)
        {
            P1 = new Point(P1.X + dx, P1.Y + dy);
            P2 = new Point(P2.X + dx, P2.Y + dy);
        }

        public override Rectangle GetBounds()
        {
            return Rectangle.FromLTRB(
                Math.Min(P1.X, P2.X), Math.Min(P1.Y, P2.Y),
                Math.Max(P1.X, P2.X), Math.Max(P1.Y, P2.Y));
        }
    }

    // ================================================================
    // RECTANGLE
    // ================================================================
    public class RectShape : Shape
    {
        public Rectangle Rect;
        public bool Filled;

        public RectShape(Rectangle r, Pen pen, Brush brush, bool filled) : base(pen, brush)
        {
            Rect = r;
            Filled = filled;
        }

        public override void Draw(Graphics g)
        {
            if (Filled && Brush != null) g.FillRectangle(Brush, Rect);
            g.DrawRectangle(Pen, Rect);
            DrawSelection(g);
        }

        public override bool HitTest(Point p)
        {
            Rectangle r = Rect;
            r.Inflate(4, 4);
            return r.Contains(p);
        }

        public override void Move(int dx, int dy)
        {
            Rect = new Rectangle(Rect.X + dx, Rect.Y + dy, Rect.Width, Rect.Height);
        }

        public override Rectangle GetBounds()
        {
            return Rect;
        }
    }

    // ================================================================
    // ELLIPSE
    // ================================================================
    public class EllipseShape : Shape
    {
        public Rectangle Rect;
        public bool Filled;

        public EllipseShape(Rectangle r, Pen pen, Brush brush, bool filled) : base(pen, brush)
        {
            Rect = r;
            Filled = filled;
        }

        public override void Draw(Graphics g)
        {
            if (Filled && Brush != null) g.FillEllipse(Brush, Rect);
            g.DrawEllipse(Pen, Rect);
            DrawSelection(g);
        }

        public override bool HitTest(Point p)
        {
            float cx = Rect.X + Rect.Width / 2f;
            float cy = Rect.Y + Rect.Height / 2f;
            float rx = Rect.Width / 2f + 4;
            float ry = Rect.Height / 2f + 4;
            if (rx <= 0 || ry <= 0) return false;
            float val = ((p.X - cx) * (p.X - cx)) / (rx * rx)
                      + ((p.Y - cy) * (p.Y - cy)) / (ry * ry);
            return val <= 1;
        }

        public override void Move(int dx, int dy)
        {
            Rect = new Rectangle(Rect.X + dx, Rect.Y + dy, Rect.Width, Rect.Height);
        }

        public override Rectangle GetBounds()
        {
            return Rect;
        }
    }

    // ================================================================
    // CIRCLE (ellipse vuong)
    // ================================================================
    public class CircleShape : EllipseShape
    {
        public CircleShape(Rectangle r, Pen pen, Brush brush, bool filled)
            : base(MakeSquare(r), pen, brush, filled) { }

        private static Rectangle MakeSquare(Rectangle r)
        {
            int s = Math.Min(Math.Abs(r.Width), Math.Abs(r.Height));
            return new Rectangle(r.X, r.Y, s, s);
        }
    }

    // ================================================================
    // FREEHAND
    // ================================================================
    public class FreehandShape : Shape
    {
        public List<Point> Points = new List<Point>();

        public FreehandShape(Pen pen) : base(pen, null) { }

        public void AddPoint(Point p)
        {
            Points.Add(p);
        }

        public override void Draw(Graphics g)
        {
            if (Points.Count < 2) return;
            g.DrawLines(Pen, Points.ToArray());
            DrawSelection(g);
        }

        public override bool HitTest(Point p)
        {
            Rectangle b = GetBounds();
            b.Inflate(8, 8);
            return b.Contains(p);
        }

        public override void Move(int dx, int dy)
        {
            for (int i = 0; i < Points.Count; i++)
                Points[i] = new Point(Points[i].X + dx, Points[i].Y + dy);
        }

        public override Rectangle GetBounds()
        {
            if (Points.Count == 0) return Rectangle.Empty;
            int x1 = int.MaxValue, y1 = int.MaxValue;
            int x2 = int.MinValue, y2 = int.MinValue;
            foreach (Point p in Points)
            {
                if (p.X < x1) x1 = p.X;
                if (p.Y < y1) y1 = p.Y;
                if (p.X > x2) x2 = p.X;
                if (p.Y > y2) y2 = p.Y;
            }
            return Rectangle.FromLTRB(x1, y1, x2, y2);
        }
    }

    // ================================================================
    // GROUP
    // ================================================================
    public class GroupShape : Shape
    {
        public List<Shape> Children;

        public GroupShape(List<Shape> children) : base(new Pen(Color.Transparent), null)
        {
            Children = children;
        }

        public override void Draw(Graphics g)
        {
            foreach (Shape c in Children) c.Draw(g);
            DrawSelection(g);
        }

        public override bool HitTest(Point p)
        {
            foreach (Shape c in Children)
                if (c.HitTest(p)) return true;
            return false;
        }

        public override void Move(int dx, int dy)
        {
            foreach (Shape c in Children) c.Move(dx, dy);
        }

        public override Rectangle GetBounds()
        {
            if (Children.Count == 0) return Rectangle.Empty;
            Rectangle b = Children[0].GetBounds();
            for (int i = 1; i < Children.Count; i++)
                b = Rectangle.Union(b, Children[i].GetBounds());
            return b;
        }
    }
}