using UnityEngine;
using System.Collections;

/// <summary>
/// 专门用来管理资源常量，变量,路径
/// </summary>
public class GameResources  {
    #region 单例
    private static GameResources instance;
    public static GameResources Instance
    {
        get
        {
            if(instance ==null)
            {
                instance = new GameResources();
            }
            return instance;
        }
    }
    #endregion

    /// <summary>
    /// 注册函数
    /// </summary>
    public void Register()
    {
        RegisterSceneName();
        RegisterCanvasTag();
        RegisterSystemUI();
        RegisterItem();
        RegisterMusicTag();
    }

    /// <summary>
    /// 资源类型
    /// </summary>
    public enum ResourceType
    {
        //文本
        TEXTASSET,
        //音效
        AUDIO,
        //图片
        SPRITE,
        //预制
        PREFAB
    }

    #region 场景
    /// <summary>
    /// 场景枚举定义
    /// </summary>
    public enum SceneName
    {
        START,//开始场景
        LOGO,//加载场景
        LOGIN,//登录场景
        MAIN,//主场景
        BATTLE//对战场景
    }

    /// <summary>
    /// 注册场景名称
    /// </summary>
    void RegisterSceneName()
    {
        GameData.Instance.SceneName.Add(SceneName.START, "Start");
        GameData.Instance.SceneName.Add(SceneName.LOGO, "Logo");
        GameData.Instance.SceneName.Add(SceneName.MAIN, "Main");
        GameData.Instance.SceneName.Add(SceneName.LOGIN, "Login");
        GameData.Instance.SceneName.Add(SceneName.BATTLE, "Battle");
    }
    #endregion

    #region uicanvas
    /// <summary>
    /// 场景枚举值
    /// </summary>
    public enum CanvasTag
    {
        CANVASLOGO,//初始化场景
        CANVASLOGIN,//登录场景
        CANVASMAIN,//主界面UI
        CANVASBATTLE,//游戏UI
    }

    /// <summary>
    /// 注册UIcanvas资源名称
    /// </summary>
    void RegisterCanvasTag()
    {
        GameData.Instance.CanvasName.Add(CanvasTag.CANVASLOGO, "logoCanvas");
        GameData.Instance.CanvasName.Add(CanvasTag.CANVASLOGIN, "loginCanvas");
        GameData.Instance.CanvasName.Add(CanvasTag.CANVASMAIN, "MainCanvas");
        GameData.Instance.CanvasName.Add(CanvasTag.CANVASBATTLE, "BattleCanvas");
    }
    #endregion

    #region systemui
    public enum SystemUIType
    {
        NULL,
        UIHINTLOGPANEL,
        UIOPTIONSPANEL,//设置界面
        MATCHPANEL,//匹配页面
        CARDOTHER_TP,//三张游戏桌子页面
        UIFIGHT_TP,//三张游戏UI页面
    }
    /// <summary>
    /// 系统页面
    /// </summary>
    void RegisterSystemUI()
    {
        GameData.Instance.SystemUI.Add(SystemUIType.UIHINTLOGPANEL, "system/hintLogPanel");
        GameData.Instance.SystemUI.Add(SystemUIType.UIOPTIONSPANEL, "system/optionsPanel");
        GameData.Instance.SystemUI.Add(SystemUIType.MATCHPANEL, "system/MatchPanel");
        GameData.Instance.SystemUI.Add(SystemUIType.CARDOTHER_TP, "system/CardPanel_TP");
        GameData.Instance.SystemUI.Add(SystemUIType.UIFIGHT_TP, "system/UI_FightTP");
    }
    #endregion

    #region Item
    /// <summary>
    /// 组件枚举值
    /// </summary>
    public enum ItemTag
    {
        HINTBOXITEM=0,//通用提示框
        TPHEAD=1,//赢三张头像
    }

    /// <summary>
    /// 注册组件
    /// </summary>
    void RegisterItem()
    {
        GameData.Instance.ItemName.Add(ItemTag.HINTBOXITEM, "hintitem");
        GameData.Instance.ItemName.Add(ItemTag.TPHEAD, "headImage0");
    }
    #endregion

    #region music
    public enum MusicTag
    {
        NULL=0,
        MAINBACKGROUNDMUSIC=1,//主界面背景音乐
    }
    void RegisterMusicTag()
    {
        GameData.Instance.MusicTag.Add(MusicTag.MAINBACKGROUNDMUSIC, "backmusic");
    }
    #endregion

    /// <summary>
    /// UI资源存储路径
    /// </summary>
    public const string UIResourcesPath = "prefab/ui/";

    /// <summary>
    /// 资源存储路径
    /// </summary>
    public const string ItemResourcesPath = "prefab/item/";

    /// <summary>
    /// 音乐资源存储路径
    /// </summary>
    public const string AudioResourcesPath = "Audio/";

    /// <summary>
    /// 扑克资源存储路径
    /// </summary>
    public const string PokerResourcesPath = "Texture/poker/";

    /// <summary>
    /// 扑克背景图资源存储路径
    /// </summary>
    public const string PokerBgResourcesPath = "Texture/poker/bg_";

}
