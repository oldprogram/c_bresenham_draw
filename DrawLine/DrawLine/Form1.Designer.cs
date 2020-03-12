namespace DrawLine
{
    partial class form
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.连线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.四联通填充ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.floodFillAlgorithmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanLineFillingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PortList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.运行ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(492, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.新建ToolStripMenuItem.Text = "新建";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 运行ToolStripMenuItem
            // 
            this.运行ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.连线ToolStripMenuItem,
            this.四联通填充ToolStripMenuItem,
            this.scanLineFillingToolStripMenuItem});
            this.运行ToolStripMenuItem.Name = "运行ToolStripMenuItem";
            this.运行ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.运行ToolStripMenuItem.Text = "运行";
            // 
            // 连线ToolStripMenuItem
            // 
            this.连线ToolStripMenuItem.Name = "连线ToolStripMenuItem";
            this.连线ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.连线ToolStripMenuItem.Text = "Bresenham";
            this.连线ToolStripMenuItem.Click += new System.EventHandler(this.连线ToolStripMenuItem_Click);
            // 
            // 四联通填充ToolStripMenuItem
            // 
            this.四联通填充ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.floodFillAlgorithmToolStripMenuItem});
            this.四联通填充ToolStripMenuItem.Name = "四联通填充ToolStripMenuItem";
            this.四联通填充ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.四联通填充ToolStripMenuItem.Text = "Seed Filling";
            // 
            // floodFillAlgorithmToolStripMenuItem
            // 
            this.floodFillAlgorithmToolStripMenuItem.Name = "floodFillAlgorithmToolStripMenuItem";
            this.floodFillAlgorithmToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.floodFillAlgorithmToolStripMenuItem.Text = "Flood Fill Algorithm";
            this.floodFillAlgorithmToolStripMenuItem.Click += new System.EventHandler(this.floodFillAlgorithmToolStripMenuItem_Click);
            // 
            // scanLineFillingToolStripMenuItem
            // 
            this.scanLineFillingToolStripMenuItem.Name = "scanLineFillingToolStripMenuItem";
            this.scanLineFillingToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.scanLineFillingToolStripMenuItem.Text = "Scan Line Filling";
            this.scanLineFillingToolStripMenuItem.Click += new System.EventHandler(this.scanLineFillingToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PortList
            // 
            this.PortList.FormattingEnabled = true;
            this.PortList.Location = new System.Drawing.Point(427, 3);
            this.PortList.Name = "PortList";
            this.PortList.Size = new System.Drawing.Size(60, 20);
            this.PortList.TabIndex = 1;
            this.PortList.SelectedIndexChanged += new System.EventHandler(this.PortList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Menu;
            this.label1.Location = new System.Drawing.Point(382, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "像素：";
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(341, 7);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(15, 14);
            this.checkBox.TabIndex = 3;
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.MenuBar;
            this.label2.Location = new System.Drawing.Point(260, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "4 yes 8 no：";
            // 
            // form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(492, 404);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PortList);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "form";
            this.Text = "Beautifulzzzz";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 连线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox PortList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem 四联通填充ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem floodFillAlgorithmToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem scanLineFillingToolStripMenuItem;
    }
}

