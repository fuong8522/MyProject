using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public int timeDelay = 0;
    public float timeDelayCountDown = 0;
    bool check = true;
    private int countWave;
    private Coroutine spawnzombie;
    public Button buttonNextWave;


    public TextMeshProUGUI timeSpawn;
    void Start()
    {
        countWave = 0;
        spawnzombie = StartCoroutine(DelaySpawnZombie());
    }

    void Update()
    {

        SpawnWave();
        timeSpawn.text = "Time: " + (int)timeDelayCountDown;
        timeDelayCountDown -= Time.deltaTime;
    } 

    public void SpawnWave()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Enemy");
        int zombieCount = coins.Length;

        if (zombieCount == 0 && check && countWave <= 10)
        {
            buttonNextWave.gameObject.SetActive(true);
            spawnzombie = StartCoroutine(DelaySpawnZombie());

        }
    }

    public void NextWave()
    {
        if (spawnzombie != null)
        {
            StopCoroutine(spawnzombie);
            spawnzombie = null;
            SpawnZombie();
        }
    }
    public IEnumerator DelaySpawnZombie()
    {
        check = false;
        timeDelayCountDown = timeDelay;
        yield return new WaitForSeconds(timeDelay);
        SpawnZombie();
    }

    public void SpawnZombie()
    {
        buttonNextWave.gameObject.SetActive(false);
        check = true;
        countWave++;
        Vector3 spawnPos = new Vector3(Random.Range(-4, 4), 0, MovementPlayer.instance.transform.position.z + 23);
        int zombieIndex = Random.Range(0, zombiePrefabs.Length);
        Instantiate(zombiePrefabs[zombieIndex], spawnPos, zombiePrefabs[zombieIndex].transform.rotation);
    }
}
