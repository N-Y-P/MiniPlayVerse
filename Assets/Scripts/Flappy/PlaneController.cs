using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneController : MonoBehaviour
{
    public float speed = 3f;//����Ⱑ ������ �̵��ϴ� ��
    public float flapForce = 6f;
    public bool isDead = false;
    bool isFlap = false; //������ �ߴ��� ���ߴ���

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

        Vector3 velocity = rb.velocity;//���ӵ�
        velocity.x = speed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        rb.velocity = velocity;//�������� ������ ���� ���Ѱ� �ƴϰ� ����ȰŶ� �ٽ� �־��ִ� �۾�

        float angle = Mathf.Clamp((rb.velocity.y * 10), -90, 90);//clamp�� ���� �����Ѵٰ� ����
        //y���� -90������ 90���� ���� �Ʒ��� �������� ������ ���� �Ʒ���, ���� �ö󰡰� ������ ���� ���� 

        transform.rotation = Quaternion.Euler(0, 0, angle);//transform.rotation�� ���ʹϾ��̱⶧���� �̰� ���(x��, y��, z��)�� �󸶸�ŭ ȸ����ų�ų�

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        animator.SetInteger("IsDie", 1);
        isDead = true;
    }
}
