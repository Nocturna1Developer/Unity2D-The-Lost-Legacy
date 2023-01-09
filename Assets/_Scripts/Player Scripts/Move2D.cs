using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MonoBehaviour
{
    //float movement = 0f;
    public float moveSpeed = 10f;
    public bool isGrounded = false;
    public Animator animator;
    
    void Update() 
    {
        //animator.SetFloat("Speed", Mathf.Abs(movement));
        
    }
    void FixedUpdate()
    {
        // This will check if our player is constantly moving and then checks if it is updating its position
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        //movement = Input.GetAxisRaw("Horizontal") * moveSpeed;
        transform.position += movement * Time.deltaTime * moveSpeed;
        Jump();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 20f), ForceMode2D.Impulse);
        }
       
    }
}
