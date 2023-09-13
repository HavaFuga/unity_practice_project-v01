using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class sparrow_movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody sparrow;
    public float speed;
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
            sparrow.AddForce(Vector3.up * 40);
        }

        sparrow.transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        // if (moveDirection != Vector3.zero)
        // {
        //     sparrow.transform.forward = moveDirection;
        //     Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        //     sparrow.transform.rotation =
        //         Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        // }
        
        float targetAngle = Mathf.Atan2(xDirection, zDirection) * Mathf.Rad2Deg + camera.eulerAngles.y;
        float angle =
            Mathf.SmoothDampAngle(sparrow.transform.eulerAngles.y, targetAngle, ref currentVelocity, smothness);
        if (moveDirection.x != 0 && moveDirection.z >= 0)
        {
            sparrow.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            sparrow.transform.forward = moveDirection;
        }
        
        if (moveDirection.z > 0)
        {
            sparrow.transform.position += new Vector3(0, 0, moveDirection.z + sparrow.rotation.y) * speed;
            sparrow.transform.position += sparrow.transform.TransformDirection();
        }
        
        // var currentPos = transform.position;
        // var targetPos = _cart.transform.position 
        //                 + _cart.transform.TransformDirection(_offset);
        // transform.position = Vector3.SmoothDamp(currentPos, targetPos, 
        //     ref _velocity, smoothTime);
        
        // walking movement
        Debug.Log("moveDirection.x: " + moveDirection.x);
        Debug.Log("moveDirection.z: " + moveDirection.z);
    }
    
}
