using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : UICanvas
{
    public void RetryButton()
    {
        LevelManager.Ins.OnRetry();
        Close(0);
    }

    public void NextButton()
    {
        LevelManager.Ins.OnNextLevel();
        Close(0);
    }
}
