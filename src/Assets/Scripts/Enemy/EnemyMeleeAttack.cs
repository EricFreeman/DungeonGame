using Assets.Scripts.People;
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
                GetComponent<EnemySounds>().PlayAttackSound();            
                var hitContext = new HitContext
                {
                    Force = 3,
                    Damage = Damage,
                    Direction = transform.forward
                };
                _player.GetComponent<HealthBehavior>().TakeDamage(hitContext);
            }
        }
    }
}
