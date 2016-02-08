using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Gore
{
    public class BloodEjector : MonoBehaviour
    {
        public GameObject BloodDroplet;

        public float MinForce;
        public float MaxForce;

        public void Eject()
        {
            for (var i = 0; i < 5; i++)
            {
                var blood = Instantiate(BloodDroplet);
                blood.transform.position = transform.position + new Vector3(0, .2f, 0);
                blood.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere.PositiveY() * Random.Range(MinForce, MaxForce));
            }
        }
    }
}