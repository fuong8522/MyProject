using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWeapon : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        rb.AddForce(MovementPlayer.instance.transform.forward * speed * Time.deltaTime,ForceMode.Impulse);
    }
    

}
