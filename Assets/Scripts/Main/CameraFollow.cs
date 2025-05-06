using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("따라갈 대상")]
    public Transform target;
    [Header("거리 제한 사용 여부")]
    public bool useClamp = true;
    public Vector2 min;
    public Vector2 max;
    [Header("상하이동 가능")]
    public bool followY = true;
    [Header("오프셋")]
    public Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 desirePos = target.position + offset;
        Vector3 nextPos = transform.position;

        if(followY )
        {
            nextPos.y = desirePos.y;
        }

        nextPos.x = desirePos.x;
        nextPos.z = transform.position.z;

        if(useClamp)
        {
            //거리제한
            nextPos.x = Mathf.Clamp(nextPos.x, min.x, max.x);
            nextPos.y = Mathf.Clamp(nextPos.y, min.y, max.y);
        }
        transform.position = nextPos;
    }
}
