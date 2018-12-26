using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using GameProtocol.model.fight;
using GameProtocol;

public class TPSettlement : MonoBehaviour {
    void Awake()
    {
        OnAddClick();
    }

    public void OnAddClick()
    {
        Button closeBtn = transform.FindChild("closeButton").GetComponent<Button>();
        closeBtn.onClick.AddListener(delegate () {
            this.Write(TypeProtocol.USER, UserProtocol.GETINFO_CREQ, null);
        });
    }

    public void ShowGameOver(List<TPSettlementModel> list)
    {
        //获取待添加的父节点
        Transform tf = transform.Find("Scroll View/Viewport/Content");
        //路径
        string path = GameResources.ItemResourcesPath + GameData.Instance.ItemName[GameResources.ItemTag.TPSETTLEMENTITEM];
        for (int i = 0; i < list .Count; i++)
        {
            GameObject go = GameApp.Instance.ResourcesManagerScript.LoadInstantiateGameObject(path, tf, Vector3.zero);
            Text nick = go.transform.FindChild("nickNameText").GetComponent<Text>();
            Text score = go.transform.FindChild("scoreText").GetComponent<Text>();
            nick.text = list[i].nickName;
            score.text = list[i].score.ToString();
            //刷新手牌的扑克
            for (int j = 0; j < list [i].poker .Count; j++)
            {
                //获取到扑克组件
                Image img = go.transform.Find("poker"+(j+1)).GetComponent<Image>();
                string pokerPath = GameResources.PokerResourcesPath + "_" + list[i].poker[j].Value + "_" + list[i].poker[j].Color;
                img.sprite = GameApp.Instance.ResourcesManagerScript.LoadSprite(pokerPath);
            }
        }

    }
}
