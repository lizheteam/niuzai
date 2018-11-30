using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour {
    /// <summary>
    /// 已加载的资源数量
    /// </summary>
    int LoadResourcesNumber = 0;

    /// <summary>
    /// 资源加载进度值（资源加载占50%   场景加载占50%）
    /// </summary>
    int ResProgressValue = 0;

    /// <summary>
    /// 场景加载进度值；
    /// </summary>
    int SceneProgressValue = 0;

    /// <summary>
    /// 加载进度条组件
    /// </summary>
    Slider ProgressSlider;

    /// <summary>
    /// 加载进度条显示组件
    /// </summary>
    Text ProgressText;

    /// <summary>
    /// 加载进度组件
    /// </summary>
    GameObject ProgressGameObject;

    /// <summary>
    /// 是否开始加载
    /// </summary>
    bool IsStartLoading = false;

    /// <summary>
    /// 异步加载场景的对象
    /// </summary>
    AsyncOperation async;

    /// <summary>
    /// 最大资源加载进度
    /// </summary>
    int MaxResourcesProgressValue = 100;

    /// <summary>
    /// 资源加载显示进度
    /// </summary>
    int ResProgress = 0;

    public delegate void CallBack();

    void Awake()
    {
        GameApp.Instance.LoadManagerScript = this;
        //获取组件
        ProgressSlider = transform.FindChild("bg/valueSlider").GetComponent<Slider>();
        ProgressText = transform.FindChild("bg/valueText").GetComponent<Text>();
        ProgressGameObject = transform.FindChild("bg").gameObject;
        ProgressGameObject.SetActive(false);
    }




    /// <summary>
    /// 更新进度显示
    /// </summary>
    void FixedUpdate()
    {
        if(IsStartLoading)
        {
            ProgressSlider.value = SceneProgressValue/100f+ResProgress /100f /*/2+ ResProgressValue / 100f/2*/;
            ProgressText.text = "正在加载中..." + (SceneProgressValue+ResProgress )  + "%";
        }
    }

    /// <summary>
    /// 开始加载下一个场景
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="res"></param>
    public void StartLoadScene(GameResources .SceneName scene,List<LoadResourcesModel > res,CallBack loadcall)
    {
        MaxResourcesProgressValue = 100;
        //重置进度条显示
        ProgressSlider.value = 0;
        //重置进度显示
        ProgressText.text = "正在加载中...  0%";
        //重置资源加载进度值
        ResProgressValue = 0;
        //重置已经加载的资源
        LoadResourcesNumber = 0;
        //重置场景加载进度值
        SceneProgressValue = 0;
        //重置资源加载显示进度值
        ResProgress = 0;
        //开始加载场景
        IsStartLoading = true;
        ProgressGameObject.SetActive(true);
        StartCoroutine(LoadScene(scene, res, loadcall));
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    IEnumerator LoadScene(GameResources .SceneName scene,List <LoadResourcesModel > list,CallBack loadcall=null )
    {
        //展示用的进度
        int displaypro = 0;
        //实际的进度
        int topro = 0;
        //开始加载下一个场景
        async= SceneManager.LoadSceneAsync(GameData.Instance.SceneName[scene]);
        //暂时不进去下一个场景
        async.allowSceneActivation = false;
        //yield return async;
        //在加载进度不足90%时，进行进度条缓动动画
        while (async .progress < 0.9f)
        {
            //如果我们的显示进度尚未达到实际进度时，每帧增加百分之一
            topro =(int)(async .progress *100f);
            while (displaypro <topro)
            {
                displaypro++;
                SceneProgressValue = (int)(displaypro * 0.5f);
                yield return new WaitForFixedUpdate();
            }
        }
        //加载最后一段进度
        topro = 100;
        while (displaypro <topro)
        {
            displaypro++;
            SceneProgressValue = displaypro / 2;
            yield return new WaitForFixedUpdate();
        }
        //加载资源
        LoadResources(list);
        displaypro = 0;
        //如果我们的显示进度尚未达到实际进度时，每帧增加百分之一
        while (ResProgressValue <= 100 && displaypro <MaxResourcesProgressValue )
        {
            displaypro++;
            ResProgress = displaypro / 2;
            yield return new WaitForFixedUpdate();
        }

        //全部加载完毕后，进入下一个场景
        async.allowSceneActivation = true;
        //等待场景真正加载完毕
        while (!async.isDone)
            yield return new WaitForFixedUpdate();
        IsStartLoading = false;
        //全部加载完毕后，执行回调
        if (loadcall != null)
            loadcall();

        //将加载页面隐藏
        ProgressGameObject.SetActive(false);
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="res"></param>
    void LoadResources(List <LoadResourcesModel > res)
    {
        for (int i = 0; i < res.Count; i++)
        {
            switch (res [i].type)
            {
                case GameResources.ResourceType.AUDIO:
                    GameApp.Instance.MusicManagerScript.LoadClip(res[i].path);
                    LoadResourcesCallBack(res.Count); 
                    break;
                case GameResources.ResourceType.PREFAB:
                    {
                        GameApp.Instance.ResourcesManagerScript.LoadGameObject(res[i].path);
                        LoadResourcesCallBack(res.Count);
                    }
                    break;
                case GameResources.ResourceType.SPRITE:
                    LoadResourcesCallBack(res.Count);
                    break;
                case GameResources.ResourceType.TEXTASSET:
                    LoadResourcesCallBack(res.Count);
                    break;
            }
        }
        if (res.Count == 0)
            LoadResourcesCallBack(0);
    }

    /// <summary>
    /// 加载资源完成后回调，更新加载数量
    /// </summary>
    /// <param name="rModelCount"></param>
    void LoadResourcesCallBack(int rModelCount)
    {
        //已经加载数量/待加载数量*100%，即为资源加载进度
        float num = 100f / rModelCount;
        LoadResourcesNumber++;
        ResProgressValue = (int)(LoadResourcesNumber * num);
    }
}
