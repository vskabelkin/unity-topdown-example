using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Player player;
    CapsuleCollider capsuleCollider;
    Animator animator;
    MovementAnimator movementAnimator;
    bool dead;
    ParticleSystem particleSystem;
    Counter counter;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();
        movementAnimator = GetComponent<MovementAnimator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        counter = FindObjectOfType<Counter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
            return;
        navMeshAgent.SetDestination(player.transform.position);
    }

    public void Kill()
    {
        if (!dead)
        {
            dead = true;
            counter.AddKill();
            Destroy(capsuleCollider);
            Destroy(movementAnimator);
            Destroy(navMeshAgent);
            particleSystem.Play();
            animator.SetTrigger("Died");
            Destroy(gameObject, 10.0f);
        }
    }
}


