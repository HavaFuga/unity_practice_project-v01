using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Rigidbody sparrow;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -3.5f);
    private Vector3 _velocity;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private float _rotationSpeed = 1f;

    private void Start() {
    }

    private void FixedUpdate() {
        var currentPos = transform.position;
        var targetPos = sparrow.transform.position 
                        + sparrow.transform.TransformDirection(_offset);
        transform.position = Vector3.SmoothDamp(currentPos, targetPos, 
            ref _velocity, smoothTime);
        
        // transform.LookAt(_cart.transform);
        var targetRot = Quaternion.LookRotation(
            sparrow.transform.position - transform.position, Vector3.up);
        var currRot = transform.rotation;
        
        transform.rotation = 
            Quaternion.Slerp(currRot, targetRot, Time.deltaTime * _rotationSpeed);

    }
}
