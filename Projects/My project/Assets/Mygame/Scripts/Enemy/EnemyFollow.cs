using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public Transform player;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }
    float z1 = 0;

    void Update()
    {
        agent.SetDestination(player.position);
        float z2 = transform.position.z;
        if(z2 > z1 || z2 < z1)
        {
            animator.SetBool("Walk", true);
            z1 = z2;
        }
        else
        {
            animator.SetBool("Walk", false);
        }
            
    }
}
