using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyMeleeAttack : MonoBehaviour
    {
        public float MinDistance = .9f;
        public int Damage = 5;

        private PlayerHealth _player;

        private void Start()
        {
            _player = GameObject.Find("Player").GetComponent<PlayerHealth>();
        }

        public void Attack()
        {
            if (Vector3.Distance(transform.position, _player.transform.position) < MinDistance)
            {
                _player.TakeDamage(Damage);
            }
        }
    }
}
