using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] float runSpeed = 6.0f;
    [SerializeField] float crouchSpeed = 1.0f;
    [SerializeField] float jumpForce = 5.0f;

    [SerializeField] AudioClip[] footsteps;

    private InputHandler inputHandler;
    private Rigidbody rigidBody;
    private CapsuleCollider playerCollider;

    private float originalHeight;
    private float crouchHeight = 1.0f;

    private bool isCrouching = false;
    private bool isGrounded = false;

    private float nextFootstepPlayTime = 0.0f;
    private Vector3 startPos = Vector3.zero;

    public bool Freeze { get; set; }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous; // <– ADD THIS
        playerCollider = GetComponent<CapsuleCollider>();
        inputHandler = GameManager.Instance.InputHandler;
        startPos = transform.position;

        originalHeight = playerCollider.height;
    }

    private void Jump()
    {
        if (inputHandler.Jump)
        {
            Debug.Log($"[Jump] Space pressed. isGrounded = {isGrounded}, isCrouching = {isCrouching}");

            if (isGrounded && !isCrouching)
            {
                isGrounded = false; // prevent multi-jumps
                rigidBody.WakeUp(); // <– Force physics to be active
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                Debug.Log("[Jump] Jumping!");
            }
            else
            {
                Debug.Log("[Jump] Cannot jump.");
            }
        }
    }

    private bool crouchState = false;
    private void HandleCrouch()
    {
        if (crouchState == inputHandler.Crouch)
            return;
        if (inputHandler.Crouch)
        {
            Debug.Log("[Crouch] Crouch key down");
            playerCollider.height = crouchHeight;
            isCrouching = true;
        }
        else
        {
            Debug.Log("[Crouch] Crouch key up");
            playerCollider.height = originalHeight;
            isCrouching = false;
        }
        crouchState = inputHandler.Crouch;
    }

    private void Move()
    {
        if (inputHandler.Move.HasValue)
        {
            var move = inputHandler.Move.Value;

            if (move.magnitude > 1.0f)
                move.Normalize();

            if (move.magnitude < 0.01f)
                return;

            var moveVector = transform.rotation * new Vector3(move.x, 0, move.y);

            float currentSpeed = moveSpeed;
            if (inputHandler.Run && !isCrouching)
                currentSpeed = runSpeed;
            else if (isCrouching)
                currentSpeed = crouchSpeed;

            rigidBody.MovePosition(rigidBody.position + moveVector * Time.deltaTime * currentSpeed);
        }
    }

    void FixedUpdate()
    {
        if (Freeze)
        {
            if (!rigidBody.isKinematic)
                rigidBody.isKinematic = true;

            return;
        }

        if (isGrounded)
        {
            var speed = (rigidBody.position - startPos).magnitude / Time.deltaTime;

            if (speed > 1.0f && Time.time >= nextFootstepPlayTime)
            {
                AudioSource.PlayClipAtPoint(footsteps[Random.Range(0, footsteps.Length)], transform.position + Vector3.down, 0.4f);
                nextFootstepPlayTime = Time.time + 1.0f / speed;
            }

            startPos = rigidBody.position;
        }

        Move();
        Jump();
        HandleCrouch();
    }

    private List<GameObject> groundList = new();
    // ✅ Ground detection using collision
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundList.Add(collision.gameObject);
            isGrounded = true;
            Debug.Log("[Grounded] Entered ground collision");
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundList.Remove(collision.gameObject);
            if (groundList.Count == 0)
                isGrounded = false;
            Debug.Log("[Grounded] Left ground");
        }
    }

}
