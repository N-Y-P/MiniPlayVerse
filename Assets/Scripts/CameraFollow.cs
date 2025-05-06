using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public Vector2 min;
    public Vector2 max;

    public Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 desirePos = target.transform.position + offset;
        desirePos.z = transform.position.z;

        //거리제한
        desirePos.x = Mathf.Clamp(desirePos.x, min.x, max.x);
        desirePos.y = Mathf.Clamp(desirePos.y, min.y, max.y);

        transform.position = desirePos;
    }
}
