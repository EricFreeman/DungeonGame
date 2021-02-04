using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Bullet : MonoBehaviour {

        public float Speed;
        public int Damage = 10;

        public bool IsFriendly;

        void Update()
        {
            transform.position += (transform.forward.normalized * Speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider col)
        {
            if ((IsFriendly && col.tag != "Player") || !IsFriendly && col.tag == "Player")
            {
                if (col.tag == "Player")
                {
                    CameraScreenShake.Instance.Shake(.2f, .05f, .05f);
                }

                Destroy(gameObject);
            }
        }
    }
}
