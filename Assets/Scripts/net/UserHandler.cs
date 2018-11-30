using UnityEngine;
using System.Collections;
using ClientNetFrame;
using System;
using GameProtocol;
using GameProtocol.model.login;

public class UserHandler: MonoBehaviour, IHandler
{
    public void MessageReceive(SocketModel model)
    {
        switch (model .command)
        {
            case UserProtocol.GETINFO_SRES:
                {
                    UserModel um = model.GetMessage<UserModel>();
                    if(um!=null)
                    {
                        GameApp.Instance.CommonHintDlgScript.OpenHint("获取用户信息成功"+um.nickname);
                        GameSession.Instance.UserInfo = um;
                        //用户信息加载成功后，加载main场景
                        GameApp.Instance.GameLevelManagerScript.LoadScene(GameResources.SceneName.MAIN);
                    }else
                    {
                        //用户信息加载失败，显示提示信息，先关闭网络再开启网络
                        GameApp.Instance.CommonHintDlgScript.OpenHint("获取用户信息失败");
                        ExtendHandler.Close();
                        ExtendHandler.Connect();
                    }
                    GameApp.Instance.CommonHintDlgScript.OpenHint("获取no" + um.nickname);
                }
                break;
        }
    }
}
