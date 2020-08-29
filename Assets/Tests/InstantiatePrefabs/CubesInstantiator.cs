using System.IO;
using UnityEngine;

public class CubesInstantiator : MonoBehaviour
{
    private void Start()
    {
        InstantiateFromAssetBundle("cube1","Cube_1",new Vector3(0, 0, 0));
        InstantiateFromAssetBundle("cube2", "Cube_2",new Vector3(1, 1, 1));
    }

    public void InstantiateFromAssetBundle(string bundleName, string prefabName, Vector3 position)
    {
        AssetBundle myLoadedAssetBundle
            = AssetBundle.LoadFromFile("Assets/Tests/InstantiatePrefabs/AssetBundles/"+ bundleName);
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>(prefabName);
        Instantiate(prefab);
    }
}
