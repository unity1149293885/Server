using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    //协议执行事件
    public class Protocol
    {
        public static byte[] Protocol_0(byte[] ClientData)
        {
            Console.WriteLine("服务端没有该协议，请检查protocol类");
            byte[] ServerData = Encoding.Default.GetBytes("未知的协议");
            return ServerData;
        }
        public static byte[] Protocol_1(byte[] ClientData)
        {
            byte[] ServerData = ClientData;
            return ServerData;
        }

        public static byte[] Protocol_100001(byte[] ClientData)
        {
            return null;
        }
        public static LoginRst Protocol_100002(byte[] ClientData)
        {
            byte[] ServerData = ClientData;
            string jsonStr = Encoding.UTF8.GetString(ClientData);

            LoginReq req = JsonConvert.DeserializeObject<LoginReq>(jsonStr);

            //检测是否可以登录
            string name = req.Name;
            string phone = req.Phone;
            DateTime LoginTime = req.Time;
            
            var rst = UserManager.isLoginSuccess(name, phone);

            var code = rst.StateCode;
            string loginstate = "";
            switch(code)
            {
                case LoginCode.Login_Success:
                    loginstate = "登录成功";
                    break;
                case LoginCode.Login_Fail_UnLogin:
                    loginstate = "账号未注册";
                    break;
                case LoginCode.Login_Fail_PasswordError:
                    loginstate = "名字电话号不对应";
                    break;
            }

            Console.WriteLine("用户uid:" + rst.uid + "登录状态："+loginstate+" 登录时间：" + LoginTime.Month + "月" + LoginTime.Day + "日" + LoginTime.Hour + "时" + LoginTime.Minute + "分" + LoginTime.Second + "秒");

            return rst;

        }
        public static byte[] Protocol_200001(byte[] ClientData)
        {
            return null;
        }
        public static Animal Protocol_200002(byte[] ClientData)
        {
            byte[] ServerData = ClientData;

            string jsonStr = Encoding.UTF8.GetString(ClientData);
            Animal animal = JsonConvert.DeserializeObject<Animal>(jsonStr);

            int count = 0;
            foreach (var t in animal.SkillIdList)
            {
                count += t;
            }

            //操作对象
            animal.price = count;

            return animal;
        }
        public static byte[] Protocol_200003(byte[] ClientData)
        {
            return null;
        }
        public static People Protocol_200004(byte[] ClientData)
        {
            byte[] ServerData = ClientData;

            string jsonStr = Encoding.UTF8.GetString(ClientData);
            People peo = JsonConvert.DeserializeObject<People>(jsonStr);

            peo.friends = new Dictionary<string, string>();

            peo.friends.Add("1", "张三");
            peo.friends.Add("2", "李四");
            peo.friends.Add("3", "王五");


            return peo;
        }
    }
}
