using System;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public float FieldOfView = 180f;
        public float ViewDistance = 4;
        public float MoveSpeed = 2f;
        public float TurnSpeed = 6f;

        public EnemyState State = EnemyState.Patrolling;
        private Vector3 _lastKnownLocation;

        private GameObject _player;

        void Start()
        {
            _player = GameObject.Find("Player");
        }

        void FixedUpdate()
        {
            switch (State)
            {
                case EnemyState.Idle:
                    if (CanSeePlayer()) State = EnemyState.Detect;
                    break;
                case EnemyState.Patrolling:
                    if (CanSeePlayer()) State = EnemyState.Detect;
                    break;
                case EnemyState.Searching:
                    SearchState();
                    break;
                case EnemyState.Detect:
                    AlertState();
                    break;
            }
        }

        #region Search State

        private bool _searchDir;
        private int _searchAmount;

        private void SearchState()
        {
            if (CanSeePlayer())
            {
                _searchAmount = 0;
                State = EnemyState.Detect;
            }

            var stopAmount = Math.Abs(_searchAmount - transform.rotation.eulerAngles.y);
            if (stopAmount < 3 || (stopAmount - 360 < 3 && stopAmount - 360 > 0))
            {
                _searchDir = !_searchDir;
                _searchAmount = (int)transform.rotation.eulerAngles.y + (120 * (_searchDir ? 1 : -1));
            }

            transform.Rotate(0, 1 * (_searchDir ? 1 : -1), 0);
        }

        #endregion

        #region Alert State

        private void AlertState()
        {
            if (!CanSeePlayer())
            {
                GetComponent<NavMeshAgent>().destination = _lastKnownLocation;

                if (Vector3.Distance(transform.position, _lastKnownLocation) < .1f)
                {
                    State = EnemyState.Searching;
                }
            }
            else
            {
                _lastKnownLocation = _player.transform.position;
                GetComponent<NavMeshAgent>().destination = _player.transform.position;

                var targetRotation = Quaternion.LookRotation(_player.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TurnSpeed * Time.deltaTime);
            }
        }

        #endregion

        private bool CanSeePlayer()
        {
            var rayDirection = _player.transform.position - transform.position;

            if ((Vector3.Angle(rayDirection, transform.forward)) < FieldOfView)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, rayDirection, out hit) && Vector3.Distance(transform.position, _player.transform.position) < ViewDistance)
                {
                    if (hit.transform.tag == "Player")
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        void OnTriggerEnter(Collider col)
        {
            var bullet = col.GetComponent<Bullet>();

            if (bullet != null)
            {
                if (State != EnemyState.Detect)
                {
                    State = EnemyState.Searching;

                    var player = GameObject.Find("Player");
                    var size = .15f;
                    var points = new[]
                    {
                        player.transform.position,
                        player.transform.position + new Vector3(size, 0, size),
                        player.transform.position + new Vector3(-size, 0, -size),
                        player.transform.position + new Vector3(-size, 0, size),
                        player.transform.position + new Vector3(size, 0, -size)
                    };

                    foreach (var point in points)
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, point - transform.position + new Vector3(0, .2f, 0), out hit))
                        {
                            if (hit.transform.tag == "Player")
                            {
                                State = EnemyState.Detect;
                                _lastKnownLocation = player.transform.position;
                            }
                        }
                    }
                }
            }
        }
    }

    public enum EnemyState
    {
        Idle,
        Patrolling,
        Searching,
        Detect,
        Dead
    }
}