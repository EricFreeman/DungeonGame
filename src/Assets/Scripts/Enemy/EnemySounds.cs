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
            AudioSource.PlayClipAtPoint(Attack.Random(), transform.position);
        }
        
        private void PlayFootstepSound()
        {
            AudioSource.PlayClipAtPoint(Footsteps.Random(), transform.position);
        }

        public void PlayHitSound()
        {
            AudioSource.PlayClipAtPoint(Hit.Random(), transform.position);
        }

        public void PlayDeathSound()
        {
            AudioSource.PlayClipAtPoint(Die.Random(), transform.position);
        }
    }
}