using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    RectTransform rt;
    Camera mainCam;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        mainCam = Camera.main;
    }
    private void Update()
    {
        Vector3 screenPos = mainCam.WorldToScreenPoint(target.position + offset);
        rt.position = screenPos;
    }
}
