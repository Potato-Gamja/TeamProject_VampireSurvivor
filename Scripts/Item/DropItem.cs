using UnityEngine;

[System.Serializable]
public struct DropData //��������� ������
{
    public GameObject item; //������
    public float dropChance; //��� Ȯ��
}

public class DropItem : MonoBehaviour
{
    public enum Rate //������ ���
    {
        Normal, Named
    }
    public Rate rate;
    public DropData[] normalDrops; //�븻 ������ ���������
    public DropData[] namedDrops; //���ӵ� ������ ���������

    //��Ȱ��ȭ ��
    private void OnDisable()
    {
        //���� �ε� ���°� �ƴ� ��� ����
        if (!gameObject.scene.isLoaded) 
            return;

        DropItems(); //������ ���
    }

    private void DropItems()
    {
        //��� �����Ͱ� ���ӵ��� ��� ���ӵ� ���, �ƴ� ��� �븻 ���
        DropData[] currentDrops = rate == Rate.Named ? namedDrops : normalDrops;

        float ran = Random.value; //���� ��
        float sum = 0f; //�հ�

        foreach (var drop in currentDrops) //��������� �ݺ���
        {
            sum += drop.dropChance; //������ �� ��� Ȯ�� �հ�
            if (ran <= sum)
            {
                Instantiate(drop.item, transform.position, Quaternion.identity); //�ش� ��� ������ ����
                return;
            }
        }
    }
}
