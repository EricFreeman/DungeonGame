using Assets.Scripts.Gore;
using Assets.Scripts.People;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyHealthBehavior : MonoBehaviour, IDamageBehavior
    {
        private bool _isDead;
        public Sprite DeadBody;

        public void OnHit(HitContext hitContext)
        {
            if (_isDead) return;

            var ejector = gameObject.GetComponent<BloodEjector>();
            if (ejector != null)
            {
                ejector.Eject(hitContext);
            }
        }

        public void OnDeath()
        {
            Destroy(GetComponent<EnemyMovement>());
            Destroy(GetComponent<NavMeshAgent>());

            _isDead = true;
            GetComponentInChildren<SpriteRenderer>().sprite = DeadBody;
        }
    }
}
