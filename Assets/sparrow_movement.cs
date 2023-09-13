using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class sparrow_movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody sparrow;
    public float speed;
    public float jumpForce = 0.5f;
    public float rotationSpeed;
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
        
        xDirection = moveDirection.normalized.x;
        zDirection = moveDirection.normalized.z;
        yDirection = moveDirection.normalized.y;
        
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
    
    // private Vector3 _offset = new Vector3(0, 2, -3.5f);
    void moveObject()
    {

        // jumping movement
        if (Keyboard.current.spaceKey.isPressed && sparrow.position.y > 1.0 == false)
        {
            sparrow.AddForce(Vector3.up * jumpForce);
        }
        
        // walking movement
        transform.Translate(Vector3.forward * Time.deltaTime * speed * zDirection);
        transform.Translate(Vector3.right * Time.deltaTime * speed * xDirection);
        

        // rotation movement
        // .atan2() returns angle, so rad2deg needed to convert into deg
        var targetAngle = Mathf.Atan2(xDirection, zDirection) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smothness);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
        
    }
    
}
