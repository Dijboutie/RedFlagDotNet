namespace RedFlag
{
    partial class frmDebuggedProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDebuggedProcess));
            this.tbThisProcess = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbThisProcess
            // 
            this.tbThisProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbThisProcess.Location = new System.Drawing.Point(13, 13);
            this.tbThisProcess.Multiline = true;
            this.tbThisProcess.Name = "tbThisProcess";
            this.tbThisProcess.Size = new System.Drawing.Size(366, 208);
            this.tbThisProcess.TabIndex = 0;
            this.tbThisProcess.Text = "Debugged Process";
            // 
            // frmDebuggedProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 233);
            this.Controls.Add(this.tbThisProcess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDebuggedProcess";
            this.Text = "Debugged Process Info";
            this.Load += new System.EventHandler(this.frmDebuggedProcess_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbThisProcess;
    }
}