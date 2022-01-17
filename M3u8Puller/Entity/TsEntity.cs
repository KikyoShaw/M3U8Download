using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M3u8Puller.Entity
{
    [Serializable]
    class TsEntity
    {
        /// <summary>
        /// TS文件地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 数据内容
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// 0等待下载 1下载中 2下载完成
        /// </summary>
        public int Status { get; set; }
    }
}
