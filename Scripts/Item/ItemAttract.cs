using UnityEngine;

public class ItemAttract : MonoBehaviour
{
    [Header("����ġ ����")]
    [SerializeField] protected float magnetDistance = 0.5f;    //�ڼ� ��� �Ÿ�
    [SerializeField] protected float baseSpeed = 5f;         //�⺻ �ӵ�
    [SerializeField] protected float maxSpeed = 15f;         //�ִ� �ӵ�
    [SerializeField] protected float accelerationRate = 2f;  //�ʴ� �ӵ� ������
    [SerializeField] protected float force = 3f;

    protected Player player; //�÷��̾� ��ũ��Ʈ
    protected Transform target; //�÷��̾� �߽�
    [SerializeField] protected SpriteRenderer spriteRenderer; //��������Ʈ ������
    [SerializeField] protected Rigidbody2D rb; //������ٵ�2D

    protected bool isAttracting; //�⺻ �ڼ� ��� Ȱ��ȭ ����
    protected float currentSpeed; //���� �ӵ�
    protected float attractTimer = 0f; //�ڼ� Ÿ�̸�

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        target = player.gameObject.transform.GetChild(0).transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed;
    }

    protected virtual void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position); //�����۰� Ÿ���� �Ÿ�

        // �÷��̾���� �Ÿ��� �ڼ� �Ÿ����� ������
        if (distanceToPlayer <= magnetDistance)
        {
            isAttracting = true; //�⺻ �ڼ� ��� Ȱ��ȭ
        }

        //�⺻ �ڼ� ��� Ȱ��ȭ ��
        if (isAttracting)
        {
            attractTimer += Time.deltaTime; //�ڼ� Ÿ�̸� �ð� ����
            currentSpeed = Mathf.Min(baseSpeed + (accelerationRate * attractTimer), maxSpeed); //�ð��� ���� ���� �ӵ� ����

            transform.position = Vector2.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime); //�÷��̾� �������� �̵�
            
        }
    }

    //����ġ �� ƨ��� ȿ��
    protected virtual void Launch()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized; //���� ������� ������ ���� ����ȭ
        force = Random.Range(0.5f, 1.5f); //������ �� ũ��
        rb.AddForce(randomDir * force, ForceMode2D.Impulse); //�ش� �������� ���� ����
        Invoke(nameof(AddForceZero), 0.1f); //AddForceȿ���� 0.1�ʸ� ����
    }

    protected virtual void AddForceZero()
    {
        rb.linearVelocity = Vector2.zero; //���� ����
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100); //������ ���̾� ����
    }

    //����ġ �� Ȱ��ȭ ��
    protected virtual void OnEnable()
    {
        Launch(); //����ġ �� ƨ��� �Լ� ����
    }

    //��Ȱ��ȭ ��
    protected virtual void OnDisable()
    {
        isAttracting = false; //�⺻ �ڼ� ��� ���� ��Ȱ��ȭ
        currentSpeed = baseSpeed; //�ӵ� �ʱ�ȭ
        attractTimer = 0; //Ÿ�̸� �ʱ�ȭ
        rb.linearVelocity = Vector2.zero; //���� ����
    }

}