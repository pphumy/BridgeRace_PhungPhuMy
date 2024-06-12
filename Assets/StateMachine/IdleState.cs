using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Constants.ANIM_IDLE);
    }

  

    public void OnExecute(Bot t)
    {
        t.ChangeState(new GetBrickState());
    }

 

    public void OnExit(Bot t)
    {
        
    }
}
