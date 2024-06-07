using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{

    Transform finishPoint;
    public UnityEngine.AI.NavMeshAgent agent;

    IState<Bot> currentState;
    List<Vector3> listTarget = new List<Vector3>();
    private Vector3 destination;
    private Vector3 target;
    private int index = 0;
    private bool isStair = false;


    private void Update()
    {
        if (currentState != null && GameManager.Ins.IsState(GameState.Gameplay))
        {
            currentState.OnExecute(this);
        }
        else if (GameManager.Ins.gameState == GameState.Victory)
        {
            return;
        }
        else
        {
            agent.isStopped = true;
        }

    }
   
    public void GetBrickPos()
    {
        listTarget = this.platform.GetBrickPoint(this.colorType);
    }
    public void MoveToTarget()
    {
        GetBrickPos();
        for(int i =0; i < listTarget.Count; i++)
        {
            target = listTarget[i];
            this.agent.SetDestination(target);
        }
    }

    public void MoveToFinishPoint()
    {
        finishPoint = LevelManager.Ins.finishPos;
        this.agent.SetDestination(finishPoint.position);
    }

    public int NOBrickToTake()
    {
        return UnityEngine.Random.Range(6,10);
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(Constants.TAG_PLATFORM))
        {
            this.platform = Cache.GetPlatform(other.collider);
        }
    }

}
