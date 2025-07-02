using UnityEngine;

public class HealItem : ItemAttract
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player.Heal(10.0f); //�÷��̾� ��ũ��Ʈ�� ȸ�� �Լ� ȣ��
            SoundManager.Instance.PlaySFX("heal");
            gameObject.SetActive(false); //������Ʈ ��Ȱ��ȭ
        }
    }
}
