using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject WinMenu;
    public enum Spawnstate { spawning,waiting,counting}
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    public Wave[] waves;
    int nextWave = 0;
    public int NextWave
    {
        get { return nextWave+1; }
    }
    float searchCountdown = 1;
    public float timeBetweenWaves;
    public float waveCountdown;
    public float WaveCountdown
    {
        get { return waveCountdown; }
    }
    public Transform[] spawnPoints;
    Spawnstate state = Spawnstate.counting;
    public Spawnstate State
    {
        get { return state; }
    }
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0) 
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }
    private void Update()
    {
        
        if (state==Spawnstate.waiting)
        {
            if (!EnemyIsAlive())
            {
                print("wave completed");
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if (waveCountdown<=0)
        {
            if (state!=Spawnstate.spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }
    IEnumerator SpawnWave(Wave _wave)
    {
        print("spawning wave " + _wave.name);
        state = Spawnstate.spawning;
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1 / _wave.rate);
        }
        state = Spawnstate.waiting;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, spawnPoint.position, spawnPoint.rotation);
        print("spawning......  " + _enemy.name);
    }
    void WaveCompleted()
    {
        waveCountdown = timeBetweenWaves;
        state = Spawnstate.counting;
        if (nextWave+1>waves.Length-1)
        {
            WinMenu.SetActive(true);
            enabled = false;
            return;
        }
        nextWave++;
    }
}