using M3u8Puller.Config;
using M3u8Puller.Kit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace M3u8Puller.Entity
{
    [Serializable]
    public class M3u8TaskEntity
    {
        public long Id { get; set; }
        /// <summary>
        /// 0等待 1下载 2暂停 -1删除
        /// </summary>
        public Int32 Status { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        internal List<TsEntity> Parts { get; set; }
        public int CompleteNum { get; set; }
        public int PartNum { get; set; }

        public M3u8TaskEntity(string m3u8, string name)
        {
            this.Id = SeqKit.Next();
            this.Name = name;
            this.Url = m3u8;
            this.Parts = new List<TsEntity>();

            string m3u8Content = GetM3u8Content(m3u8);
            string[] attrs = m3u8Content.Split('\n');
            foreach (string line in attrs)
            {
                if (String.IsNullOrEmpty(line))
                {
                    continue;
                }
                if (line.Trim().StartsWith("#EXT"))
                {
                    continue;
                }
                TsEntity ts = new TsEntity();
                ts.Status = 0;
                ts.Url = line.Trim();
                this.Parts.Add(ts);
            }
            this.CompleteNum = 0;
            this.PartNum = this.Parts.Count;
        }

        public string GetM3u8Content(string m3u8)
        {
            Console.WriteLine($"解析M3u8内容->{m3u8}");
            this.Url = m3u8;
            string m3u8Content = HttpStringDownload(m3u8);
            if (m3u8Content.ToLower().StartsWith("#EXTM3U".ToLower()))
            {
                if (!m3u8Content.ToLower().Contains(".m3u8"))
                {
                    return m3u8Content;
                }
                foreach (string line in m3u8Content.Split('\n'))
                {
                    if (!line.Trim().ToLower().EndsWith(".m3u8"))
                    {
                        continue;
                    }
                    if (!line.ToLower().StartsWith("http"))
                    {
                        if (line.StartsWith("/"))
                        {
                            Uri uri = new Uri(m3u8);
                            return GetM3u8Content(String.Format("{0}://{1}:{2}{3}", uri.Scheme, uri.Host, uri.Port, line));
                        }

                        return GetM3u8Content(m3u8.Substring(0, m3u8.LastIndexOf("/")) + line);
                    }
                    return GetM3u8Content(line);
                }
            }
            m3u8Content = HttpUtility.UrlDecode(m3u8Content);
            if (!m3u8Content.ToLower().Contains(".m3u8"))
            {
                WebBrowser browser = new WebBrowser();
                browser.Url = new Uri(m3u8);

                browser.DocumentCompleted += (send, e) =>
                {
                    m3u8Content = browser.Document.Body.InnerHtml;
                };
            }

            int index = m3u8Content.ToLower().IndexOf(".m3u8");
            if (index < 0)
            {
                return null;
            }
            string content = m3u8Content.Substring(0, index + 5);

            int start = 0;

            string[] splits = new string[] { "\"", "\n", "\t", "    ", " ", "'" };
            foreach (string split in splits)
            {
                int temp = content.LastIndexOf(split);
                if (start < temp)
                {
                    start = temp;
                }
            }
            string url = content.Substring(start + 1);
            if (!url.ToLower().StartsWith("http"))
            {
                if (url.StartsWith("/"))
                {
                    Uri uri = new Uri(m3u8);
                    return GetM3u8Content(String.Format("{0}://{1}:{2}/{3}", uri.Scheme, uri.Host, uri.Port, url));
                }
                return GetM3u8Content(m3u8.Substring(0, m3u8.LastIndexOf("/")) + url);
            }
            return GetM3u8Content(url);
        }
        public void Download()
        {
            Status = 1;
            for (int i = 0; i < SystemConfig.TASK_THREAD; i++)
            {
                Task.Factory.StartNew(() =>
                {

                    while (Parts.Count > 0)
                    {
                        try
                        {
                            if (Status == -1)
                            {
                                break;
                            }
                            if (Status != 1)
                            {
                                Thread.Sleep(100);
                                continue;
                            }
                            foreach (TsEntity part in Parts)
                            {
                                try
                                {
                                    lock (part)
                                    {
                                        if (part.Status != 0)
                                        {
                                            continue;
                                        }
                                        part.Status = 1;
                                    }
                                    string url = part.Url;
                                    if (!url.ToLower().StartsWith("http"))
                                    {
                                        if (url.StartsWith("/"))
                                        {
                                            Uri uri = new Uri(Url);
                                            url = String.Format("{0}://{1}:{2}{3}", uri.Scheme, uri.Host, uri.Port, url);
                                        }
                                        else
                                        {
                                            url = Url.Substring(0, Url.LastIndexOf("/") + 1) + url;
                                        }
                                    }
                                    byte[] data = HttpDownload(url);
                                    part.Data = data;
                                    part.Status = 2;
                                    CompleteNum++;
                                    Console.WriteLine($"下载进度->{CompleteNum * 100D / PartNum}");
                                    MergeToFile();
                                    break;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"下载失败->{e.Message}:{Url}");
                                    part.Status = 0;
                                }
                            }
                        }
                        catch { }
                    }
                }, TaskCreationOptions.LongRunning);
            }
        }
        private static string HttpStringDownload(string url)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            request.Method = "GET";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36";
            request.Referer = url;
            request.Headers.Add("Upgrade-Insecure-Requests", "1");


            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream stream = response.GetResponseStream();

            MemoryStream buffer = new MemoryStream();

            int bye;
            while ((bye = stream.ReadByte()) != -1)
            {
                buffer.WriteByte((byte)bye);
            }
            stream.Close();
            byte[] bytes = buffer.ToArray();
            buffer.Close();

            Encoding encode = Encoding.UTF8;
            foreach (string head in response.Headers.AllKeys)
            {
                if (!head.ToLower().Equals("content-type"))
                {
                    continue;
                }
                string value = response.Headers.Get(head).Replace(" ", "");
                if (!value.ToLower().Contains("charset="))
                {
                    continue;
                }

                try { encode = Encoding.GetEncoding(value.Substring(value.IndexOf("charset=") + 8).Trim()); } catch { }

            }
            return encode.GetString(bytes);
        }
        private static byte[] HttpDownload(string url)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            request.Method = "GET";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36";
            request.Referer = url;
            request.Headers.Add("Upgrade-Insecure-Requests", "1");


            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream stream = response.GetResponseStream();

            MemoryStream buffer = new MemoryStream();

            int bye;
            while ((bye = stream.ReadByte()) != -1)
            {
                buffer.WriteByte((byte)bye);
            }
            stream.Close();
            byte[] bytes = buffer.ToArray();
            buffer.Close();
            return bytes;
        }
        public void MergeToFile()
        {
            lock (String.Intern(Url))
            {
                if (Parts.Count < 1)
                {
                    return;
                }
                string _file = Name + ".cache";
                if (!File.Exists(_file))
                {
                    File.Create(_file);
                    if (Parts[0].Status != 2)
                    {
                        return;
                    }
                }
                FileStream fStream = new FileStream(_file, FileMode.Append);
                while (Parts.Count > 0 && Parts[0].Status == 2)
                {
                    fStream.Write(Parts[0].Data, 0, Parts[0].Data.Length);
                    Parts.Remove(Parts[0]);
                }
                fStream.Close();
                if (Parts.Count == 0)
                {
                    File.Move(Name + ".cache", Name);
                }
            }
        }
    }
}
