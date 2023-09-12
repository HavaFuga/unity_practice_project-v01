using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class sparrow_movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody sparrow;
    public float speed;
    public Animator animator;
    public InputAction playerControls;
    public Transform camera;
    public float smothness;
    protected bool isJumping = false;
    protected float xDirection;
    protected float yDirection;
    protected float zDirection;
    protected Vector3 moveDirection;
    protected float currentVelocity;
    protected Vector2 direction;
    
    void Start()
    {
        Debug.Log("Hello World! ^^");
    }
    
    // Update is called once per frame
    void FixedUpdate()
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
        
        // jumping movement
        if (Keyboard.current.spaceKey.isPressed && sparrow.position.y > 1.0 == false)
        {
            sparrow.AddForce(Vector3.up * 40);
        }
        
        // walking movement
        transform.position += Time.fixedDeltaTime * new Vector3(moveDirection.x, 0, moveDirection.z) * speed;
    }
    
}
