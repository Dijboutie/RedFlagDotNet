namespace RedFlag
{
    partial class FindVariable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindVariable));
            this.label1 = new System.Windows.Forms.Label();
            this.tbValueRegexp = new System.Windows.Forms.TextBox();
            this.lvFindResult = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExceptionType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFrame = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pbSearchProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNameRegex = new System.Windows.Forms.TextBox();
            this.cbLogicalOp = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Value (regular expression)";
            // 
            // tbValueRegexp
            // 
            this.tbValueRegexp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbValueRegexp.Location = new System.Drawing.Point(145, 48);
            this.tbValueRegexp.Name = "tbValueRegexp";
            this.tbValueRegexp.Size = new System.Drawing.Size(404, 20);
            this.tbValueRegexp.TabIndex = 1;
            this.tbValueRegexp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbValueRegexp_KeyDown);
            // 
            // lvFindResult
            // 
            this.lvFindResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFindResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colValue,
            this.colExceptionType,
            this.colFrame});
            this.lvFindResult.FullRowSelect = true;
            this.lvFindResult.Location = new System.Drawing.Point(12, 74);
            this.lvFindResult.Name = "lvFindResult";
            this.lvFindResult.Size = new System.Drawing.Size(535, 259);
            this.lvFindResult.TabIndex = 2;
            this.lvFindResult.UseCompatibleStateImageBehavior = false;
            this.lvFindResult.View = System.Windows.Forms.View.Details;
            this.lvFindResult.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_ColumnClick);
            this.lvFindResult.DoubleClick += new System.EventHandler(this.lvFindResult_DoubleClick);
            this.lvFindResult.KeyUp += new System.Windows.Forms.KeyEventHandler(this.f1Control_KeyDown);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 100;
            // 
            // colValue
            // 
            this.colValue.Text = "Variable Value";
            this.colValue.Width = 150;
            // 
            // colExceptionType
            // 
            this.colExceptionType.Text = "Exception Type";
            this.colExceptionType.Width = 200;
            // 
            // colFrame
            // 
            this.colFrame.Text = "Frame";
            this.colFrame.Width = 200;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbSearchProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 350);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(564, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pbSearchProgress
            // 
            this.pbSearchProgress.Name = "pbSearchProgress";
            this.pbSearchProgress.Size = new System.Drawing.Size(300, 16);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name (regular expression)";
            // 
            // tbNameRegex
            // 
            this.tbNameRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNameRegex.Location = new System.Drawing.Point(147, 9);
            this.tbNameRegex.Name = "tbNameRegex";
            this.tbNameRegex.Size = new System.Drawing.Size(400, 20);
            this.tbNameRegex.TabIndex = 5;
            this.tbNameRegex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbNameRegex_KeyDown);
            // 
            // cbLogicalOp
            // 
            this.cbLogicalOp.FormattingEnabled = true;
            this.cbLogicalOp.Items.AddRange(new object[] {
            "And",
            "Or"});
            this.cbLogicalOp.Location = new System.Drawing.Point(36, 26);
            this.cbLogicalOp.Name = "cbLogicalOp";
            this.cbLogicalOp.Size = new System.Drawing.Size(64, 21);
            this.cbLogicalOp.TabIndex = 6;
            this.cbLogicalOp.Text = "And";
            // 
            // FindVariable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 372);
            this.Controls.Add(this.cbLogicalOp);
            this.Controls.Add(this.tbNameRegex);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lvFindResult);
            this.Controls.Add(this.tbValueRegexp);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FindVariable";
            this.Text = "Find Variable";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindVariable_FormClosing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.f1Control_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbValueRegexp;
        private System.Windows.Forms.ListView lvFindResult;
        private System.Windows.Forms.ColumnHeader colExceptionType;
        private System.Windows.Forms.ColumnHeader colFrame;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar pbSearchProgress;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNameRegex;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ComboBox cbLogicalOp;
    }
}