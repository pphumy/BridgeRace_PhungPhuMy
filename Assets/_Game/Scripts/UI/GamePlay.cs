using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gameplay : UICanvas
{
    public TextMeshProUGUI levelUi;
    

    private void OnEnable()
    {
        int level = LevelManager.Ins.levelIndex+1;
        levelUi.SetText("Level " + level);
    }

    public void SettingButton()
    {
        
        GameManager.Ins.ChangeState(GameState.Pause);
        UIManager.Ins.OpenUI<Settings>();
        Close(0);
    }

}
