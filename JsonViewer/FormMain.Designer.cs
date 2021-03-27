namespace JsonViewer
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.openJsonFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.imageListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.chkGlobalPoint = new System.Windows.Forms.CheckBox();
            this.lblFind = new System.Windows.Forms.Label();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.chkFilter = new System.Windows.Forms.CheckBox();
            this.menuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.menuMain.GripMargin = new System.Windows.Forms.Padding(10);
            this.menuMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openJsonFileToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.optionToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Margin = new System.Windows.Forms.Padding(20);
            this.menuMain.Name = "menuMain";
            this.menuMain.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuMain.Size = new System.Drawing.Size(1122, 55);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "Main Menu";
            // 
            // openJsonFileToolStripMenuItem
            // 
            this.openJsonFileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openJsonFileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.openJsonFileToolStripMenuItem.Margin = new System.Windows.Forms.Padding(10);
            this.openJsonFileToolStripMenuItem.Name = "openJsonFileToolStripMenuItem";
            this.openJsonFileToolStripMenuItem.Size = new System.Drawing.Size(158, 29);
            this.openJsonFileToolStripMenuItem.Text = "Open Json File...";
            this.openJsonFileToolStripMenuItem.Click += new System.EventHandler(this.openJsonFileToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.refreshToolStripMenuItem.Margin = new System.Windows.Forms.Padding(10);
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(91, 29);
            this.refreshToolStripMenuItem.Text = "Refresh!";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.optionToolStripMenuItem.Margin = new System.Windows.Forms.Padding(10);
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(110, 29);
            this.optionToolStripMenuItem.Text = "Columns...";
            this.optionToolStripMenuItem.Click += new System.EventHandler(this.optionToolStripMenuItem_Click);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 55);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.treeView);
            this.splitContainerMain.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.dataGridView);
            this.splitContainerMain.Size = new System.Drawing.Size(1122, 777);
            this.splitContainerMain.SplitterDistance = 262;
            this.splitContainerMain.SplitterWidth = 10;
            this.splitContainerMain.TabIndex = 1;
            // 
            // treeView
            // 
            this.treeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView.ForeColor = System.Drawing.Color.White;
            this.treeView.HideSelection = false;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageListTreeView;
            this.treeView.Indent = 25;
            this.treeView.LineColor = System.Drawing.Color.White;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(262, 777);
            this.treeView.StateImageList = this.imageListTreeView;
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // imageListTreeView
            // 
            this.imageListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeView.ImageStream")));
            this.imageListTreeView.TransparentColor = System.Drawing.Color.White;
            this.imageListTreeView.Images.SetKeyName(0, "object.png");
            this.imageListTreeView.Images.SetKeyName(1, "array.png");
            this.imageListTreeView.Images.SetKeyName(2, "json.png");
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidth = 62;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(850, 777);
            this.dataGridView.TabIndex = 0;
            // 
            // chkGlobalPoint
            // 
            this.chkGlobalPoint.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkGlobalPoint.AutoSize = true;
            this.chkGlobalPoint.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.chkGlobalPoint.FlatAppearance.BorderSize = 0;
            this.chkGlobalPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkGlobalPoint.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.chkGlobalPoint.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chkGlobalPoint.Location = new System.Drawing.Point(427, 10);
            this.chkGlobalPoint.Name = "chkGlobalPoint";
            this.chkGlobalPoint.Size = new System.Drawing.Size(133, 35);
            this.chkGlobalPoint.TabIndex = 2;
            this.chkGlobalPoint.Text = "Global Points";
            this.chkGlobalPoint.UseVisualStyleBackColor = false;
            this.chkGlobalPoint.CheckedChanged += new System.EventHandler(this.chkGlobalPoint_CheckedChanged);
            // 
            // lblFind
            // 
            this.lblFind.AutoSize = true;
            this.lblFind.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.lblFind.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.lblFind.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblFind.Location = new System.Drawing.Point(583, 15);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(48, 25);
            this.lblFind.TabIndex = 3;
            this.lblFind.Text = "Find";
            // 
            // txtFind
            // 
            this.txtFind.AcceptsReturn = true;
            this.txtFind.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFind.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.txtFind.Location = new System.Drawing.Point(633, 15);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(231, 26);
            this.txtFind.TabIndex = 4;
            this.txtFind.WordWrap = false;
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            // 
            // chkFilter
            // 
            this.chkFilter.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkFilter.AutoSize = true;
            this.chkFilter.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.chkFilter.FlatAppearance.BorderSize = 0;
            this.chkFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkFilter.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.chkFilter.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chkFilter.Location = new System.Drawing.Point(869, 11);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Size = new System.Drawing.Size(69, 35);
            this.chkFilter.TabIndex = 2;
            this.chkFilter.Text = "Filter!";
            this.chkFilter.UseVisualStyleBackColor = false;
            this.chkFilter.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1122, 832);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.lblFind);
            this.Controls.Add(this.chkFilter);
            this.Controls.Add(this.chkGlobalPoint);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.menuMain);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Json Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem openJsonFileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ImageList imageListTreeView;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkGlobalPoint;
        private System.Windows.Forms.Label lblFind;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.CheckBox chkFilter;
    }
}

