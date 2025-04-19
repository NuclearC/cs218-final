using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] float runSpeed = 2.3f;
    private InputHandler inputHandler;
    private Rigidbody rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        inputHandler = GameManager.Instance.InputHandler;
    }


    // move the player according to the input
    private void Move()
    {
        if (inputHandler.Move.HasValue)
        {
            var move = inputHandler.Move.Value;

            if (move.magnitude > 1.0f)
            {
                move.Normalize();
            }

            var moveVector = transform.rotation * new Vector3(move.x, 0, move.y);

            var finalMoveSpeed = moveSpeed;
            if (inputHandler.Run)
            {
                finalMoveSpeed = runSpeed;
            }

            rigidBody.MovePosition(rigidBody.position + moveVector * Time.deltaTime * finalMoveSpeed);
        }
    }

    void FixedUpdate()
    {
        Move();
    }
}
