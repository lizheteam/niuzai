using UnityEngine;
using System.Collections;
using GameProtocol;
using UnityEngine.UI;
using System;

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

    void Awake()
    {
        GameApp.Instance.UI_FightScript = this;
        StartRoom();
    }

	void Start () {
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
        GameApp.Instance.TimeManagerScript.AddShedule(delegate () {
            UpdateTime();
        },60*1000);
    }


}
