using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyFollow : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public Transform player;
    private float health = 6f;
    private float zFirst = 0;

    public Button punch;
    private float testZom = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        OnAnimationZombie();
    }

    public void OnAnimationZombie()
    {

        agent.SetDestination(player.position);
        float zLast = transform.position.z;
        if (zLast > zFirst || zLast < zFirst)
        {
            animator.SetBool("Walk", true);
            zFirst = zLast;
            testZom += Time.deltaTime;
        }
        else
        {
            animator.SetBool("Walk", false);
            //animator.SetTrigger("Collision");
        }
        if((animator.GetBool("Walk") == false) && testZom > 1)
        {
            transform.forward = player.transform.position - transform.position;
            animator.SetTrigger("Collision");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Weapon"))
        {
            health--;
            if(health == 0)
            {
                animator.SetTrigger("Death");
                agent.speed= 0;
                testZom = 0;
            }
        }
    }

}
