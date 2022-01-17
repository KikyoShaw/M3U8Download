namespace M3u8Puller
{
    partial class FrmMain
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvClients = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TaskListContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.暂停任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出程序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.TaskListContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvClients);
            this.groupBox1.Location = new System.Drawing.Point(4, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(547, 315);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "任务列表";
            // 
            // lvClients
            // 
            this.lvClients.BackColor = System.Drawing.Color.White;
            this.lvClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.lvClients.ContextMenuStrip = this.TaskListContextMenuStrip;
            this.lvClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvClients.Font = new System.Drawing.Font("新宋体", 8.65F);
            this.lvClients.FullRowSelect = true;
            this.lvClients.HideSelection = false;
            this.lvClients.Location = new System.Drawing.Point(3, 17);
            this.lvClients.MultiSelect = false;
            this.lvClients.Name = "lvClients";
            this.lvClients.Size = new System.Drawing.Size(541, 295);
            this.lvClients.TabIndex = 14;
            this.lvClients.UseCompatibleStateImageBehavior = false;
            this.lvClients.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "任务ID";
            this.columnHeader8.Width = 100;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "M3u8地址";
            this.columnHeader9.Width = 200;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "保存路劲";
            this.columnHeader10.Width = 160;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "下载进度";
            // 
            // TaskListContextMenuStrip
            // 
            this.TaskListContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加任务ToolStripMenuItem,
            this.删除任务ToolStripMenuItem,
            this.开始任务ToolStripMenuItem,
            this.暂停任务ToolStripMenuItem,
            this.打开目录ToolStripMenuItem,
            this.退出程序ToolStripMenuItem});
            this.TaskListContextMenuStrip.Name = "TaskListContextMenuStrip";
            this.TaskListContextMenuStrip.Size = new System.Drawing.Size(125, 136);
            // 
            // 添加任务ToolStripMenuItem
            // 
            this.添加任务ToolStripMenuItem.Name = "添加任务ToolStripMenuItem";
            this.添加任务ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.添加任务ToolStripMenuItem.Text = "添加任务";
            this.添加任务ToolStripMenuItem.Click += new System.EventHandler(this.添加任务ToolStripMenuItem_Click);
            // 
            // 删除任务ToolStripMenuItem
            // 
            this.删除任务ToolStripMenuItem.Name = "删除任务ToolStripMenuItem";
            this.删除任务ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.删除任务ToolStripMenuItem.Text = "删除任务";
            this.删除任务ToolStripMenuItem.Click += new System.EventHandler(this.删除任务ToolStripMenuItem_Click);
            // 
            // 开始任务ToolStripMenuItem
            // 
            this.开始任务ToolStripMenuItem.Name = "开始任务ToolStripMenuItem";
            this.开始任务ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.开始任务ToolStripMenuItem.Text = "开始任务";
            this.开始任务ToolStripMenuItem.Click += new System.EventHandler(this.开始任务ToolStripMenuItem_Click);
            // 
            // 暂停任务ToolStripMenuItem
            // 
            this.暂停任务ToolStripMenuItem.Name = "暂停任务ToolStripMenuItem";
            this.暂停任务ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.暂停任务ToolStripMenuItem.Text = "暂停任务";
            this.暂停任务ToolStripMenuItem.Click += new System.EventHandler(this.暂停任务ToolStripMenuItem_Click);
            // 
            // 打开目录ToolStripMenuItem
            // 
            this.打开目录ToolStripMenuItem.Name = "打开目录ToolStripMenuItem";
            this.打开目录ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.打开目录ToolStripMenuItem.Text = "打开目录";
            this.打开目录ToolStripMenuItem.Click += new System.EventHandler(this.打开目录ToolStripMenuItem_Click);
            // 
            // 退出程序ToolStripMenuItem
            // 
            this.退出程序ToolStripMenuItem.Name = "退出程序ToolStripMenuItem";
            this.退出程序ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.退出程序ToolStripMenuItem.Text = "退出程序";
            this.退出程序ToolStripMenuItem.Click += new System.EventHandler(this.退出程序ToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(553, 322);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "M3u8Puller Beta V1.0";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.TaskListContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvClients;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ContextMenuStrip TaskListContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 添加任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开始任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 暂停任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开目录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出程序ToolStripMenuItem;
    }
}

