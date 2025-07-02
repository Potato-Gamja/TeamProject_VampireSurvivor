using UnityEngine;

public class FixedGuillotione : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f; //���ƾ�� �������� �ӵ�
    private float dropDelay = 2f; //�������� ������ �ɸ��� ������ �ð�
    private bool isPlay = false; //�÷��� ����
    private bool isDrop = false; //������ ����

    private Vector3 targetVec; //��ǥ ����
    [SerializeField] GameObject blade; //Į�� ������Ʈ
    [SerializeField] private Animator animator; //�ִϸ�����
    [SerializeField] private SpriteRenderer spriteRenderer; //��������Ʈ ������
    [SerializeField] private SpriteRenderer bladeSpriteRenderer; //���ƾ �� ��������Ʈ ������
    [SerializeField] private ParticleSystem dustParticle; //���� ��ƼŬ
    private Player player; //�÷��̾� ��ũ��Ʈ 
    private BoxCollider2D boxCol; //�ڽ� �ݶ��̴�

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boxCol = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Attack_Guillotione(); //���ƾ ���� �Լ� ����
    }

    private void Attack_Guillotione()
    {
        //������ ���� ���� �� ����
        if (!isDrop)
            return;

        //���ƾ ���� ��ġ�� ��ǥ ���� �������� �̵�
        blade.transform.localPosition = Vector3.MoveTowards(blade.transform.localPosition, targetVec, speed * Time.deltaTime);

        //��ǥ �������� �Ÿ��� �ش� �Ÿ� �̸� �� �ٴڿ� ������ ������ �Ǵ�
        if (Vector3.Distance(blade.transform.localPosition, targetVec) < 0.05f)
        {
            OnHit();
        }

        //���ƾ ���� ���İ��� 0�� ���� ��� ���� -> �ִϸ��̼��� ���� ���İ� ����.
        if (bladeSpriteRenderer.color.a == 0.0f)
        {
            gameObject.SetActive(false); //������Ʈ ��Ȱ��ȭ
        }
    }

    //���ƾ ���� �غ�
    private void Ready()
    {
        //��ǥ ���� ���� -> ���ƾ ���� ���������ǿ��� y���� ����
        targetVec = new Vector3(blade.transform.localPosition.x, blade.transform.localPosition.y - 1.05f, blade.transform.localPosition.z);

        if (animator != null)
        {
            animator.SetBool("isReady", true); //���� �ִϸ��̼� ����
        }

        Invoke(nameof(Drop), dropDelay); //���ƾ ���� �������� ��� �Լ� ����
    }

    //���ƾ �� ������
    private void Drop()
    {
        isDrop = true;
        animator.SetBool("isDrop", true);
    }

    private void SoundPlay()
    {
        SoundManager.Instance.PlaySFX("fixedGuillotione");
    }

    //�ٴڿ� ������
    private void OnHit()
    {
        dustParticle.Play();
        //��� ���� ��ƼŬ ����
    }

    public void Set()
    {
        animator.SetBool("isDrop", false); //�ִϸ����� �� ����
        animator.SetBool("isReady", false); //�ִϸ����� �� ����
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f); //������ �� �ʱ�ȭ
        bladeSpriteRenderer.color = new Color(bladeSpriteRenderer.color.r, bladeSpriteRenderer.color.g, bladeSpriteRenderer.color.b, 1f); //������ �� �ʱ�ȭ
        blade.transform.localPosition = new Vector3(0, 1.3f, 0);
        isDrop = false;
        isPlay = false;
    }

    //Ȱ��ȭ ��
    private void OnEnable()
    {
        Set();
        //���ƾ�� ���ƾ ���� ���̾� ����
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
        bladeSpriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    //��Ȱ��ȭ ��
    private void OnDisable()
    {
        Set();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //�÷��̾�� ����, ���ƾ�� ���� ���� ���� Ȯ��
        if (other.CompareTag("Player") && !isPlay)
        {
            isPlay = true; //���� ���� ��
            Ready(); //���ƾ ���� �غ�
        }
    }
}