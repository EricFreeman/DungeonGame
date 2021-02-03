using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class TurnSway : MonoBehaviour
    {
        public float MoveAmount = .5f;
        public float MoveSpeed = 20f;

        private List<float> _runningAvg;

        void Update ()
        {
            var moveOnX = Input.GetAxis("Mouse X") * MoveAmount;
            _runningAvg.Add(moveOnX);

            if (_runningAvg.Count > 10)
            {
                _runningAvg.RemoveAt(0);
            }

            var avgPos = new Vector3(_runningAvg.Sum() / _runningAvg.Count, transform.localPosition.y, transform.localPosition.z);

            transform.localPosition = Vector3.Lerp(transform.localPosition, avgPos, MoveSpeed * Time.deltaTime);
        }
    }
}
