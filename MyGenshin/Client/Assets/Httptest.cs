using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class Httptest : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1/StreamingAssetsServer/AssetInfo.json");
        print("播放等待UI开始转圈圈 屏蔽用户操作");
        yield return request.SendWebRequest();    
        print("关闭等待UI");
        if (request.isNetworkError)
        {
            print(request.error);
            
        }
        else
        {
            if (request.responseCode == 200)
            {
                if (!Directory.Exists(Application.persistentDataPath))
                {
                    Directory.CreateDirectory(Application.persistentDataPath);
                }
              
                File.WriteAllBytes(Application.persistentDataPath + "/AssetInfo.json", request.downloadHandler.data);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
