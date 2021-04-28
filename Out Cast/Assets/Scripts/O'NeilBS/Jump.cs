using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public bool canDoubleJump;
    public bool doubleJump;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            doubleJump = true;

        }



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (Input.GetButtonDown("Jump") && doubleJump == true && isGrounded == false && canDoubleJump == true)
        {
            Jump();
            doubleJump = false;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        void Jump()
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DoubleJumpEnable")
        {
            canDoubleJump = true;
            Debug.Log("DoubleJumpEnable");
        }
    }
}
