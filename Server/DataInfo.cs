using System;
using System.Collections.Generic;

namespace Server
{
    public static class ConnectInfo
    {
        public static string ipAddress = "10.12.20.93";
        public static int Port = 7788;
    }

    public static class MySqlInfo
    {
        public static string server = "localhost";
        public static string user = "root";
        public static string database = "UserDataBase";
        public static string password = "zzs20001114";
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


    public enum UserType
    {
        Mamager,
        Teamer,
        Broker,
    }
    public class UserInfo
    {
        public int Uid;
        public string Name;
        public string Phone;
        public Login_state State;
        public UserType Type;

        public UserInfo(int uid,string name,string phone,int type)
        {
            this.Uid = uid;
            this.Name = name;
            this.Phone = phone;
            this.Type = (UserType)type;
        }
    }
    #region 登录数据
    public enum Login_state
    {
        logined = 1,
        unlogin = 2
    }
    public class LoginReq
    {
        public string Name;
        public string Phone;
        public DateTime Time;
    }

    public class LoginRst
    {
        public LoginCode StateCode;
        public int uid;
    }

    public enum LoginCode
    {
        //10001 登录成功
        //20001 密码错误
        //20002 未注册
        Login_Success = 10001,
        Login_Fail_PasswordError = 20001,
        Login_Fail_UnLogin = 20002,
    }
    #endregion


}
