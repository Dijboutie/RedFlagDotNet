namespace RedFlag
{
    partial class DebuggerSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebuggerSettings));
            this.num_ObjectDepth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.num_StackLength = new System.Windows.Forms.NumericUpDown();
            this.cb_ArrayItems = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbIgnore = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.but_ChooseFile = new System.Windows.Forms.Button();
            this.num_LineNumber = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_SourceFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.but_OK = new System.Windows.Forms.Button();
            this.but_cancel = new System.Windows.Forms.Button();
            this.lbDefNet = new System.Windows.Forms.Label();
            this.cbNetVersion = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.num_ObjectDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_StackLength)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_LineNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // num_ObjectDepth
            // 
            this.num_ObjectDepth.Location = new System.Drawing.Point(113, 10);
            this.num_ObjectDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_ObjectDepth.Name = "num_ObjectDepth";
            this.num_ObjectDepth.Size = new System.Drawing.Size(51, 20);
            this.num_ObjectDepth.TabIndex = 0;
            this.num_ObjectDepth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Stack Object Depth";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Stack Length";
            // 
            // num_StackLength
            // 
            this.num_StackLength.Location = new System.Drawing.Point(113, 47);
            this.num_StackLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_StackLength.Name = "num_StackLength";
            this.num_StackLength.Size = new System.Drawing.Size(51, 20);
            this.num_StackLength.TabIndex = 1;
            this.num_StackLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cb_ArrayItems
            // 
            this.cb_ArrayItems.AutoSize = true;
            this.cb_ArrayItems.Location = new System.Drawing.Point(146, 93);
            this.cb_ArrayItems.Name = "cb_ArrayItems";
            this.cb_ArrayItems.Size = new System.Drawing.Size(15, 14);
            this.cb_ArrayItems.TabIndex = 2;
            this.cb_ArrayItems.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_ArrayItems.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Include Array Items";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbIgnore);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cb_ArrayItems);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.num_ObjectDepth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.num_StackLength);
            this.groupBox1.Location = new System.Drawing.Point(19, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 186);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information Collection";
            // 
            // lbIgnore
            // 
            this.lbIgnore.Location = new System.Drawing.Point(7, 144);
            this.lbIgnore.Name = "lbIgnore";
            this.lbIgnore.Size = new System.Drawing.Size(220, 20);
            this.lbIgnore.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Ignore these exception types:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.but_ChooseFile);
            this.groupBox2.Controls.Add(this.num_LineNumber);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tb_SourceFile);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(20, 216);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 110);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Breakpoint";
            // 
            // but_ChooseFile
            // 
            this.but_ChooseFile.Location = new System.Drawing.Point(174, 37);
            this.but_ChooseFile.Name = "but_ChooseFile";
            this.but_ChooseFile.Size = new System.Drawing.Size(32, 23);
            this.but_ChooseFile.TabIndex = 5;
            this.but_ChooseFile.Text = "...";
            this.but_ChooseFile.UseVisualStyleBackColor = true;
            this.but_ChooseFile.Click += new System.EventHandler(this.but_ChooseFile_Click);
            // 
            // num_LineNumber
            // 
            this.num_LineNumber.Location = new System.Drawing.Point(117, 75);
            this.num_LineNumber.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.num_LineNumber.Name = "num_LineNumber";
            this.num_LineNumber.Size = new System.Drawing.Size(62, 20);
            this.num_LineNumber.TabIndex = 6;
            this.num_LineNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Line";
            // 
            // tb_SourceFile
            // 
            this.tb_SourceFile.Location = new System.Drawing.Point(10, 37);
            this.tb_SourceFile.Name = "tb_SourceFile";
            this.tb_SourceFile.Size = new System.Drawing.Size(158, 20);
            this.tb_SourceFile.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Source file name";
            // 
            // but_OK
            // 
            this.but_OK.Location = new System.Drawing.Point(19, 372);
            this.but_OK.Name = "but_OK";
            this.but_OK.Size = new System.Drawing.Size(75, 23);
            this.but_OK.TabIndex = 8;
            this.but_OK.Text = "Apply";
            this.but_OK.UseVisualStyleBackColor = true;
            this.but_OK.Click += new System.EventHandler(this.but_OK_Click);
            // 
            // but_cancel
            // 
            this.but_cancel.Location = new System.Drawing.Point(177, 372);
            this.but_cancel.Name = "but_cancel";
            this.but_cancel.Size = new System.Drawing.Size(75, 23);
            this.but_cancel.TabIndex = 9;
            this.but_cancel.Text = "Cancel";
            this.but_cancel.UseVisualStyleBackColor = true;
            this.but_cancel.Click += new System.EventHandler(this.but_cancel_Click);
            // 
            // lbDefNet
            // 
            this.lbDefNet.AutoSize = true;
            this.lbDefNet.Location = new System.Drawing.Point(19, 333);
            this.lbDefNet.Name = "lbDefNet";
            this.lbDefNet.Size = new System.Drawing.Size(107, 13);
            this.lbDefNet.TabIndex = 10;
            this.lbDefNet.Text = "Default .NET Version";
            // 
            // cbNetVersion
            // 
            this.cbNetVersion.FormattingEnabled = true;
            this.cbNetVersion.Items.AddRange(new object[] {
            "v2.0.50727",
            "v4.0.30319"});
            this.cbNetVersion.Location = new System.Drawing.Point(137, 333);
            this.cbNetVersion.Name = "cbNetVersion";
            this.cbNetVersion.Size = new System.Drawing.Size(115, 21);
            this.cbNetVersion.TabIndex = 11;
            // 
            // DebuggerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 407);
            this.Controls.Add(this.cbNetVersion);
            this.Controls.Add(this.lbDefNet);
            this.Controls.Add(this.but_cancel);
            this.Controls.Add(this.but_OK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DebuggerSettings";
            this.Text = "Debugger Settings";
            this.Load += new System.EventHandler(this.DebuggerSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_ObjectDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_StackLength)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_LineNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown num_ObjectDepth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown num_StackLength;
        private System.Windows.Forms.CheckBox cb_ArrayItems;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown num_LineNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_SourceFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button but_OK;
        private System.Windows.Forms.Button but_cancel;
        private System.Windows.Forms.Button but_ChooseFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox lbIgnore;
        private System.Windows.Forms.Label lbDefNet;
        private System.Windows.Forms.ComboBox cbNetVersion;
    }
}