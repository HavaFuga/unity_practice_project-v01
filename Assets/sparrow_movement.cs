using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sparrow_movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody sparrow;
    void Start()
    {
        Debug.Log("Hello World! ^^");
        Debug.Log(sparrow.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
