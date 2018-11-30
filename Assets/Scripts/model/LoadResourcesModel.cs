using UnityEngine;
using System.Collections;

/// <summary>
/// 加载资源类
/// </summary>
public class LoadResourcesModel  {
    /// <summary>
    /// 资源的类型
    /// </summary>
    public GameResources.ResourceType type = GameResources.ResourceType.PREFAB;
   
    /// <summary>
    /// 待加载的资源的路径
    /// </summary>
    public string path = "";

    public LoadResourcesModel() { }
    public LoadResourcesModel(GameResources .ResourceType Type,string Path) {
        this.type = Type;
        this.path = Path;
    }

}
