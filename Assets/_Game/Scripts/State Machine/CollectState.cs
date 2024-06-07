using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : IState<Bot>
{
    int targetBrick;

    public void OnEnter(Bot t)
    {
        targetBrick = t.NOBrickToTake();
    }

    public void OnExecute(Bot t)
    {
        
    }

    public void OnExit(Bot t)
    {
        throw new System.NotImplementedException();
    }
}
