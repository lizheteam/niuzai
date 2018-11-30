using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol;
using GameProtocol.model.login;

public class UI_Login : MonoBehaviour {
    Button QuickBtn;//快速登录
    Button WeChatBtn;//微信账号登录
    InputField UsernameIF;//账号输入框

    void Awake()
    {
        GameApp.Instance.UI_LoginScript = this;
        QuickBtn = transform.FindChild("Panel/QuickButton").GetComponent<Button>();
        WeChatBtn = transform.FindChild("Panel/WeChatButton").GetComponent<Button>();
        UsernameIF = transform.FindChild("Panel/username").GetComponent<InputField >();
        OnClick();
    }

    void OnClick()
    {
        //为快速登录添加回调事件
        QuickBtn.onClick.AddListener(delegate () {
            //向服务器发送请求快速注册
            ExtendHandler.SendMessage(TypeProtocol.LOGIN, LoginProtocol.QUICKREG_CREQ, null);
            //this.Write(TypeProtocol.LOGIN, LoginProtocol.QUICKREG_CREQ, null);
            //GameApp.Instance.NetMessageUtilScript.NetIO.write(TypeProtocol.LOGIN, LoginProtocol.QUICKREG_CREQ, null);
            Debug.Log("请求快速注册登录");
            GameApp.Instance.CommonHintDlgScript.OpenHint("请求快速注册登录");
            GameApp.Instance.CommonHintDlgScript.OpenHint("请求快速注册登录");
            GameApp.Instance.CommonHintDlgScript.OpenHint("请求快速注册登录");
            GameApp.Instance.CommonHintDlgScript.OpenHint("请求快速注册登录");
            GameApp.Instance.CommonHintDlgScript.OpenHint("请求快速注册登录");
            GameApp.Instance.CommonHintDlgScript.OpenHint("请求快速注册登录");
        });
        //为账号登录添加回调事件
        WeChatBtn.onClick.AddListener(delegate () {
            string user = UsernameIF.text;
            if (user.Length < 6) return;
            //创建一个账号登录对象
            RequestLoginModel rlm = new RequestLoginModel();
            rlm.Ditch = 0;
            rlm.UserName = user;
            rlm.Password = "password";
            this.Write(TypeProtocol.LOGIN, LoginProtocol.ENTER_CREQ, rlm);
            //GameApp.Instance.NetMessageUtilScript.NetIO.write(TypeProtocol.LOGIN, LoginProtocol.ENTER_CREQ, rlm);
            Debug.Log("请求账号登录");
        });
    }
}
