using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameProtocol.model.fight;
using UnityEngine.UI;

public class TPCardOther : CardOther {
    /// <summary>
    /// 用户手牌列表
    /// </summary>
    Dictionary<int, GameObject> UserCardList = new Dictionary<int, GameObject>();

    void Awake()
    {
        GameApp.Instance.CardOtherScript = this;
    }

    void Start()
    {
        transform.Find("mypoker").gameObject.SetActive(false);
        transform.Find("playerPoker/player1").gameObject.SetActive(false);
        transform.Find("playerPoker/player2").gameObject.SetActive(false);
        transform.Find("playerPoker/player3").gameObject.SetActive(false);
        transform.Find("playerPoker/player4").gameObject.SetActive(false);
    }

    protected override void UpdateItem(FightUserModel model)
    {
        int userid = GameSession.Instance.UserInfo.id;
        int userdir = TeamInfo[userid].direction;
        //待刷新的用户是否为玩家自己
        if(model .id==userid)
        {
            //添加玩家id和玩家手牌的绑定
            if(!UserCardList .ContainsKey (userid))
            {
                UserCardList.Add(userid, transform.Find("mypoker").gameObject);
            }else
            {
                UserCardList [userid ]=transform.Find("mypoker").gameObject;
            }
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
            if(!UserCardList.ContainsKey (userid ))
            {
                UserCardList.Add(userid, transform.Find("playerPoker/poker" + dir).gameObject);
            }else
            {
                UserCardList[userid] = transform.Find("playerPoker/poker" + dir).gameObject;
            }
            UserCardList[userid].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 摸牌
    /// </summary>
    /// <param name="userid"></param>
    public void DrawCard(int userid)
    {
        if (!UserCardList.ContainsKey(userid)) return;
        //炸金花为三张牌
        for (int i = 0; i < 4; i++)
        {
            //获取手牌组件
            Image spr = UserCardList[userid].transform.Find("poker" + i).GetComponent<Image>();
            spr.sprite = GameApp.Instance.ResourcesManagerScript.LoadSprite(GameResources .PokerBgResourcesPath);
        }
        if(userid ==GameSession .Instance .UserInfo.id)
        {
            UserCardList[userid].transform.Find("Image").gameObject.SetActive(true);
            UserCardList[userid].transform.Find("Image/Text").GetComponent<Text>().text = "点击看牌";
        }
        else
        {
            UserCardList[userid].transform.Find("Image").gameObject.SetActive(false);
        }
        UserCardList[userid].gameObject.SetActive(true);
    }
}
