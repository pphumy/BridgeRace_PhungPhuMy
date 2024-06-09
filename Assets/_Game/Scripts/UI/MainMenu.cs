using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    {
        LevelManager.Ins.OnStartGame();
        UIManager.Ins.OpenUI<Gameplay>();
        Close(0);
    }
}
