using M3u8Puller.Config;
using M3u8Puller.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M3u8Puller
{
    public partial class FrmMain : Form
    {
        List<M3u8TaskEntity> tasks = new List<M3u8TaskEntity>();

        public static Icon icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
        public FrmMain()
        {
            InitializeComponent();
        }

        private void 添加任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTaskAdd frm = new FrmTaskAdd(this);
            frm.Left = this.Left + (this.Width - frm.Width) / 2;
            frm.Top = this.Top + (this.Height - frm.Height) / 2;
            frm.ShowDialog();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Icon = icon;
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(100);
                    lock (this)
                    {
                        int thread = 0;
                        foreach (M3u8TaskEntity task in tasks)
                        {
                            if (task.Status == 1)
                            {
                                thread++;
                            }
                        }

                        for (int i = thread; i < SystemConfig.TASK_PARALLEL; i++)
                        {
                            foreach (M3u8TaskEntity task in tasks)
                            {
                                if (task.Status == 0)
                                {
                                    Task.Factory.StartNew(() =>
                                    {
                                        task.Status = 1;
                                        task.Download();
                                    }, TaskCreationOptions.LongRunning);
                                }
                            }

                        }
                        foreach (M3u8TaskEntity task in tasks)
                        {
                            if (task.Status != 1)
                            {
                                continue;
                            }
                            this.BeginInvoke(new MethodInvoker(() =>
                            {
                                foreach (ListViewItem item in lvClients.Items)
                                {
                                    if (Convert.ToString(item.SubItems[0].Text).Equals(Convert.ToString(task.Id)))
                                    {
                                        double speed = task.CompleteNum * 100D / task.PartNum;
                                        if (speed > 100)
                                        {
                                            speed = 100;
                                        }
                                        item.SubItems[3].Text = String.Format("{0:F}", speed) + "%";
                                    }
                                }
                            }));
                        }
                    }
                }
            }, TaskCreationOptions.LongRunning);


        }

        public void AddTask(M3u8TaskEntity task)
        {
            ListViewItem item = new ListViewItem(Convert.ToString(task.Id));
            item.SubItems.Add(task.Url);
            item.SubItems.Add(task.Name);
            item.SubItems.Add("等待下载");
            lvClients.Items.Add(item);
            tasks.Add(task);
        }

        private void 开始任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (this)
            {
                try
                {
                    string id = lvClients.SelectedItems[0].SubItems[0].Text;
                    foreach (M3u8TaskEntity task in tasks)
                    {
                        if (Convert.ToString(task.Id).Equals(id))
                        {
                            lvClients.SelectedItems[0].SubItems[3].Text = "等待下载";
                            task.Status = 0;
                        }
                    }
                }
                catch { }
            }
        }

        private void 删除任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (this)
            {
                try
                {
                    string id = lvClients.SelectedItems[0].SubItems[0].Text;
                    foreach (M3u8TaskEntity task in tasks)
                    {
                        if (Convert.ToString(task.Id).Equals(id))
                        {

                            task.Status = -1;
                            lvClients.Items.Remove(lvClients.SelectedItems[0]);
                        }
                    }
                }
                catch { }
            }
        }

        private void 暂停任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (this)
            {
                try
                {
                    string id = lvClients.SelectedItems[0].SubItems[0].Text;
                    foreach (M3u8TaskEntity task in tasks)
                    {
                        if (Convert.ToString(task.Id).Equals(id))
                        {
                            lvClients.SelectedItems[0].SubItems[3].Text = "暂停下载";
                            task.Status = 2;
                        }
                    }
                }
                catch { }
            }
        }

        private void 打开目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (this)
            {
                try
                {
                    string path = lvClients.SelectedItems[0].SubItems[2].Text;
                    FileInfo file = new FileInfo(path);

                    System.Diagnostics.Process.Start("explorer.exe", file.Directory.FullName);
                }
                catch { }
            }
        }

        private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Really want to exit?", "Remind", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                Process.GetCurrentProcess().Kill();
            }
        }
    }
}
