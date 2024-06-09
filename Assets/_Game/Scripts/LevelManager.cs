using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Bot botPrefab;

    public List<Level> levels = new List<Level>();
    public List<ColorType> listSelectedColors = new List<ColorType>();
    public List<Transform> spawnPos;
    public int levelIndex;
    public Vector3 startPoint;
    public Transform finishPos;
    public Transform winPos;
    public ColorSO dataColor;
    

    private Level currentLevel;

    private void Awake()
    {
        LoadLevel(levelIndex);
        OnInit();
    }

    private void Start()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        
    }

    public void SpawnBots()
    {
        //player.initPoint = SpawnPos[0].position;
        for (var i = 1; i < spawnPos.Count; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(botPrefab, spawnPos[i].position, Quaternion.identity);
            bot.SetColor(listSelectedColors[i]);
            bot.ChangeState(new GetBrickState());
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



    public void OnRetry()
    {
        LoadLevel(levelIndex);
        OnInit();
    }

    public void OnStartGame()
    {
        SpawnBots();
    }

    public void OnNextLevel()
    {
        levelIndex++;
        PlayerPrefs.SetInt("Level", levelIndex);
        LoadLevel(levelIndex);
        OnInit();
    }

    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if (level < levels.Count)
        {
            currentLevel = Instantiate(levels[level]);
        }
        else if (level == levels.Count)
        {
            levelIndex = 0;
            PlayerPrefs.SetInt("Level", 0);
        }
    }

    private void OnInit()
    {
        levelIndex = PlayerPrefs.GetInt("Level");
        finishPos = currentLevel.finishPoint;
        spawnPos = currentLevel.startPoint;
        winPos = currentLevel.winPos;
        Shuffle(spawnPos);
        listSelectedColors = dataColor.GetListColor();
        startPoint = spawnPos[0].position;
    }
}
