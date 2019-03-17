using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefense
{
    public class GameManager : MonoBehaviour
    {
        public static GameObject[] _enemies;

        private void Update()
        {
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
    }

}
