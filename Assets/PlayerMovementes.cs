using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementes : MonoBehaviour
{
    public float jumpForce = 5f;
    float playerHeight = 2f;

    [Header("Movement")]
    public float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    public float movementMultiplier = 10f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header ("Drag")]
    float groundDrag = 6f;
    float airDrag = 2f;

    float horMove;
    float vertMove;

    bool isGrounded;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);
        MyInput();
        ControlDrag();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            jump();
        }
    }
    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }

    }
    void MyInput()
    {
        horMove = Input.GetAxisRaw("Horizontal");
        vertMove = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * vertMove + transform.right * horMove;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    void jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
