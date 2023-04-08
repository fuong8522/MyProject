using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private CapsuleCollider capsuleCollider;
    private Animator animator;
    private bool deadth;
    private float health = 6f;
    private float lastPositionZ;

    public static bool attacked;


    void Start()
    {
        attacked = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        lastPositionZ = transform.position.z;
        animator = GetComponent<Animator>();
        deadth = false;
    }

    void Update()
    {
        OnAnimationZombieWalk();
        NavMove();
        OnAnimationAttack();
        if (MovementPlayer.instance.checkPunch == false)
        {
            attacked = false;
        }
        OnDeadth();
    }

    public void OnAnimationZombieWalk()
    {
        if (transform.position.z != lastPositionZ)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        lastPositionZ = transform.position.z;
    }
    public void OnAnimationAttack()
    {
        //Zombie attack player
        if ((animator.GetBool("Walk") == false) && !deadth)
        {
            transform.forward = player.transform.position - transform.position;
            animator.SetTrigger("Collision");
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Weapon") && MovementPlayer.instance.checkPunch == true && attacked == false)
        {
            if(deadth== false)
            {
                animator.SetTrigger("IsHitted");
            }
            health--;
            attacked = true;
        }

    }



    //Delay zombie disappear
    IEnumerator DelayDisActiveZombie()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

    public void OnDeadth()
    {
        if(health == 0)
        {
            animator.SetTrigger("Death");
            deadth = true;
            capsuleCollider.isTrigger = true;
            DelayDisActiveZombie();
        }
    }
    public void NavMove()
    {
        if(deadth== false)
        {
            agent.SetDestination(player.position);
        }
    }
}
