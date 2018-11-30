using UnityEngine;
using System.Collections;
using ClientNetFrame;
using System;
using GameProtocol;
using GameProtocol.model.login;

/// <summary>
/// 处理服务器二级协议消息分发处理
/// </summary>
public class LoginHandler : MonoBehaviour, IHandler
{
    public void MessageReceive(SocketModel model)
    {
        switch (model .command)
        {
            //处理服务器返回的登录结果
            case LoginProtocol.ENTER_SRES:
                {
                    int Status = model.GetMessage<int>();
                    /// <summary>
                    /// 服务器返回登录结果
                    /// res:int Status
                    ///0 表示登录成功
                    ///-1 请求错误
                    ///-2 账号密码不合法
                    ///-3 表示没有此账号
                    ///-4 表示密码错误
                    ///-5 表示账号已经登录
                    /// </summary>
                    switch (Status)
                    {
                        case 0:
                            GameApp.Instance.CommonHintDlgScript.OpenHint("登录成功");
                            LoginReceive();
                            break;
                        case -1:
                            GameApp.Instance.CommonHintDlgScript.OpenHint("请求错误");
                            break;
                        case -2:
                            GameApp.Instance.CommonHintDlgScript.OpenHint("账号密码不合法");
                            break;
                        case -3:
                            GameApp.Instance.CommonHintDlgScript.OpenHint("没有此账号");
                            break;
                        case -4:
                            GameApp.Instance.CommonHintDlgScript.OpenHint("密码错误");
                            break;
                        case -5:
                            GameApp.Instance.CommonHintDlgScript.OpenHint("账号已经登录");
                            break;
                    }
                }
                break;
            //处理服务器返回的注册结果
            case LoginProtocol.QUICKREG_SRES:
                {
                    //接收注册结果
                    ResponseRegisterModel rrm = model.GetMessage<ResponseRegisterModel>();
                    if(rrm ==null || rrm .Status !=0)
                    {
                        GameApp.Instance.CommonHintDlgScript.OpenHint("快速注册登录失败");
                        Debug.Log("注册失败");
                        return;
                    }
                    GameApp.Instance.CommonHintDlgScript.OpenHint("成功快速注册登录");
                    GameApp.Instance.CommonHintDlgScript.OpenHint("注册密码为：" + rrm.password);
                    LoginReceive();
                }
                break;
        }
    }

    private void LoginReceive()
    {
        this.Write(TypeProtocol.USER, UserProtocol.GETINFO_CREQ, null);
    }
}
