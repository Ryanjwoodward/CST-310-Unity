using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float drag;

    [Header("Keybinds")]
    public KeyCode up = KeyCode.Space;
    public KeyCode down = KeyCode.LeftShift;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();

        SpeedControl();

        rb.drag = drag;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(up))
        {
            rb.AddForce(transform.up * moveSpeed, ForceMode.Force);
        }

        if (Input.GetKey(down))
        {
            rb.AddForce(transform.up * -1 * moveSpeed, ForceMode.Force);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVe1 = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

        if(flatVe1.magnitude > moveSpeed)
        {
            Vector3 limitedVe1 = flatVe1.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVe1.x, limitedVe1.y, limitedVe1.z);
        }
    }
}
