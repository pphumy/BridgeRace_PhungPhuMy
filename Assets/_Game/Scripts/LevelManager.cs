using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    Level currentLevel;
    public List<Level> levels = new List<Level>();
    public List<Transform> SpawnPos;
    [SerializeField] Bot botPrefab;
    public Vector3 startPoint;
    public Transform finishPos;
    public ColorSO dataColor;

    public List<ColorType> listSelectedColors = new List<ColorType>();

    private void Awake()
    {
        LoadLevel();
        finishPos = currentLevel.finishPoint;
        SpawnPos = currentLevel.StartPoint;
        Shuffle(SpawnPos);
        listSelectedColors = dataColor.GetListColor();
        startPoint = SpawnPos[0].position;
        SpawnBots();
    }
    public void SpawnBots()
    {
        
        //player.initPoint = SpawnPos[0].position;
        for (var i = 1; i < SpawnPos.Count; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(botPrefab, SpawnPos[i].position, Quaternion.identity);
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

    public void LoadLevel()
    {
        if (currentLevel != null) Destroy(currentLevel.gameObject);

        currentLevel = Instantiate(levels[0]);

        

    }
}
