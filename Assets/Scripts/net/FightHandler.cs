using UnityEngine;
using System.Collections;
using ClientNetFrame;
using System;
using GameProtocol;
using GameProtocol.model.fight;
using System.Collections.Generic;

public class FightHandler : MonoBehaviour, IHandler
{
    /// <summary>
    /// 消息缓存列表
    /// </summary>
    List<SocketModel> SocketModelList = new List<SocketModel>();

    void Update()
    {
        //在房间加载完毕之后开始执行消息刷新
        if(GameApp .Instance .UI_FightScript) {
            while (SocketModelList .Count > 0)
            {
                MessageReceiveCallBack(SocketModelList [0]);
                SocketModelList.RemoveAt(0);
            }
        }
    }

    public void MessageReceiveCallBack(SocketModel model)
    {
        switch (model.command)
        {
            //玩家信息
            case FightProtocol.PLAYERINFO_BRQ:
                {
                    FightUserModel user = model.GetMessage<FightUserModel>();
                    GameApp.Instance.UI_FightScript.UpdateTeam(user);
                }
                break;
            //玩家请求准备结果
            case FightProtocol.ENTERFIGHT_SRES:
                {
                    int res = model.GetMessage<int>();
                    switch (res)
                    {
                        case -1:
                        case 0:
                            GameApp.Instance.CommonHintDlgScript.OpenHint("已准备");
                            break;
                        case -2:
                            GameApp.Instance.CommonHintDlgScript.OpenHint("房间错误");
                            break;
                    }
                }
                break;
            //确认准备的玩家列表
            case FightProtocol.ENTERFIGHT_BRQ:
                {
                    //已经准备的玩家id的数组
                    List<int> arr = model.GetMessage<List<int>>();
                }
                break;
            //玩家摸到的牌
            case FightProtocol.TPDRAWCARD_BRQ:
                {
                    List<PokerModel> poker = new List<PokerModel>();
                }
                break;
            //摸牌的玩家
            case FightProtocol.TPDRAWCARDUSER_BRQ:
                {
                    int id = model.GetMessage<int>();
                    GameApp.Instance.CardOtherScript.GetCardOther<TPCardOther>().DrawCard(id);
                }
                break;
        }
    }


    public void MessageReceive(SocketModel model)
    {
        //将服务回执消息添加到列表中
        SocketModelList.Add(model);       
    }
}
