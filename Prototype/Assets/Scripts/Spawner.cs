using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum SpawnerState {Spawning, Waiting, Counting};
   
    //vaiables to set for a wave of enemies
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    //sorts waves into an array
    public Wave[] waves;
    private int nextWave = 0;

    //array of positions to hold as spawnpoints
    public Transform[] spawnPoints;
    public float timeBetween;
    public float waveCountdown;

    private float searchCountdown = 1f;
    private SpawnerState state = SpawnerState.Counting;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.Log("No spawn points created");
        }
        waveCountdown = timeBetween;
    }

    // Update is called once per frame
    void Update()
    {
        //wait for player to kill the current wave
        if (state == SpawnerState.Waiting)
        {
            if (EnemiesDead())
            {
                //start new round 
                WaveFinished();
            }
            else
            {
                //enemies still remain 
                return;
            }
        }
        //if countdown is zero new wave should spawn.
        if (waveCountdown <= 0)
        { 
            if (state!= SpawnerState.Spawning)
            {
                //start spawning enemies
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }

    }

    //spawns next wave of enemies
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave " + _wave.name);
        state = SpawnerState.Spawning;
        for (int i = 0; i < _wave.count; i++)
        {
            //spawns enemies
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        //once all enemies spawn state changes to waiting 
        state = SpawnerState.Waiting;
        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {        
        Debug.Log("Spawned enemy");
        
        //access spawnpoints in the array at random and will spawn enemy at that position
        Transform sp = spawnPoints[ Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, sp.position, sp.rotation);
    }

    //check to see if enemies have been killed and if wave counter can be reset
    bool EnemiesDead()
    {
        //search countdown is how often the game will check if enemies are dead
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return true;
            }            
        }
        return false;
    }

   void WaveFinished()
    {
        Debug.Log("Wave done");
        state = SpawnerState.Counting;

        waveCountdown = timeBetween;
        
        //if next wave is at the end of the array
        if(nextWave +1 > waves.Length - 1)
        {
            nextWave = 0;// try remove this and see if it stops waves from looping
            Debug.Log("All waves complete");
            //add endstate
        }
        else
        {
            //increments nextWave to the next wave in the array
            nextWave++;
        }
        
    }


}
