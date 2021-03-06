﻿using UnityEngine;
using System.Collections;
using ClientNetFrame;
using System;
using GameProtocol;

/// <summary>
/// 客户端的消息分发中心
/// </summary>
public class NetMessageUtil : MonoBehaviour {
    /// <summary>
    /// 客户端通讯组件
    /// </summary>
    public InitializeNetIO NetIO;

    #region 二级协议分发组件
    /// <summary>
    /// 登录分发组件
    /// </summary>
    private LoginHandler loginHandler;
    private LoginHandler LoginHandler
    {
        get
        {
            if (loginHandler ==null)
            {
                if (!transform.GetComponent<LoginHandler>())
                    loginHandler = gameObject.AddComponent<LoginHandler>();
                else
                    loginHandler = transform.GetComponent<LoginHandler>();
            }
            return loginHandler;
        }
    }

    /// <summary>
    /// 玩家信息组件
    /// </summary>
    private UserHandler userHandler;
    private UserHandler UserHandler
    {
        get
        {
            if(userHandler == null)
            {
                if(!transform.GetComponent <UserHandler>())
                {
                    userHandler = gameObject.AddComponent<UserHandler>();
                }else
                {
                    userHandler = gameObject.GetComponent<UserHandler>();
                }
            }
            return userHandler;
        }
    }

    /// <summary>
    /// 匹配组件
    /// </summary>
    private MatchHandler matchHandler;
    private MatchHandler MatchHandler
    {
        get
        {
            if(matchHandler ==null)
            {
                if (!GetComponent<MatchHandler>())
                    matchHandler = gameObject.AddComponent<MatchHandler>();
                matchHandler = gameObject.GetComponent<MatchHandler>();
            }
            return matchHandler;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private FightHandler fightHandler;
    private FightHandler FightHandler
    {
        get
        {
            if(fightHandler ==null)
            {
                if (!GetComponent<FightHandler>())
                    fightHandler = gameObject.AddComponent<FightHandler>();
                fightHandler = gameObject.GetComponent<FightHandler>();
            }
            return fightHandler;
        }
    }
    #endregion

    /// <summary>
    /// 脚本第一次生效时执行
    /// </summary>
    void Awake()
    {
        GameApp.Instance.NetMessageUtilScript = this;
        //GameApp.Instance.DebugManagerScript.Log("Net message util Awake");
    }

    /// <summary>
    /// 脚本挂载的对象第一次显示在场景时执行
    /// </summary>
	void Start() {
        Debug.Log("Net message util start");
        //初始化一个客户端网路组件
        NetIO = new InitializeNetIO();
        //为网路组件添加日志输出回调函数
        NetIO.DebugCallBack = delegate (object obj) { Debug.Log(obj); };
        //添加链接服务器失败回调
        NetIO.ConnectFeiledCallBack = delegate (Exception e) { };
        //添加向服务器发送消息失败回调
        NetIO.WriteFeiledCallBack = delegate (Exception e) { };
        //添加接受消息失败回调
        NetIO.ReceiveFeiledCallBack = delegate (Exception e) { };
        //127.0.0.1    本机IP  localhost
        //192.168.X.X  局域网IP
        //119.4.X.X    远程IP   公网IP < 动态IP  静态IP  >
        //IP+端口  可以访问一个程序的网络接口
        //www.baidu.com = 61.135.169.121:80
        //为网络组件进行服务器连接初始化
        NetIO.Initialize("192.168.50.234", 6650);
        //开始连接服务器
        NetIO.ConnnectToSever();

        #region 测试代码
        ////尝试关闭网络，并返回关闭是否成功
        //bool isClose = NetIO.CloseSocket();
        ////返回是否正在尝试重新连接
        //bool isReConnect = NetIO.IsReconnect;
        ////向服务器发送数据
        //NetIO.write(1, 2, null);
        #endregion
    }

    /// <summary>
    /// 帧刷新   
    /// </summary>
    void Update() {
        //获取客户端消息本帧缓存的个数
        while (NetIO.GetSocketMessageCount() > 0)
        {
            //获取网络消息
            SocketModel Model = NetIO.GetMessage();
            if (Model == null)
                continue;
            //开启协程，以达到异步执行消息回调
            StartCoroutine("MessageReceiveCallBack", Model);
        }
    }
    /// <summary>
    /// 消息到达回调函数
    /// 进行服务器消息分发
    /// </summary>
    void MessageReceiveCallBack(SocketModel Model)
    {
        //一级协议，大的模块分发
        switch (Model .type)
        {
            //登录模块
            case TypeProtocol.LOGIN:
                LoginHandler.MessageReceive(Model);
                break;
            case TypeProtocol.USER:
                UserHandler.MessageReceive(Model);
                break;
            case TypeProtocol.MATCH:
                MatchHandler.MessageReceive(Model);
                break;
            case TypeProtocol.FIGHT:
                FightHandler.MessageReceive(Model);
                break;
        }
    }

    /// <summary>
    /// 时间刷新  20ms刷新一次   1秒固定的刷新50次  
    /// </summary>
    void FixedUpdate() { }
    /// <summary>
    /// 删除调用
    /// </summary>
    void OnDestroy() { }
}
