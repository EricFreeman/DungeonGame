using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Gore
{
    public class BloodSplat : MonoBehaviour
    {
        private float _traceLength = .05f;
        private Vector3[] _debugTraceLines = {};

        void Start()
        {
            var size = Random.Range(.3f, 1f);
            var opacity = Random.Range(.1f, 1f);

            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, opacity);
            transform.localScale = new Vector3(size, size, size);

            CheckFourCornersCollideWithGround();
            PoolBlood();
        }

        void Update()
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

            Debug.DrawLine(transform.position, transform.position + extents);
            Debug.DrawLine(transform.position, transform.position - extents);
            Debug.DrawLine(transform.position, transform.position + otherExtents);
            Debug.DrawLine(transform.position, transform.position - otherExtents);

            Debug.DrawLine(transform.position + extents, transform.position + extents + transform.forward * .1f);
            Debug.DrawLine(transform.position - extents, transform.position - extents + transform.forward * .1f);
            Debug.DrawLine(transform.position + otherExtents, transform.position + otherExtents + transform.forward * .1f);
            Debug.DrawLine(transform.position - otherExtents, transform.position - otherExtents + transform.forward * .1f);
        }

        private void CheckFourCornersCollideWithGround()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var x = spriteRenderer.bounds.extents.x;
            var y = spriteRenderer.bounds.extents.y;
            var z = spriteRenderer.bounds.extents.z;

            if (x < .05f) x = 0f;
            if (y < .05f) y = 0f;
            if (z < .05f) z = 0f;

            var corners = new[]
            {
                transform.position + new Vector3(-x, -y, -z),
                transform.position + new Vector3(-x, y, z),
                transform.position + new Vector3(x, -y, -z),
                transform.position + new Vector3(x, y, z)
            };

            if (Math.Abs(Math.Abs(transform.forward.x) - 1) < .01f)
            {
                corners = new[]
                {
                    transform.position + new Vector3(-z, -y, 0),
                    transform.position + new Vector3(-z, y, 0),
                    transform.position + new Vector3(z, -y, 0),
                    transform.position + new Vector3(z, y, 0)
                };
            }

            _debugTraceLines = corners;

            foreach (var point in _debugTraceLines)
            {
                var ray = new Ray(point, transform.forward);
                var hits = Physics.RaycastAll(ray, _traceLength);

//                if (hits.Length == 0)
//                {
//                    Destroy(gameObject);
//                }

                foreach (var hit in hits)
                {
                    Debug.Log(hit.transform.gameObject.layer);
                }
            }
        }

        private void PoolBlood()
        {
        }
    }
}