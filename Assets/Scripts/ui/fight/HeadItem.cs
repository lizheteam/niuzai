using UnityEngine;
using System.Collections;
using GameProtocol.model.fight;
using UnityEngine.UI;

public class HeadItem : MonoBehaviour {
    Text nick;
    Text coin;

    /// <summary>
    /// 刷新玩家的信息
    /// </summary>
    public void UpdateItem(FightUserModel model)
    {
        if (!nick)
            nick = transform.FindChild("nickText").GetComponent<Text>();
        if (!coin)
            coin = transform.FindChild("coinText").GetComponent<Text>();
        nick.text = model.nickname;
        coin.text = model.coin.ToString();
    }
}
