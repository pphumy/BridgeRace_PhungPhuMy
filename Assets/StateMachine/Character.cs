using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    private IState<Character> currentState;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Renderer skinCharacter;
    [SerializeField] protected Brick brickPrefab;
    [SerializeField] protected Transform brickHolder;
    [SerializeField] protected LayerMask bridgeLayer;


    public ColorType colorType;
    public Platform platform;
    public ColorSO colorData;
    public Transform root;
    public LayerMask platformLayer;

    protected string currentAnimName;



    List<Brick> brickList = new List<Brick>();

    private void Start()    
    {
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    protected bool CanMove()
    {
        RaycastHit hit;
        if (Physics.Raycast(TF.position + new Vector3(0, 1, 1), Vector3.down,out hit, 5f, bridgeLayer))
        {
            Stair stair = Cache.GetStair(hit.collider);
            if(TF.transform.forward.z > 0)
            {
                if(this.colorType == stair.colorType)
                {
                    return true;
                }
                else
                {
                    if(brickList.Count > 0)
                    {
                        RemoveBrick();
                        stair.ChangeColor(colorType);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }
        return true;
    }
    public void ChangeState(IState<Character> state)
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
    protected void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void SetColor(ColorType colorType)
    {
        this.colorType = colorType;
        skinCharacter.material = colorData.GetMat(colorType);
    }

    protected void AddBrick()
    {
        Brick brick = Instantiate(brickPrefab, brickHolder);
        brick.ChangeColor(this.colorType);
        brick.transform.localPosition = new Vector3(0, brickList.Count * 0.6f, 0);
        brickList.Add(brick);
    }

    protected void RemoveBrick()
    {
        if (brickList.Count > 0)
        {
            Brick brick = brickList[brickList.Count - 1];
            brickList.Remove(brick);
            Destroy(brick.gameObject);
        }
    }

    protected void ClearBrick()
    {
        foreach (Brick brick in brickList)
        {
            Destroy(brick.gameObject);
        }
        brickList.Clear();
    }

    protected void OnTriggerEnter(Collider other)
    {
        CollideWithBrick(other);
        
    }
    protected void CollideWithBrick(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BRICK))
        {
            Brick brick = Cache.GetBrick(other);
            if(this.colorType == brick.colorType) {
                brick.DeActiveBrick();
                AddBrick();
                Debug.Log("Add");
            }
            

        }
    }
}
