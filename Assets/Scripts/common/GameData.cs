using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 程序运行时的本地数据缓存单例
/// 运行产生的数据都要存储在该脚本
/// </summary>
public class GameData  {
    #region 单例
    private static GameData instance;
    public static GameData Instance
    {
        get
        {
            if(instance ==null)
            {
                instance = new GameData();
            }
            return instance;
        }
    }
    #endregion

    /// <summary>
    /// 是否打印日志到本地
    /// </summary>
    public bool IsDebugWrite = true;

    /// <summary>
    /// 预制体资源加载缓存
    /// </summary>
    public Dictionary<string, GameObject> LoadGameObjectCache = new Dictionary<string, GameObject>();

    /// <summary>
    /// 用来存储场景名称(场景类型，场景名字)
    /// </summary>
    public Dictionary<GameResources.SceneName, string> SceneName = new Dictionary<GameResources.SceneName, string>();

    /// <summary>
    /// 游戏当前场景
    /// </summary>
    public string GameLevelName = "Start";

    /// <summary>
    /// 用来存储UIcanvas
    /// </summary>
    public Dictionary<GameResources.CanvasTag, string> CanvasName = new Dictionary<GameResources.CanvasTag, string>();

    /// <summary>
    /// 用来存储系统功能UI
    /// </summary>
    public Dictionary<GameResources.SystemUIType, string> SystemUI = new Dictionary<GameResources.SystemUIType, string>();

    /// <summary>
    /// 用来存储组件名称
    /// </summary>
    public Dictionary<GameResources.ItemTag, string> ItemName = new Dictionary<GameResources.ItemTag, string>();

    /// <summary>
    /// 音乐资源
    /// </summary>
    public Dictionary<GameResources.MusicTag, string> MusicTag = new Dictionary<GameResources.MusicTag, string>(); 
}
