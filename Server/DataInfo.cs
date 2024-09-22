using System;
using System.Collections.Generic;

namespace Server
{
    public enum IpType
    {
        Home = 1,
        Cyou = 2
    }
    public static class ConnectInfo
    {
        public static string ipAddressCyou = "10.12.20.93";

        public static string ipAddressHome = "192.168.137.1";

        public static int Port = 7788;

        public static IpType type = IpType.Home;
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
        Mamager = 1,
        Teamer = 2,
        Broker = 3,
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
}
