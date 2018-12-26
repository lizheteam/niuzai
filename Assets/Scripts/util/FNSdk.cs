using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// 外部接入函数调用
/// function sdk
/// </summary>
public class FNSdk {
    /// <summary>
    /// 单例对象
    /// </summary>
    private static FNSdk instance;
    public static FNSdk Instance
    {
        get
        {
            if (instance ==null)
            {
                instance = new FNSdk();
            }
            return instance;
        }
    }

    private FNSdk()
    {
        InitCallBack();
    }

    /// <summary>
    /// 安卓回调委托
    /// </summary>
    /// <param name="function"></param>
    /// <param name="obj">params表示参数是不固定的，动态变更的参数</param>
    private delegate void AndroidCall(string function, params object[] obj);

    /// <summary>
    /// 声明回调的对象
    /// </summary>
    private AndroidCall _callAndroid;

    /// <summary>
    /// 初始化和安卓平台的回调
    /// </summary>
    private void InitCallBack()
    {
        //当前平台
        if(Application.platform ==RuntimePlatform.Android)
        {
            //unity和Java的交互AndroidJavaClass
            AndroidJavaClass JClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject @static = JClass.GetStatic<AndroidJavaObject>("currentActivity");
            _callAndroid = new AndroidCall(@static.Call);
        }
    }

    /// <summary>
    /// 苹果平台外部扩展包引用方式  添加一个外部静态无返回函数,名称和oc代码必须一致
    /// </summary>
    /// <param name="code"></param>
    [DllImport("_internal")]
    private extern static void WeChatIOSLogin(string code);

    public void WeChatLogin(string code)
    {
        if(GameApp .Instance .IsAndroid)
        {
            if (_callAndroid != null)
            {
                _callAndroid("WeChatLogin", code);
            }
        }
        if(GameApp .Instance .IsIPhone)
        {
            WeChatIOSLogin(code);
        }
    }
}
