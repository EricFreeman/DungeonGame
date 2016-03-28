using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public float MinSpawnDelay = .1f;
        public float MaxSpawnDelay = 5f;

        public List<GameObject> EnemyList; 

        private float _nextSpawnTime;

        void Start()
        {
            ResetSpawnTime();
        }

        void Update()
        {
            _nextSpawnTime -= Time.deltaTime;

            if (_nextSpawnTime <= 0)
            {
                SpawnEnemy();
                ResetSpawnTime();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, .1f);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * .2f);
        }

        private void SpawnEnemy()
        {
            var enemy = Instantiate(EnemyList.Random());
            enemy.transform.position = transform.position;
            enemy.transform.rotation = transform.rotation;
        }

        private void ResetSpawnTime()
        {
            _nextSpawnTime = Random.Range(MinSpawnDelay, MaxSpawnDelay);
        }
    }
}