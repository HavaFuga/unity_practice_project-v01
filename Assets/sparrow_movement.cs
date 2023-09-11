using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class sparrow_movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody sparrow;
    public float speed = 0.1f;
    protected bool isJumping = false;
    
    void Start()
    {
        Debug.Log("Hello World! ^^");
        Debug.Log(sparrow.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        moveObject(sparrow);
    }

    bool checkIsJumping()
    {
        if (sparrow.position.y > 1.0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    void moveObject(Rigidbody rb)
    {
        // wasd movement
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");
        float yDirection = 0.0f;

        Vector3 moveDirection = new Vector3(xDirection, yDirection, zDirection);
        rb.position += moveDirection * speed;

        // jumping movement
        if (Input.GetKeyDown(KeyCode.Space) && checkIsJumping() == false)
        {
            sparrow.AddForce(Vector3.up * 400);
        }
    }
    
}
