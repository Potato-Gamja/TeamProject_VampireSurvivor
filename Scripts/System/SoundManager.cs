using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; //�̱����� ���� �ν��Ͻ�

    [Header("����� �ҽ�")]
    public AudioSource bgmSource; //������� ������ҽ�
    [SerializeField] private List<AudioSource> sfxSources; //ȿ���� ������ҽ� ����Ʈ
    private int currentSfxIndex = 0; //ȿ���� ������ҽ� ����Ʈ �ε��� ����

    [Header("���� ����� Ŭ��")]
    public AudioClip bookAttackSound; //å ���� ����
    public AudioClip watchAttackSound; //ȸ�߽ð� ���� ����
    public AudioClip teaAttackSound; //ȫ�� ���� ����
    public AudioClip catAttackSound; //ü��Ĺ ���� ����
    public AudioClip swordAttackSound; //���Ȱ� ���� ����
    public AudioClip hatAttackSound; //���� ���� ����
    public AudioClip cardAttackSound; //ī�� ���� ����
    public AudioClip appleAttackSound; //��� ���� ����
    public AudioClip defaultAttackSound; //�⺻ ���� ���� -> ������ ���� ������ ���� ���� �⺻��

    [Header("���� ����� Ŭ��")]
    public AudioClip fixedGuillotione; //������ ���ƾ ����
    public AudioClip movedGuillotione; //�̵��� ���ƾ ����
    [SerializeField] private AudioClip enemyHit; //�� �ǰ� ����
    [SerializeField] private AudioClip enemyDeath; //�� ��� ����
    [SerializeField] private AudioClip enemyExplosion; //�� ���� ����

    [Header("��Ÿ ����� Ŭ��")]
    [SerializeField] private AudioClip heal; //ȸ�� ȹ�� ����
    [SerializeField] private AudioClip expGem; //����ġ �� ȹ�� ����
    [SerializeField] private AudioClip levelUp; //������ ����
    [SerializeField] private AudioClip magnet; //�ڼ� ȹ�� ����

    float hitTimer = 0;   //���尡 ��ø�Ǿ� ���尡 �������ʰ� �ϱ� ���� Ÿ�̸�
    float expTimer = 0;   //��� ȿ���� �ߺ� ������ ���� Ÿ�̸�
    float deathTimer = 0; //...

    private void Awake()
    {
        //�̱��� ������ �����Ͽ� �ν��Ͻ��� �ߺ����� �ʵ��� �Ѵ�
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        //�� Ÿ�̸��� ������ �� ������ ����
        hitTimer += Time.deltaTime;
        expTimer += Time.deltaTime;
        deathTimer += Time.deltaTime;
    }


    //ȿ���� ���
    public void PlaySFX(string effect)
    {
        //ȿ������ ������ ����
        AudioSource source = sfxSources[currentSfxIndex];

        //switch ǥ����
        AudioClip clip = effect switch
        {
            "heal" => heal,
            "expGem" when expTimer > 0.08f => expGem,
            "levelUp" => levelUp,
            "enemyDeath" when deathTimer > 0.1f => enemyDeath,
            "explosion" => enemyExplosion,
            "magnet" => magnet,
            "fixedGuillotione" => fixedGuillotione,
            "movedGuillotione" => movedGuillotione,
            _ => null
        };

        if (clip != null)
        {
            if (effect == "expGem") expTimer = 0;
            if (effect == "enemyDeath") deathTimer = 0;

            source.pitch = Random.Range(0.9f, 1.1f);
            source.PlayOneShot(clip);
        }

        currentSfxIndex = (currentSfxIndex + 1) % sfxSources.Count; //�ε��� �� ����
    }

    //���� �ǰ� ����
    public void PlayHitSound(WeaponType weaponType)
    {
        //�������� ���� ������ ��� ���尡 �����Ƿ� ������ ����
        if (weaponType == WeaponType.Pipe || weaponType == WeaponType.Firecracker)
            return;

        //�ǰ� Ÿ�̸� ���ǹ�
        if (hitTimer > 0.1f)
        {
            AudioSource source = sfxSources[currentSfxIndex];

            hitTimer = 0; //�ǰ� Ÿ�̸� �ʱ�ȭ
            source.pitch = Random.Range(0.8f, 1.2f); //���� ��ġ��
            source.PlayOneShot(enemyHit); //���� ���

            currentSfxIndex = (currentSfxIndex + 1) % sfxSources.Count; //�ε��� �� ����
        }
    }

    //�÷��̾� ���� ����
    public void PlayWeaponSound(WeaponType weaponType)
    {
        AudioSource source = sfxSources[currentSfxIndex];

        //switch ǥ����
        AudioClip clip = weaponType switch
        {
            WeaponType.Book => bookAttackSound,
            WeaponType.Watch => watchAttackSound,
            WeaponType.Cat => catAttackSound,
            WeaponType.Sword => swordAttackSound,
            WeaponType.Tea=> teaAttackSound,
            WeaponType.Hat=> hatAttackSound,
            WeaponType.Card => cardAttackSound,
            WeaponType.Apple=> appleAttackSound,
            _ => defaultAttackSound
        };

        if (clip != null)
        {
            source.pitch = Random.Range(0.9f, 1.1f);
            source.PlayOneShot(clip);
            currentSfxIndex = (currentSfxIndex + 1) % sfxSources.Count; //�ε��� �� ����
        }

    }
}