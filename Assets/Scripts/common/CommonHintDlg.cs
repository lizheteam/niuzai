using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 常用通用提示框
/// </summary>
public class CommonHintDlg : MonoBehaviour {
    /// <summary>
    /// 待显示的消息列表
    /// </summary>
    List<string> MessageList = new List<string>();

    /// <summary>
    /// 过滤文本
    /// </summary>
    List<string> FilterMessage = new List<string>();

    /// <summary>
    /// 提示框的缓存
    /// </summary>
    List<GameObject> Cache = new List<GameObject>();

    void Awake()
    {
        GameApp.Instance.CommonHintDlgScript = this;
    }

    /// <summary>
    /// 打开提示框，显示将要显示的提示
    /// </summary>
    /// <param name="msg"></param>
    public void OpenHint(string msg)
    {
        //如果当前过滤文本中含有此消息,则直接过滤
        if (FilterMessage.Contains(msg))
            return;
        //将要打印的消息存储到MessageList中
        MessageList.Add(msg);
        UpdateBox();
    }

    /// <summary>
    /// 刷新提示框
    /// </summary>
    void UpdateBox()
    {
        //如果当前没有消息，直接返回
        if (MessageList.Count < 0)
            return;
        //获取一个新的提示框
        GameObject go;
        //如果当前缓存中没有提示框组件的缓存，则创建一个新的
        if(Cache .Count == 0)
        {
            //组件路径
            string path = GameResources.ItemResourcesPath + GameData.Instance.ItemName[GameResources.ItemTag.HINTBOXITEM];
            go = GameApp.Instance.ResourcesManagerScript.LoadInstantiateGameObject(path, transform, Vector3.zero);
        }
        else
        {
            //否则使用缓存中的组件
            go = Cache[0];
            Cache.RemoveAt(0);      
            //将组件显示设置为true
            go.SetActive(true);
        }
        //将提示框默认透明值设为不透明
        go.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //获取hintitem的子对象text
        Text text= go.transform.FindChild("Text").GetComponent<Text>();
        //将text内容设置wei消息缓存中的第一个文本
        string msg= MessageList[0];
        text.text =msg;
        //将当前正在显示的文本添加到过滤消息中
        FilterMessage.Add(msg);
        //开始尝试关闭提示框
        StartCoroutine(CloseBox(go, msg));
        MessageList.RemoveAt(0);
        if (MessageList.Count > 0)
            UpdateBox();
    }

    /// <summary>
    /// 关闭提示框
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    IEnumerator CloseBox(GameObject go,string hint)
    {
        //等待2.0f秒钟执行下面的语句
        yield return new WaitForSeconds(2.0f);
        FilterMessage.Remove(hint);
        for (int i = 19; i > -1; i--)
        {
            //等待一帧之后执行下列语句
            yield return new WaitForFixedUpdate();
            //逐渐让提示框变为透明
            go.GetComponent<Image>().color = new Color(1f, 1f, 1f, i / 20f);
        }
        //让组件不显示之后添加到组件缓存中
        go.SetActive(false);
        //Destroy(go);
        Cache.Add(go);
    }
}
