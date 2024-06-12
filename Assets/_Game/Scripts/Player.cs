using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Player player;
    public VariableJoystick joystick;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] GameObject joystickUi;
    [SerializeField] private float rotationSpeed = 5f;
    
    public Vector3 initPoint;
    private Vector3 movement;


    private void Awake()
    {
        OnInit();
    }

    void Update()
    {
        if (GameManager.Ins.gameState == GameState.Gameplay)
        {
            won = false;
        }
        else
        {
            won = true;
        }
        Move();

    }

    private void Move()
    {
        movement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if(CanMove() && movement.sqrMagnitude > 0.1f && GameManager.Ins.gameState == GameState.Gameplay )
        {
            transform.Translate(moveSpeed * Time.deltaTime * movement,Space.World);

            //rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
            ChangeAnim(Constants.ANIM_RUN);

        }
        else if(GameManager.Ins.gameState == GameState.Victory)
        {
            
            ChangeAnim(Constants.ANIM_WIN);
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
        
    }

    private void CollideWithDoor(Collider other)
    {
        if (other.CompareTag(Constants.TAG_DOOR))
        {
            other.isTrigger = false;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.CollideWithBrick(other);
        CollideWithWinPos(other);
    }
    protected override void CollideWithWinPos(Collider other)
    {
        base.CollideWithWinPos(other);
        if (other.CompareTag(Constants.TAG_WIN)){
            GameManager.Ins.ChangeState(GameState.Victory);
            joystickUi.SetActive(false);
            UIManager.Ins.CloseUI<Gameplay>();
            UIManager.Ins.OpenUI<Win>();
            CameraManager.Ins.MoveToStepUp();
        }
    }

    public void OnInit()
    {
        movement = Vector3.zero;
        initPoint = LevelManager.Ins.startPoint;
        ChangeAnim(Constants.ANIM_IDLE);
        this.colorType = LevelManager.Ins.listSelectedColors[0];
        this.SetColor(this.colorType);
        transform.position = initPoint;
    }

}
