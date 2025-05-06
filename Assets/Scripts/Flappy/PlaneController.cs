using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneController : MonoBehaviour
{
    public float speed = 3f;//비행기가 앞으로 이동하는 힘
    public float flapForce = 6f;
    public bool isDead = false;
    bool isFlap = false; //점프를 했는지 안했는지

    Animator animator;
    Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void OnPlaneMove()
    {
        isFlap = true;
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = rb.velocity;//가속도
        velocity.x = speed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        rb.velocity = velocity;//위에서는 실제로 값이 변한건 아니고 복사된거라 다시 넣어주는 작업

        float angle = Mathf.Clamp((rb.velocity.y * 10), -90, 90);//clamp는 값을 제한한다고 생각
        //y축을 -90도에서 90까지 만약 아래로 내려가고 있으면 고개도 아래로, 위로 올라가고 있으면 고개도 위로 

        transform.rotation = Quaternion.Euler(0, 0, angle);//transform.rotation은 쿼터니언이기때문에 이거 사용(x축, y축, z축)을 얼마만큼 회전시킬거냐

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        animator.SetInteger("IsDie", 1);
        isDead = true;
    }
}
