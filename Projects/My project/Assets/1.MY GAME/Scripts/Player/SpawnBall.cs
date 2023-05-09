using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public Transform posBall;
    public GameObject spawnBall;
    public GameObject ballObject;

    void SpawBall()
    {
        if (ballObject.gameObject.active)
        {
            Instantiate(spawnBall, posBall.position, Quaternion.identity);
        }

    }

}
