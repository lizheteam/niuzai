using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 游戏入口
/// </summary>
public class GameLevelManager : MonoBehaviour {
    /// <summary>
    /// 系统UI缓存
    /// </summary>
    public Dictionary<GameResources.SystemUIType, GameObject> SystemUICache = new Dictionary<GameResources.SystemUIType, GameObject>();

    /// <summary>
    /// 用来存储当前显示的系统的队列
    /// 当前显示最靠前的系统一定在队列的最后一项
    /// </summary>
    List<GameResources.SystemUIType> SystemList = new List<GameResources.SystemUIType>();

    void Awake()
    {
        GameApp.Instance.GameLevelManagerScript = this;
        GameResources.Instance.Register();
    }

    void Start()
    {
        //告知unity不要将gamecanvas删除掉
        DontDestroyOnLoad(transform.parent);
        //设置分辨率
        Screen.SetResolution(1920, 1080, false);
        //游戏开始4秒后加载LOGO场景
        GameApp.Instance.TimeManagerScript.AddShedule(delegate ()
            {
                LoadScene(GameResources.SceneName.LOGO);
            }, 4000);
    }


    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="tag"></param>
    public void LoadScene(GameResources .SceneName tag)
    {
        //如果当前场景名称和要加载的场景名称一致，则直接返回
        if (GameData.Instance.GameLevelName == GameData.Instance.SceneName[tag])
            return;
        //要加载的资源
        List<LoadResourcesModel> rModel = new List<global::LoadResourcesModel>();
        //加载完成后的回调
        LoadManager.CallBack call = null;
        switch (tag)
        {
            case GameResources.SceneName.LOGIN:
                {
                    LoadResourcesModel rM = new global::LoadResourcesModel();
                    rM.type = GameResources.ResourceType.PREFAB;
                    rM.path = GameResources.UIResourcesPath + GameData.Instance.CanvasName[GameResources.CanvasTag.CANVASLOGIN];
                    rModel.Add(rM);
                    call = delegate ()
                    {
                        //场景是空白场景，UI或其他资源在场景加载后进行实例化生成，添加到本场景
                        GameObject go = GameApp.Instance.ResourcesManagerScript.LoadInstantiateGameObject(rM.path, null, Vector3.zero);
                        //UI和代码同样也是分离的,方便代码热更新
                        go.AddComponent<UI_Login>();
                    };
                }
                break;
            case GameResources.SceneName.LOGO:
                {
                    LoadResourcesModel rM = new global::LoadResourcesModel();
                    rM.type = GameResources.ResourceType.PREFAB;
                    rM.path = GameResources.UIResourcesPath + GameData.Instance.CanvasName[GameResources.CanvasTag.CANVASLOGO];
                    rModel.Add(rM);
                    call = delegate ()
                    {
                        //场景是空白场景，UI或其他资源在场景加载后进行实例化生成，添加到本场景
                        GameObject go = GameApp.Instance.ResourcesManagerScript.LoadInstantiateGameObject(rM.path, null, Vector3.zero);
                        //UI和代码同样也是分离的,方便代码热更新
                        go.AddComponent<UI_logo>();
                    };
                }
                break;
            case GameResources.SceneName.MAIN:
                {
                    LoadResourcesModel rM = new LoadResourcesModel();
                    rM.type = GameResources.ResourceType.PREFAB;
                    rM.path = GameResources.UIResourcesPath + GameData.Instance.CanvasName[GameResources.CanvasTag.CANVASMAIN];
                    rModel.Add(rM);
                    call = delegate ()
                     {
                         GameObject go = GameApp.Instance.ResourcesManagerScript.LoadInstantiateGameObject(rM.path, null, Vector3.zero);
                         go.AddComponent<UI_main>();
                     };
                }
                break;
            case GameResources.SceneName.BATTLE:
                {
                    LoadResourcesModel rM = new LoadResourcesModel();
                    rM.type = GameResources.ResourceType.PREFAB;
                    rM.path = GameResources.UIResourcesPath + GameData.Instance.CanvasName[GameResources.CanvasTag.CANVASBATTLE];
                    rModel.Add(rM);

                    LoadResourcesModel rM1 = new LoadResourcesModel();
                    rM1.type = GameResources.ResourceType.PREFAB;
                    rM1.path = GameResources.UIResourcesPath + GameData.Instance.SystemUI[GameResources.SystemUIType.CARDOTHER_TP];
                    rModel.Add(rM1);

                    LoadResourcesModel rM2 = new LoadResourcesModel();
                    rM2.type = GameResources.ResourceType.PREFAB;
                    rM2.path = GameResources.UIResourcesPath + GameData.Instance.SystemUI[GameResources.SystemUIType.UIFIGHT_TP];
                    rModel.Add(rM2);

                    call = delegate ()
                    {
                        GameObject go = GameApp.Instance.ResourcesManagerScript.LoadInstantiateGameObject(rM.path, null, Vector3.zero);
                        go.AddComponent<GameOther>();
                    };

                }
                break;
        }
        //开始加载场景
        GameApp.Instance.LoadManagerScript.StartLoadScene(tag, rModel, call);
    }

    /// <summary>
    /// 加载系统UI
    /// </summary>
    /// <param name="type"></param>
    /// <param name="call"></param>
    public void LoadSyStemUI(GameResources .SystemUIType type,LoadManager .CallBack call=null)
    {
        //设置父对象为system
        Transform tfp = transform.parent.FindChild("system");
        GameObject go;
        //尝试获取缓存中是否含有加载对象
        if (!SystemUICache.TryGetValue(type, out go))
        {
            //如果没有，则加载生成之后，将页面对象添加到缓存中
            string path = GameResources.UIResourcesPath + GameData.Instance.SystemUI[type];
            go = GameApp.Instance.ResourcesManagerScript.LoadInstantiateGameObject(path, tfp, Vector3.zero);
            SystemUICache.Add(type, go);
        }else
        {
            //否则直接将该页面调至最前并显示出来
            go.transform.SetAsLastSibling();
            go.SetActive(true);
        }
        //如果当前队列中该页面已经显示，则在队列中剔除
        for (int i=0;i<SystemList.Count; i++)
        {
            if(SystemList [i]==type)
            {
                SystemList.RemoveAt(i);
                break;
            }
        }
        //将页面显示添加在队列最后一位
        SystemList.Add(type);
        if (call != null)
            call();
    }

    /// <summary>
    /// 关闭系统UI
    /// </summary>
    /// <param name="type"></param>
    public void CloseSystemUI(GameResources .SystemUIType type=GameResources .SystemUIType .NULL,LoadManager .CallBack call=null )
    {
        //如果当前没有页面显示，则直接返回
        if (SystemList.Count <= 0)
            return;
        //如果直接关闭当前最前一层ui
        if(type== GameResources .SystemUIType.NULL)
        {
            GameObject go;
            if (!SystemUICache.TryGetValue(SystemList[SystemList.Count - 1], out go))
                return;
            go.SetActive(false);
            SystemList.RemoveAt(SystemList.Count - 1);
        }else
        {
            //关闭指定的UI界面
            GameObject go;
            if (!SystemUICache.TryGetValue(type, out go))
                return;
            go.SetActive(false);
            //将UI界面从队列中剔除
            for (int i=0;i<SystemList.Count; i++)
            {
                if(SystemList [i]==type)
                {
                    SystemList.RemoveAt(i);
                    break;  
                }
            }
        }
        if (call != null)
            call();
    }
}
