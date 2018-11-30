using GameProtocol.model.login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 程序运行时来自网络消息存储的单例
/// </summary>
public class GameSession
{
    /// <summary>
    /// 声明一个单例
    /// </summary>
    private static GameSession instance;
    public static GameSession Instance
    {
        get
        {
            if(instance ==null)
            {
                instance = new GameSession();
            }
            return instance;
        }
    }

    /// <summary>
    /// 用户信息修改监听
    /// </summary>
    public Action UserInfoChangeHandler;
    
    /// <summary>
    /// 用户信息的存储
    /// </summary>
    private UserModel userinfo;
    public UserModel UserInfo
    {
        get
        {
            return userinfo;
        }
        set
        {
            userinfo = value;
            if (UserInfoChangeHandler != null)
                UserInfoChangeHandler();
        }
    }
}

