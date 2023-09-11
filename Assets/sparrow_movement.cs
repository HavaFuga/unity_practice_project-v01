using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class sparrow_movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody sparrow;
    public float speed;
    public Animator animator;
    protected bool isJumping = false;
    protected float xDirection;
    protected float yDirection;
    protected float zDirection;
    
    void Start()
    {
        Debug.Log("Hello World! ^^");
        Debug.Log(sparrow.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        xDirection = Input.GetAxis("Horizontal");
        zDirection = Input.GetAxis("Vertical");
        yDirection = Input.GetAxis("Jump");
        
        
        
        animateObject(animator);
        moveObject(sparrow);
    }

    protected bool checkIsJumping()
    {
        return (sparrow.position.y > 1.0) ? true : false;
    }

    void animateObject(Animator a)
    {
        
        // animate jumping
        if (yDirection != 0)
        {
            a.SetBool("isJumping", true);
        }
        else if (yDirection == 0 && !checkIsJumping())
        {
            a.SetBool("isJumping", false);
        }
        
        // animate walking
        if (xDirection != 0 || zDirection != 0)
        {
            a.SetBool("isWalking", true);
        }
        else
        {
            a.SetBool("isWalking", false);
        }
    }
    
    void moveObject(Rigidbody rb)
    {
        // wasd movement
        Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);
        rb.position += moveDirection * speed/100;
        rb.transform.SetPositionAndRotation(rb.position, new Quaternion(0.0f, xDirection*2, 0.0f, 5.0f));

        // jumping movement
        if (Input.GetKeyDown(KeyCode.Space) && checkIsJumping() == false)
        {
            sparrow.AddForce(Vector3.up * 400);
        }
    }
    
}
