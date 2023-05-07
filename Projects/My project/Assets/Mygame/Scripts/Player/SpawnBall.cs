using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public Transform posBall;
    public GameObject spawnBall;

    void SpawBall()
    {
            Instantiate(spawnBall, posBall.position,Quaternion.identity);

    }

}
