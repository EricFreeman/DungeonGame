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
//            CheckFourCornersCollideWithGround();
            for(var i = 0; i < _debugTraceLines.Length; i++)
            {
//                _debugTraceLines[i] = RotatePointAroundPivot(_debugTraceLines[i], gameObject.transform.position, gameObject.transform.rotation.eulerAngles * .1f);
                var ray = _debugTraceLines[i];
                Debug.DrawRay(ray, transform.forward * _traceLength);
            }
        }

        private void CheckFourCornersCollideWithGround()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var width = spriteRenderer.bounds.extents.x;
            var height = spriteRenderer.bounds.extents.z;

            var isRotated = Math.Abs(transform.forward.y) < .1f;

            var corners = new[]
            {
                transform.position + new Vector3(-width, 0, -height),
                transform.position + new Vector3(-width, 0, height),
                transform.position + new Vector3(width, 0, -height),
                transform.position + new Vector3(width, 0, height)
            };

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