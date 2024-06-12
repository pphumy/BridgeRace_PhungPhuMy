using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Bot botPrefab;
    [SerializeField] Player playerPrefab;

    public List<Level> levels = new List<Level>();
    public List<ColorType> listSelectedColors = new List<ColorType>();
    public List<Transform> spawnPos;
    public int levelIndex;
    public Vector3 startPoint;
    public Transform finishPos;
    public Transform winPos;
    public ColorSO dataColor;
    public List<Bot> bots;


    public Player player;
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
        for (int i = 1; i < spawnPos.Count; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(botPrefab, spawnPos[i].position, Quaternion.identity);
            bot.SetColor(listSelectedColors[i]);
            bot.ChangeState(new IdleState());
            bots.Add(bot);
        }
        
    }

    public void SetBotPos()
    {
        for(int i = 1; i < spawnPos.Count; i++)
        {
            bots[i-1].TF.position = spawnPos[i].position;
            bots[i - 1].ClearBrick();
            bots[i - 1].ChangeState(new IdleState());
        }
    }

    public void DestroyBot()
    {
        if(bots.Count != 0)
        {
            for (int i = 0; i < bots.Count; i++)
            {

                SimplePool.Despawn(bots[i]);
            }
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
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void OnStartGame()
    {
        SpawnBots();
    }

    public void OnNextLevel()
    {
        UIManager.Ins.OpenUI<Gameplay>();
        Debug.Log("run");
        GameManager.Ins.ChangeState(GameState.Gameplay);
        levelIndex++;
        PlayerPrefs.SetInt("Level", levelIndex);
        player.OnInit();
        OnInit();
        SetBotPos();
        
        
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
        else if (level >= levels.Count)
        {
            levelIndex = 0;
            currentLevel = Instantiate(levels[levelIndex]);
            PlayerPrefs.SetInt("Level", 0);
            
        }
    }

    private void OnInit()
    {
        levelIndex = PlayerPrefs.GetInt("Level");
        LoadLevel(levelIndex);
        Debug.Log(levelIndex);
        finishPos = currentLevel.finishPoint;
        spawnPos = currentLevel.startPoint;
        startPoint = spawnPos[0].position;
        Shuffle(spawnPos);
        listSelectedColors = dataColor.GetListColor();
        winPos = currentLevel.winPos;
    }
}
