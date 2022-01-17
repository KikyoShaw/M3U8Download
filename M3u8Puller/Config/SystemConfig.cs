using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M3u8Puller.Config
{
    class SystemConfig
    {
        public static int TASK_THREAD = 10;

        public static int TASK_PARALLEL = 5;

        public static string SAVE_DIR = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }
}
