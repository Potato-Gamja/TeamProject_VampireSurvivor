using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI text; //�ؽ�Ʈ
    public float floatSpeed = 1.5f; //�ؽ�Ʈ �̵��ӵ�
    public float lifetime = 2.5f; //�ؽ�Ʈ �����ð�
    float timer; //Ÿ�̸�

    private Vector3 moveDir = new Vector3(0, 1f, 0); //������ ���� -> ���� �̵�

    //����
    public void Setup(float damage)
    {
        //������ ���� ���ڿ��� �����Ͽ� �ؽ�Ʈ�� ����
        text.text = Mathf.RoundToInt(damage).ToString();
    }

    private void Update()
    {   
        //�����ð� �ʰ� ��
        if(timer >= lifetime)
            gameObject.SetActive(false); //������Ʈ ��Ȱ��ȭ

        timer += Time.deltaTime; //Ÿ�̸� ����
        transform.position += moveDir * floatSpeed * Time.deltaTime; //�ؽ�Ʈ�� ���� �̵�
        text.alpha = Mathf.Lerp(text.alpha, 0, Time.deltaTime * 10); //���İ��� �����Ͽ� ���� ������� ȿ��
    }

    private void OnDisable()
    {
        text.text = ""; //�ؽ�Ʈ�� �ʱ�ȭ
        text.alpha = 1; //���İ� �ʱ�ȭ
        timer = 0.0f; //Ÿ�̸� �ʱ�ȭ
    }
}
