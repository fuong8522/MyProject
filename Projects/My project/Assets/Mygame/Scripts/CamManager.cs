using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CamManager : MonoBehaviour
{
    //The object the camera will follow
    public GameObject player;
    public Transform cameraPivot;

    //Camera control area for looking around
    public TouchFieldController touchpadField;


    //Smooth camera
    private Transform targetTransform;
    public float cameraFollowSpeed = 0.2f;
    private Vector3 cameraFollowVelocity = Vector3.zero;

    //Camera looking up and down, left and right
    private float lookAngle;
    private float pivotAngle;

    //Camera rotation limit
    private float minimum = -35f;
    private float maximum = 35f;
    public float lookUpDown = 0.5f;
    public float lookLeftRight = 1f;

    private void Awake()
    {
        targetTransform = player.transform;
    }


    private void LateUpdate()
    {
        FollowTarget();
        RorateCamera();
    }

    //Camera follow player
    public void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position,ref cameraFollowVelocity,cameraFollowSpeed);
        transform.position = targetPosition;
    }

    //Rotate Camera
    public void RorateCamera()
    {
        lookAngle += touchpadField.direction.x * lookLeftRight * Time.deltaTime;
        pivotAngle -= touchpadField.direction.y * lookUpDown * Time.deltaTime;
        pivotAngle = Mathf.Clamp(pivotAngle, minimum,maximum);

        
        //Look left and right
        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        //Look up and down
        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }
}