using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public Transform target;
    public Vector3 offset;
    public Vector3 pos;

    void Update()
    {
        if (GameManager.Ins.IsState(GameState.Gameplay))
        {
            FollowTarget();
        }else if (GameManager.Ins.IsState(GameState.Victory))
        {
            MoveToStepUp();
        }
    }

    private void FollowTarget()
    {
        transform.position = target.position + offset;
    }
    public void MoveToStepUp()
    {
        transform.position = pos;
    }
}
