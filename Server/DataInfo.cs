using System;
using System.Collections.Generic;

namespace Server
{

    public class UserManager
    {
        private List<UserInfo> UserList = new List<UserInfo>();

        private static readonly UserManager instance = new UserManager();

        public UserManager Instance {
            get => instance;
        }
        

        //注册
        public void Regiester(string account,string password)
        {
            UserInfo user = new UserInfo(); 
            user.Account = account;
            user.Password = password;

            user.uid = (new Random().Next(10000000, 99999999)).ToString();
            while (ishaveUid(user.uid))
            {
                user.uid = (new Random().Next(10000000, 99999999)).ToString();
            }

            UserList.Add(user);
        }

        //登录
        public void Login(string account, string password)
        {
            var user = getHaveUser(account);
            user.state = Login_state.logined;
        }

        //通过账号获取user信息
        public UserInfo getHaveUser(string account)
        {
            foreach(var it in UserList)
            {
                if(account == it.Account)
                {
                    return it;
                }
            }
            return null;
        }

        //校验密码
        public bool isLoginSuccess(string account,string password)
        {
            var user = getHaveUser(account);
            return user.Password == password;
        }

        //uid是否已占用（uid唯一）
        public bool ishaveUid(string uid)
        {
            foreach(var it in UserList)
            {
                if (it.uid != null)
                {
                    if (it.uid == uid)
                    {
                        return true;
                    }
                } 
            }
            return false;
        }

        public void changeUserInfo(string password,string name)
        {
            ///
        }
    }
    public class Animal
    {
        public int id;
        public string name;
        public List<int> SkillIdList;
        public int price;
    }

    public class People
    {
        public string name;
        public Dictionary<string, string> friends;
    }

    public enum Login_state
    {
        logined = 1,
        unlogin = 2
    }
    public class UserInfo
    {
        public string Account;
        public string Password;
        public string uid;
        public string name;
        public Login_state state;
    }
    public class LoginReq
    {
        public string Account;
        public string Password;
        public DateTime Time;
    }

    public class LoginRst
    {
        public bool isSuccessLogin;
        public string uid;
    }

}
