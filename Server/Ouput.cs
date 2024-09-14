using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Output
    {
        public static void OutputCurTime()
        {
            var time = DateTime.Now;
            Console.WriteLine("| 日志时间：" + time.Month + "月" + time.Day + "日" + time.Hour + "时" + time.Minute + "分" + time.Second + "秒");
        }

        public static void OutputInfo(string info)
        {
            Console.Write(info);
            OutputCurTime();
        }
    }
}
