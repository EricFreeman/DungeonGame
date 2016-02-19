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

        void OnTriggerEnter(Collider col)
        {
            var bullet = col.GetComponent<Bullet>();

            if (bullet != null && ShouldCollideWith(bullet))
            {
                var hitContext = new HitContext {
                    Direction = bullet.transform.forward, 
                    Force = bullet.Speed, 
                    Damage = bullet.Damage, 
                    IsMelee = false
                };
                TakeDamage(hitContext);
            }
        }

        public void TakeDamage(HitContext context)
        {
            Health -= context.Damage;

            if (Health <= 0)
            {
                Health = 0;
                _damageBehavior.OnDeath(context);
            }
            else
            {
                _damageBehavior.OnHit(context);
            }
        }

        private bool ShouldCollideWith(Bullet bullet)
        {
            return ((bullet.IsFriendly && transform.tag != "Player") || (!bullet.IsFriendly && transform.tag == "Player"));
        }
    }
}