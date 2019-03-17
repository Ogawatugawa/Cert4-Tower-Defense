using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace TowerDefense
{
    [AddComponentMenu("Tower Defense/Enemy/Enemy")]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Stats")]
        public float enemyHealth = 100f;
        [Header("Enemy Movement")]
        public Transform target;
        public GameObject waypointParent;
        public float moveSpeed;
        public float stoppingDistance;
        private Transform[] waypoints;
        private int index = 1;
        // private NavMeshAgent agent;
        // Use this for initialization
        void Start()
        {
            waypointParent = GameObject.FindGameObjectWithTag("Waypoint Parent");
            waypoints = waypointParent.GetComponentsInChildren<Transform>();
            //agent = GetComponent <NavMeshAgent>();
            //target = GameObject.FindGameObjectWithTag("Endzone").transform;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            //agent.SetDestination(target.position);
        }

        public void Move ()
        {
            if (index < waypoints.Length)
            {
                Transform point = waypoints[index];
                float distance = Vector3.Distance(transform.position, point.position);
                if (distance <= stoppingDistance)
                {
                    index++;
                }
                transform.position = Vector3.MoveTowards(transform.position, point.position, moveSpeed * Time.deltaTime);
            }
        }

        public void Death()
        {
            if (enemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
