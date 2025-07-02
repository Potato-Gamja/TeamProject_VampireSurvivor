using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVLoader
{
    //���� �̸��� �����͸� �ε�
    public static List<WaveData> LoadWaveData(string fileName)
    {
        Dictionary<float, WaveData> waveMap = new Dictionary<float, WaveData>();
        //�ð� �� ���̺� �����͸� ������ ��ųʸ� ����

        TextAsset csvFile = Resources.Load<TextAsset>(fileName); //Resources ���� �ȿ� �ִ� CSV �ε�
        StringReader reader = new StringReader(csvFile.text); //�ؽ�Ʈ �� ������ �б� ���� �ʱ�ȭ

        bool isFirstLine = true; //ù��° �� �ǳʶٱ� ���� ��

        //CSV ���� ������ �� �پ� �б�
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine(); //���� �� �б�

            //ù��° �� �ǳʶٱ�
            if (isFirstLine)
            {
                isFirstLine = false;
                continue;
            } 

            string[] values = line.Split(','); //�޸� �������� ���� ����

            float startTime = float.Parse(values[0]); //���̺� ���� �ð�
            string enemyType = values[1]; //���� Ÿ��
            int spawnCount = int.Parse(values[2]); //���� ���� ��
            float spawnInterval = float.Parse(values[3]); //���� ����

            //���� ���̺� ������ ����
            SubWaveData subWave = new SubWaveData
            {
                enemyType = enemyType, //�� Ÿ��
                spawnCount = spawnCount, //�� ���� ��
                spawnInterval = spawnInterval //���� ����
            };

            //�ش� startTime�� ���� ���̺갡 �̹� �ִٸ� �߰�, ������ ���� ����
            if (!waveMap.ContainsKey(startTime))
            {
                waveMap[startTime] = new WaveData 
                { startTime = startTime, 
                  subWaves = new List<SubWaveData>()
                };
                //���ο� ���̺� ������ ����
                
            }

            waveMap[startTime].subWaves.Add(subWave); //���̺꿡 ���� ���̺� �߰�
        }

        List<WaveData> waveList = new List<WaveData>(waveMap.Values); // Dictionary�� ����Ʈ�� ��ȯ
        waveList.Sort((a, b) => a.startTime.CompareTo(b.startTime)); //�ð� ������ ����

        return waveList; //���̺� ����Ʈ ��ȯ
    }
}