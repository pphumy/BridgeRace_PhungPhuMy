using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Character
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] Transform WinPos;
    
    public Vector3 initPoint;

    private Vector3 dir;
    private Vector3 movement;
    private bool Won = false;

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
        if(CanMove() && movement.sqrMagnitude > 0.1f && !Won)
        {
            transform.Translate(moveSpeed * Time.deltaTime * movement,Space.World);

            //rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
            ChangeAnim(Constants.ANIM_RUN);

        }
        else if( Won)
        {
            joystick.enabled = false;
            TF.position = WinPos.position+Vector3.up*20;
            TF.rotation = Quaternion.LookRotation(Vector3.back);
            this.ClearBrick();
            ChangeAnim(Constants.ANIM_WIN);
        }
        else
        {
            ChangeAnim(Constants.ANIM_IDLE);
        }
        
        Vector2 look = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (look.sqrMagnitude >= 0.1f && !Won) // avoid small movements
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

    private void CollideWithWinPos(Collider other)
    {
        if (other.CompareTag(Constants.TAG_WIN))
        {
            Won = true;
            ChangeAnim(Constants.ANIM_WIN);
        }
    }

    private void OnInit()
    {
        initPoint = LevelManager.Ins.startPoint;
        this.colorType = LevelManager.Ins.listSelectedColors[0];
        this.SetColor(this.colorType);
        transform.position = initPoint;
    }

}
