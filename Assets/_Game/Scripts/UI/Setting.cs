using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : UICanvas
{

    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
    }

    public override void CloseDirectly()
    {
        Time.timeScale = 1;
        base.CloseDirectly();
    }
    public void ContinueButton()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
        CloseDirectly();
        UIManager.Ins.OpenUI<Gameplay>();
    }

    public void RetryButton()
    {
        LevelManager.Ins.OnRetry();
        CloseDirectly();
        UIManager.Ins.OpenUI<Gameplay>();
    }
}
