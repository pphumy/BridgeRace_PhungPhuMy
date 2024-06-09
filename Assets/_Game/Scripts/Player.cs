using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    
    
    public Vector3 initPoint;

    private Vector3 dir;
    private Vector3 movement;


    private void Awake()
    {
        OnInit();
    }

    void Update()
    {
        Move();

    }

    private void Move()
    {
        movement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if(CanMove() && movement.sqrMagnitude > 0.1f && !won)
        {
            transform.Translate(moveSpeed * Time.deltaTime * movement,Space.World);

            //rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
            ChangeAnim(Constants.ANIM_RUN);

        }
        else if( won)
        {
            joystick.enabled = false;
            TF.position = winPos.position+Vector3.up*22+Vector3.back*4;
            TF.rotation = Quaternion.LookRotation(Vector3.back);
            this.ClearBrick();
            ChangeAnim(Constants.ANIM_WIN);
            GameManager.Ins.ChangeState(GameState.Victory);
        }
        else
        {
            ChangeAnim(Constants.ANIM_IDLE);
        }
        
        Vector2 look = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (look.sqrMagnitude >= 0.1f && !won) // avoid small movements
        {
            float targetAngle = Mathf.Atan2(look.x, look.y) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CollideWithDoor(other);
        CollideWithWinPos(other);
    }

    private void CollideWithDoor(Collider other)
    {
        if (other.CompareTag(Constants.TAG_DOOR))
        {
            other.isTrigger = false;
        }
    }

    

    private void OnInit()
    {
        initPoint = LevelManager.Ins.startPoint;
        this.colorType = LevelManager.Ins.listSelectedColors[0];
        this.SetColor(this.colorType);
        winPos = LevelManager.Ins.winPos;
        transform.position = initPoint;
    }

}
