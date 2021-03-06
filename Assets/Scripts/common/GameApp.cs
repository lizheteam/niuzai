﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 存储客户端单一实例的单例,达到访问其他非静态脚本的目的
/// </summary>
public class GameApp  {
    #region 单例
    private static GameApp instance;
    public static GameApp Instance
    {
        get
        {
            if(instance ==null)
            {
                instance = new GameApp();
            }
            return instance;
        }
    }
    #endregion

    /// <summary>
    /// 日志管理器
    /// </summary>
    public DebugManager DebugManagerScript;

    /// <summary>
    /// 计时器管理类
    /// </summary>
    public TimeManager TimeManagerScript;

    /// <summary>
    /// 资源管理类
    /// </summary>
    public ResourcesManager ResourcesManagerScript;

    /// <summary>
    /// 音乐管理类
    /// </summary>
    public MusicManager MusicManagerScript;

    /// <summary>
    /// 加载（场景，资源（文本，图片，音乐，预制体））管理器
    /// </summary>
    public LoadManager LoadManagerScript;

    /// <summary>
    /// 游戏场景管理器
    /// </summary>
    public GameLevelManager GameLevelManagerScript;

    /// <summary>
    /// 登录脚本
    /// </summary>
    public UI_Login UI_LoginScript;

    /// <summary>
    /// 网络消息分发中心
    /// </summary>
    public NetMessageUtil NetMessageUtilScript;

    /// <summary>
    ///常用通用提示框
    /// </summary>
    public CommonHintDlg CommonHintDlgScript;

    /// <summary>
    /// 主场景UI
    /// </summary>
    public UI_main UI_mainScript;

    /// <summary>
    /// 设置界面UI
    /// </summary>
    public UI_options UI_optionsScript;

    /// <summary>
    /// 匹配
    /// </summary>
    public UI_match UI_matchScript;

    /// <summary>
    /// 游戏常量
    /// 是常量，不是单例
    /// </summary>
    public GameConst GameConstScript=new GameConst ();

    /// <summary>
    /// 游戏总管理脚本
    /// </summary>
    public GameOther GameOtherScript;

    /// <summary>
    /// 游戏UI管理脚本
    /// </summary>
    public UI_Fight UI_FightScript;

    /// <summary>
    /// 玩家战斗时头像组件
    /// </summary>
    public UI_Head UI_HeadScript;

    /// <summary>
    /// 游戏手牌管理
    /// </summary>
    public CardOther CardOtherScript;

    /// <summary>
    /// 获取玩家人数
    /// </summary>
    /// <returns></returns>
    public int GetPlayerCount()
    {
        switch (GameSession .Instance .RoomeType)
        {
            case GameProtocol.SConst.GameType.WINTHREEPOKER:
                return 2;
            default:
                return 4;
        }
    }

    /// <summary>
    /// 判断当前是否为安卓平台
    /// </summary>
    public bool IsAndroid
    {
        get
        {
            //unity的平台宏定义
#if UNITY_ANDROID
            return true;
#else
            return false;
#endif
        }
    }

    /// <summary>
    /// 判断当前是否为苹果平台
    /// </summary>
    public bool IsIPhone
    {
        get
        {
            //unity的平台宏定义
#if UNITY_IPHONE
            return true;
#else
            return false;
#endif
        }
    }

}
