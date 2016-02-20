using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class MessageZone : MonoBehaviour
    {
        public AudioClip Clip;

        private bool _hasPlayed;

        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Player" && !_hasPlayed)
            {
                _hasPlayed = true;
                AudioSource.PlayClipAtPoint(Clip, transform.position);
            }
        }
    }
}
