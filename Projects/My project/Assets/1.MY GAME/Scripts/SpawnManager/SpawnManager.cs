using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance = null;

    public static SpawnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpawnManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject[] zombiePrefabs;
    private int timeDelay = 10;
    public float timeDelayCountDown = 0;
    public bool check = true;
    public int countWave;
    public Coroutine spawnzombie = null;
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

        if (zombieCount == 0 && check)
        {
            if (countWave < 3)
            {
                buttonNextWave.gameObject.SetActive(true);
                spawnzombie = StartCoroutine(DelaySpawnZombie());
            }
            else
            {
                Debug.Log("check next scene");
                check = false;
                //SceneManager.LoadScene("Day02");
            }
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
        int numberofwave = SceneManager.GetActiveScene().buildIndex;

            Instantiate(zombiePrefabs[zombieIndex], spawnPos, zombiePrefabs[zombieIndex].transform.rotation);
        

    }
}
