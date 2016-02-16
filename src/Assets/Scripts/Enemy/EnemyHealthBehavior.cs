using System.Collections.Generic;
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

        public List<AudioClip> Hit;
        public List<AudioClip> Die;

        public void OnHit(HitContext hitContext)
        {
            if (_isDead) return;

            AudioSource.PlayClipAtPoint(Hit.Random(), transform.position);
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
            gameObject.layer = LayerMask.NameToLayer("TurnStaticSoon");
            AudioSource.PlayClipAtPoint(Die.Random(), transform.position);

            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddExplosionForce(hitContext.Force, transform.position - hitContext.Direction, 1f, 1f, ForceMode.Impulse);
        }
    }
}