using UnityEngine;
using UnityEngine.InputSystem;


public class sparrow_movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody sparrow;
    public float speed;
    public Animator animator;
    public InputAction playerControls;
    protected bool isJumping = false;
    protected float xDirection;
    protected float yDirection;
    protected float zDirection;
    protected Vector3 moveDirection;
    
    void Start()
    {
        Debug.Log("Hello World! ^^");
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = playerControls.ReadValue<Vector3>();
        
        xDirection = moveDirection.x;
        zDirection = moveDirection.z;
        yDirection = moveDirection.y;
        
        animateObject(animator);
        moveObject();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void animateObject(Animator a)
    {
        // animate jumping
        if (sparrow.position.y >= 1)
        {
            a.SetBool("isJumping", true);
        }
        else if (sparrow.position.y < 1)
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
    
    void moveObject()
    {
        // wasd movement
        sparrow.velocity = new Vector3(xDirection * speed, sparrow.velocity.y, zDirection * speed);
        sparrow.transform.SetPositionAndRotation(sparrow.position, new Quaternion(0.0f, xDirection*2, 0.0f, 5.0f));
        
        // jumping movement
        if (Keyboard.current.spaceKey.wasPressedThisFrame && sparrow.position.y > 1.0 == false)
        {
            sparrow.AddForce(Vector3.up * 400);
        }
    }
    
}
