using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class CameraFollowScript : MonoBehaviour
{
    public static CameraFollowScript instance;
    public   Vector3 offset = Vector3.zero;
    public Vector3 rotateOffset;
    public float smoothTime = 0.5f;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;
    public Transform target;

    Vector3 centerPoint = Vector3.zero;
    Vector3 rotatePoint;
    Vector3 velocity = Vector3.zero;
    Camera cam;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        cam = GetComponent<Camera>();
    
    }
    public void SetCamera(Transform t)
    {
        target = t;
        offset = this.transform.position - target.position;
        rotateOffset = transform.eulerAngles - target.eulerAngles;

    }
    private void LateUpdate()
    {
      
       Move();
    }
    private void Move()
    {
        centerPoint = target.position + offset;
       // centerPoint.x = transform.position.x;
        transform.position = Vector3.SmoothDamp(transform.position, centerPoint, ref velocity, smoothTime);

        rotatePoint = target.eulerAngles + rotateOffset;
        rotatePoint.x = transform.eulerAngles.x;
        rotatePoint.z = 0; //transform.eulerAngles.z;
      //  rotatePoint.y = transform.eulerAngles.y;
     //   transform.eulerAngles = rotatePoint;//    Vector3.SmoothDamp(transform.eulerAngles, rotatePoint, ref velocity, smoothTime);

    }



}
