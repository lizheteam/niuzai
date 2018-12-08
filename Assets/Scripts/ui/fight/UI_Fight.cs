using UnityEngine;
using System.Collections;
using GameProtocol;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using GameProtocol.model.fight;

public class UI_Fight : MonoBehaviour {
    /// <summary>
    /// 设置按钮
    /// </summary>
    Button OptionsBtn;

    /// <summary>
    /// 时间标签
    /// </summary>
    Text TimeText;

    /// <summary>
    /// 电量
    /// </summary>
    Image ElectricSlider;

    /// <summary>
    /// 时间刷新计时器id
    /// </summary>
    int UpdateTimeId = -1;

    /// <summary>
    /// 玩家id和对战信息的队伍信息集
    /// </summary>
    Dictionary<int, FightUserModel> TeamInfo = new Dictionary<int, FightUserModel>();

    void Awake()
    {
        GameApp.Instance.UI_FightScript = this;
        StartRoom();
    }

	void Start () {
    }

    void OnDestroy()
    {
        if (UpdateTimeId != -1)
            GameApp.Instance.TimeManagerScript.Remove(UpdateTimeId);
    }

    /// <summary>
    /// 房间初始化
    /// </summary>
    protected void StartRoom()
    {
        //通过组件获取父组件和父组件的子组件来获取设置按钮
        OptionsBtn = transform.parent.Find("roomInfo/optionsImage").GetComponent<Button>();
        TimeText = transform.parent.Find("roomInfo/timeText").GetComponent<Text>();
        ElectricSlider = transform.parent.Find("roomInfo/ElectricSliderImage/childImage").GetComponent<Image>();
        //为设置按钮添加事件
        OptionsBtn.onClick.AddListener(delegate () {
            GameApp.Instance.GameLevelManagerScript.LoadSyStemUI(GameResources.SystemUIType.UIOPTIONSPANEL);
        });
        //执行完毕
        this.Write(TypeProtocol.FIGHT, FightProtocol.ENTERFIGHT_CREQ, null);
        UpdateTime();
    }

    void UpdateTime()
    {
        TimeText.text = DateTime.Now.ToString("hh:mm");
        UpdateTimeId = GameApp.Instance.TimeManagerScript.AddShedule(delegate () {
            UpdateTime();
        },60*1000);
    }

    /// <summary>
    /// id缓存
    /// </summary>
    List<int> cacheId = new List<int>();

    /// <summary>
    /// 刷新队伍成员信息
    /// </summary>
    /// <param name="model"></param>
    public void UpdateTeam(FightUserModel model)
    {
        switch (GameSession .Instance.RoomeType )
        {
            case SConst.GameType.WINTHREEPOKER:
                GameApp.Instance.CardOtherScript.GetCardOther<TPCardOther>().UpdateData(model);
                break;
        }
        if(TeamInfo .ContainsKey (model .id))
        {
            //TODO:更新玩家信息
            return;
        }
        //添加队伍成员
        TeamInfo.Add(model.id, model);
        //将队伍成员的id进行缓存，直到收到玩家自己的信息
        if(!TeamInfo.ContainsKey(GameSession .Instance .UserInfo.id))
        {
            cacheId.Add(model.id);
            return;
        }
        int userdir = TeamInfo[GameSession.Instance.UserInfo.id].direction;
        for (int i = 0; i < cacheId .Count; i++)
        {
            GameApp.Instance.UI_HeadScript.UpdateItem(TeamInfo[cacheId[i]], userdir);
        }
        cacheId.Clear();
        GameApp.Instance.UI_HeadScript.UpdateItem(model, userdir);
        if(TeamInfo .Count >= GameApp .Instance .GetPlayerCount ())
        {
            //TODO:游戏即将开始

        }
    }
}
