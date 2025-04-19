using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] float runSpeed = 2.3f;

    [SerializeField] AudioClip[] footsteps;
    private InputHandler inputHandler;
    private Rigidbody rigidBody;

    private float nextFootstepPlayTime = 0.0f;

    private Vector3 startPos = Vector3.zero;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        inputHandler = GameManager.Instance.InputHandler;
        startPos = transform.position;
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

            if (move.magnitude < 0.01f)
                return;

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
        var speed = (rigidBody.position - startPos).magnitude / Time.deltaTime;

        if (speed > 1.0f)
        {
            // play the sounds
            if (Time.time >= nextFootstepPlayTime)
            {
                AudioSource.PlayClipAtPoint(footsteps[Random.Range(0, footsteps.Length)], transform.position + Vector3.down);
                nextFootstepPlayTime = Time.time + 1.0f / speed;
            }
        }
        startPos = rigidBody.position;


        Move();
    }
}
