using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Billboard : MonoBehaviour
    {
        private void Update()
        {
            var v = UnityEngine.Camera.main.transform.position - transform.position;
            v.x = v.z = 0.0f;
            transform.LookAt(UnityEngine.Camera.main.transform.position - v);
        }
    }
}