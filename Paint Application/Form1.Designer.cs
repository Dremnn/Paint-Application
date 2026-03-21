namespace Paint_Application
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlCanvas = new Panel();
            pnlToolbar = new Panel();
            grpShapes = new GroupBox();
            btnSelect = new Button();
            btnFreehand = new Button();
            btnLine = new Button();
            btnRectangle = new Button();
            btnFillRect = new Button();
            btnEllipse = new Button();
            btnFillEllipse = new Button();
            btnCircle = new Button();
            btnFillCircle = new Button();
            grpTools = new GroupBox();
            btnPenColor = new Button();
            btnBrushColor = new Button();
            lblSize = new Label();
            trkSize = new TrackBar();
            lblDash = new Label();
            cboDash = new ComboBox();
            lblFill = new Label();
            cboFill = new ComboBox();
            btnUndo = new Button();
            btnRedo = new Button();
            btnGroup = new Button();
            btnUngroup = new Button();
            btnClear = new Button();
            colorDialog1 = new ColorDialog();
            pnlCanvas.SuspendLayout();
            pnlToolbar.SuspendLayout();
            grpShapes.SuspendLayout();
            grpTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkSize).BeginInit();
            SuspendLayout();
            // 
            // pnlCanvas
            // 
            pnlCanvas.BackColor = Color.White;
            pnlCanvas.BorderStyle = BorderStyle.FixedSingle;
            pnlCanvas.Controls.Add(pnlToolbar);
            pnlCanvas.Dock = DockStyle.Fill;
            pnlCanvas.Location = new Point(0, 0);
            pnlCanvas.Name = "pnlCanvas";
            pnlCanvas.Size = new Size(1282, 733);
            pnlCanvas.TabIndex = 0;
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = Color.FromArgb(45, 45, 50);
            pnlToolbar.Controls.Add(grpShapes);
            pnlToolbar.Controls.Add(grpTools);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Location = new Point(0, 0);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Size = new Size(1280, 120);
            pnlToolbar.TabIndex = 0;
            // 
            // grpShapes
            // 
            grpShapes.Controls.Add(btnSelect);
            grpShapes.Controls.Add(btnFreehand);
            grpShapes.Controls.Add(btnLine);
            grpShapes.Controls.Add(btnRectangle);
            grpShapes.Controls.Add(btnFillRect);
            grpShapes.Controls.Add(btnEllipse);
            grpShapes.Controls.Add(btnFillEllipse);
            grpShapes.Controls.Add(btnCircle);
            grpShapes.Controls.Add(btnFillCircle);
            grpShapes.Dock = DockStyle.Fill;
            grpShapes.ForeColor = Color.White;
            grpShapes.Location = new Point(750, 0);
            grpShapes.Name = "grpShapes";
            grpShapes.Size = new Size(530, 120);
            grpShapes.TabIndex = 1;
            grpShapes.TabStop = false;
            grpShapes.Text = "SHAPE";
            // 
            // btnSelect
            // 
            btnSelect.ForeColor = Color.Black;
            btnSelect.Location = new Point(6, 26);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(60, 76);
            btnSelect.TabIndex = 0;
            btnSelect.Text = "🖱 Select";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // btnFreehand
            // 
            btnFreehand.ForeColor = Color.Black;
            btnFreehand.Location = new Point(72, 26);
            btnFreehand.Name = "btnFreehand";
            btnFreehand.Size = new Size(58, 35);
            btnFreehand.TabIndex = 1;
            btnFreehand.Text = "✏ Free";
            btnFreehand.UseVisualStyleBackColor = true;
            btnFreehand.Click += btnFreehand_Click;
            // 
            // btnLine
            // 
            btnLine.ForeColor = Color.Black;
            btnLine.Location = new Point(72, 67);
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(58, 35);
            btnLine.TabIndex = 2;
            btnLine.Text = "╱ Line";
            btnLine.UseVisualStyleBackColor = true;
            btnLine.Click += btnLine_Click;
            // 
            // btnRectangle
            // 
            btnRectangle.ForeColor = Color.Black;
            btnRectangle.Location = new Point(136, 26);
            btnRectangle.Name = "btnRectangle";
            btnRectangle.Size = new Size(58, 35);
            btnRectangle.TabIndex = 3;
            btnRectangle.Text = "▭ Rect";
            btnRectangle.UseVisualStyleBackColor = true;
            btnRectangle.Click += btnRectangle_Click;
            // 
            // btnFillRect
            // 
            btnFillRect.ForeColor = Color.Black;
            btnFillRect.Location = new Point(136, 67);
            btnFillRect.Name = "btnFillRect";
            btnFillRect.Size = new Size(58, 35);
            btnFillRect.TabIndex = 4;
            btnFillRect.Text = "▬ ";
            btnFillRect.UseVisualStyleBackColor = true;
            btnFillRect.Click += btnFillRect_Click;
            // 
            // btnEllipse
            // 
            btnEllipse.ForeColor = Color.Black;
            btnEllipse.Location = new Point(200, 26);
            btnEllipse.Name = "btnEllipse";
            btnEllipse.Size = new Size(58, 35);
            btnEllipse.TabIndex = 5;
            btnEllipse.Text = "⬭ ";
            btnEllipse.UseVisualStyleBackColor = true;
            btnEllipse.Click += btnEllipse_Click;
            // 
            // btnFillEllipse
            // 
            btnFillEllipse.ForeColor = Color.Black;
            btnFillEllipse.Location = new Point(200, 67);
            btnFillEllipse.Name = "btnFillEllipse";
            btnFillEllipse.Size = new Size(58, 35);
            btnFillEllipse.TabIndex = 6;
            btnFillEllipse.Text = "⬬ ";
            btnFillEllipse.UseVisualStyleBackColor = true;
            btnFillEllipse.Click += btnFillEllipse_Click;
            // 
            // btnCircle
            // 
            btnCircle.ForeColor = Color.Black;
            btnCircle.Location = new Point(264, 26);
            btnCircle.Name = "btnCircle";
            btnCircle.Size = new Size(58, 35);
            btnCircle.TabIndex = 7;
            btnCircle.Text = "○ Circ";
            btnCircle.UseVisualStyleBackColor = true;
            btnCircle.Click += btnCircle_Click;
            // 
            // btnFillCircle
            // 
            btnFillCircle.ForeColor = Color.Black;
            btnFillCircle.Location = new Point(264, 67);
            btnFillCircle.Name = "btnFillCircle";
            btnFillCircle.Size = new Size(58, 35);
            btnFillCircle.TabIndex = 8;
            btnFillCircle.Text = "● ";
            btnFillCircle.UseVisualStyleBackColor = true;
            btnFillCircle.Click += btnFillCircle_Click;
            // 
            // grpTools
            // 
            grpTools.Controls.Add(btnPenColor);
            grpTools.Controls.Add(btnBrushColor);
            grpTools.Controls.Add(lblSize);
            grpTools.Controls.Add(trkSize);
            grpTools.Controls.Add(lblDash);
            grpTools.Controls.Add(cboDash);
            grpTools.Controls.Add(lblFill);
            grpTools.Controls.Add(cboFill);
            grpTools.Controls.Add(btnUndo);
            grpTools.Controls.Add(btnRedo);
            grpTools.Controls.Add(btnGroup);
            grpTools.Controls.Add(btnUngroup);
            grpTools.Controls.Add(btnClear);
            grpTools.Dock = DockStyle.Left;
            grpTools.ForeColor = Color.White;
            grpTools.Location = new Point(0, 0);
            grpTools.Name = "grpTools";
            grpTools.Size = new Size(750, 120);
            grpTools.TabIndex = 0;
            grpTools.TabStop = false;
            grpTools.Text = "TOOL";
            // 
            // btnPenColor
            // 
            btnPenColor.Location = new Point(11, 26);
            btnPenColor.Name = "btnPenColor";
            btnPenColor.Size = new Size(74, 76);
            btnPenColor.TabIndex = 0;
            btnPenColor.Text = "Pen Color";
            btnPenColor.UseVisualStyleBackColor = false;
            btnPenColor.Click += btnPenColor_Click;
            // 
            // btnBrushColor
            // 
            btnBrushColor.Location = new Point(91, 26);
            btnBrushColor.Name = "btnBrushColor";
            btnBrushColor.Size = new Size(74, 76);
            btnBrushColor.TabIndex = 1;
            btnBrushColor.Text = "Brush Color";
            btnBrushColor.UseVisualStyleBackColor = false;
            btnBrushColor.Click += btnBrushColor_Click;
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.ForeColor = Color.White;
            lblSize.Location = new Point(185, 23);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(39, 20);
            lblSize.TabIndex = 2;
            lblSize.Text = "Size:";
            // 
            // trkSize
            // 
            trkSize.Location = new Point(185, 45);
            trkSize.Maximum = 50;
            trkSize.Minimum = 1;
            trkSize.Name = "trkSize";
            trkSize.Size = new Size(130, 56);
            trkSize.TabIndex = 3;
            trkSize.TickStyle = TickStyle.None;
            trkSize.Value = 3;
            trkSize.ValueChanged += trkSize_ValueChanged;
            // 
            // lblDash
            // 
            lblDash.AutoSize = true;
            lblDash.ForeColor = Color.White;
            lblDash.Location = new Point(325, 23);
            lblDash.Name = "lblDash";
            lblDash.Size = new Size(45, 20);
            lblDash.TabIndex = 4;
            lblDash.Text = "Dash:";
            // 
            // cboDash
            // 
            cboDash.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDash.FormattingEnabled = true;
            cboDash.Location = new Point(325, 45);
            cboDash.Name = "cboDash";
            cboDash.Size = new Size(100, 28);
            cboDash.TabIndex = 5;
            cboDash.SelectedIndexChanged += cboDash_SelectedIndexChanged;
            // 
            // lblFill
            // 
            lblFill.AutoSize = true;
            lblFill.ForeColor = Color.White;
            lblFill.Location = new Point(435, 23);
            lblFill.Name = "lblFill";
            lblFill.Size = new Size(31, 20);
            lblFill.TabIndex = 6;
            lblFill.Text = "Fill:";
            // 
            // cboFill
            // 
            cboFill.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFill.FormattingEnabled = true;
            cboFill.Location = new Point(435, 45);
            cboFill.Name = "cboFill";
            cboFill.Size = new Size(100, 28);
            cboFill.TabIndex = 7;
            cboFill.SelectedIndexChanged += cboFill_SelectedIndexChanged;
            // 
            // btnUndo
            // 
            btnUndo.ForeColor = Color.Black;
            btnUndo.Location = new Point(545, 26);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(48, 35);
            btnUndo.TabIndex = 8;
            btnUndo.Text = "↩ Undo";
            btnUndo.UseVisualStyleBackColor = true;
            btnUndo.Click += btnUndo_Click;
            // 
            // btnRedo
            // 
            btnRedo.ForeColor = Color.Black;
            btnRedo.Location = new Point(545, 67);
            btnRedo.Name = "btnRedo";
            btnRedo.Size = new Size(48, 35);
            btnRedo.TabIndex = 9;
            btnRedo.Text = "↪ Redo";
            btnRedo.UseVisualStyleBackColor = true;
            btnRedo.Click += btnRedo_Click;
            // 
            // btnGroup
            // 
            btnGroup.ForeColor = Color.Black;
            btnGroup.Location = new Point(599, 26);
            btnGroup.Name = "btnGroup";
            btnGroup.Size = new Size(76, 35);
            btnGroup.TabIndex = 10;
            btnGroup.Text = "Group";
            btnGroup.UseVisualStyleBackColor = true;
            btnGroup.Click += btnGroup_Click;
            // 
            // btnUngroup
            // 
            btnUngroup.ForeColor = Color.Black;
            btnUngroup.Location = new Point(599, 67);
            btnUngroup.Name = "btnUngroup";
            btnUngroup.Size = new Size(76, 35);
            btnUngroup.TabIndex = 11;
            btnUngroup.Text = "Ungroup";
            btnUngroup.UseVisualStyleBackColor = true;
            btnUngroup.Click += btnUngroup_Click;
            // 
            // btnClear
            // 
            btnClear.ForeColor = Color.Black;
            btnClear.Location = new Point(681, 26);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(63, 76);
            btnClear.TabIndex = 12;
            btnClear.Text = "🗑 Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // colorDialog1
            // 
            colorDialog1.FullOpen = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1282, 733);
            Controls.Add(pnlCanvas);
            Name = "Form1";
            Text = "Paint App";
            pnlCanvas.ResumeLayout(false);
            pnlToolbar.ResumeLayout(false);
            grpShapes.ResumeLayout(false);
            grpTools.ResumeLayout(false);
            grpTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkSize).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCanvas;
        private Panel pnlToolbar;
        private GroupBox grpTools;
        private GroupBox grpShapes;
        private Button btnPenColor;
        private Button btnBrushColor;
        private Label lblSize;
        private TrackBar trkSize;
        private Label lblDash;
        private ComboBox cboDash;
        private Label lblFill;
        private ComboBox cboFill;
        private Button btnUndo;
        private Button btnRedo;
        private Button btnGroup;
        private Button btnUngroup;
        private Button btnClear;
        private Button btnSelect;
        private Button btnFreehand;
        private Button btnLine;
        private Button btnRectangle;
        private Button btnFillRect;
        private Button btnEllipse;
        private Button btnFillEllipse;
        private Button btnCircle;
        private Button btnFillCircle;
        private ColorDialog colorDialog1;
    }
}