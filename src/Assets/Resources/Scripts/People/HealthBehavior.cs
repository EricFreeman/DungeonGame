using Assets.Resources.Scripts.Gore;
using Assets.Resources.Scripts.Weapons;
using UnityEngine;

namespace Assets.Resources.Scripts.People
{
    public class HealthBehavior : MonoBehaviour
    {
        public int Health = 5;
        private IDamageBehavior _damageBehavior;

        void Start()
        {
            _damageBehavior = GetComponent<IDamageBehavior>();
        }

        void Update()
        {
            if (Health <= 0)
            {
                _damageBehavior.OnDeath();
            }
        }

        void OnTriggerEnter(Collider col)
        {
            var bullet = col.GetComponent<Bullet>();

            if (bullet != null)
            {
                Health -= bullet.Damage;
                _damageBehavior.OnHit();
            }
        }
    }
}
