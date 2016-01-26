using UnityEngine;

namespace Assets.Resources.Scripts.Gore
{
    public class BloodEjector : MonoBehaviour
    {
        public GameObject BloodDroplet;

        public float MinForce;
        public float MaxForce;

        public void Eject()
        {
            for (var i = 0; i < 23; i++)
            {
                var blood = Instantiate(BloodDroplet);
                blood.transform.position = transform.position + new Vector3(0, .2f, 0);
                blood.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * Random.Range(MinForce, MaxForce));
            }
        }
    }
}