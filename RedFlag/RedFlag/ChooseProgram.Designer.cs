namespace RedFlag
{
    partial class ChooseProgram
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseProgram));
            this.tbExecutable = new System.Windows.Forms.TextBox();
            this.tbArgs = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.butOk = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.butChoose = new System.Windows.Forms.Button();
            this.rb_Desktop = new System.Windows.Forms.RadioButton();
            this.rb_Website = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // tbExecutable
            // 
            this.tbExecutable.Location = new System.Drawing.Point(12, 63);
            this.tbExecutable.Name = "tbExecutable";
            this.tbExecutable.Size = new System.Drawing.Size(326, 20);
            this.tbExecutable.TabIndex = 0;
            // 
            // tbArgs
            // 
            this.tbArgs.Location = new System.Drawing.Point(11, 106);
            this.tbArgs.Name = "tbArgs";
            this.tbArgs.Size = new System.Drawing.Size(326, 20);
            this.tbArgs.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Program Executable";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Arguments";
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(11, 131);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 4;
            this.butOk.Text = "OK";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(262, 132);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 5;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butChoose
            // 
            this.butChoose.Location = new System.Drawing.Point(359, 59);
            this.butChoose.Name = "butChoose";
            this.butChoose.Size = new System.Drawing.Size(31, 23);
            this.butChoose.TabIndex = 6;
            this.butChoose.Text = "...";
            this.butChoose.UseVisualStyleBackColor = true;
            this.butChoose.Click += new System.EventHandler(this.butChoose_Click);
            // 
            // rb_Desktop
            // 
            this.rb_Desktop.AutoSize = true;
            this.rb_Desktop.Checked = true;
            this.rb_Desktop.Location = new System.Drawing.Point(11, 13);
            this.rb_Desktop.Name = "rb_Desktop";
            this.rb_Desktop.Size = new System.Drawing.Size(120, 17);
            this.rb_Desktop.TabIndex = 7;
            this.rb_Desktop.TabStop = true;
            this.rb_Desktop.Text = "Desktop Application";
            this.rb_Desktop.UseVisualStyleBackColor = true;
            this.rb_Desktop.CheckedChanged += new System.EventHandler(this.rb_Desktop_CheckedChanged);
            // 
            // rb_Website
            // 
            this.rb_Website.AutoSize = true;
            this.rb_Website.Location = new System.Drawing.Point(153, 13);
            this.rb_Website.Name = "rb_Website";
            this.rb_Website.Size = new System.Drawing.Size(116, 17);
            this.rb_Website.TabIndex = 8;
            this.rb_Website.TabStop = true;
            this.rb_Website.Text = "ASP .NET Website";
            this.rb_Website.UseVisualStyleBackColor = true;
            this.rb_Website.CheckedChanged += new System.EventHandler(this.rb_Website_CheckedChanged);
            // 
            // ChooseProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 168);
            this.ControlBox = false;
            this.Controls.Add(this.rb_Website);
            this.Controls.Add(this.rb_Desktop);
            this.Controls.Add(this.butChoose);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbArgs);
            this.Controls.Add(this.tbExecutable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChooseProgram";
            this.Text = "Choose Program";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbExecutable;
        private System.Windows.Forms.TextBox tbArgs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butChoose;
        private System.Windows.Forms.RadioButton rb_Desktop;
        private System.Windows.Forms.RadioButton rb_Website;
    }
}