//#undef UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Network;
using Services;
using Managers;
using DG.Tweening;
using System.IO;
using System.Configuration;
using UnityEngine.EventSystems;

public class GameStart : MonoSingleton<GameStart>
{

    public enum State
    {
        GameTips,             //开场
        CheckExtractResource, //初次运行游戏时需要解压资源文件
        UpdateResourceFromNet,//热更阶段：从服务器上拿到最新的资源
        InitAssetBundle,      //初始化AssetBundle
        StartLogin,           //登录流程
        StartGame,            //正式进入场景游戏
        Playing,              //完成启动流程了，接下来把控制权交给玩法逻辑
        None,                 //无
    }

    public enum SubState
    {
        Enter,
        Update
    }

    State currentState = State.None;
    SubState currentSubState = SubState.Enter;

    Coroutine coroutine;
    public UILoadingSliderView sliderView;
    public UITip uiTip;
    public GameObject obj;
    protected override void OnStart()
    {

        var a = log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.xml"));
        Common.Log.Init("Unity");
        Common.Log.Info("Log4Net");
        UnityLogger.Init();
        Debug.Log("GameStart");
        this.gameObject.AddComponent<LuaBehaviour>();
        this.gameObject.AddComponent<NetClient>();
        this.gameObject.AddComponent<HotFixManager>();
        UserService.Instance.Init();
        ItemService.Instance.Init();
        MapService.Instance.Init();
        StatusService.Instance.Init();
        QuestService.Instance.Init();
        MessageService.Instance.Init();
        FriendService.Instance.Init();
        JumpToState(State.GameTips);
        //JumpToState(State.Playing);
    }

    //private void LateUpdate()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {

    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit,100))
    //        {
    //            print(hit.transform.name);
    //        }
    //    }

    //}
    void Update()
    {


        if (currentState == State.Playing) return;
        switch (currentState)
        {
            case State.GameTips:
                {
                    if (currentSubState == SubState.Enter)
                    {
                        currentSubState = SubState.Update;
#if UNITY_EDITOR
                        //如果EDITOR模式就跳过之前的
                        sliderView.gameObject.SetActive(true);
                        sliderView.ShowSlider();
                        obj.SetActive(true);
#else
                        uiTip.gameObject.SetActive(true);
                        Camera.main.gameObject.SetActive(true);
                        uiTip.Show(() =>
                        {
                            obj.SetActive(true);
                            uiTip.Hide(() =>
                            {
                                uiTip.gameObject.SetActive(false);
                                sliderView.gameObject.SetActive(true);
                                sliderView.ShowSlider();
                            });
                            sliderView.gameObject.SetActive(true);

                        });
#endif
                        sliderView.OnSliderShowed += () =>
                        {

                            JumpToState(State.CheckExtractResource);
                        };

                    }
                    break;
                }

            case State.CheckExtractResource:
                {
                    if (currentSubState == SubState.Enter)
                    {
                        currentSubState = SubState.Update;

                        sliderView.Reset("检查文件中", SliderMode.Fraction, () =>
                         {
                             StopCoroutine(coroutine);
                             JumpToState(State.UpdateResourceFromNet);
                         });
                        coroutine = StartCoroutine(gameObject.GetComponent<HotFixManager>().Check(sliderView.SetData));
                    }
                    break;
                }
            case State.UpdateResourceFromNet:
                {
                    if (currentSubState == SubState.Enter)
                    {
                        currentSubState = SubState.Update;
                        sliderView.Reset("下载文件中", SliderMode.Download, () =>
                         {
                             StopCoroutine(coroutine);
                             GetComponent<HotFixManager>().UpdateAssetInfo();
                             JumpToState(State.InitAssetBundle);
                         });
                        coroutine = StartCoroutine(gameObject.GetComponent<HotFixManager>().Download(sliderView.AddData));
                    }
                    break;
                }
            case State.InitAssetBundle:
                {
                    if (currentSubState == SubState.Enter)
                    {
                        currentSubState = SubState.Update;
                        sliderView.Reset("初始化中", SliderMode.Percent, () =>
                         {
                             StopCoroutine(coroutine);
                             JumpToState(State.StartLogin);
                         });
                        coroutine = StartCoroutine(GameInit(sliderView.AddData));
                    }
                    break;
                }
            case State.StartLogin:
                {
                    if (currentSubState == SubState.Enter)
                    {
                        currentSubState = SubState.Update;
                        LuaBehaviour.Instance.LuaEnvInit();
                        this.gameObject.AddComponent<GameObjectManager>();
                        this.gameObject.AddComponent<EntityManager>();
                        NPCManager.Instance.NpcInit();
                        sliderView.OnSliderHided += () =>
                        {
                            LuaBehaviour.Instance.CallLuaEvent("EnterLogin");
                        };
                        sliderView.HideSlider();

                    }

                    break;
                }

        }
    }


    void JumpToState(State state)
    {

        currentState = state;
        currentSubState = SubState.Enter;
        Debug.Log($"GameState jump to {currentState}");
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("编辑状态游戏退出");
#else
        Application.Quit();
        Debug.Log("游戏退出");
#endif
    }



    IEnumerator GameInit(Action<string, long, long> addData)
    {

        this.gameObject.AddComponent<SceneManager>();
        this.gameObject.AddComponent<InputManager>();
        DontDestroyOnLoad(GameObject.Find("EventSystem"));
        yield return DataManager.Instance.LoadData(addData);
    }



}
