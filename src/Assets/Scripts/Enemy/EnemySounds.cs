using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySounds : MonoBehaviour
    {
        public List<AudioClip> Footsteps;

        public List<AudioClip> Attack;
        public List<AudioClip> Hit;
        public List<AudioClip> Die;

        public void PlayAttackSound()
        {
            if (Attack.Count > 0) {
                AudioSource.PlayClipAtPoint(Attack.Random(), transform.position);
            }
        }
        
        private void PlayFootstepSound()
        {
            if (Footsteps.Count > 0) {
                AudioSource.PlayClipAtPoint(Footsteps.Random(), transform.position);
            }
        }

        public void PlayHitSound()
        {
            if (Hit.Count > 0) {
                AudioSource.PlayClipAtPoint(Hit.Random(), transform.position);
            }
        }

        public void PlayDeathSound()
        {
            if (Die.Count > 0) {
                AudioSource.PlayClipAtPoint(Die.Random(), transform.position);
            }
        }
    }
}