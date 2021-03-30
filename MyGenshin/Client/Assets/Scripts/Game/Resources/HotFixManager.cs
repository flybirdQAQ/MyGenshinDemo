using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

using UnityEngine.Networking;
public class HotFixManager : MonoBehaviour
{

    public const string httpAddress = "http://127.0.0.1/StreamingAssetsServer/";
    public AllBundleInfo localInfo;

    public AllBundleInfo serverInfo;
    Queue<SingleBundleInfo> downloadQueue = new Queue<SingleBundleInfo>();




    public IEnumerator Check(Action<string, long, long> setData)
    {

        setData("下载资源列表", 0, 1);
        UnityWebRequest req = UnityWebRequest.Get(httpAddress + AllBundleInfo.Name);
        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            setData("更新资源列表失败", 0, 1);
            GameStart.QuitGame();
                         
        }
        if (!Directory.Exists(Application.persistentDataPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }


        File.WriteAllBytes(Application.persistentDataPath + "/" + AllBundleInfo.Name, req.downloadHandler.data);
        yield return CompareAsset(setData);

    }

    IEnumerator CompareAsset(Action<string, long, long> setData)
    {
        int cur = 0;
        LoadInfo(Application.persistentDataPath + "/" + AllBundleInfo.Name, out serverInfo);
        if (LoadInfo(Application.streamingAssetsPath + "/" + AllBundleInfo.Name, out localInfo))
        {
            setData("更新列表中", 0, serverInfo.Infos.Count);
            foreach (var asset in serverInfo.Infos)
            {
                if (!localInfo.Infos.ContainsKey(asset.Key) || localInfo.Infos[asset.Key].MD5 != asset.Value.MD5)
                {
                    downloadQueue.Enqueue(asset.Value);
                    setData(null, cur, serverInfo.Infos.Count);
                    yield return null;
                }
                cur += 1;
            }

        }
        else
        {
            foreach (var asset in serverInfo.Infos)
            {

                downloadQueue.Enqueue(asset.Value);
                setData(null, cur, serverInfo.Infos.Count);
                yield return null;
                cur += 1;
            }

        }
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        if (File.Exists(Application.streamingAssetsPath + "/" + AllBundleInfo.Name))
        {
            File.Delete(Application.streamingAssetsPath + "/" + AllBundleInfo.Name);
        }
        setData("列表更新成功", cur, serverInfo.Infos.Count);
    }


    public void UpdateAssetInfo()
    {

        File.Copy(Application.persistentDataPath + "/" + AllBundleInfo.Name, Application.streamingAssetsPath + "/" + AllBundleInfo.Name);
    }

    public bool LoadInfo(string path, out AllBundleInfo allBundleInfo)
    {
        try
        {
            string json = File.ReadAllText(path);
            allBundleInfo = JsonConvert.DeserializeObject<AllBundleInfo>(json);
            return true;
        }
        catch
        {
            allBundleInfo = null;
            return false;
        }


    }

    public IEnumerator Download(Action<string, long, long> addData)
    {
        long total = downloadQueue.Select(x => x.Size).ToList().Sum();
        addData("下载资源中", 0, total);
        while (downloadQueue.Count > 0)
        {
            var info = downloadQueue.Dequeue();
            var download = new DownloadEnum()
            {
                Url = httpAddress + info.Name + "@" + info.MD5,
                Path = Application.streamingAssetsPath + "/" + info.Name,
                callback = () =>
                {
                    addData(null, info.Size, total);               
                }
            };
            yield return null;
            HttpUtil.Downloader(download);
        }



    }
}

