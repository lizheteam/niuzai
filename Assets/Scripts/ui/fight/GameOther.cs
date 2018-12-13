using UnityEngine;
using System.Collections;
using GameProtocol;

public class GameOther : MonoBehaviour {
    GameObject OtherPanel;
    GameObject UIPanel;

    void Awake()
    {
        GameApp.Instance.GameOtherScript = this;
    }

    void Start()
    {
        //获取父物体
        Transform cardParent = transform.FindChild("CardPanel");
        Transform uiParent = transform.FindChild("UIPanel/gameInfo");
        //根据游戏类型选择待生成的桌面和UI
        switch (GameSession.Instance.RoomeType)
        {
            case GameProtocol.SConst.GameType.WINTHREEPOKER:
                //生成桌面
                string path = GameResources.UIResourcesPath + GameData.Instance.SystemUI[GameResources.SystemUIType.CARDOTHER_TP];
                OtherPanel = GameApp.Instance.ResourcesManagerScript.LoadInstantiateGameObject(path, cardParent, Vector3.zero);
                //生成UI
                path = GameResources.UIResourcesPath + GameData.Instance.SystemUI[GameResources.SystemUIType.UIFIGHT_TP];
                UIPanel = GameApp.Instance.ResourcesManagerScript.LoadInstantiateGameObject(path, uiParent, Vector3.zero);
                //添加脚本
                OtherPanel.gameObject.AddComponent<TPCardOther>();
                uiParent.gameObject.AddComponent<TPUI_Fight>().SetGameInfoPanel (UIPanel);
                UIPanel.AddComponent<UI_Head>();
                break;
            case GameProtocol.SConst.GameType.XZDD:

                break;
        }
    }

    /// <summary>
    /// 游戏结束，将UI删除
    /// </summary>
    public void GameOver()
    {
        Destroy(OtherPanel);
        Destroy(UIPanel);
    }
}
