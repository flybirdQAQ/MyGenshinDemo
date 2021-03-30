using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Bindings;
using Newtonsoft.Json;

public static class PackageManager
{

    static List<AssetBundleBuild> Maps = new List<AssetBundleBuild>();
    static List<string> Files = new List<string>();
    static AllBundleInfo AllInfo = new AllBundleInfo();
    public static RuntimePlatform BuildTargetToPlatform(BuildTarget target)
    {
        if (target == BuildTarget.StandaloneWindows64 || target == BuildTarget.StandaloneWindows)
            return RuntimePlatform.WindowsEditor;
        else if (target == BuildTarget.Android)
            return RuntimePlatform.Android;
        else if (target == BuildTarget.iOS)
            return RuntimePlatform.IPhonePlayer;
        else
            return RuntimePlatform.WindowsEditor;
    }

    [MenuItem("AssetBundleBuild/Build Windows Resource")]
    public static void BuildAssetResourceWindows()
    {

        BuildAssetResource(BuildTarget.StandaloneWindows64);
    }

    [MenuItem("AssetBundleBuild/Build Windows Stream")]
    public static void BuildAssetSteamWindows()
    {

        BuildAssetInfo(BuildTarget.StandaloneWindows64);
    }
    public static void BuildAssetResource(BuildTarget target)
    {

        string streamPath = SysDefine.GetAssetsTargetPathByPlatform(BuildTargetToPlatform(target));
        Debug.Log(streamPath);
        if (Directory.Exists(streamPath))
        {
            Directory.Delete(streamPath, true);
        }
        Directory.CreateDirectory(streamPath);
        AssetDatabase.Refresh();
        AllInfo.Infos.Clear();
        Maps.Clear();
        foreach (var dir in Directory.GetDirectories(SysDefine.PATH_ASSETBUNDLE_LOCAL))
        {
            string dirName = Path.GetFileName(dir);
            Debug.Log($"Handle {dirName}");
            HandleBundles(dirName);
        }

        BuildPipeline.BuildAssetBundles(streamPath, Maps.ToArray(), BuildAssetBundleOptions.None, target);


        AssetDatabase.Refresh();
    }


    private static SingleBundleInfo GetInfo(string path)
    {
        if (string.Empty == path || !File.Exists(path))
        {
            throw new Exception("找不到文件");
        }
        SingleBundleInfo info = new SingleBundleInfo();
        info.Name = Path.GetFileName(path);
        FileStream file = new FileStream(path, FileMode.Open);
        info.Size = file.Length;
        info.MD5 = GetMD5HashFromFile(file);
        file.Close();
        return info;
    }
    private static string GetMD5HashFromFile(FileStream file)
    {
        try
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("GetMD5HashFromFile() fail, error:" + ex.Message);
        }
    }



    public static void BuildAssetInfo(BuildTarget target)
    {
        string streamPath = SysDefine.GetStreamingAssetsTargetPathByPlatform(BuildTargetToPlatform(target));
        string path = SysDefine.GetAssetsTargetPathByPlatform(BuildTargetToPlatform(target));
        if (Directory.Exists(streamPath))
        {
            Directory.Delete(streamPath, true);
        }
        Directory.CreateDirectory(streamPath);
        AllBundleInfo AllInfos = new AllBundleInfo();
        string[] files = Directory.GetFiles(path);

        foreach (var file in files)
        {
            //if (Path.GetFileName(file)!= "AssetBundles.manifest" && Path.GetExtension(file) == ".manifest") continue;

            var info = GetInfo(file);
            AllInfos.Infos.Add(info.Name, info);
            File.Copy(file, Path.Combine(streamPath, info.Name + "@" + info.MD5));

        }
        string json = JsonConvert.SerializeObject(AllInfos, Formatting.Indented);
        if (File.Exists(streamPath + "/" + AllBundleInfo.Name))
        {
            File.Delete(streamPath + "/" + AllBundleInfo.Name);

        }
        File.WriteAllText(streamPath + "/" + AllBundleInfo.Name, json);

    }

    private static void HandleBundles(string prefix)
    {
        string path = $"{SysDefine.PATH_ASSETBUNDLE_LOCAL}/{prefix}/";
        string[] dirs = Directory.GetDirectories(path);
        Debug.Log("dirs.Length : " + dirs.Length.ToString());
        if (dirs.Length == 0)
            return;
        for (int i = 0; i < dirs.Length; i++)
        {
            string asset_name = $"{prefix}_" + Path.GetFileName(dirs[i]);
            Debug.Log("dir:" + asset_name);
            Files.Clear();
            Recursive(dirs[i], false);
            if (Files.Count > 0)
            {
                AssetBundleBuild build = new AssetBundleBuild();
                build.assetBundleName = asset_name;
                build.assetNames = Files.ToArray();
                Maps.Add(build);
            }
        }
    }

    private static void Recursive(string path, bool ignore_meta = true)
    {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        foreach (var filename in names)
        {
            string ext = Path.GetExtension(filename);
            if (ignore_meta && ext.Equals(".meta")) continue;
            Files.Add(filename.Replace('\\', '/'));
        }
        foreach (var dir in dirs)
        {
            Recursive(dir, ignore_meta);
        }
    }
}

