﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] Transform camera;

    [SerializeField] float speed = 6f;
    //[SerializeField] float crouchSpeed = 4f;
    float currentSpeed;
    [SerializeField] float gravity = -9.81f;
    //[SerializeField] float jumpHeight = 3f;

    //[SerializeField] Transform groundCheck; 
    //[SerializeField] float groundDistance = 0.4f;
    //[SerializeField] LayerMask groundMask;

    //bool isGrounded;

    Vector3 velocity;

    float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    bool mouseVisible;

    float currentAnimationSpeed;
    [SerializeField] Animator animator;

    bool isDead = false;

    private void Start()
    {
        currentSpeed = speed;
        Cursor.lockState = CursorLockMode.Locked;
        mouseVisible = false;
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (mouseVisible == false)
            {
                Cursor.lockState = CursorLockMode.None;
                mouseVisible = true;
            }
            else if (mouseVisible == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                mouseVisible = false;
            }

        }
        
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (/*isGrounded &&*/ velocity.y < 0)
        {
            velocity.y = -5f;
        }
        if (!isDead)
        {
            Move();
        }
        //Jump();
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Move()
    {
        //if (isGrounded)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
                currentAnimationSpeed = .8f;
            }
            else
            {
                currentAnimationSpeed = 0;
            }
            //if (crouched)
            //{
            //    animator.Play("Crouch");
            //}
            //else if (!crouched)
            //{
            //    animator.Play("Walking");
            //}
            animator.SetFloat("Move", Mathf.Lerp(animator.GetFloat("Move"), currentAnimationSpeed, 5 * Time.deltaTime));
        }
    }

    //void Jump()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space)  && isGrounded)
    //    {
    //        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    //    }
    //}
    public void Die()
    {
        animator.Play("Knockout");
        isDead = true;
        StartCoroutine(ReloadSceneAfterX());
    }

    IEnumerator ReloadSceneAfterX()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
}
