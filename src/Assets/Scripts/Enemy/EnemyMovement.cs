﻿using System;
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
        public float MinDistance = .75f;
        public EnemyState State = EnemyState.Patrolling;

        private Vector3 _lastKnownLocation;
        private GameObject _player;

        private UnityEngine.AI.NavMeshAgent _navAgent;
        private bool _traversingLink;
        private UnityEngine.AI.OffMeshLinkData _currLink;

        void Start()
        {
            _player = GameObject.Find("Player");
            _navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            _navAgent.autoTraverseOffMeshLink = false;
        }

        void FixedUpdate()
        {
            GetComponent<Animator>().SetBool("IsAttacking", false);

            switch (State)
            {
                case EnemyState.Idle:
                    if (CanSeePlayer())
                    {
                        State = EnemyState.Detect;
                        GetComponent<Animator>().SetBool("IsMoving", true);
                    }
                    break;
                case EnemyState.Patrolling:
                    if (CanSeePlayer())
                    {
                        State = EnemyState.Detect;
                        GetComponent<Animator>().SetBool("IsMoving", true);
                    }
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
                GetComponent<Animator>().SetBool("IsMoving", true);

                GetComponent<UnityEngine.AI.NavMeshAgent>().destination = _lastKnownLocation;

                if (Vector3.Distance(transform.position, _lastKnownLocation) < .1f)
                {
                    State = EnemyState.Searching;
                    GetComponent<Animator>().SetBool("IsMoving", false);
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, _player.transform.position) < MinDistance)
                {
                    GetComponent<Animator>().SetBool("IsMoving", false);
                    GetComponent<Animator>().SetBool("IsAttacking", true);
                    _lastKnownLocation = _player.transform.position;
                    GetComponent<UnityEngine.AI.NavMeshAgent>().destination = transform.position;
                }
                else
                {
                    if (_navAgent.isOnOffMeshLink)
                    {
                        if (!_traversingLink)
                        {
                            _currLink = _navAgent.currentOffMeshLinkData;
                            _traversingLink = true;
                        }

                        var tlerp = .1f;
                        var newPos = Vector3.Lerp(_currLink.startPos, _currLink.endPos, tlerp);
                        newPos.y += 2f * Mathf.Sin(Mathf.PI * tlerp);
                        transform.position = newPos;

                        if (_currLink.startPos == _currLink.endPos)
                        {
                            transform.position = _currLink.endPos;
                            _traversingLink = false;
                            _navAgent.CompleteOffMeshLink();
                            _navAgent.Resume();
                        }
                    }
                    else
                    {
                        GetComponent<Animator>().SetBool("IsMoving", true);

                        _lastKnownLocation = _player.transform.position;
                        GetComponent<UnityEngine.AI.NavMeshAgent>().destination = _player.transform.position;

                        var targetRotation = Quaternion.LookRotation(_player.transform.position - transform.position);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TurnSpeed * Time.deltaTime);
                    }
                }
            }
        }

        #endregion

        private bool CanSeePlayer()
        {
            var rayDirection = _player.transform.position - transform.position - new Vector3(0, .5f);

            if ((Vector3.Angle(rayDirection, transform.forward)) < FieldOfView)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + new Vector3(0, .5f), rayDirection, out hit) && Vector3.Distance(transform.position, _player.transform.position) < ViewDistance)
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