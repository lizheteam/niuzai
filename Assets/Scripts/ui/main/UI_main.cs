using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol;

public class UI_main : MonoBehaviour {
    Image HeadImg;//头像图片
    Text NickNameText;//昵称标签
    Text IdText;//id标签
    Text CoinText;//金币标签
    Text CashText;//钻石标签
    Button UI_OptionsBtn;//设置按钮
    Button UI_TPokerBtn;//炸金花按钮

    void Awake()
    {
        GameApp.Instance.UI_mainScript = this;
        //预先为组件赋值
        HeadImg = transform.FindChild("head").GetComponent<Image>();
        NickNameText = transform.FindChild("nikeName").GetComponent<Text>();
        IdText = transform.FindChild("id").GetComponent<Text>();
        CoinText = transform.FindChild("coin/cointext").GetComponent<Text>();
        CashText = transform.FindChild("cash/cashtext").GetComponent<Text>();
        UI_OptionsBtn = transform.FindChild("system/setting").GetComponent<Button>();
        UI_TPokerBtn = transform.FindChild("threePoker").GetComponent<Button>();
        GameSession.Instance.UserInfoChangeHandler += UpdateData;
    }

    void Start()
    {
        UpdateData();
        UI_OptionsBtn.onClick.AddListener(
            delegate ()
            {
                GameApp.Instance.GameLevelManagerScript.LoadSyStemUI(GameResources.SystemUIType.UIOPTIONSPANEL,delegate () {
                    GameObject go;
                    if(GameApp .Instance .GameLevelManagerScript .SystemUICache.TryGetValue(GameResources .SystemUIType .UIOPTIONSPANEL,out go))
                    {
                        if (!go.GetComponent <UI_options>())
                        {
                            go.AddComponent<UI_options>();
                        }
                        GameApp.Instance.UI_optionsScript.UpdateData();
                    }
                });
            }
        );

        UI_TPokerBtn.onClick.AddListener(delegate ()
        {
            this.Write(TypeProtocol.MATCH, MatchProtocol.STARTMATCH_CREQ, SConst .GameType .WINTHREEPOKER);
        });

        string path = GameResources.AudioResourcesPath + GameData.Instance.MusicTag[GameResources.MusicTag.MAINBACKGROUNDMUSIC];
        GameApp.Instance.MusicManagerScript.PlayBgmAudio(path);
    }

    void OnDestroy()
    {
        GameSession.Instance.UserInfoChangeHandler -= UpdateData;
    }

    void UpdateData()
    {
        if (HeadImg == null||GameSession .Instance .UserInfo ==null)
            return;
        NickNameText.text = GameSession.Instance.UserInfo.nickname;
        IdText.text = "ID:"+(100000+GameSession.Instance.UserInfo.id);
        CoinText.text = GameSession.Instance.UserInfo.coin.ToString();
        CashText.text = GameSession.Instance.UserInfo.cash.ToString();
    }
}
