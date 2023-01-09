using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public GameObject gameObject;

    //public PlayerMovement controller;
    //public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    public Animator animator;

    void Update()
    {
        // Gets the movemnet from the player (A and D keys)
        horizontalMove = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // If this button is pressed then jump is true
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }
        else{
            animator.SetBool("isJumping", false);  
        }
    }

    // Updates only when a certain action is presed
    void FixedUpdate()
    {
        // Takes in a float at how much you want to move 
        // Time.delta time is the ifxed time since function was last called
        //controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        // makes jump equal false so you are not continuoulsy jumping
        
        jump = false;
    }
}