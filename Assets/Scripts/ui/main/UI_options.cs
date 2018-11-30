using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_options : MonoBehaviour {
    //音乐开关
    Toggle MusicOpenTog;
    Toggle MusicCloseTog;
    //音效开关
    Toggle EffectOpenTog;
    Toggle EffectCloseTog;
    //定位开关
    Toggle PositionOpenTog;
    Toggle PositionCloseTog;
    //关闭显示界面
    Button CloseBtn;

    void Awake()
    {
        GameApp.Instance.UI_optionsScript = this;
        MusicOpenTog = transform.FindChild("music/openTog").GetComponent<Toggle>();
        MusicCloseTog = transform.FindChild("music/closeTog").GetComponent<Toggle>();
        EffectOpenTog = transform.FindChild("effect/openTog").GetComponent<Toggle>();
        EffectCloseTog = transform.FindChild("effect/closeTog").GetComponent<Toggle>();
        PositionOpenTog = transform.FindChild("position/openTog").GetComponent<Toggle>();
        PositionCloseTog = transform.FindChild("position/closeTog").GetComponent<Toggle>();
        CloseBtn = transform.FindChild("closeBtn").GetComponent<Button>();
        AddOnClick();
    }

    void AddOnClick()
    {
        MusicOpenTog.onValueChanged.RemoveAllListeners();
        MusicCloseTog.onValueChanged.RemoveAllListeners();
        EffectOpenTog.onValueChanged.RemoveAllListeners();
        EffectCloseTog.onValueChanged.RemoveAllListeners();
        PositionOpenTog.onValueChanged.RemoveAllListeners();
        PositionCloseTog.onValueChanged.RemoveAllListeners();
        CloseBtn.onClick.RemoveAllListeners();
        //音乐打开
        MusicOpenTog.onValueChanged.AddListener(
            delegate (bool isOn)
            {
                if (isOn)
                    GameApp.Instance.MusicManagerScript.SetPlayBgmAudio(true);
                else
                    GameApp.Instance.MusicManagerScript.SetPlayBgmAudio(false);
            }    
        );
        //音效打开
        EffectOpenTog.onValueChanged.AddListener(
            delegate (bool isOn)
            {
                if (isOn)
                    GameApp.Instance.MusicManagerScript.SetPlayEffectAudio(true);
                else
                    GameApp.Instance.MusicManagerScript.SetPlayEffectAudio(false);
            }
        );
        //执行关闭
        CloseBtn.onClick.AddListener(
            delegate () {
                GameApp.Instance.GameLevelManagerScript.CloseSystemUI(GameResources.SystemUIType.UIOPTIONSPANEL);
            }    
        );
    }

    public void UpdateData()
    {
        //获取是否播放音乐
        bool isplaymusic = GameApp.Instance.MusicManagerScript.IsPlayAudioBgm;
        //获取是否播放音效
        bool isplayeff = GameApp.Instance.MusicManagerScript.IsPlayAudioEff;
        if (isplaymusic)
            MusicOpenTog.isOn = true;
        else
            MusicCloseTog.isOn = true;

        if (isplayeff)
            EffectOpenTog.isOn = true;
        else
            EffectCloseTog.isOn = true;
    }
}
