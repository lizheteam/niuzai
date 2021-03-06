﻿using UnityEngine;
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
            //获取底注
            case FightProtocol.TPBETBASECOIN_BRQ:
                {
                    int coin = model.GetMessage<int>();
                    GameApp.Instance.CardOtherScript.GetCardOther<TPCardOther>().BetBaseCoin(coin);
                }
                break;
            /// <summary>
            /// 返回下注结果
            /// -1 请求错误，没有此玩家
            /// -2 请求错误，当前不是此玩家
            /// -3 请求错误，游戏尚未开始
            /// -4 低于当前可下最小金额
            /// -5 大于当前可下最大金额
            /// </summary>          
            case FightProtocol.TPBETCOIN_SRES:
                {
                    int res = model.GetMessage<int>();
                    switch (res)
                    {
                        case -4:
                            {
                                GameApp.Instance.CommonHintDlgScript.OpenHint("小于当前最小金额");
                            }
                            break;
                        case -5:
                            {
                                GameApp.Instance.CommonHintDlgScript.OpenHint("大于当前最大金额");
                            }
                            break;
                    }
                }
                break;
            //玩家下注广播
            case FightProtocol.TPBETCOIN_BRQ:
                {
                    TPBetModel m = model.GetMessage<TPBetModel>();
                    GameApp.Instance.CardOtherScript.GetCardOther<TPCardOther>().BetCoin(m.coin);
                    //播放音效，默认加注，否则跟注
                    string path = GameResources.TPAudioResourcesPath + GameData.Instance.MusicTag[GameResources.MusicTag.TPADDBETCOIN];
                    if (!m.isAdd )
                        path = GameResources.TPAudioResourcesPath + GameData.Instance.MusicTag[GameResources.MusicTag.TPWITHBETCOIN];
                    GameApp.Instance.MusicManagerScript.PlayAudioEffect(path);
                }
                break;
            case FightProtocol.TPCHECKCARD_SRES:
                {
                    GameApp.Instance.CardOtherScript.GetCardOther<TPCardOther>().CheckCard(model.GetMessage<List <PokerModel>>());
                }
                break;
            case FightProtocol.TPCHECKCARD_BRQ:
                {
                    string path = GameResources.AudioResourcesPath + GameData.Instance.MusicTag[GameResources.MusicTag.TPCHECKCARD];
                    GameApp.Instance.MusicManagerScript.PlayAudioEffect(path);
                    GameApp.Instance.CardOtherScript.GetCardOther<TPCardOther>().RechangeStatus(0, model.GetMessage<int>());
                }
                break;
            case FightProtocol.TPDISCARD_BRQ:
                {
                    GameApp.Instance.CardOtherScript.GetCardOther<TPCardOther>().RechangeStatus(1, model.GetMessage<int>());
                }
                break;
            case FightProtocol.TPCOMCARD_BRQ:
                {
                    TPCompareModel tpcm = model.GetMessage<TPCompareModel>();
                    string path = GameResources.AudioResourcesPath + GameData.Instance.MusicTag[GameResources.MusicTag.TPCOMCARD];
                    GameApp.Instance.MusicManagerScript.PlayAudioEffect(path);
                    GameApp.Instance.CardOtherScript.GetCardOther<TPCardOther>().Compare(tpcm);
                }
                break;
            //游戏结算
            case FightProtocol.GAMESETTLMENT_BRQ:
                {
                    GameApp.Instance.UI_FightScript.GetUIFight<TPUI_Fight>().GameOver(model.GetMessage<List <TPSettlementModel >>());
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
