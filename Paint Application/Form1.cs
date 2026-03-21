using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint_Application
{
    public partial class Form1 : Form
    {
        // ============================================================
        // FIELDS
        // ============================================================
        private List<Shape> shapes = new List<Shape>();
        private List<Shape> selectedShapes = new List<Shape>();

        private DrawTool currentTool = DrawTool.Freehand;
        private bool isDrawing = false;
        private bool isDragging = false;
        private Point startPoint;
        private Point lastMousePos;

        private FreehandShape currentFreehand = null;
        private Shape previewShape = null;

        private Pen myPen;
        private Brush myBrush;
        private Color penColor = Color.Black;
        private Color brushColor = Color.SkyBlue;

        private Stack<List<Shape>> undoStack = new Stack<List<Shape>>();
        private Stack<List<Shape>> redoStack = new Stack<List<Shape>>();

        private Button activeBtn = null;

        // ============================================================
        // CONSTRUCTOR
        // ============================================================
        public Form1()
        {
            InitializeComponent();

            // Pen & Brush mac dinh
            myPen = new Pen(penColor, 3);
            myPen.StartCap = LineCap.Round;
            myPen.EndCap = LineCap.Round;
            myBrush = new SolidBrush(brushColor);

            // ComboBox items
            cboDash.Items.AddRange(new string[] { "Solid", "Dash", "Dot", "DashDot" });
            cboDash.SelectedIndex = 0;

            cboFill.Items.AddRange(new string[] { "Solid", "Horizontal", "Vertical", "Cross" });
            cboFill.SelectedIndex = 0;

            // Mau nut ban dau
            btnPenColor.BackColor = penColor;
            btnPenColor.ForeColor = Color.White;
            btnBrushColor.BackColor = brushColor;

            // Gan events cho canvas
            pnlCanvas.MouseDown += Canvas_MouseDown;
            pnlCanvas.MouseMove += Canvas_MouseMove;
            pnlCanvas.MouseUp += Canvas_MouseUp;
            pnlCanvas.Paint += Canvas_Paint;

            // Tool mac dinh
            activeBtn = btnFreehand;
            HighlightBtn(btnFreehand);

            // Phim tat
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
        }

        // ============================================================
        // TOOL BUTTONS
        // ============================================================
        private void btnSelect_Click(object sender, EventArgs e)
        {
            SetTool(DrawTool.Select, (Button)sender);
        }

        private void btnFreehand_Click(object sender, EventArgs e)
        {
            SetTool(DrawTool.Freehand, (Button)sender);
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            SetTool(DrawTool.Line, (Button)sender);
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            SetTool(DrawTool.Rectangle, (Button)sender);
        }

        private void btnFillRect_Click(object sender, EventArgs e)
        {
            SetTool(DrawTool.FilledRectangle, (Button)sender);
        }

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            SetTool(DrawTool.Ellipse, (Button)sender);
        }

        private void btnFillEllipse_Click(object sender, EventArgs e)
        {
            SetTool(DrawTool.FilledEllipse, (Button)sender);
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            SetTool(DrawTool.Circle, (Button)sender);
        }

        private void btnFillCircle_Click(object sender, EventArgs e)
        {
            SetTool(DrawTool.FilledCircle, (Button)sender);
        }

        private void SetTool(DrawTool tool, Button btn)
        {
            currentTool = tool;

            if (activeBtn != null)
                ResetBtn(activeBtn);

            activeBtn = btn;
            HighlightBtn(btn);

            pnlCanvas.Cursor = (tool == DrawTool.Select) ? Cursors.Default : Cursors.Cross;

            selectedShapes.Clear();
            foreach (Shape s in shapes)
            {
                s.IsSelected = false;
                // Xoa vien xanh ca ben trong GroupShape
                if (s is GroupShape grp)
                    foreach (Shape child in grp.Children)
                        child.IsSelected = false;
            }

            pnlCanvas.Invalidate();
        }

        private void HighlightBtn(Button b)
        {
            b.BackColor = Color.SteelBlue;
            b.ForeColor = Color.White;
            b.FlatStyle = FlatStyle.Flat;
        }

        private void ResetBtn(Button b)
        {
            b.BackColor = SystemColors.Control;
            b.ForeColor = Color.Black;
            b.FlatStyle = FlatStyle.Standard;
        }

        // ============================================================
        // PEN / BRUSH CONTROLS
        // ============================================================
        private void btnPenColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = penColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                penColor = colorDialog1.Color;
                myPen.Color = penColor;
                btnPenColor.BackColor = penColor;
                btnPenColor.ForeColor = (penColor.GetBrightness() < 0.5f) ? Color.White : Color.Black;
            }
        }

        private void btnBrushColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = brushColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                brushColor = colorDialog1.Color;
                btnBrushColor.BackColor = brushColor;
                RebuildBrush();
            }
        }

        private void trkSize_ValueChanged(object sender, EventArgs e)
        {
            myPen.Width = trkSize.Value;
        }

        private void cboDash_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboDash.SelectedIndex)
            {
                case 1: myPen.DashStyle = DashStyle.Dash; break;
                case 2: myPen.DashStyle = DashStyle.Dot; break;
                case 3: myPen.DashStyle = DashStyle.DashDot; break;
                default: myPen.DashStyle = DashStyle.Solid; break;
            }
        }

        private void cboFill_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebuildBrush();
        }

        private void RebuildBrush()
        {
            if (myBrush != null)
                myBrush.Dispose();

            switch (cboFill.SelectedIndex)
            {
                case 1: myBrush = new HatchBrush(HatchStyle.Horizontal, brushColor, Color.White); break;
                case 2: myBrush = new HatchBrush(HatchStyle.Vertical, brushColor, Color.White); break;
                case 3: myBrush = new HatchBrush(HatchStyle.Cross, brushColor, Color.White); break;
                default: myBrush = new SolidBrush(brushColor); break;
            }
        }

        // ============================================================
        // UNDO / REDO / CLEAR / GROUP / UNGROUP
        // ============================================================
        private void btnUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            SaveUndo();
            shapes.Clear();
            selectedShapes.Clear();
            pnlCanvas.Invalidate();
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            GroupSelected();
        }

        private void btnUngroup_Click(object sender, EventArgs e)
        {
            UngroupSelected();
        }

        private void SaveUndo()
        {
            undoStack.Push(new List<Shape>(shapes));
            redoStack.Clear();
        }

        private void Undo()
        {
            if (undoStack.Count == 0) return;
            redoStack.Push(new List<Shape>(shapes));
            shapes = undoStack.Pop();
            selectedShapes.Clear();
            pnlCanvas.Invalidate();
        }

        private void Redo()
        {
            if (redoStack.Count == 0) return;
            undoStack.Push(new List<Shape>(shapes));
            shapes = redoStack.Pop();
            selectedShapes.Clear();
            pnlCanvas.Invalidate();
        }

        private void GroupSelected()
        {
            if (selectedShapes.Count < 2) return;
            SaveUndo();

            GroupShape grp = new GroupShape(new List<Shape>(selectedShapes));
            foreach (Shape s in selectedShapes)
                shapes.Remove(s);

            shapes.Add(grp);
            selectedShapes.Clear();
            foreach (Shape s in shapes)
                s.IsSelected = false;
            grp.IsSelected = true;
            selectedShapes.Add(grp);
            pnlCanvas.Invalidate();
        }

        private void UngroupSelected()
        {
            List<Shape> toProcess = new List<Shape>(selectedShapes);
            foreach (Shape s in toProcess)
            {
                GroupShape grp = s as GroupShape;
                if (grp == null) continue;

                SaveUndo();
                int idx = shapes.IndexOf(grp);
                shapes.Remove(grp);
                shapes.InsertRange(idx, grp.Children);
                selectedShapes.Remove(grp);

                foreach (Shape c in grp.Children)
                {
                    c.IsSelected = true;
                    selectedShapes.Add(c);
                }
            }

            foreach (Shape s in shapes)
                s.IsSelected = false;
            selectedShapes.Clear();
            pnlCanvas.Invalidate();
        }

        // ============================================================
        // CANVAS MOUSE EVENTS
        // ============================================================
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            startPoint = e.Location;
            lastMousePos = e.Location;

            if (currentTool == DrawTool.Select)
            {
                Shape hit = null;
                for (int i = shapes.Count - 1; i >= 0; i--)
                {
                    if (shapes[i].HitTest(e.Location))
                    {
                        hit = shapes[i];
                        break;
                    }
                }

                // Nếu KHÔNG giữ Ctrl → xóa selection cũ
                if (!ModifierKeys.HasFlag(Keys.Control))
                {
                    foreach (Shape s in shapes)
                        s.IsSelected = false;
                    selectedShapes.Clear();
                }

                if (hit != null)
                {
                    // Nếu giữ Ctrl và shape đã được chọn → bỏ chọn
                    if (ModifierKeys.HasFlag(Keys.Control) && hit.IsSelected)
                    {
                        hit.IsSelected = false;
                        selectedShapes.Remove(hit);
                    }
                    else
                    {
                        hit.IsSelected = true;
                        if (!selectedShapes.Contains(hit))
                            selectedShapes.Add(hit);
                        isDragging = true;
                    }
                }

                pnlCanvas.Invalidate();
                return;
            }

            isDrawing = true;

            if (currentTool == DrawTool.Freehand)
            {
                SaveUndo();
                currentFreehand = new FreehandShape(myPen);
                currentFreehand.AddPoint(e.Location);
                shapes.Add(currentFreehand);
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && selectedShapes.Count > 0)
            {
                int dx = e.X - lastMousePos.X;
                int dy = e.Y - lastMousePos.Y;
                foreach (Shape s in selectedShapes)
                    s.Move(dx, dy);
                lastMousePos = e.Location;
                pnlCanvas.Invalidate();
                return;
            }

            if (!isDrawing) return;

            if (currentTool == DrawTool.Freehand && currentFreehand != null)
            {
                currentFreehand.AddPoint(e.Location);
                pnlCanvas.Invalidate();
            }
            else
            {
                previewShape = BuildShape(startPoint, e.Location);
                pnlCanvas.Invalidate();
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                return;
            }

            if (!isDrawing) return;
            isDrawing = false;

            if (currentTool != DrawTool.Freehand)
            {
                Shape s = BuildShape(startPoint, e.Location);
                if (s != null)
                {
                    SaveUndo();
                    shapes.Add(s);
                }
                previewShape = null;
            }

            currentFreehand = null;
            pnlCanvas.Invalidate();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (Shape s in shapes)
                s.Draw(e.Graphics);

            if (previewShape != null)
                previewShape.Draw(e.Graphics);
        }

        // ============================================================
        // HELPER METHODS
        // ============================================================
        private Shape BuildShape(Point p1, Point p2)
        {
            Rectangle r = MakeRect(p1, p2);

            switch (currentTool)
            {
                case DrawTool.Line: return new LineShape(p1, p2, myPen);
                case DrawTool.Rectangle: return new RectShape(r, myPen, null, false);
                case DrawTool.FilledRectangle: return new RectShape(r, myPen, myBrush, true);
                case DrawTool.Ellipse: return new EllipseShape(r, myPen, null, false);
                case DrawTool.FilledEllipse: return new EllipseShape(r, myPen, myBrush, true);
                case DrawTool.Circle: return new CircleShape(r, myPen, null, false);
                case DrawTool.FilledCircle: return new CircleShape(r, myPen, myBrush, true);
                default: return null;
            }
        }

        private Rectangle MakeRect(Point p1, Point p2)
        {
            return Rectangle.FromLTRB(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Max(p1.X, p2.X),
                Math.Max(p1.Y, p2.Y));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z) Undo();
            if (e.Control && e.KeyCode == Keys.Y) Redo();

            if (e.KeyCode == Keys.Delete)
            {
                SaveUndo();
                foreach (Shape s in selectedShapes)
                    shapes.Remove(s);
                selectedShapes.Clear();
                pnlCanvas.Invalidate();
            }
        }
    }
}