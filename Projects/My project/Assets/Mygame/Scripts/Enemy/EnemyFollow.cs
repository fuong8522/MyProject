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



    void Start()
    {
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            health--;
            if (health == 0)
            {
                OnDeadth();
                StartCoroutine(DelayDisActiveZombie());
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            MovementPlayer.Instance.baseBall.SetActive(false);
        }
    }

    //Delay zombie disappear
    IEnumerator DelayDisActiveZombie()
    {
        yield return new WaitForSeconds(15f);
        gameObject.SetActive(false);
    }

    public void OnDeadth()
    {
        animator.SetTrigger("Death");
        deadth = true;
        agent.speed = 0;
        capsuleCollider.isTrigger = true;
    }
    public void NavMove()
    {
        agent.SetDestination(player.position);
    }
}
