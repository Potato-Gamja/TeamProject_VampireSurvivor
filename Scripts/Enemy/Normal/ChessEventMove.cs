using System.Collections.Generic;
using UnityEngine;

public class ChessEventMove : MonoBehaviour
{
    public enum EventType //ü�� �� ����
    {
        Rook, Bishop //�̵��ϴ� �̺�Ʈ�� ��
    }

    [Header("�̺�Ʈ�� ü�� ����")]
    public EventType type; //ü�� �� ����
    public Vector2 moveDir; //ü�� ���� �̵��� ����
    public int dirNum; //�̵� ���� ��ȣ
    [SerializeField] float moveSpeed;
    private bool hasDir = false; //������ ������������ ���� ����

    [SerializeField] List<GameObject> chessEnemy = new();

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            chessEnemy.Add(transform.GetChild(i).gameObject);
        }

        gameObject.SetActive(false);
    }

    private void Update()
    {
        switch (type)
        {
            case EventType.Rook: //���� ��: ���� ������θ� �̵��ϴ� �̺�Ʈ�� ü����
                if (!hasDir) //���� ���� ���� ���� ��
                {
                    moveDir = RookMove(); //���� �̵� ���� �Լ� ��� ��������
                }
                transform.Translate(moveDir * moveSpeed * Time.deltaTime);

                break;

            case EventType.Bishop: //���� ���: ������ ������θ� �̵��ϴ� �̺�Ʈ�� ü����
                if (!hasDir)
                {
                    moveDir = BishopMove(); //����� �̵� ���� �Լ� ��� ��������
                }
                transform.Translate(moveDir * moveSpeed * Time.deltaTime);

                break;
        }
    }

    //���� �̵�����
    private Vector2 RookMove()
    {
        hasDir = true; //���� ���� ���� ��
        switch (dirNum)
        {
            case 0:
                return Vector2.up; //��
            case 1:
                return Vector2.down; //�Ʒ�
            case 2:
                return Vector2.left; //����
            default:
                return Vector2.right; //������
        }
    }

    //����� �̵�����
    private Vector2 BishopMove()
    {
        hasDir = true; //���� ���� ���� ��
        switch (dirNum)
        {
            case 0:
                return new Vector2(1, 1).normalized; //������ ��
            case 1:
                return new Vector2(-1, 1).normalized; //���� ��
            case 2:
                return new Vector2(1, -1).normalized; //������ �Ʒ�
            default:
                return new Vector2(-1, -1).normalized; //���� �Ʒ�
        }
    }

    //��Ȱ��ȭ ��
    private void OnDisable()
    {
        hasDir = false; //���� ���� ���� ����
        for (int i = 0; i < transform.childCount; i++)
        {
            chessEnemy[i].SetActive(true);
        }
    }
}
