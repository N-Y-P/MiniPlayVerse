using UnityEngine;

public class BaseController : MonoBehaviour
{
    #region [변수, 할당 모음]
    protected Rigidbody2D rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;

    protected Vector2 moveDir = Vector2.zero; //이동하는 방향
    public Vector2 MovementDirection { get { return moveDir; } }

    protected Vector2 lookDir = Vector2.zero; //바라보는 방향
    public Vector2 LookDirection { get { return lookDir; } }

    protected AnimationHandler animationHandler;
    #endregion
    protected virtual void Awake()//상속받아 사용할거라서 virtual 많이 쓸예정
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        Rotate(lookDir);
    }

    protected virtual void FixedUpdate()
    {
        Movment(moveDir);
    }

    //움직임
    private void Movment(Vector2 direction)
    {
        direction = direction * 5f;
        rigidbody.velocity = direction;
        animationHandler.Move(direction);

    }

    //회전
    private void Rotate(Vector2 direction)
    {
        //아크탄젠트 *순서확인!
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//y와 x축사이의 세타값
        bool isLeft = Mathf.Abs(rotZ) > 90f;//90도보다 크다면 왼쪽을 바라봐

        characterRenderer.flipX = isLeft;
    }

}
