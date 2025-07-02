using System.Collections;
using UnityEngine;

public class MovedGuillotione : MonoBehaviour
{
    [SerializeField] private float speed = 15.0f; //���ƾ�� ���󰡴� �ӵ�
    private float dir; //�̵����� - �¿�
    private float attackTime = 0.0f; //���� ����ð�
    private bool isMoving = false; //���ƾ�� �̵� ����
    private bool isAttack = false; //���ƾ�� ���� ����

    [SerializeField] private Vector3 guillotioneVec; //���ƾ�� ��ġ
    [SerializeField] GameObject warn; //��� ������Ʈ
    [SerializeField] GameObject guillotione; //���ƾ ������Ʈ
    [SerializeField] private Animator warnAnimator; //��� �ִϸ�����
    [SerializeField] private Animator guillotioneAnimator; //���ƾ �ִϸ�����
    [SerializeField] private SpriteRenderer guillotioneSpriteRenderer; //���ƾ ��������Ʈ ������
    [SerializeField] private SpriteRenderer bladeSpriteRenderer; //���ƾ �� ��������Ʈ ������
    private Player player; //�÷��̾�
    private BoxCollider2D boxCol; //�ڽ� �ݶ��̴�


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //�������� �� ��
        if (isMoving)
            MoveGuillotine();

        Attack_Guillotione();
    }

    //���ƾ ���� Ȱ��ȭ
    private void Attack_Guillotione()
    {
        //���ݿ��� ��, ������ ���� -> �ѹ��� ����ǰ� �ϱ� ����
        if (isAttack && !isMoving)
        {
            boxCol.enabled = true; //�ڽ� �ݶ��̴� Ȱ��ȭ
            isMoving = true; //������ ��
            attackTime = 0f; //���� ����ð� �ʱ�ȭ
        }

        // ���ƾ ���� ���İ��� 0�� ���� �� -> �ִϸ��̼��� ���� ���İ��� 0�� ��.
        if (bladeSpriteRenderer.color.a == 0.0f)
        {
            gameObject.SetActive(false); //���ƾ �׷� ��Ȱ��ȭ
            guillotione.SetActive(false); //���ƾ ��Ȱ��ȭ
            guillotioneAnimator.SetBool("isReady", false); //���ƾ �ִϸ����� �� ����

            isMoving = false; //������ ����
        }
    }

    //���ƾ �̵�
    private void MoveGuillotine()
    {
        attackTime += Time.deltaTime;

        //�ӵ� ���: 0~1�ʱ��� ���� ��������, ���� Ȯ ������
        float t = attackTime / 2.0f;
        float speedMultiplier = -Mathf.Pow(t - 1, 2) + 1; //������ ���·� ��ȭ (�ִ밪 1)
        float currentSpeed = speed * speedMultiplier;

        Vector3 moveDirection = (dir > 0.5f) ? Vector3.left : Vector3.right; //�̵� ���� ����
        transform.position += moveDirection * currentSpeed * Time.deltaTime; //�̵�


        //�̵� ���� ����
        if (attackTime > 1.0f)
        {
            boxCol.enabled = false; //�ڽ� �ݶ��̴� ��Ȱ��ȭ
            isMoving = false; //������ ���� ����
            isAttack = false; //���� ���� ����
            guillotioneAnimator.SetBool("isAttack", false); //���ƾ �ִϸ��̼� �� ����
        }
    }

    //���ƾ ����
    private void SetGuillotione()
    {
        transform.position = player.transform.position; //�÷��̾� ��ġ��
        guillotione.transform.localPosition = guillotioneVec; //���ƾ�� ��ġ�� ���� ���������� �ʱ�ȭ
        guillotioneSpriteRenderer.color = new Color(guillotioneSpriteRenderer.color.r, guillotioneSpriteRenderer.color.g, guillotioneSpriteRenderer.color.b, 1f); //������ �� �ʱ�ȭ
        bladeSpriteRenderer.color = new Color(bladeSpriteRenderer.color.r, bladeSpriteRenderer.color.g, bladeSpriteRenderer.color.b, 1f); //������ �� �ʱ�ȭ


        //���ƾ�� ���ƾ ���� ������ ���̾� ����
        guillotioneSpriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100); 
        bladeSpriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);

        warn.SetActive(true); //��� Ȱ��ȭ

        StartCoroutine(nameof(Attack)); //���� �ڷ�ƾ ����
    }

    //���� �ڷ�ƾ
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1.0f); //1�� ������
        warn.gameObject.SetActive(false); //��� ��Ȱ��ȭ
        isAttack = true; //���� ���� ��
        guillotione.gameObject.SetActive(true); //���ƾ ������Ʈ Ȱ��ȭ
        guillotioneAnimator.SetBool("isReady", true); //���ƾ �ִϸ����� �� ����
    }

    //������Ʈ Ȱ��ȭ ��
    private void OnEnable()
    {
        dir = Random.value; //���� ����
        transform.position = player.transform.position; //�÷��̾��� ��ġ�� �̵�

        //������ ���� �����Ͽ� ���ݹ����� ����
        if (dir > 0.5f)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1); //���� �������� ���� 
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1); //���� �������� ����
        }

        SetGuillotione(); //���ƾ ���� �Լ� ����
    }
}
