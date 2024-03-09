using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Controls_Listener input;

    [SerializeField]
    private float speed;

    private float moveDir;

    private bool isDropping;

    private void Start()
    {
        input.PlayerMoveEvent += HandleMove;
        input.DropEvent += HandleDrop;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleDrop()
    {
        isDropping = true;
    }
    private void HandleMove(float dir)
    {
        moveDir = dir;
    }
    private void Move()
    {
        if (moveDir == 0.0f)
            return;
        transform.position += new Vector3(moveDir, 0, 0) * (speed * Time.deltaTime);
    }
}
