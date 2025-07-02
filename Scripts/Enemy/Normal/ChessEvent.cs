using System.Collections.Generic;
using UnityEngine;

public class ChessEvent : MonoBehaviour
{
    [SerializeField] private GameObject player; //�÷��̾� ������Ʈ

    [SerializeField] private List<GameObject> rooks = new(); //�� ������Ʈ ����Ʈ
    [SerializeField] private List<GameObject> bishops = new(); //��� ������Ʈ ����Ʈ

    [SerializeField] private List<Vector3> rooks_Vec = new(); //�� ��ġ ��ǥ ����Ʈ
    [SerializeField] private List<Vector3> bishops_Vec = new(); //��� ��ġ ��ǥ ����Ʈ

    [SerializeField] private List<GameObject> rooks_Warn = new(); //�� ��� ������Ʈ ����Ʈ
    [SerializeField] private List<GameObject> bishops_Warn = new(); //��� ��� ������Ʈ ����Ʈ

    private List<List<GameObject>> pieces; //2�� ����Ʈ
    private List<List<GameObject>> warnings; //2�� ����Ʈ
    private List<List<Vector3>> positions; //2�� ����Ʈ

    private int type; //Ÿ��: ��, ���
    private int num; //���� ��ȣ: 0~5
    private float lifeTime = 10f; //���ӽð�
    private float timer = 0f; //Ÿ�̸�
    private bool isSpawn = false; //���� ���� ����

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //�� 2�� ����Ʈ�� ��, ����� ����
        pieces = new List<List<GameObject>> { rooks, bishops };
        warnings = new List<List<GameObject>> { rooks_Warn, bishops_Warn };
        positions = new List<List<Vector3>> { rooks_Vec, bishops_Vec };
    }

    private void Update()
    {
        if (isSpawn)
        {
            timer += Time.deltaTime; //Ÿ�̸� ����
            //���ӽð� �̻� �� ü�� ��Ȱ��ȭ
            if (timer >= lifeTime)
                ChessDisable();
        }
    }

    //���� ����
    public void SetPattern()
    {
        isSpawn = true; //���� ���� ���� ��
        timer = 0f; //Ÿ�̸� �ʱ�ȭ

        type = Random.Range(0, 2); //0: ��, 1: ���
        num = Random.Range(0, 6);  //0~5 ����

        SetWarning(true); //�ش� ������ ��� ������Ʈ Ȱ��ȭ
        Invoke(nameof(SetEvent), 1f); //���� ���� Ȱ��ȭ
    }

    //��� ���� ��ȯ
    private void SetWarning(bool enable)
    {
        foreach (int i in GetIndices(num))
            warnings[type][i].SetActive(enable); //��� ������Ʈ�� ���� ��ȯ
    }

    //�̺�Ʈ ����
    private void SetEvent()
    {
        SetWarning(false); //��� ��Ȱ��ȭ
        SetPos(); //��ġ ����
        ActivatePattern(); //���� Ȱ��ȭ
    }

    //��ġ ����
    private void SetPos()
    {
        transform.position = player.transform.position; //�÷��̾��� ��ġ�� �̵�

        //2�� ����Ʈ�� �ش� Ÿ���� ������ ������������ ����
        for (int i = 0; i < pieces[type].Count; i++)
            pieces[type][i].transform.localPosition = positions[type][i];
    }

    //���� Ȱ��ȭ
    private void ActivatePattern()
    {
        foreach (int i in GetIndices(num))
            pieces[type][i].SetActive(true);
    }

    //ü�� ��Ȱ��ȭ
    private void ChessDisable()
    {
        isSpawn = false;

        foreach (var obj in rooks)
            obj.SetActive(false);

        foreach (var obj in bishops) 
            obj.SetActive(false);
    }

    //���� ó��
    private int[] GetIndices(int pattern)
    {
        return pattern switch
        {
            0 => new[] { 0, 1 }, //0��, 1�� ������Ʈ Ȱ��ȭ
            1 => new[] { 2, 3 }, //2��. 3�� ������Ʈ Ȱ��ȭ
            2 => new[] { 0, 3 }, 
            3 => new[] { 0, 2 },
            4 => new[] { 1, 3 },
            5 => new[] { 1, 2 },
            _ => new int[0],
        };
    }
}
