using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Mummy : MonoBehaviour
{
    public static event Action<Mummy> Died;

    [SerializeField] float attackRange = 1f;
    [SerializeField] int health = 2;

    int currentHealth;

    NavMeshAgent navMeshAgent;
    Animator animator;

    bool alive => currentHealth > 0;
    
    void Awake() 
    {
        currentHealth = health;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision other) 
    {
        var BlasterShot = other.collider.GetComponent<BlasterShot>();
        if(BlasterShot != null)
        {
            currentHealth--;
            if(currentHealth <= 0)
                Die();
            else
                TakeHit();
        }
    }

    void TakeHit()
    {
        navMeshAgent.enabled = false;
        animator.SetTrigger("Hit");
    }

    void Die()
    {
        GetComponent<Collider>().enabled = false;
        navMeshAgent.enabled = false;
        animator.SetTrigger("Die");
        Died?.Invoke(this);
        Destroy(gameObject, 5);
    }

    void Update()
    {
        if(!alive) { return; }

        var player = FindObjectOfType<PlayerMovement>();
        if(navMeshAgent.enabled)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }

        if(Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Atack");
        navMeshAgent.enabled = false;
    }

    void AtackComplete()
    {
        if(!alive)
            navMeshAgent.enabled = true;
    }

    void AtackHit()
    {
        Debug.Log("Hitted");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void HitComplete()
    {
        if(alive)
            navMeshAgent.enabled = true;
    }

}
