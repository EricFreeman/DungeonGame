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

        public void OnDeath()
        {
            if (_isDead) return;

            Destroy(GetComponent<EnemyMovement>());
            Destroy(GetComponent<NavMeshAgent>());

            _isDead = true;
            GetComponent<AnimationController>().PlayAnimation(GetComponent<EnemyAnimations>().Die, true);

            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddExplosionForce(7.5f, transform.position + transform.forward, 1f, 1f, ForceMode.Impulse);
        }
    }
}
