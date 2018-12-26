using UnityEngine;
using System.Collections;

/// <summary>
///外部扩展包消息通知
/// </summary>
public class SDKSendMessage : MonoBehaviour {
    /// <summary>
    /// 打开消息盒子
    /// </summary>
    /// <param name="msg"></param>
    public void OpenCommonBox(string msg)
    {
        GameApp.Instance.CommonHintDlgScript.OpenHint(msg);
    }
}
