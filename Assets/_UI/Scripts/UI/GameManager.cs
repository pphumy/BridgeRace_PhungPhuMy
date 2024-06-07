using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public enum GameState { MainMenu, Gameplay, Pause, Victory}

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    public GameState gameState;
    
    

    // Start is called before the first frame update
    protected void Awake()
    {
        ////base.Awake();
        //Input.multiTouchEnabled = false;
        //Application.targetFrameRate = 60;
        //Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //int maxScreenHeight = 1280;
        //float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        //if (Screen.currentResolution.height > maxScreenHeight)
        //{
        //    Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        //}

        ////csv.OnInit();
        ////userData?.OnInitData();

        ////ChangeState(GameState.MainMenu);

        //UIManager.Ins.OpenUI<MianMenu>();

        
    }

    private void Start()
    {
        ChangeState(GameState.Gameplay);
    }

    

    public void ChangeState(GameState state)
    {
        gameState = state;
    }

    public bool IsState(GameState state)
    {
       return gameState == state;
    }
    

    

}
