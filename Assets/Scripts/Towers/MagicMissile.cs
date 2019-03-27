using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MagicMissile : Tower {

    public Transform firePoint;
    public float lineDelay = .2f; // How long the line appears before disappearing
    public LineRenderer line;


	// Use this for initialization
	void Reset ()
    {
        line = GetComponent<LineRenderer>();
	}

    // IEnumerator is a COROUTINE
    // ShowLine below will render a line for duration of 'delay'
    IEnumerator ShowLine(float delay)
    {
        // Do this
        line.enabled = true;
        // Wait for 'delay' seconds
        yield return new WaitForSeconds(delay);
        // Then do this
        line.enabled = false;
    }

    public override void Aim(Enemy e)
    {
        // Get the orb to look at the enemy
        // orb.LookAt(e.transform);
        // Create line from orb to enemy
        line.SetPosition(0, firePoint.position);
        line.SetPosition(1, e.transform.position);
    }

    // Deal damage to enemy and show the line
    public override void Attack(Enemy e)
    {
        // Show the line
        StartCoroutine(ShowLine(lineDelay));
        // Deal the damage
        e.TakeDamage(damage);
    }
}
