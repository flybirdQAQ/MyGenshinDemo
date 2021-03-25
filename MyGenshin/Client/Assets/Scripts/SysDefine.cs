using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SysDefine
{


    //目录
    public const string PATH_ASSETBUNDLE = "AssetBundles";
    public const string PATH_ASSETBUNDLE_LOCAL = "Assets/AssetBundlesLocal";
    public const string PATH_ROOT_DEFINE = "DATA/";
    public const string PATH_DEFINE_MAP = PATH_ROOT_DEFINE + "MapDefine.txt";
    public const string PATH_DEFINE_CHARACTER = PATH_ROOT_DEFINE + "CharacterDefine.txt";
    public const string PATH_DEFINE_TELEPORTER = PATH_ROOT_DEFINE + "TeleporterDefine.txt";
    public const string PATH_DEFINE_SPAWNPOINT = PATH_ROOT_DEFINE + "SpawnPointDefine.txt";
    public const string PATH_DEFINE_NPC = PATH_ROOT_DEFINE + "NPCDefine.txt";
    public const string PATH_DEFINE_ITEM = PATH_ROOT_DEFINE + "ItemDefine.txt";
    public const string PATH_DEFINE_UI = PATH_ROOT_DEFINE + "UIDefine.txt";
    public const string PATH_DEFINE_TALK= PATH_ROOT_DEFINE + "TalkDefine.txt";
    public const string PATH_DEFINE_STORE = PATH_ROOT_DEFINE + "StoreDefine.txt";
    public const string PATH_DEFINE_GOODS = PATH_ROOT_DEFINE + "GoodsDefine.txt";
    public const string PATH_DEFINE_EQUIP = PATH_ROOT_DEFINE + "EquipDefine.txt";
    public const string PATH_DEFINE_PROPERTY = PATH_ROOT_DEFINE + "PropertyDefine.txt";
    public const string PATH_DEFINE_SPECIAL= PATH_ROOT_DEFINE + "SpecialDefine.txt";
    public const string PATH_DEFINE_QUEST = PATH_ROOT_DEFINE + "QuestDefine.txt";
    public const string PATH_DEFINE_CHAPTER = PATH_ROOT_DEFINE + "ChapterDefine.txt";

    public static string GetStreamingAssetsTargetPathByPlatform(RuntimePlatform platform)
    {
        string dataPath = Application.dataPath.Replace("/Assets", "");
        if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.WindowsPlayer || platform == RuntimePlatform.WebGLPlayer)
            return dataPath + "/StreamingAssets";
        else if (platform == RuntimePlatform.Android)
            return dataPath + "/StreamingAssetsAndroid";
        else if (platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer)
            return dataPath + "/StreamingAssetsIOS";
        else
            Debug.Log("Unspport System!");

        return string.Empty;

    }
    public static string GetAssetsTargetPathByPlatform(RuntimePlatform platform)
    {
        string dataPath = Application.dataPath.Replace("/Assets", "");
        if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.WindowsPlayer || platform == RuntimePlatform.WebGLPlayer)
            return dataPath + "/" + SysDefine.PATH_ASSETBUNDLE;
        else if (platform == RuntimePlatform.Android)
            return dataPath + "/Android/" + SysDefine.PATH_ASSETBUNDLE;
        else if (platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer)
            return dataPath + "/IOS/" + SysDefine.PATH_ASSETBUNDLE;
        else
            Debug.Log("Unspport System!");

        return string.Empty;

    }
}

