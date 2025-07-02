using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("�� ���Ÿ� ���� ����")]
    public float speed; //�̵� ũ��
    public float damage; //������
    public float lifeTime = 5f; //���ӽð�
    [SerializeField] private Player player; //�÷��̾� ��ũ��Ʈ
    Transform target;
    private Rigidbody2D rb; //������ٵ�
    string projectileType; //����ü�� Ÿ��

    Vector2 dir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.linearVelocity = dir * speed; //����� �ӵ� ���� �־� velocity ������� �̵�
    }

    //Ȱ��ȭ ��
    private void OnEnable()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
            target = player.transform.GetChild(0).GetComponent<Transform>();
        }

        if(player != null)
        {
            dir = (target.position - transform.position).normalized; //�߻� ����
        }

        Invoke(nameof(DisableObject), lifeTime); //5�� �� ����ü ��Ȱ��ȭ �Լ� ����
    }

    //��Ȱ��ȭ �Լ�
    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

    //��Ȱ��ȭ ��
    private void OnDisable()
    {
        rb.linearVelocity = Vector2.zero; //�̵� ũ�� 0
        CancelInvoke(); //�κ�ũ ���
    }

    //�浹 ��
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerProjectileCol")) //�÷��̾� �浹 ��
        {
            player.TakeDamage(damage); //�÷��̾��� ������ ���ط� ���� �����Ͽ� �ǰ� �Լ� ����
            gameObject.SetActive(false);
        }
    }
}
