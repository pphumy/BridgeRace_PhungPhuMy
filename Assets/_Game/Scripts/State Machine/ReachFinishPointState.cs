using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachFinishPointState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
 
    }

    public void OnExecute(Bot t)
    {
        if (!t.won)
        {
            if (t.CountBrick() == 0)
            {
                t.ChangeState(new GetBrickState());
            }
            else
            {
                if (t.CanMove())
                {
                    t.MoveToFinishPoint();
                }
                else
                {
                    t.ChangeState(new GetBrickState());
                }
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
