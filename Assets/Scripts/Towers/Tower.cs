using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int damage = 10;
    public float attackRate = 1f;
    public float attackRange = 2f;

    protected Enemy currentEnemy;

    private float attackTimer = 0f;

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    
    // Aims at a given enemy
    public virtual void Aim(Enemy e)
    {
        print("I am aiming at '" + e.name + "'");
    }

    // Attacks a given enemy only when 'attacking'
    public virtual void Attack(Enemy e)
    {
        print("I am attacking '" + e.name + "'");
    }

    public virtual void DetectEnemy ()
    {
        // Reset current enemy
        currentEnemy = null;
        // Get hit colliders from OverlapShere
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);
        // Loop through all hit colliders
        foreach (var hit in hits)
        {
            // If we hit an enemy
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy)
            {
                // Set current enemy to that enemy
                currentEnemy = enemy;
            }
        }
    }

	// Update is called once per frame
	protected virtual void Update ()
    {
        attackTimer += Time.deltaTime;
        DetectEnemy();
        if (currentEnemy != null)
        {
            Aim(currentEnemy);
            if (attackTimer >= attackRate)
            {
                Attack(currentEnemy);
                attackTimer = 0f;
            }
        }
	}
}
