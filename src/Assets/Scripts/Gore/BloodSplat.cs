using UnityEngine;

namespace Assets.Scripts.Gore
{
    public class BloodSplat : MonoBehaviour
    {
        void Start () {
            var size = Random.Range(.3f, 1f);
            var opacity = Random.Range(.3f, 1f);

            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, opacity);
            transform.localScale = new Vector3(size, size, size);
        }
    }
}