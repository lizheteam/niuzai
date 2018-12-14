using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameProtocol.model.fight;
using UnityEngine.UI;
using System;
using GameProtocol;

public class TPCardOther : CardOther {
    /// <summary>
    /// 用户手牌列表
    /// </summary>
    Dictionary<int, GameObject> UserCardList = new Dictionary<int, GameObject>();

    /// <summary>
    /// 下注筹码
    /// </summary>
    List<GameObject> BetCoinList = new List<GameObject>();

    /// <summary>
    /// 下注区域
    /// </summary>
    Transform BetAreaGo;

    /// <summary>
    /// 下注区域四周
    /// </summary>
    Rect BetCoinAreaBox = new Rect(8,131,1066,394);

    /// <summary>
    /// 随机数种子
    /// </summary>
    System.Random ran = new System.Random((int)DateTime.Now.Ticks);

    void Awake()
    {
        GameApp.Instance.CardOtherScript = this;
        BetAreaGo = transform.FindChild("betarea");
    }

    void Start()
    {
        transform.Find("mypoker").gameObject.SetActive(false);
        transform.Find("playerPoker/player1").gameObject.SetActive(false);
        transform.Find("playerPoker/player2").gameObject.SetActive(false);
        transform.Find("playerPoker/player3").gameObject.SetActive(false);
        transform.Find("playerPoker/player4").gameObject.SetActive(false);
    }

    /// <summary>
    /// 刷新赢三张用户手牌的绑定
    /// </summary>
    /// <param name="model"></param>
    protected override void UpdateItem(FightUserModel model)
    {
        int userid = GameSession.Instance.UserInfo.id;
        int userdir = TeamInfo[userid].direction;
        //待刷新的用户是否为玩家自己
        if(model .id==userid)
        {
            //添加玩家id和玩家手牌的绑定
            if(!UserCardList .ContainsKey (model.id))
            {
                UserCardList.Add(model.id, transform.Find("mypoker").gameObject);
            }else
            {
                UserCardList [model.id] =transform.Find("mypoker").gameObject;
            }
            RechangeStatus(2, model.id);
        }else
        {
            //玩家方位在组件中的尾缀
            int dir = 0;
            //如果用户方位大于玩家自己的方位
            if(model .direction >userdir)
            {
                dir = model.direction - userdir;
            }else
            {
                dir = GameApp.Instance.UI_HeadScript.PosList.Count - userdir + model.direction;
            }
            if(!UserCardList.ContainsKey (model .id))
            {
                UserCardList.Add(model.id, transform.Find("playerPoker/player" + dir).gameObject);
            }else
            {
                UserCardList[model.id] = transform.Find("playerPoker/player" + dir).gameObject;
            }
        }
        UserCardList[model.id].gameObject.SetActive(false);
    }

    /// <summary>
    /// 摸牌
    /// </summary>
    /// <param name="userid"></param>
    public void DrawCard(int userid)
    {
        if (!UserCardList.ContainsKey(userid)) return;
        //炸金花为三张牌
        for (int i = 1; i < 4; i++)
        {
            //获取手牌组件
            Image spr = UserCardList[userid].transform.Find("poker" + i).GetComponent<Image>();
            spr.sprite = GameApp.Instance.ResourcesManagerScript.LoadSprite(GameResources .PokerBgResourcesPath);
        }
        if(userid ==GameSession .Instance .UserInfo.id)
        {
            //刷新状态
            UserCardList[userid].transform.Find("Image").gameObject.SetActive(true);
            UserCardList[userid].transform.Find("Image/Text").GetComponent<Text>().text = "点击看牌";
        }
        else
        {
            UserCardList[userid].transform.Find("Image").gameObject.SetActive(false);
        }
        //将玩家手牌显示出来
        UserCardList[userid].gameObject.SetActive(true);
    }

    /// <summary>
    /// 下底注
    /// </summary>
    /// <param name="coin"></param>
    public void BetBaseCoin(int coin)
    {
        for (int i = 0; i < coin; i++)
        {
            BetCoin(1);
        }
    }

    /// <summary>
    /// 下注
    /// </summary>
    /// <param name="coin"></param>
    public void BetCoin(int coin)
    {
        string path = GameResources.ItemResourcesPath + GameData.Instance.ItemName[GameResources.ItemTag.TPBETCOIN] + coin;
        //随机一个下注区域内的x/y坐标
        int x = ran.Next((int)BetCoinAreaBox.width / 2, (int)BetCoinAreaBox.width);
        int y = ran.Next((int)BetCoinAreaBox.height / 2, (int)BetCoinAreaBox.height);
        Vector3 pos = new Vector3(x- BetCoinAreaBox.width / 2, y- BetCoinAreaBox.height / 2);
        GameObject go = GameApp.Instance.ResourcesManagerScript.LoadInstantiateGameObject(path, BetAreaGo, pos);
        BetCoinList.Add(go);
        string moveEffectPath = GameResources.TPAudioResourcesPath + GameData.Instance.MusicTag[GameResources.MusicTag.TPMOVEBETCOIN];
        GameApp.Instance.MusicManagerScript.PlayAudioEffect(moveEffectPath);
     }

    /// <summary>
    /// 看牌
    /// </summary>
    /// <param name="poker"></param>
    public void CheckCard(List <PokerModel > poker)
    {
        if (!UserCardList.ContainsKey(GameSession.Instance.UserInfo.id)) return;
        //mypoker 
        Transform tf= UserCardList[GameSession.Instance.UserInfo.id].transform;
        for (int i = 0; i < poker .Count; i++)
        {
            if (i > 2) break;
            Image img = tf.Find("poker" + (i + 1)).GetComponent<Image>();
            string path = GameResources.PokerResourcesPath + "_" + poker[i].Value + "_" + poker[i].Color;
            img.sprite = GameApp.Instance.ResourcesManagerScript.LoadSprite(path);
        }
    }

    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="status"></param>
    /// <param name="uid"></param>
    public void RechangeStatus(int status,int uid)
    {
        if (!UserCardList.ContainsKey(uid)) return;
        //mypoker
        Transform tf= UserCardList[uid].transform;
        tf.FindChild ("Image").gameObject.SetActive(true);
        switch (status)
        {
            //看牌
            case 0:
                {
                    tf.FindChild("Image/Text").GetComponent<Text>().text = "已看牌";
                    string path = GameResources.TPAudioResourcesPath + GameData.Instance.MusicTag[GameResources.MusicTag.TPCHECKCARD];
                    GameApp.Instance.MusicManagerScript.PlayAudioEffect(path);
                }
                break;
            //弃牌
            case 1:
                {
                    tf.FindChild("Image/Text").GetComponent<Text>().text = "已弃牌";
                    string path = GameResources.TPAudioResourcesPath + GameData.Instance.MusicTag[GameResources.MusicTag.TPDISCARD ];
                    GameApp.Instance.MusicManagerScript.PlayAudioEffect(path);
                }
                break;
            //点击看牌
            case 2:
                {
                    //为看牌按钮添加一个点击事件
                    tf.FindChild("Image").GetComponent<Button>().onClick.RemoveAllListeners();
                    tf.FindChild("Image").GetComponent<Button>().onClick.AddListener(delegate () {
                        this.Write(TypeProtocol.FIGHT, FightProtocol.TPCHECKCARD_CREQ, null);
                    });
                    tf.FindChild("Image/Text").GetComponent<Text>().text = "点击看牌";
                    //string path = GameResources.TPAudioResourcesPath + GameData.Instance.MusicTag[GameResources.MusicTag.TPCHECKCARD];
                    //GameApp.Instance.MusicManagerScript.PlayAudioEffect(path);
                }
                break;
        }
    }

}
