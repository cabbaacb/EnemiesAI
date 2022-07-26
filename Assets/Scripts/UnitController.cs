using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Ziggurat
{
    public class UnitController : MonoBehaviour
    {
        private UnitData _unit;


        private Vector3 _targetPoint = Vector3.zero;

        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _unit = GetComponent<UnitData>();
        }

        private void Start()
        {
            _navMeshAgent.speed = _unit.Speed;
            MoveTo(_targetPoint);
        }

        private void MoveTo(Vector3 targetPoint)
        {
            //transform.position = Vector3.MoveTowards(transform.position, targetPoint, _speed);
            _navMeshAgent.destination = targetPoint;
            // TATSNOT MOVING ANIMATION!!_unit.gameObject.GetComponent<UnitEnvironment>().Moving(1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            //if (collision.gameObject.layer != 8) return;
            if (collision.gameObject.GetComponent<UnitData>().Color == _unit.Color) return;

        }
    }
}
