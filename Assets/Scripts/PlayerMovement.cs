using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
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

            var moveVector = transform.rotation * new Vector3(move.x, 0, move.y);

            rigidBody.MovePosition(rigidBody.position + moveVector * Time.deltaTime * moveSpeed);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var a = FindObjectOfType<NightVisionManager>();
            a.Toggle();
        }
    }

    void FixedUpdate()
    {
        Move();
    }
}
