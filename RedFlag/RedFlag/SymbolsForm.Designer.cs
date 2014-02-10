namespace RedFlag
{
    partial class SymbolsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SymbolsForm));
            this.lvSymbolDocs = new System.Windows.Forms.ListView();
            this.colSymbolFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSourceFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvSymbolDocs
            // 
            this.lvSymbolDocs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSymbolDocs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSymbolFile,
            this.colSourceFile});
            this.lvSymbolDocs.FullRowSelect = true;
            this.lvSymbolDocs.Location = new System.Drawing.Point(12, 12);
            this.lvSymbolDocs.Name = "lvSymbolDocs";
            this.lvSymbolDocs.Size = new System.Drawing.Size(725, 438);
            this.lvSymbolDocs.TabIndex = 0;
            this.lvSymbolDocs.UseCompatibleStateImageBehavior = false;
            this.lvSymbolDocs.View = System.Windows.Forms.View.Details;
            this.lvSymbolDocs.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_ColumnClick);
            this.lvSymbolDocs.KeyUp += new System.Windows.Forms.KeyEventHandler(this.f1Control_KeyDown);
            // 
            // colSymbolFile
            // 
            this.colSymbolFile.Text = "Symbol File";
            this.colSymbolFile.Width = 349;
            // 
            // colSourceFile
            // 
            this.colSourceFile.Text = "Source File";
            this.colSourceFile.Width = 312;
            // 
            // SymbolsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 462);
            this.Controls.Add(this.lvSymbolDocs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SymbolsForm";
            this.Text = "Symbol Details";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvSymbolDocs;
        private System.Windows.Forms.ColumnHeader colSourceFile;
        private System.Windows.Forms.ColumnHeader colSymbolFile;
    }
}