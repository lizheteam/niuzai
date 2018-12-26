using ClientNetFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GameProtocol;
using GameProtocol.model.match;

public class MatchHandler : MonoBehaviour, IHandler
{
    public void MessageReceive(SocketModel model)
    {
        switch (model .command)
        {
            //0离开成功 -1游戏已经开始 -2不在房间内
            case MatchProtocol.LEAVEMATCH_SRES:
                {
                    switch (model .GetMessage<int>())
                    {
                        case 0:
                        case -2:
                            //离开匹配成功后，加载主界面,关闭匹配界面
                            GameApp.Instance.GameLevelManagerScript.CloseSystemUI(GameResources.SystemUIType.MATCHPANEL);
                            break;
                        case -1:
                            GameApp.Instance.CommonHintDlgScript.OpenHint("游戏已经开始");
                            break;
                    }
                }
                break;
            //0开始匹配 -1当前余额不足 -2玩家已经在房间中
            case MatchProtocol.STARTMATCH_SRES:
                {
                    //接受匹配结果
                    ResponseStartMatchInfo m = model.GetMessage<ResponseStartMatchInfo>();
                    switch (m.Status)
                    {
                        //匹配成功
                        case 0:
                            //加载匹配页面
                            GameApp.Instance.GameLevelManagerScript.LoadSyStemUI(GameResources.SystemUIType.MATCHPANEL,delegate () {
                                GameObject go;
                                if(GameApp .Instance .GameLevelManagerScript .SystemUICache .TryGetValue (GameResources.SystemUIType .MATCHPANEL ,out go))
                                {
                                    if(!go.GetComponent <UI_match>())
                                    {
                                        go.AddComponent<UI_match>();
                                    }
                                    //刷新匹配界面信息
                                    go.GetComponent<UI_match>().StartMatch(m);
                                }
                            });
                            break;
                        case -1:
                            GameApp.Instance.CommonHintDlgScript.OpenHint("当前余额不足");
                            break;
                        case -2:
                            GameApp.Instance.CommonHintDlgScript.OpenHint("已经在其他房间中,不可进行匹配");
                            break;
                    }
                }
                break;
            case MatchProtocol.MATCHCLOSE_BRQ:
                {
                    GameApp.Instance.TimeManagerScript.AddShedule(delegate ()
                    {
                        GameApp.Instance.CommonHintDlgScript.OpenHint("游戏将于五秒后自动返回大厅");
                        this.Write(TypeProtocol.USER, UserProtocol.GETINFO_CREQ, null);
                    }, 5000);
                }
                break;
            case MatchProtocol.MATCHFINISH_BRQ:
                {
                    //TODO:LODE BATTLE
                    //匹配成功加载battle场景
                    GameApp.Instance.GameLevelManagerScript.LoadScene(GameResources.SceneName.BATTLE);
                    GameApp.Instance.GameLevelManagerScript.CloseSystemUI(GameResources.SystemUIType.MATCHPANEL);
                }
                break;
            case MatchProtocol.MATCHINFO_BRQ:
                {
                    //刷新接收到的匹配信息
                    MatchInfoModel m = model.GetMessage<MatchInfoModel>();
                    if (GameApp.Instance.UI_matchScript != null)
                        GameApp.Instance.UI_matchScript.UpdateRoomRoleInfo(m);
                }
                break;
        }
    }
}

