using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    #region Variables
    public float maxHealth = 5;
    public Transform target;

    private NavMeshAgent agent;
    private float health;
    #endregion
    #region Start
    void Start () 
    {
		health = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
	}
    #endregion
    #region TakeDamage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
