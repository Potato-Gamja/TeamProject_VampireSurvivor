using System.Collections.Generic;

[System.Serializable]
public class SubWaveData
{
    public string enemyType; //���� Ÿ��
    public int spawnCount; //���� ��
    public float spawnInterval; //�� ���� ����
}

[System.Serializable]
public class WaveData
{
    public float startTime; //���̺� ���� �ð�
    public List<SubWaveData> subWaves; //���� �� ����Ʈ
}