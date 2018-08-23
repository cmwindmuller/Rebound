using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public static Game main;

    public enum GAME_STATE { Initialize, Setup, Play };
    public GAME_STATE gs;
    
    public ManageAR manageAR;
    public ManageBuild manageBuild;
    public ManageUI manageUI;
    public ManageTouch manageTouch;
    IManage[] managers;

    // Use this for initialization
    private void Awake()
    {
        main = this;
        gs = GAME_STATE.Initialize;

        manageAR = GetComponent<ManageAR>();
        manageBuild = GetComponent<ManageBuild>();
        manageUI = GetComponent<ManageUI>();
        manageTouch = GetComponent<ManageTouch>();
        managers = this.GetComponents<IManage>();
    }

    private void Update()
    {
        switch(gs)
        {
            case GAME_STATE.Initialize:
                break;
            case GAME_STATE.Setup:
                break;
            case GAME_STATE.Play:
                break;
        }
    }

}
