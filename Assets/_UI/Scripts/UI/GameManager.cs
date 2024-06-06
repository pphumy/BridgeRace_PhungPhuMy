using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    //private static GameState gameState = GameState.MainMenu;
    public List<Transform> SpawnPos;
    [SerializeField] Bot botPrefab;
    public Vector3 startPoint;
    public ColorSO dataColor;

    public List<ColorType> listSelectedColors = new List<ColorType>();

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

        Shuffle(SpawnPos);
        listSelectedColors = dataColor.GetListColor();
        startPoint = SpawnPos[0].position;
        SpawnBots();
    }
    public void SpawnBots()
    {
        //player.initPoint = SpawnPos[0].position;
        for(var i =1 ; i< SpawnPos.Count; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(botPrefab, SpawnPos[i].position, Quaternion.identity);
            bot.SetColor(listSelectedColors[i]);
        }
    }

    public void Shuffle<T>(IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    //public static void ChangeState(GameState state)
    //{
    //    gameState = state;
    //}

    //public static bool IsState(GameState state)
    //{
    //    return gameState == state;
    //}
    

    

}
