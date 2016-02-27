using Assets.Scripts.Gore;
using Assets.Scripts.People;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Enemy
{
    public class EnemyHealthBehavior : MonoBehaviour, IDamageBehavior
    {
        private bool _isDead;
        public Sprite DeadBody;

        private Vector3 _pushback;

        void Update()
        {
            _pushback *= .9f;
            if (_pushback.magnitude < .01f)
            {
                _pushback = Vector3.zero;
            }

            transform.position += _pushback;
        }

        public void OnHit(HitContext hitContext)
        {
            if (_isDead) return;

            _pushback = hitContext.Direction / 20.0f;

            GetComponent<Animator>().SetTrigger("IsHit");
            GetComponent<EnemySounds>().PlayHitSound();

            if (!hitContext.IsMelee) {
                var ejector = gameObject.GetComponent<BloodEjector>();
                if (ejector != null)
                {
                    ejector.Eject(hitContext);
                }
            }
        }

        public void OnDeath(HitContext hitContext)
        {
            if (_isDead) return;

            if (!hitContext.IsMelee) {
                var ejector = gameObject.GetComponent<BloodEjector>();
                if (ejector != null)
                {
                    ejector.Eject(hitContext);
                    ejector.Eject(hitContext);
                    ejector.Eject(hitContext);
                }
            }

            Destroy(GetComponent<EnemyMovement>());
            Destroy(GetComponent<NavMeshAgent>());

            _isDead = true;
            GetComponent<Animator>().SetBool("IsDead", true);
            GetComponent<Animator>().SetBool("IsAttacking", false);
            gameObject.layer = LayerMask.NameToLayer("TurnStaticSoon");
            GetComponent<EnemySounds>().PlayDeathSound();

            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddExplosionForce(hitContext.Force, transform.position - hitContext.Direction, 1f, 1f, ForceMode.Impulse);
            
            EventAggregator.SendMessage(new EnemyKilledMessage());
        }
    }
}