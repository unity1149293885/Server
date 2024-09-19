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

        //登录
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

        //注册
        public static byte[] Protocol_200001(byte[] ClientData)
        {
            return null;
        }
        public static RegiesterUserRst Protocol_200002(byte[] ClientData)
        {
            byte[] ServerData = ClientData;
            string jsonStr = Encoding.UTF8.GetString(ClientData);

            RegiesterUserReq req = JsonConvert.DeserializeObject<RegiesterUserReq>(jsonStr);

            //检测是否可以注册
            string name = req.name;
            string phone = req.phone;
            UserType type = req.type;

            LoginCode code;
            RegiesterUserRst rst = null;
            var user = UserManager.getHaveUser(name);
            if (user != null)
            {
                code = LoginCode.Register_Fail_isHave;
                rst = new RegiesterUserRst(code, -1);
            }
            else
            {
                //随机uid
                 int uid = (new Random().Next(1000001, 1999999));
                //写入数据库
                MySqlTools.RegisterUser(uid, name, phone, (int)type);

                code = LoginCode.Register_Success;
                rst = new RegiesterUserRst(code, uid);

                //数据库写完之后需要更新当前维护的玩家列表
                MySqlTools.UpdateAllUserInfo();
            }
            
            string registerstate = "";
            switch (code)
            {
                case LoginCode.Register_Success:
                    registerstate = "注册成功";
                    break;
                case LoginCode.Register_Fail_isHave:
                    registerstate = "账号之前已注册";
                    break;
            }

            string message = "用户uid:" + rst.uid + "注册状态：" + registerstate;
            Output.OutputInfo(message);

            return rst;

        }
        public static byte[] Protocol_200003(byte[] ClientData)
        {
            return null;
        }
        public static Animal Protocol_200004(byte[] ClientData)
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
    }
}
