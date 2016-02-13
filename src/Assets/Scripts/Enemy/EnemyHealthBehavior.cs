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
            Destroy(GetComponent<BoxCollider>());

            _isDead = true;
            GetComponent<AnimationController>().PlayAnimation(GetComponent<EnemyAnimations>().Die, true);
        }
    }
}
