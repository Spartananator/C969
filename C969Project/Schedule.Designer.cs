﻿namespace Scheduling_Software
{
    partial class Schedule
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.deleteButton = new System.Windows.Forms.Button();
            this.modifyButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.dataViewer = new System.Windows.Forms.TabControl();
            this.appointmentTab = new System.Windows.Forms.TabPage();
            this.appointmentGrid = new System.Windows.Forms.DataGridView();
            this.customerTab = new System.Windows.Forms.TabPage();
            this.customerGrid = new System.Windows.Forms.DataGridView();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.userLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pastBox = new System.Windows.Forms.CheckBox();
            this.generateReports = new System.Windows.Forms.Button();
            this.monthBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.dataViewer.SuspendLayout();
            this.appointmentTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentGrid)).BeginInit();
            this.customerTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customerGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.73034F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.26966F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer3, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 459);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(550, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.monthCalendar1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.monthBox);
            this.splitContainer1.Panel2.Controls.Add(this.generateReports);
            this.splitContainer1.Size = new System.Drawing.Size(247, 453);
            this.splitContainer1.SplitterDistance = 170;
            this.splitContainer1.TabIndex = 1;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(9, 3);
            this.monthCalendar1.MaxSelectionCount = 1;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.ShowTodayCircle = false;
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(89, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataViewer);
            this.splitContainer2.Size = new System.Drawing.Size(455, 453);
            this.splitContainer2.SplitterDistance = 27;
            this.splitContainer2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.deleteButton);
            this.panel1.Controls.Add(this.modifyButton);
            this.panel1.Controls.Add(this.addButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(455, 27);
            this.panel1.TabIndex = 0;
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.deleteButton.AutoSize = true;
            this.deleteButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.deleteButton.Location = new System.Drawing.Point(404, 2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(48, 23);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.delete_Click);
            // 
            // modifyButton
            // 
            this.modifyButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.modifyButton.AutoSize = true;
            this.modifyButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.modifyButton.Location = new System.Drawing.Point(350, 2);
            this.modifyButton.Name = "modifyButton";
            this.modifyButton.Size = new System.Drawing.Size(48, 23);
            this.modifyButton.TabIndex = 1;
            this.modifyButton.Text = "Modify";
            this.modifyButton.UseVisualStyleBackColor = true;
            this.modifyButton.Click += new System.EventHandler(this.modify_Click);
            // 
            // addButton
            // 
            this.addButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.addButton.AutoSize = true;
            this.addButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addButton.Location = new System.Drawing.Point(308, 2);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(36, 23);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.add_Click);
            // 
            // dataViewer
            // 
            this.dataViewer.Controls.Add(this.appointmentTab);
            this.dataViewer.Controls.Add(this.customerTab);
            this.dataViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataViewer.Location = new System.Drawing.Point(0, 0);
            this.dataViewer.Name = "dataViewer";
            this.dataViewer.SelectedIndex = 0;
            this.dataViewer.Size = new System.Drawing.Size(455, 422);
            this.dataViewer.TabIndex = 1;
            this.dataViewer.Selected += new System.Windows.Forms.TabControlEventHandler(this.dataViewer_Selected);
            // 
            // appointmentTab
            // 
            this.appointmentTab.Controls.Add(this.appointmentGrid);
            this.appointmentTab.Location = new System.Drawing.Point(4, 22);
            this.appointmentTab.Name = "appointmentTab";
            this.appointmentTab.Padding = new System.Windows.Forms.Padding(3);
            this.appointmentTab.Size = new System.Drawing.Size(447, 396);
            this.appointmentTab.TabIndex = 0;
            this.appointmentTab.Text = "Appointments";
            this.appointmentTab.UseVisualStyleBackColor = true;
            // 
            // appointmentGrid
            // 
            this.appointmentGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.appointmentGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appointmentGrid.Location = new System.Drawing.Point(3, 3);
            this.appointmentGrid.Name = "appointmentGrid";
            this.appointmentGrid.Size = new System.Drawing.Size(441, 390);
            this.appointmentGrid.TabIndex = 1;
            // 
            // customerTab
            // 
            this.customerTab.Controls.Add(this.customerGrid);
            this.customerTab.Location = new System.Drawing.Point(4, 22);
            this.customerTab.Name = "customerTab";
            this.customerTab.Padding = new System.Windows.Forms.Padding(3);
            this.customerTab.Size = new System.Drawing.Size(452, 396);
            this.customerTab.TabIndex = 1;
            this.customerTab.Text = "Customer\'s";
            this.customerTab.UseVisualStyleBackColor = true;
            // 
            // customerGrid
            // 
            this.customerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customerGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customerGrid.Location = new System.Drawing.Point(3, 3);
            this.customerGrid.Name = "customerGrid";
            this.customerGrid.RowHeadersVisible = false;
            this.customerGrid.Size = new System.Drawing.Size(446, 390);
            this.customerGrid.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.panel3);
            this.splitContainer3.Size = new System.Drawing.Size(80, 453);
            this.splitContainer3.SplitterDistance = 82;
            this.splitContainer3.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.userLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(80, 82);
            this.panel2.TabIndex = 0;
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(-3, 14);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(52, 13);
            this.userLabel.TabIndex = 0;
            this.userLabel.Text = "Welcome";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.pastBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(80, 367);
            this.panel3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filters";
            // 
            // pastBox
            // 
            this.pastBox.AutoSize = true;
            this.pastBox.Location = new System.Drawing.Point(0, 24);
            this.pastBox.Name = "pastBox";
            this.pastBox.Size = new System.Drawing.Size(80, 17);
            this.pastBox.TabIndex = 0;
            this.pastBox.Text = "Past Appts.";
            this.pastBox.UseVisualStyleBackColor = true;
            this.pastBox.CheckedChanged += new System.EventHandler(this.pastBox_CheckedChanged);
            // 
            // generateReports
            // 
            this.generateReports.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.generateReports.Location = new System.Drawing.Point(129, 249);
            this.generateReports.Name = "generateReports";
            this.generateReports.Size = new System.Drawing.Size(107, 23);
            this.generateReports.TabIndex = 2;
            this.generateReports.Text = "Generate Reports";
            this.generateReports.UseVisualStyleBackColor = true;
            this.generateReports.Click += new System.EventHandler(this.generateReports_Click);
            // 
            // monthBox
            // 
            this.monthBox.AutoSize = true;
            this.monthBox.Location = new System.Drawing.Point(9, 4);
            this.monthBox.Name = "monthBox";
            this.monthBox.Size = new System.Drawing.Size(175, 17);
            this.monthBox.TabIndex = 3;
            this.monthBox.Text = "SHOW MONTHLY MEETINGS";
            this.monthBox.UseVisualStyleBackColor = true;
            this.monthBox.CheckedChanged += new System.EventHandler(this.monthBox_CheckedChanged);
            // 
            // Schedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 483);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(856, 522);
            this.Name = "Schedule";
            this.Text = "Schedule";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.dataViewer.ResumeLayout(false);
            this.appointmentTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.appointmentGrid)).EndInit();
            this.customerTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customerGrid)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button modifyButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox pastBox;
        private System.Windows.Forms.TabControl dataViewer;
        private System.Windows.Forms.TabPage appointmentTab;
        private System.Windows.Forms.DataGridView appointmentGrid;
        private System.Windows.Forms.TabPage customerTab;
        private System.Windows.Forms.DataGridView customerGrid;
        private System.Windows.Forms.Button generateReports;
        private System.Windows.Forms.CheckBox monthBox;
    }
}