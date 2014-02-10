namespace RedFlag
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attachProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detachProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readSymbolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpStacksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpHeapStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHandlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debuggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findVariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbStackTrace = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvExceptions = new System.Windows.Forms.ListView();
            this.lveType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lveTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.butNext = new System.Windows.Forms.Button();
            this.butPrevious = new System.Windows.Forms.Button();
            this.lvLocals = new System.Windows.Forms.ListView();
            this.lvColName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvColValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvColType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvColID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.lbArguments = new System.Windows.Forms.ListBox();
            this.tabGroupException = new System.Windows.Forms.TabControl();
            this.tabExceptions = new System.Windows.Forms.TabPage();
            this.tabMethod = new System.Windows.Forms.TabPage();
            this.tabPageAssemblies = new System.Windows.Forms.TabPage();
            this.lv_Assemblies = new System.Windows.Forms.ListView();
            this.colFullName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSymbols = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabAppDomains = new System.Windows.Forms.TabPage();
            this.lvAppDomains = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabMessages = new System.Windows.Forms.TabPage();
            this.lvTraceMessages = new System.Windows.Forms.ListView();
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSwitch = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabGroupException.SuspendLayout();
            this.tabExceptions.SuspendLayout();
            this.tabMethod.SuspendLayout();
            this.tabPageAssemblies.SuspendLayout();
            this.tabAppDomains.SuspendLayout();
            this.tabMessages.SuspendLayout();
            this.lbStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(678, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "&Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launchProcessToolStripMenuItem,
            this.attachProcessToolStripMenuItem,
            this.detachProcessToolStripMenuItem,
            this.readSymbolsToolStripMenuItem,
            this.stopProcessToolStripMenuItem,
            this.dumpStacksToolStripMenuItem,
            this.dumpHeapStatisticsToolStripMenuItem,
            this.showHandlesToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "&Actions";
            // 
            // launchProcessToolStripMenuItem
            // 
            this.launchProcessToolStripMenuItem.Name = "launchProcessToolStripMenuItem";
            this.launchProcessToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.launchProcessToolStripMenuItem.Text = "&Launch Process";
            this.launchProcessToolStripMenuItem.Click += new System.EventHandler(this.launchProcessToolStripMenuItem_Click);
            // 
            // attachProcessToolStripMenuItem
            // 
            this.attachProcessToolStripMenuItem.Name = "attachProcessToolStripMenuItem";
            this.attachProcessToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.attachProcessToolStripMenuItem.Text = "&Attach Process";
            this.attachProcessToolStripMenuItem.Click += new System.EventHandler(this.attachProcessToolStripMenuItem_Click);
            // 
            // detachProcessToolStripMenuItem
            // 
            this.detachProcessToolStripMenuItem.Name = "detachProcessToolStripMenuItem";
            this.detachProcessToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.detachProcessToolStripMenuItem.Text = "&Detach Process";
            this.detachProcessToolStripMenuItem.Click += new System.EventHandler(this.detachProcessToolStripMenuItem_Click);
            // 
            // readSymbolsToolStripMenuItem
            // 
            this.readSymbolsToolStripMenuItem.Name = "readSymbolsToolStripMenuItem";
            this.readSymbolsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.readSymbolsToolStripMenuItem.Text = "List Source &Files";
            this.readSymbolsToolStripMenuItem.Click += new System.EventHandler(this.readSymbolsToolStripMenuItem_Click);
            // 
            // stopProcessToolStripMenuItem
            // 
            this.stopProcessToolStripMenuItem.Name = "stopProcessToolStripMenuItem";
            this.stopProcessToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.stopProcessToolStripMenuItem.Text = "Stop &Process";
            this.stopProcessToolStripMenuItem.Click += new System.EventHandler(this.stopProcessToolStripMenuItem_Click);
            // 
            // dumpStacksToolStripMenuItem
            // 
            this.dumpStacksToolStripMenuItem.Name = "dumpStacksToolStripMenuItem";
            this.dumpStacksToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.dumpStacksToolStripMenuItem.Text = "Dump &Stacks";
            this.dumpStacksToolStripMenuItem.Click += new System.EventHandler(this.dumpStacksToolStripMenuItem_Click);
            // 
            // dumpHeapStatisticsToolStripMenuItem
            // 
            this.dumpHeapStatisticsToolStripMenuItem.Name = "dumpHeapStatisticsToolStripMenuItem";
            this.dumpHeapStatisticsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.dumpHeapStatisticsToolStripMenuItem.Text = "Dump &Heap Statistics";
            this.dumpHeapStatisticsToolStripMenuItem.Click += new System.EventHandler(this.dumpHeapStatisticsToolStripMenuItem_Click);
            // 
            // showHandlesToolStripMenuItem
            // 
            this.showHandlesToolStripMenuItem.Name = "showHandlesToolStripMenuItem";
            this.showHandlesToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.showHandlesToolStripMenuItem.Text = "Show Ha&ndles";
            this.showHandlesToolStripMenuItem.Click += new System.EventHandler(this.showHandlesToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debuggingToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // debuggingToolStripMenuItem
            // 
            this.debuggingToolStripMenuItem.Name = "debuggingToolStripMenuItem";
            this.debuggingToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.debuggingToolStripMenuItem.Text = "&Debugging";
            this.debuggingToolStripMenuItem.Click += new System.EventHandler(this.debuggingToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findVariableToolStripMenuItem,
            this.processInfoToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // findVariableToolStripMenuItem
            // 
            this.findVariableToolStripMenuItem.Name = "findVariableToolStripMenuItem";
            this.findVariableToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.findVariableToolStripMenuItem.Text = "&Find Variable";
            this.findVariableToolStripMenuItem.Click += new System.EventHandler(this.findVariableToolStripMenuItem_Click);
            // 
            // processInfoToolStripMenuItem
            // 
            this.processInfoToolStripMenuItem.Name = "processInfoToolStripMenuItem";
            this.processInfoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.processInfoToolStripMenuItem.Text = "Process &Info";
            this.processInfoToolStripMenuItem.Click += new System.EventHandler(this.processInfoToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Exceptions";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Stack Trace";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Local Variables (double-click for hierarchy)";
            // 
            // lbStackTrace
            // 
            this.lbStackTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStackTrace.FormattingEnabled = true;
            this.lbStackTrace.HorizontalScrollbar = true;
            this.lbStackTrace.Location = new System.Drawing.Point(5, 255);
            this.lbStackTrace.Name = "lbStackTrace";
            this.lbStackTrace.Size = new System.Drawing.Size(640, 95);
            this.lbStackTrace.TabIndex = 9;
            this.lbStackTrace.SelectedIndexChanged += new System.EventHandler(this.lbStackTrace_SelectedIndexChanged);
            this.lbStackTrace.DoubleClick += new System.EventHandler(this.lbStackTrace_DoubleClick);
            this.lbStackTrace.KeyUp += new System.Windows.Forms.KeyEventHandler(this.f1Control_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lvExceptions);
            this.groupBox1.Controls.Add(this.lbStackTrace);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(651, 362);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Exception";
            // 
            // lvExceptions
            // 
            this.lvExceptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvExceptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lveType,
            this.lveTime,
            this.Message});
            this.lvExceptions.FullRowSelect = true;
            this.lvExceptions.HideSelection = false;
            this.lvExceptions.Location = new System.Drawing.Point(6, 45);
            this.lvExceptions.MultiSelect = false;
            this.lvExceptions.Name = "lvExceptions";
            this.lvExceptions.Size = new System.Drawing.Size(638, 190);
            this.lvExceptions.TabIndex = 10;
            this.lvExceptions.UseCompatibleStateImageBehavior = false;
            this.lvExceptions.View = System.Windows.Forms.View.Details;
            this.lvExceptions.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_ColumnClick);
            this.lvExceptions.SelectedIndexChanged += new System.EventHandler(this.lvExceptions_SelectedIndexChanged);
            this.lvExceptions.KeyUp += new System.Windows.Forms.KeyEventHandler(this.f1Control_KeyDown);
            // 
            // lveType
            // 
            this.lveType.Text = "Type";
            this.lveType.Width = 168;
            // 
            // lveTime
            // 
            this.lveTime.Tag = "rgTimeSpanSort";
            this.lveTime.Text = "Time";
            this.lveTime.Width = 96;
            // 
            // Message
            // 
            this.Message.Text = "Message";
            this.Message.Width = 250;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.butNext);
            this.groupBox2.Controls.Add(this.butPrevious);
            this.groupBox2.Controls.Add(this.lvLocals);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lbArguments);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(1, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(669, 391);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected Method";
            // 
            // butNext
            // 
            this.butNext.Location = new System.Drawing.Point(332, 20);
            this.butNext.Name = "butNext";
            this.butNext.Size = new System.Drawing.Size(75, 23);
            this.butNext.TabIndex = 18;
            this.butNext.Text = "Next";
            this.butNext.UseVisualStyleBackColor = true;
            this.butNext.Click += new System.EventHandler(this.butNext_Click);
            // 
            // butPrevious
            // 
            this.butPrevious.Location = new System.Drawing.Point(249, 20);
            this.butPrevious.Name = "butPrevious";
            this.butPrevious.Size = new System.Drawing.Size(75, 23);
            this.butPrevious.TabIndex = 17;
            this.butPrevious.Text = "Previous";
            this.butPrevious.UseVisualStyleBackColor = true;
            this.butPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // lvLocals
            // 
            this.lvLocals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvLocals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvColName,
            this.lvColValue,
            this.lvColType,
            this.lvColID});
            this.lvLocals.FullRowSelect = true;
            this.lvLocals.Location = new System.Drawing.Point(17, 169);
            this.lvLocals.MultiSelect = false;
            this.lvLocals.Name = "lvLocals";
            this.lvLocals.Size = new System.Drawing.Size(623, 216);
            this.lvLocals.TabIndex = 16;
            this.lvLocals.UseCompatibleStateImageBehavior = false;
            this.lvLocals.View = System.Windows.Forms.View.Details;
            this.lvLocals.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_ColumnClick);
            this.lvLocals.DoubleClick += new System.EventHandler(this.lvLocals_DoubleClick);
            this.lvLocals.KeyUp += new System.Windows.Forms.KeyEventHandler(this.f1Control_KeyDown);
            // 
            // lvColName
            // 
            this.lvColName.Text = "Name";
            // 
            // lvColValue
            // 
            this.lvColValue.Text = "Value";
            this.lvColValue.Width = 170;
            // 
            // lvColType
            // 
            this.lvColType.Text = "Type";
            this.lvColType.Width = 159;
            // 
            // lvColID
            // 
            this.lvColID.Tag = "rgNumericSort";
            this.lvColID.Text = "ID";
            this.lvColID.Width = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Arguments";
            // 
            // lbArguments
            // 
            this.lbArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbArguments.FormattingEnabled = true;
            this.lbArguments.HorizontalScrollbar = true;
            this.lbArguments.Location = new System.Drawing.Point(17, 57);
            this.lbArguments.Name = "lbArguments";
            this.lbArguments.Size = new System.Drawing.Size(623, 82);
            this.lbArguments.TabIndex = 14;
            this.lbArguments.KeyUp += new System.Windows.Forms.KeyEventHandler(this.f1Control_KeyDown);
            // 
            // tabGroupException
            // 
            this.tabGroupException.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabGroupException.Controls.Add(this.tabExceptions);
            this.tabGroupException.Controls.Add(this.tabMethod);
            this.tabGroupException.Controls.Add(this.tabPageAssemblies);
            this.tabGroupException.Controls.Add(this.tabAppDomains);
            this.tabGroupException.Controls.Add(this.tabMessages);
            this.tabGroupException.Location = new System.Drawing.Point(0, 27);
            this.tabGroupException.Name = "tabGroupException";
            this.tabGroupException.SelectedIndex = 0;
            this.tabGroupException.ShowToolTips = true;
            this.tabGroupException.Size = new System.Drawing.Size(678, 415);
            this.tabGroupException.TabIndex = 14;
            // 
            // tabExceptions
            // 
            this.tabExceptions.Controls.Add(this.groupBox1);
            this.tabExceptions.Location = new System.Drawing.Point(4, 22);
            this.tabExceptions.Name = "tabExceptions";
            this.tabExceptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabExceptions.Size = new System.Drawing.Size(670, 389);
            this.tabExceptions.TabIndex = 0;
            this.tabExceptions.Text = "Exceptions";
            this.tabExceptions.ToolTipText = "The exceptions that had occurred";
            this.tabExceptions.UseVisualStyleBackColor = true;
            // 
            // tabMethod
            // 
            this.tabMethod.Controls.Add(this.groupBox2);
            this.tabMethod.Location = new System.Drawing.Point(4, 22);
            this.tabMethod.Name = "tabMethod";
            this.tabMethod.Padding = new System.Windows.Forms.Padding(3);
            this.tabMethod.Size = new System.Drawing.Size(670, 389);
            this.tabMethod.TabIndex = 1;
            this.tabMethod.Text = "Stack Objects";
            this.tabMethod.ToolTipText = "Local variables for the specified method. You can move up and down the stack usin" +
    "g the Previous and Next buttons.";
            this.tabMethod.UseVisualStyleBackColor = true;
            // 
            // tabPageAssemblies
            // 
            this.tabPageAssemblies.Controls.Add(this.lv_Assemblies);
            this.tabPageAssemblies.Location = new System.Drawing.Point(4, 22);
            this.tabPageAssemblies.Name = "tabPageAssemblies";
            this.tabPageAssemblies.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAssemblies.Size = new System.Drawing.Size(670, 389);
            this.tabPageAssemblies.TabIndex = 2;
            this.tabPageAssemblies.Text = "Assemblies";
            this.tabPageAssemblies.ToolTipText = "The list of loaded .NET modules in this process";
            this.tabPageAssemblies.UseVisualStyleBackColor = true;
            // 
            // lv_Assemblies
            // 
            this.lv_Assemblies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_Assemblies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFullName,
            this.colPath,
            this.colSymbols});
            this.lv_Assemblies.FullRowSelect = true;
            this.lv_Assemblies.HideSelection = false;
            this.lv_Assemblies.Location = new System.Drawing.Point(3, 6);
            this.lv_Assemblies.Name = "lv_Assemblies";
            this.lv_Assemblies.Size = new System.Drawing.Size(667, 379);
            this.lv_Assemblies.TabIndex = 11;
            this.lv_Assemblies.UseCompatibleStateImageBehavior = false;
            this.lv_Assemblies.View = System.Windows.Forms.View.Details;
            this.lv_Assemblies.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_ColumnClick);
            this.lv_Assemblies.VisibleChanged += new System.EventHandler(this.lv_Assemblies_VisibleChanged);
            this.lv_Assemblies.DoubleClick += new System.EventHandler(this.lv_Assemblies_DoubleClick);
            this.lv_Assemblies.KeyUp += new System.Windows.Forms.KeyEventHandler(this.f1Control_KeyDown);
            // 
            // colFullName
            // 
            this.colFullName.Text = "Full Name";
            this.colFullName.Width = 350;
            // 
            // colPath
            // 
            this.colPath.Tag = "";
            this.colPath.Text = "File Name";
            this.colPath.Width = 350;
            // 
            // colSymbols
            // 
            this.colSymbols.Text = "Symbols";
            this.colSymbols.Width = 220;
            // 
            // tabAppDomains
            // 
            this.tabAppDomains.Controls.Add(this.lvAppDomains);
            this.tabAppDomains.Location = new System.Drawing.Point(4, 22);
            this.tabAppDomains.Name = "tabAppDomains";
            this.tabAppDomains.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppDomains.Size = new System.Drawing.Size(670, 389);
            this.tabAppDomains.TabIndex = 4;
            this.tabAppDomains.Text = "AppDomains";
            this.tabAppDomains.UseVisualStyleBackColor = true;
            // 
            // lvAppDomains
            // 
            this.lvAppDomains.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAppDomains.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvAppDomains.FullRowSelect = true;
            this.lvAppDomains.HideSelection = false;
            this.lvAppDomains.Location = new System.Drawing.Point(2, 5);
            this.lvAppDomains.Name = "lvAppDomains";
            this.lvAppDomains.Size = new System.Drawing.Size(667, 379);
            this.lvAppDomains.TabIndex = 12;
            this.lvAppDomains.UseCompatibleStateImageBehavior = false;
            this.lvAppDomains.View = System.Windows.Forms.View.Details;
            this.lvAppDomains.VisibleChanged += new System.EventHandler(this.lvAppDomains_VisibleChanged);
            this.lvAppDomains.KeyUp += new System.Windows.Forms.KeyEventHandler(this.f1Control_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 550;
            // 
            // tabMessages
            // 
            this.tabMessages.Controls.Add(this.lvTraceMessages);
            this.tabMessages.Location = new System.Drawing.Point(4, 22);
            this.tabMessages.Name = "tabMessages";
            this.tabMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tabMessages.Size = new System.Drawing.Size(670, 389);
            this.tabMessages.TabIndex = 3;
            this.tabMessages.Text = "Messages";
            this.tabMessages.ToolTipText = "The debug and trace message output from the process";
            this.tabMessages.UseVisualStyleBackColor = true;
            // 
            // lvTraceMessages
            // 
            this.lvTraceMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTraceMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMessage,
            this.colName,
            this.colSwitch,
            this.id});
            this.lvTraceMessages.FullRowSelect = true;
            this.lvTraceMessages.HideSelection = false;
            this.lvTraceMessages.Location = new System.Drawing.Point(2, 5);
            this.lvTraceMessages.Name = "lvTraceMessages";
            this.lvTraceMessages.Size = new System.Drawing.Size(667, 379);
            this.lvTraceMessages.TabIndex = 12;
            this.lvTraceMessages.UseCompatibleStateImageBehavior = false;
            this.lvTraceMessages.View = System.Windows.Forms.View.Details;
            this.lvTraceMessages.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_ColumnClick);
            this.lvTraceMessages.KeyUp += new System.Windows.Forms.KeyEventHandler(this.f1Control_KeyDown);
            // 
            // colMessage
            // 
            this.colMessage.Text = "Message";
            this.colMessage.Width = 380;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 144;
            // 
            // colSwitch
            // 
            this.colSwitch.Tag = "";
            this.colSwitch.Text = "MessageSwitch";
            this.colSwitch.Width = 102;
            // 
            // id
            // 
            this.id.Tag = "rgNumericSort";
            this.id.Text = "ID";
            this.id.Width = 220;
            // 
            // lbStatus
            // 
            this.lbStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.lbStatus.Location = new System.Drawing.Point(0, 445);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(678, 22);
            this.lbStatus.TabIndex = 15;
            this.lbStatus.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel1.Text = "Idle...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 467);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.tabGroupException);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Red Gate Red Flag v1.5";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabGroupException.ResumeLayout(false);
            this.tabExceptions.ResumeLayout(false);
            this.tabMethod.ResumeLayout(false);
            this.tabPageAssemblies.ResumeLayout(false);
            this.tabAppDomains.ResumeLayout(false);
            this.tabMessages.ResumeLayout(false);
            this.lbStatus.ResumeLayout(false);
            this.lbStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attachProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbStackTrace;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl tabGroupException;
        private System.Windows.Forms.TabPage tabExceptions;
        private System.Windows.Forms.TabPage tabMethod;
        private System.Windows.Forms.StatusStrip lbStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbArguments;
        private System.Windows.Forms.ListView lvLocals;
        private System.Windows.Forms.ColumnHeader lvColName;
        private System.Windows.Forms.ColumnHeader lvColValue;
        private System.Windows.Forms.ColumnHeader lvColType;
        private System.Windows.Forms.Button butPrevious;
        private System.Windows.Forms.Button butNext;
        private System.Windows.Forms.ColumnHeader lvColID;
        private System.Windows.Forms.ToolStripMenuItem detachProcessToolStripMenuItem;
        private System.Windows.Forms.ListView lvExceptions;
        private System.Windows.Forms.ColumnHeader lveType;
        private System.Windows.Forms.ColumnHeader lveTime;
        private System.Windows.Forms.ToolStripMenuItem dumpStacksToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageAssemblies;
        private System.Windows.Forms.ColumnHeader Message;
        private System.Windows.Forms.ListView lv_Assemblies;
        private System.Windows.Forms.ColumnHeader colFullName;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.ColumnHeader colSymbols;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debuggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.TabPage tabMessages;
        private System.Windows.Forms.ListView lvTraceMessages;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.ColumnHeader colSwitch;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ToolStripMenuItem stopProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readSymbolsToolStripMenuItem;
        private System.Windows.Forms.TabPage tabAppDomains;
        private System.Windows.Forms.ListView lvAppDomains;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStripMenuItem dumpHeapStatisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findVariableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHandlesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processInfoToolStripMenuItem;
    }
}

