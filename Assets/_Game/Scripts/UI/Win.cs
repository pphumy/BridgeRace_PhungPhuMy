using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : UICanvas
{
    public void RetryButton()
    {
        LevelManager.Ins.OnRetry();
        CloseDirectly();
    }

    public void NextButton()
    {
        LevelManager.Ins.OnNextLevel();
        CloseDirectly();
    }
}
