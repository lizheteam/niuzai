using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class DebugManager : MonoBehaviour {
    void Awake()
    {
        //为单例组件赋值
        GameApp.Instance.DebugManagerScript = this;
    }

    void Start()
    {
        //为unity的log事件添加一个监听
        if (GameData.Instance.IsDebugWrite)
            Application.logMessageReceived += UnityLogMessage;
    }

    #region 对unity打印函数的封装
    public void Log(object obj)
    {
        Debug.Log(obj);
    }

    public void LogWarning(object obj)
    {
        Debug.Log(obj);
    }

    public void LogError(object obj)
    {
        Debug.Log(obj);
    }
    #endregion

    /// <summary>
    /// 为unity添加监听事件
    /// </summary>
    /// <param name="logMessage"></param>
    /// <param name="stackTrace"></param>
    /// <param name="type"></param>
    void UnityLogMessage(string logMessage, string stackTrace, LogType type)
    {
        switch (type)
        {
            //如果是普通日志和警告日志，则只打印日志时间和日志消息
            case LogType.Log:
            case LogType.Warning:
                logMessage = DateTime.Now.ToString("yyyy-MM-dd") + logMessage;
                break;
            //如果是错误日志和异常日志，则添加打印时间和异常错误脚本组件的发生位置及调用信息
            default:
                logMessage = DateTime.Now.ToString("yyyy-MM-dd") + logMessage + "<" + stackTrace + ">";
                break;
        }
        Write(logMessage, type);
    }

    //声明一个数据流对象
    StreamWriter streamWrite;

    /// <summary>
    /// 将输出存储至本地
    /// </summary>
    /// <param name="logMessage"></param>
    /// <param name="type"></param>
    void Write(string logMessage, LogType type)
    {
        //获取本机的沙盒路径
        string path = Application.persistentDataPath+"/OutLog/"+type;
        //pc平台上的数据存储路径为  c:/  d：/  e:/   f:/
        //pc平台上的沙盒路径为   C:\Users\Administrator\AppData\LocalLow\nzqp\棋牌
        //android平台沙盒路径为  /storage/emulated/0/Android/data/com.nzqp.game/files

        try
        {
            //如果路径不存在，创建路径
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path += "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            //文件存在则加载，不存在则创建
            if (streamWrite == null)
                streamWrite = !File.Exists(path) ? File.CreateText(path) : File.AppendText(path);
            //写入
            streamWrite.WriteLine(logMessage);
        }finally
        {
            if(streamWrite !=null)
            {
                //释放数据流对象
                streamWrite.Flush();
                streamWrite.Dispose();
                streamWrite = null;
            }
        }
    }
}
