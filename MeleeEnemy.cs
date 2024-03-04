using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private Transform _PlayerPos;
    [SerializeField] private Transform _RetreatPoint;
    [SerializeField] private Animator _MeleeRatAnimator;

    public float AgentSpeedPlayerVisable = 5f;
    public float AgentSpeed = 2.5f;
    public bool _hasAttacked = false;

    private FieldOfView _fieldOfView;
    private NavMeshAgent _agent;

    void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_fieldOfView.IsPlayerVisable == true && _hasAttacked == false)
        {
            _agent.speed = AgentSpeedPlayerVisable;
            transform.LookAt(_PlayerPos);
            _MeleeRatAnimator.SetBool("Attack", true);
            _hasAttacked = true;

            if (_hasAttacked == true)
            {
                Invoke("AttackCooldown", 1f);
                Invoke("TimeToRetreat", 1f);
                _MeleeRatAnimator.SetBool("Attack", false);
            }
        }
        else
        {
            _agent.speed = AgentSpeed;
            _hasAttacked = false;
            _MeleeRatAnimator.SetBool("Attack", false);
        }
    }

    private void AttackCooldown()
    {
        _hasAttacked = false;
    }

    private void TimeToRetreat()
    {
        _fieldOfView.IsPlayerVisable = false;
        _agent.SetDestination(_RetreatPoint.transform.position);
    }
}
