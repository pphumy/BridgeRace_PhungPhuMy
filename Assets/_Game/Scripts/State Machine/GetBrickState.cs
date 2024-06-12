using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBrickState : IState<Bot>
{
    Vector3 target;
    List<Vector3> listTarget;
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Constants.ANIM_RUN);

    }

    public void OnExecute(Bot t)
    {
        if (GameManager.Ins.gameState != GameState.Victory || GameManager.Ins.gameState != GameState.Fail)
        {
            if (t.CountBrick() > t.NOBrickToTake())
            {
                t.ChangeState(new ReachFinishPointState());
            }
            else
            {
                t.MoveToTarget();
            }
        }
        else
        {
            return;
        }
        
    }

    public void OnExit(Bot t)
    {

    }

    
}
