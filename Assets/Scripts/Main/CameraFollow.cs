using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("���� ���")]
    public Transform target;
    [Header("�Ÿ� ���� ��� ����")]
    public bool useClamp = true;
    public Vector2 min;
    public Vector2 max;
    [Header("�����̵� ����")]
    public bool followY = true;
    [Header("������")]
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
            //�Ÿ�����
            nextPos.x = Mathf.Clamp(nextPos.x, min.x, max.x);
            nextPos.y = Mathf.Clamp(nextPos.y, min.y, max.y);
        }
        transform.position = nextPos;
    }
}
