using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol;
using GameProtocol.model.match;

public class UI_match : MonoBehaviour {
    /// <summary>
    /// 时间计时
    /// </summary>
    int TimeCount = 0;
    /// <summary>
    /// 时钟id
    /// </summary>
    int TimeId = -1;
    /// <summary>
    /// 计时标签
    /// </summary>
    Text TimeDownText;
    /// <summary>
    /// 匹配玩家信息
    /// </summary>
    Text MatchCountText;
    /// <summary>
    /// 房间类型text
    /// </summary>
    Text MatchGameTypeText;
    /// <summary>
    /// 关闭按钮
    /// </summary>
    Button CloseBtn;
    /// <summary>
    /// 离开匹配按钮
    /// </summary>
    Button RetMatchBtn;

    void Awake()
    {
        GameApp.Instance.UI_matchScript = this;
        #region 获取组件
        TimeDownText = transform.FindChild("MatchTimeText").GetComponent<Text>();
        //TimeDownImg = transform.FindChild("downImg").GetComponent<Image>();
        MatchCountText = transform.FindChild("MatchInfoText").GetComponent<Text>();
        MatchGameTypeText = transform.FindChild("GameTypeText").GetComponent<Text>();
        CloseBtn = transform.FindChild("CloseButton").GetComponent<Button>();
        RetMatchBtn = transform.FindChild("RetMatchBtn").GetComponent<Button>();
        #endregion
    }

    void Start()
    {
        AddOnClick();
        StartTimeCount();
    }

    void AddOnClick()
    {
        CloseBtn.onClick.AddListener(delegate () {
            //请求离开匹配
            this.Write(TypeProtocol.MATCH, MatchProtocol.LEAVEMATCH_CREQ, null);
        });
        RetMatchBtn.onClick.AddListener(delegate () {
            //请求离开匹配
            this.Write(TypeProtocol.MATCH, MatchProtocol.LEAVEMATCH_CREQ, null);
        });
    }

    /// <summary>
    /// 刷新当前匹配信息
    /// </summary>
    public void UpdateRoomRoleInfo(MatchInfoModel model)
    {
        MatchCountText.text = "正在匹配中. . . " + model.Team.Count + "/" + model.MaxPlayer;
        MatchGameTypeText.text = GameApp.Instance.GameConstScript.GameName[(int)model.GameType];
    }

    public void StartMatch(ResponseStartMatchInfo info)
    {
        MatchCountText.text = "正在匹配中. . . " + info.PlayerCount + "/" + info.MaxPlayer;
        MatchGameTypeText.text = GameApp.Instance.GameConstScript.GameName[(int)info.Type];
        TimeCount = 0;
    }

    /// <summary>
    /// 开始计时
    /// </summary>
    void StartTimeCount()
    {
        //刷新当前计时
        UpdateTimeAnim();
        //一秒钟后再次执行计时
        TimeId= GameApp.Instance.TimeManagerScript.AddShedule(delegate () {
            TimeCount++;
            StartTimeCount();
        },1000);
    }

    /// <summary>
    /// 刷新UI
    /// </summary>
    void UpdateTimeAnim()
    {
        //将当前计时显示在UI上
        TimeDownText.text = TimeCount.ToString();
        //控制图片显示进度为30秒循环一次
        //TimeDownImg.fillAmount = 1-(TimeCount %30)/30f;
    }
}
