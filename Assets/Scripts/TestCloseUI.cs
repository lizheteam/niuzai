using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestCloseUI : MonoBehaviour {
    int index = 0;
    void Start()
    {
        transform.FindChild("ButtonAdd").GetComponent<Button>().onClick.AddListener(delegate ()
          {
              index++;
              transform.FindChild("Text").GetComponent<Text>().text = "点了" + index + "下";
          });
        transform.FindChild("ButtonClose").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            GameApp.Instance.GameLevelManagerScript.CloseSystemUI(GameResources.SystemUIType.NULL, delegate ()
              {
                  Debug.Log("关闭当前页面");
              });
        });
    }
}
