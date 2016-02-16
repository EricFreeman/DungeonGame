using Assets.Scripts.Gore;
using Assets.Scripts.People;
using Assets.Scripts.Utils;
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

        public void OnDeath(HitContext hitContext)
        {
            if (_isDead) return;

            Destroy(GetComponent<EnemyMovement>());
            Destroy(GetComponent<NavMeshAgent>());

            _isDead = true;
            GetComponent<Animator>().SetTrigger("Die");

            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddExplosionForce(hitContext.Force, transform.position - hitContext.Direction, 1f, 1f, ForceMode.Impulse);
        }
    }
}
