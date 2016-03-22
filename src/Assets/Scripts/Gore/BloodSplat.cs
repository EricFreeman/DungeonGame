using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Gore
{
    public class BloodSplat : MonoBehaviour
    {
        private float _traceLength = .05f;

        void Start()
        {
            var size = Random.Range(.3f, 1f);
            var opacity = Random.Range(.1f, 1f);

            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, opacity);
            transform.localScale = new Vector3(size, size, size);

            CheckFourCornersCollideWithGround();
            PoolBlood();
        }

        private void CheckFourCornersCollideWithGround()
        {
            var extents = GetComponent<SpriteRenderer>().bounds.extents;
            if (extents.normalized.x < .5f) extents.x = 0f;
            if (extents.normalized.y < .5f) extents.y = 0f;
            if (extents.normalized.z < .5f) extents.z = 0f;

            var otherExtents = extents;
            if (Math.Abs(otherExtents.x) < .1f)
            {
                otherExtents.y = -otherExtents.y;
            }
            else
            {
                otherExtents.x = -otherExtents.x;
            }

            var corners = new[]
            {
                transform.position + extents,
                transform.position - extents,
                transform.position + otherExtents,
                transform.position - otherExtents
            };

            foreach (var point in corners)
            {
                var ray = new Ray(point, transform.forward);
                var hits = Physics.RaycastAll(ray, _traceLength);

                if (hits.Length == 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void PoolBlood()
        {
        }
    }
}