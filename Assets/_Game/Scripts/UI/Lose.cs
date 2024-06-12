using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : UICanvas
{

    public void RetryButton()
    {
        LevelManager.Ins.OnRetry();
        CloseDirectly();
    }

}


