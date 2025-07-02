using UnityEngine;

public class ChessEnemy : Enemy
{
    public enum ChessType //ü�� �� ����
    {
        Pawn, Rook, Bishop, Knight, //�Ϲ� ��
        Rook_Event_Move, Bishop_Event_Move, //�̵��ϴ� �̺�Ʈ�� ��
        Rook_Event_NoMove //������ �̺�Ʈ�� ��
    } 

    [Header("�̺�Ʈ�� ü�� ����")]
    public ChessType type; //ü�� �� ����
    private bool isSpawn = false; //���� ����
    [SerializeField] Vector3 vec; //ü������ ��ġ

    //Rook_Event_NoMove ���� ����
    [SerializeField] float lifeTime = 30.0f; //�̺�Ʈ Ȱ��ȭ �ð�
    private float timer = 0; //Ȱ��ȭ �ð� Ÿ�̸�

    private void Awake()
    {
        vec = transform.localPosition; //ü���� ��ġ ����
    }

    private new void Update()
    {
        switch (type)
        {
            case ChessType.Pawn: //�� ��: �÷��̾� �߰��ϴ� �Ϲ��� ü����
                base.Update(); //����� Enemy�� Update �Լ� ȣ��
                break;

            case ChessType.Rook: //�� ��: �÷��̾� �߰��ϴ� �Ϲ��� ü����
                base.Update(); //����� Enemy�� Update �Լ� ȣ��
                break;

            case ChessType.Bishop: //�� ���: �÷��̾� �߰��ϴ� �Ϲ��� ü����
                base.Update(); //����� Enemy�� Update �Լ� ȣ��
                break;

            case ChessType.Knight: //�� ����Ʈ: �÷��̾� �߰��ϴ� �Ϲ��� ü����
                base.Update(); //����� Enemy�� Update �Լ� ȣ��
                break;

            case ChessType.Rook_Event_Move: //���� ��: ���� ������θ� �̵��ϴ� �̺�Ʈ�� ü����
                if (transform.parent.gameObject.activeSelf == true && !isSpawn)
                {
                    isSpawn = true;
                    UpdateSpriteFlip(); //����� Enemy�� UpdateSpriteFlip �Լ� ȣ��
                }

                UpdateSpriteLayer(); //����� Enemy�� UpdateSpriteLayer �Լ� ȣ��
                break;

            case ChessType.Bishop_Event_Move: //���� ���: ������ ������θ� �̵��ϴ� �̺�Ʈ�� ü����
                if (transform.parent.gameObject.activeSelf == true && !isSpawn)
                {
                    isSpawn = true;
                    UpdateSpriteFlip(); //����� Enemy�� UpdateSpriteFlip �Լ� ȣ��
                }

                UpdateSpriteLayer(); //����� Enemy�� UpdateSpriteLayer �Լ� ȣ��
                break;

            case ChessType.Rook_Event_NoMove: //���� ��: �̵��� ���� �ʰ�, Ÿ�������� �����Ǿ� �÷��̾ ���δ� ü����
                base.Update();
                timer += Time.deltaTime; //Ÿ�̸� �� ����
                if (timer >= lifeTime) //Ȱ��ȭ �ð� �̻� �޼�
                {
                    gameObject.SetActive(false); //���ӿ�����Ʈ ��Ȱ��ȭ
                }
                break;
        }
    }

    //Ȱ��ȭ ��
    protected override void OnEnable()
    {
        base.OnEnable(); //����� Enemy�� OnEnable �Լ� ȣ��
        rb.linearVelocity = Vector3.zero; //���� ����
        transform.localPosition = vec; //������������ �����ص� ������ ����
    }

    private void OnDisable()
    {
        isSpawn= false; //���� ���� ����
    }

}
