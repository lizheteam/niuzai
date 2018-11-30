using UnityEngine;
using System.Collections;

/// <summary>
/// 网络事件扩展
/// </summary>
public static class ExtendHandler  {
    /// <summary>
    /// 封装消息发送函数
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="Commond"></param>
    /// <param name="message"></param>
    public static void SendMessage(byte Type,int Command,object message)
    {
        GameApp.Instance.NetMessageUtilScript.NetIO.write(Type, Command, message);
    }

    /// <summary>
    /// 为mono添加一个发送消息函数
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="type"></param>
    /// <param name="command"></param>
    /// <param name="message"></param>
    public static void Write(this MonoBehaviour mono,byte type,int command,object message)
    {
        SendMessage(type, command, message);
    }

    /// <summary>
    /// 封装关闭网络
    /// </summary>
    /// <returns></returns>
    public static bool Close()
    {
        return GameApp.Instance.NetMessageUtilScript.NetIO.CloseSocket();
    }

    /// <summary>
    /// 封装连接网络
    /// </summary>
    /// <returns></returns>
    public static void Connect()
    {
        GameApp.Instance.NetMessageUtilScript.NetIO.ConnnectToSever();
    }


    /// <summary>
    /// 封装是否正在重新连接
    /// </summary>
    /// <returns></returns>
    public static bool IsReConnect()
    {
        return GameApp.Instance.NetMessageUtilScript.NetIO.IsReconnect;
    }
}
