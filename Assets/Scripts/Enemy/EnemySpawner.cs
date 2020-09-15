using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    bool[] spawnPointAvailable;
    [SerializeField] GameObject[] enemyTypes;
    Vector3 smallEnemySpawn = new Vector3(0, 6, 0);

    [System.NonSerialized] public static UnityEvent WaveOver;

    //There are 3 types of waves, small going left, small going right and big wave.
    //When a wave is completed, bits are shifted left and the end activated
    const byte waveDone = 0b_0000_0111;
    byte waveProgress = waveDone;

    [SerializeField, Range(0f, 5f)] float timeBetweenSpawns = 1f;
    [SerializeField, Range(0f, 60f)] float timeBetweenWaves = 10f;

    private void Awake()
    {
        WaveOver = new UnityEvent();

        spawnPointAvailable = new bool[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPointAvailable[i] = true;
        }
    }

    private void Start()
    {
        Enemy.OnDeath.AddListener(ClearSpawnPoint);
        StartCoroutine(WaveSpawner());
    }

    IEnumerator WaveSpawner()
    {
        int wave = 0;

        yield return new WaitForSeconds(1);

        while(true)
        {
            wave++;
            waveProgress = (byte)0;

            int smallSpawns = wave*2;
            int bigSpawns = wave/5;

            SmallWaveSpawns(smallSpawns);
            StartCoroutine(BigEnemySpawnWave(bigSpawns));

            yield return new WaitUntil(() => waveProgress == waveDone);
            yield return new WaitUntil(() => GetOpenSpawnPoints().Count >= spawnPoints.Length);
            WaveOver.Invoke();
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    IEnumerator BigEnemySpawnWave(int queued)
    {
        for (int i = 0; i < queued; i++)
        {
            //if no spawn points available wait until there is one
            yield return new WaitUntil(() => GetOpenSpawnPoints().Count > 0);

            List<int> availableSpawnPointsIndexes = GetOpenSpawnPoints();
            int spawnPointToSpawnIn = availableSpawnPointsIndexes[Random.Range(0, availableSpawnPointsIndexes.Count)];

            GameObject enemy = Instantiate(enemyTypes[1], spawnPoints[spawnPointToSpawnIn].position, Quaternion.identity);
            enemy.GetComponent<Enemy>().spawnPointOccuption = spawnPointToSpawnIn;
            spawnPointAvailable[spawnPointToSpawnIn] = false;
        }

        SignalCompletionOfWave();
    }

    //Gives a list of all the indexes which correspond to an open spot in spawnPointsAvailability
    public List<int> GetOpenSpawnPoints()
    {
        List<int> openSpawnPoints = new List<int>();
        for (int i = 0; i < spawnPointAvailable.Length; i++)
        {
            if(spawnPointAvailable[i])
            {
                openSpawnPoints.Add(i);
            }
        }
        return openSpawnPoints;
    }

    public void ClearSpawnPoint(Enemy enemy)
    {
        if(enemy.spawnPointOccuption > -1)
        spawnPointAvailable[enemy.spawnPointOccuption] = true;
    }

    public void SmallWaveSpawns(int amount)
    {
        int spawnType = Random.Range(0, 3);
        switch (spawnType)
        {
            default:
            case 0: 
                SignalCompletionOfWave();
                StartCoroutine(SmallEnemySpawnWave(amount, true));
                break;
            case 1:
                SignalCompletionOfWave();
                StartCoroutine(SmallEnemySpawnWave(amount, false));
                break;
            case 2:
                StartCoroutine(SmallEnemySpawnWave(amount, true));
                StartCoroutine(SmallEnemySpawnWave(amount, false));
                break;
        }
    }

    IEnumerator SmallEnemySpawnWave(int amount, bool goLeft)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject Enemy = Instantiate(enemyTypes[0], smallEnemySpawn, Quaternion.identity);
            Enemy.GetComponent<SmallEnemy>().SetSwayDirection(goLeft);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        SignalCompletionOfWave();
    }

    public void SignalCompletionOfWave()
    {
        waveProgress <<= 1;
        waveProgress |= 1;

        /*
         * 0b_0000_0000 turns into 0b_0000_0001,
         * 0b_0000_0001 turns into 0b_0000_0011
         * etc.
         */
    }
}
