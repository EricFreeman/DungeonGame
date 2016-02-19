using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Enemy
{
    public class PlayerSounds : MonoBehaviour, IListener<EnemyKilledMessage>
    {
        public List<AudioClip> EnemyKilled;

        public void Start()
        {
            this.Register<EnemyKilledMessage>();
        }
        
        void OnRegister()
        {
            this.UnRegister<EnemyKilledMessage>();
        }
        
        public void Handle(EnemyKilledMessage message) {   
            if (EnemyKilled.Count > 0) {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = EnemyKilled.Random();
                audioSource.PlayDelayed(0.5f);
            }
        }
    }
}