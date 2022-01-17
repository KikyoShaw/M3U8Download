using M3u8Puller.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace M3u8Puller
{
    public partial class FrmTaskAdd : Form
    {
        FrmMain parent;
        public FrmTaskAdd(FrmMain main)
        {
            InitializeComponent();
            this.parent = main;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            if (textBox2.Text.Equals(""))
            {
                textBox2.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1_TextChanged(sender, e);
                M3u8TaskEntity task = new M3u8TaskEntity(textBox1.Text.Trim(), textBox2.Text.Trim());
                parent.AddTask(task);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"文件解析失败,{ex.Message}");
            }

        }

        private void FrmTaskAdd_Load(object sender, EventArgs e)
        {
            this.Icon = FrmMain.icon;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                return;
            }
            if (String.IsNullOrEmpty(textBox2.Text) || textBox2.Text.Equals(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)))
            {
                textBox2.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Md5Encript(textBox1.Text) + ".mp4";
                return;
            }

            if (Directory.Exists(textBox2.Text.Trim()))
            {
                textBox2.Text = textBox2.Text.Trim() + "\\" + Md5Encript(textBox1.Text) + ".mp4";
                return;
            }
        }

        private string Md5Encript(string str)
        {
            MD5 md5 = MD5.Create();
            //要加密的字符串
            //字节数组
            byte[] strbuffer = Encoding.Default.GetBytes(str);
            //加密并返回字节数组
            strbuffer = md5.ComputeHash(strbuffer);
            string strNew = "";
            foreach (byte item in strbuffer)
            {
                //对字节数组中元素格式化后拼接
                strNew += item.ToString("x2");
            }
            return strNew;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = dialog.FileName;
            }
        }
    }
}
