using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.People
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
                _damageBehavior.OnHit(new HitContext { Direction = bullet.transform.forward, Force = bullet.Speed});
            }
        }
    }
}