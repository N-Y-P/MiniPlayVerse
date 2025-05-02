using UnityEngine;

public class BaseController : MonoBehaviour
{
    #region [����, �Ҵ� ����]
    protected Rigidbody2D rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;

    protected Vector2 moveDir = Vector2.zero; //�̵��ϴ� ����
    public Vector2 MovementDirection { get { return moveDir; } }

    protected Vector2 lookDir = Vector2.zero; //�ٶ󺸴� ����
    public Vector2 LookDirection { get { return lookDir; } }

    protected AnimationHandler animationHandler;
    #endregion
    protected virtual void Awake()//��ӹ޾� ����ҰŶ� virtual ���� ������
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

    //������
    private void Movment(Vector2 direction)
    {
        direction = direction * 5f;
        rigidbody.velocity = direction;
        animationHandler.Move(direction);

    }

    //ȸ��
    private void Rotate(Vector2 direction)
    {
        //��ũź��Ʈ *����Ȯ��!
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//y�� x������� ��Ÿ��
        bool isLeft = Mathf.Abs(rotZ) > 90f;//90������ ũ�ٸ� ������ �ٶ��

        characterRenderer.flipX = isLeft;
    }

}
