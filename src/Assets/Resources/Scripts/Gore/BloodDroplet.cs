using UnityEngine;

namespace Assets.Resources.Scripts.Gore
{
    public class BloodDroplet : MonoBehaviour
    {
        public GameObject BloodSplat;

        void OnCollisionEnter(Collision col)
        {
            var splat = Instantiate(BloodSplat);
            splat.transform.position = col.contacts[0].point;
            splat.transform.rotation = Quaternion.FromToRotation(Vector3.up, col.contacts[0].normal);

            Destroy(gameObject);
        }
    }
}