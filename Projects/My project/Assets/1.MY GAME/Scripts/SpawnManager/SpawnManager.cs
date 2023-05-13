using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public int startDelay = 0;
    public int spawnInterval = 0;
    public int timeDelay = 0;
    bool check = true;
    void Start()
    {
    }

    void Update()
    {

        GameObject[] coins = GameObject.FindGameObjectsWithTag("Enemy");
        int zombieCount = coins.Length;

        if (zombieCount == 0 && check)
        {
              StartCoroutine(DelaySpawnZombie());
        }

    }
    public IEnumerator DelaySpawnZombie()
    {
        check = false;
        yield return new WaitForSeconds(timeDelay);
        SpawnZombie();
        check = true;
    }

    public void SpawnZombie()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-4, 4), 0, MovementPlayer.instance.transform.position.z + 23);
        int zombieIndex = Random.Range(0, zombiePrefabs.Length);
        Instantiate(zombiePrefabs[zombieIndex], spawnPos, zombiePrefabs[zombieIndex].transform.rotation);
    }
}
