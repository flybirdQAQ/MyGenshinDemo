﻿//#undef UNITY_EDITOR
using Boo.Lang;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using XLua;


[LuaCallCSharp]
public class LuaBehaviour : MonoSingleton<LuaBehaviour>
{

    LuaEnv luaEnv;
    LuaTable scriptEnv;
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;

    Action luaStart;
    Action luaUpdate;
    Action luaOnDestroy;
    Action luaLateUpdate;

    string main;

    byte[] customLoader(ref string fileName)
    {
        fileName = "lua/" + fileName.Replace('.', '/') + ".lua.txt";
#if UNITY_EDITOR
        fileName = SysDefine.PATH_ASSETBUNDLE_LOCAL + "/" + fileName;
        if (File.Exists(fileName))
        {
            return File.ReadAllBytes(fileName.ToLower());
        }
#endif  
        return ResMgr.GetLua(fileName).bytes;       
    }



    public void LuaEnvInit()
    {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(customLoader);
        scriptEnv = luaEnv.NewTable();
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        scriptEnv.Set("self", this);
        LoadScript("Main.Main");
        scriptEnv.Get("Start", out luaStart);
        scriptEnv.Get("Update", out luaUpdate);
        scriptEnv.Get("Ondestroy", out luaOnDestroy);
        scriptEnv.Get("LateUpdate", out luaLateUpdate);
        SafeDoString("LuaMain()");
    }

    public T GetAction<T>(string str)
    {
        return scriptEnv.GetInPath<T>(str);
    }


    public void CallLuaEvent(string eventType)
    {
        SafeDoString($"GlobleEventManager:Call(GlobleEvent.{eventType})");
    }

    public void SafeDoString(string scriptContent, LuaTable env = null, string chunkName = "chunk")
    {
        if (luaEnv == null) return;
        try
        {
            luaEnv.DoString(scriptContent, chunkName, env);
        }
        catch (Exception ex)
        {
            string msg = string.Format("xLua exception : {0}\n {1}", ex.Message, ex.StackTrace);
            Debug.LogError(msg, null);
        }
    }

    public void LoadScript(string name)
    {
        SafeDoString($"require '{name}'");
    }


    // Update is called once per frame
    void Update()
    {
        if (luaEnv != null)
        {
            luaUpdate?.Invoke();

            if (Time.time - LuaBehaviour.lastGCTime > GCInterval)
            {
                luaEnv.Tick();
                LuaBehaviour.lastGCTime = Time.time;
            }
        }

    }

    private void LateUpdate()
    {
        if (luaEnv != null)
        {
            luaLateUpdate?.Invoke();
        }
    }


    void OnDestroy()
    {
        luaOnDestroy?.Invoke();
        luaOnDestroy = null;
        luaUpdate = null;
        luaStart = null;
        scriptEnv?.Dispose();
    }
}
