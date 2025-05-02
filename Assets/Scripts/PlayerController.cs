using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private Camera camera;

    protected override void Awake()
    {
        base.Awake();
        camera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    void OnMove(InputValue input)
    {
        moveDir = input.Get<Vector2>();
        moveDir = moveDir.normalized;
    }
    void OnLook(InputValue input)
    {
        Vector2 mousePos = input.Get<Vector2>();
        Vector2 worldPos = (Vector2)camera.ScreenToWorldPoint(mousePos);
        lookDir = (worldPos - (Vector2)transform.position).normalized;
    }

}
