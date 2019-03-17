using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [AddComponentMenu("Tower Defense/Spawner")]
    public class Spawn : MonoBehaviour
    {
        public int enemyAmount;
        public GameObject enemy;
        public float timer;
        public float spawnTime;
        private int i;

        void Start()
        {
        }
        
        void Update()
        {
            timer += Time.deltaTime;
            if (i < enemyAmount && timer >= spawnTime)
            {
                Instantiate(enemy, transform.position, Quaternion.identity);
                i++;
                timer = 0;
            }
        }
    }

}

