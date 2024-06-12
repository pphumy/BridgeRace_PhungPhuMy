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

    private void Update()
    {
        if (currentState != null && GameManager.Ins.IsState(GameState.Gameplay))
        {
            currentState.OnExecute(this);
            agent.enabled = true;
        }
        else if (GameManager.Ins.gameState == GameState.Victory)
        {
            agent.enabled = false;
            return;
        }

    }
   
    public void GetBrickPos()
    {
        listTarget = platform.GetBrickPoint(colorType);
    }
    public virtual void MoveToTarget()
    {
        if(platform == null)
        {
            return;
        }
        else
        {
            GetBrickPos();
        }

        for (int i =0; i < listTarget.Count; i++)
        {
            target = listTarget[i];
            this.agent.SetDestination(target);
        }
    }

    public  void MoveToTargetDelay(float delayTime)
    {
        Invoke(nameof(MoveToTarget), delayTime);
        
    }

    public void MoveToFinishPoint()
    {
        finishPoint = LevelManager.Ins.finishPos;
        this.agent.SetDestination(finishPoint.position);
    }

    public int NOBrickToTake()
    {
        return UnityEngine.Random.Range(6,11);
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

    protected override void CollideWithWinPos(Collider other)
    {
        if (other.CompareTag(Constants.TAG_WIN))
        {
            winPos = LevelManager.Ins.winPos;
            ClearBrick();
            won = true;
            this.agent.isStopped = true;
            this.agent.enabled = false;
            ChangeAnim(Constants.ANIM_WIN);
            TF.position = winPos.position;
            Debug.Log("true1");
            TF.rotation = Quaternion.LookRotation(Vector3.back);
            GameManager.Ins.ChangeState(GameState.Fail);
            UIManager.Ins.OpenUI<Lose>();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(Constants.TAG_PLATFORM))
        {
            this.platform = Cache.GetPlatform(other.collider);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CollideWithWinPos(other);
    }

    public void OnInit()
    {
        ClearBrick();
    }
}
