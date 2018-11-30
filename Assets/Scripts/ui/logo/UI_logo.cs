using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_logo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        #region 测试代码
        //Debug.Log("ui_logo  start");
        //GameApp.Instance.TimeManagerScript.AddShedule(delegate ()
        //{
        //    Debug.Log("加载场景测试成功");
        //}, 10000);

        ////给logo界面按钮添加按钮方法
        //transform.FindChild("Button").GetComponent<Button>().onClick.AddListener(delegate ()
        //   {
        //       Debug.Log("button on click");
        //       //点击按钮加载生成UI界面
        //       GameApp.Instance.GameLevelManagerScript.LoadSyStemUI(GameResources.SystemUIType.UIHINTLOGPANEL,
        //          //给生成的界面上添加上脚本
        //           delegate () {
        //               GameObject go;
        //               if (GameApp .Instance .GameLevelManagerScript .SystemUICache .TryGetValue (GameResources.SystemUIType .UIHINTLOGPANEL,out go))
        //               {
        //                   if (!go.GetComponent <TestCloseUI>())
        //                   {
        //                       go.AddComponent<TestCloseUI>();
        //                   }
        //               }
        //            });
        //   });
        #endregion

        //给LOGO场景添加一个时间上的缓冲，1秒后执行场景加载
        GameApp.Instance.TimeManagerScript.AddShedule(delegate () {
            GameApp.Instance.GameLevelManagerScript.LoadScene(GameResources.SceneName.LOGIN);
        },1000);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
