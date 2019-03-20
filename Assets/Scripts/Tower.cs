using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefense
{
    [AddComponentMenu("Tower Defense/Tower")]
    [RequireComponent(typeof(LineRenderer))]
    public class Tower : MonoBehaviour
    {
        [Header("Tower Attributes")]
        public float attackStrength;
        public float cooldownTime;
        private float timer;
        public float range;

        [Header("Unity Set Up")]
        public Transform firePoint;
        public LineRenderer line;
        private CapsuleCollider rangeCollider;

        [Header("Targets")]
        public Transform target = null;
        //public List<GameObject> enemies;
        private Enemy enemy;

        [Header("Tower Rotation")]
        public float radiansPerSecond;
        public Transform partToRotate;

        void Start()
        {
            InvokeRepeating("UpdateTarget", 0f, 0.25f);
            line = GetComponent<LineRenderer>();
            //rangeCollider = GetComponent<CapsuleCollider>();
            //rangeCollider.radius = range * 2;
        }


        void Update()
        {
            timer += Time.deltaTime;
            //UpdateTarget();
            MoveTower();
            ShootTarget();
            /*if (enemiesInRange.Count > 0)
            {
                foreach (GameObject slot in enemiesInRange)
                {
                    if (enemiesInRange[i] == null)
                    {
                        enemiesInRange.Remove(enemiesInRange[i]);
                    }
                    i++;
                }
            }
            */
        }

        /*private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemiesInRange.Add(other.gameObject);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemiesInRange.Remove(other.gameObject);
                if (other.gameObject == target)
                {
                    target = null;
                }
            }
        }
        */

        void UpdateTarget()
        {
            float shortestDistance = 0f;
            if (target == null)
            {
                shortestDistance = Mathf.Infinity;
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject closestEnemy = null;
                int i = 0;
                foreach (GameObject slot in enemies)
                {
                    float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
                    if (distance < shortestDistance)
                    {
                        closestEnemy = enemies[i];
                        shortestDistance = distance;
                        if (shortestDistance < range)
                        {
                            target = closestEnemy.transform;
                            enemy = target.GetComponent<Enemy>();
                            Debug.Log("Target acquired");
                        }
                        i++;
                    }
                }
            }
            if (shortestDistance > range)
            {
                target = null;
            }
        }

        void MoveTower()
        {
            //float step = radiansPerSecond * Time.deltaTime;
            if (target != null)
            {
                Vector3 targetDir = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(targetDir);
                Vector3 newRotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * radiansPerSecond).eulerAngles;
                partToRotate.rotation = Quaternion.Euler(0f, newRotation.y, 0f);
            }
        }


        void ShootTarget()
        {
            if (target != null && timer >= cooldownTime)
            {
                line.SetPosition(0, firePoint.position);
                line.SetPosition(1, target.transform.position);
                enemy.enemyHealth -= attackStrength;
                if (enemy.enemyHealth <= 0)
                {
                    enemy.Death();
                    target = null;
                }
                timer = 0;
                Debug.Log("FIRE!");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }


}