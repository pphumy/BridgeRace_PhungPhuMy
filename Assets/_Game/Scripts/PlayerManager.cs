using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Character
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    private Vector3 m_moveVector;
    private Vector3 dir;
    public Vector3 initPoint;


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
        Vector3 movement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if(movement.sqrMagnitude > 0.1f)
        {
            transform.Translate(moveSpeed * Time.deltaTime * movement,Space.World);

            //rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
            ChangeAnim(Constants.ANIM_RUN);

        }
        else
        {
            ChangeAnim(Constants.ANIM_IDLE);
        }
        
        Vector2 look = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (look.sqrMagnitude >= 0.1f) // avoid small movements
        {
            float targetAngle = Mathf.Atan2(look.x, look.y) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.TAG_BRICK))
        {

        }
    }
    private void OnInit()
    {
        initPoint = GameManager.Ins.startPoint;
        this.colorType = GameManager.Ins.listSelectedColors[0];
        this.SetColor(this.colorType);
        transform.position = initPoint;
    }

}
