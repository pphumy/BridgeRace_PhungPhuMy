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

        if (t.CountBrick() > t.NOBrickToTake())
        {
            t.ChangeState(new ReachFinishPointState());
        }
        else
        {
            t.MoveToTarget();
        }
    }

    public void OnExit(Bot t)
    {

    }

    
}
