﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{

    //协议号
    public enum ProcolCode
    {
        Code_Login_req = 100001,//登录请求
        Code_Login_rst = 100002,//登录返回

        Code_Register_req = 100003,//注册请求
        Code__Register_rs = 100004,//注册返回
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
        //10002 注册成功
        //20001 密码错误
        //20002 未注册
        //20003 该用户已注册
        Login_Success = 10001,
        Login_Fail_PasswordError = 20001,
        Login_Fail_UnLogin = 20002,

        Register_Success = 10002,
        Register_Fail_isHave = 20003,
    }
    #endregion

    #region 注册用户
    public class RegiesterUserReq
    {
        public string name;
        public string phone;
        public UserType type;

        public RegiesterUserReq(string name, string phone, UserType type)
        {
            this.name = name;
            this.phone = phone;
            this.type = type;
        }
    }

    public class RegiesterUserRst
    {
        public LoginCode StateCode;
        public long uid;

        public RegiesterUserRst(LoginCode code,long uid)
        {
            StateCode = code;
        }
    }
    #endregion

}
