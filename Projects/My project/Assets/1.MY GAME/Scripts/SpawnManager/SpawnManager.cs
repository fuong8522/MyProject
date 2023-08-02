using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
    public Button buttonContinute;
    public GameObject rewardUI = null;
    public bool countCoint = false;
    public int x = 0;
    public TextMeshProUGUI timeSpawn;
    public TextMeshProUGUI coin;
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
        if(countCoint && x < 51)
        {
            coin.text = "$" + x.ToString();
            x += 1;
        } 
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
                check = false;
                StartCoroutine(DelayRewardUi());
                buttonContinute.onClick.AddListener(NextScene);
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

    public IEnumerator DelayRewardUi()
    {
        yield return new WaitForSeconds(1);
        rewardUI.SetActive(true);
        countCoint = true;
    }

    public void NextScene()
    {
        //rewardUI.gameObject.SetActive(false);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(nextSceneIndex + 1, LoadSceneMode.Single);
        MovementPlayer.instance.UpdateCoin(x - 1);
        MovementPlayer.instance.UpdateUiCoin();
        x = 0;
        countCoint = false;
    }

    public void SpawnZombie()
    {
        buttonNextWave.gameObject.SetActive(false);
        check = true;
        countWave++;
        int numberofwave = SceneManager.GetActiveScene().buildIndex;
        for (int i = 0; i <= numberofwave; i++)
        {
        Vector3 spawnPos = new Vector3(Random.Range(-5, 5), 0, MovementPlayer.instance.transform.position.z + Random.RandomRange(19, 30));
        int zombieIndex = Random.Range(0, zombiePrefabs.Length);
            Instantiate(zombiePrefabs[zombieIndex], spawnPos, zombiePrefabs[zombieIndex].transform.rotation);
        }
    }
}
