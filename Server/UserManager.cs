using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class UserManager
    {
        private static List<UserInfo> UserList = new List<UserInfo>();

        public static void AddUser(int uid,string name,string phone,int type)
        {
            var info = new UserInfo(uid, name, phone, type);
            UserList.Add(info);
        }

        public static List<UserInfo> GetAllUserList()
        {
            return UserList;
        }

        //注册
        public static void Regiester(string name, string phone)
        {
            //UserInfo user = new UserInfo();
            //user.Name = name;
            //user.Phone = phone;

            //user.uid = (new Random().Next(10000000, 99999999)).ToString();
            //while (ishaveUid(user.uid))
            //{
            //    user.uid = (new Random().Next(10000000, 99999999)).ToString();
            //}

            //UserList.Add(user);
        }

        //登录
        public static void Login(string name, string phone)
        {
            var user = getHaveUser(name);
            user.State = Login_state.logined;
        }

        //通过账号获取user信息
        public static UserInfo getHaveUser(string name)
        {
            foreach (var it in UserList)
            {
                if (name == it.Name)
                {
                    return it;
                }
            }
            return null;
        }

        //是否登录成功
        public static LoginRst isLoginSuccess(string name, string phone)
        {
            LoginRst rst = new LoginRst();

            LoginCode code;

            var user = getHaveUser(name);
            if (user == null) {
                rst.StateCode = LoginCode.Login_Fail_UnLogin;
                return rst;
            }
            if (user.Phone != phone)
            {
                code = LoginCode.Login_Fail_PasswordError;
            }
            else
            {
                code = LoginCode.Login_Success;
            }

            rst.StateCode = code;
            rst.uid = user.Uid;
            rst.userType = user.Type;
            return rst;
        }

        //uid是否已占用（uid唯一）
        public static bool ishaveUid(int uid)
        {
            foreach (var it in UserList)
            {
                if (it.Uid == uid)
                {
                    return true;
                }
            }
            return false;
        }

        public static void changeUserInfo(string password, string name)
        {
            ///
        }
    }
}
