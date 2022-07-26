using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Ziggurat
{
    public class UnitController : MonoBehaviour
    {
        private UnitData _unit;
        private bool _isFighting = false;

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
            //todo moving animation
            // TATSNOT MOVING ANIMATION!!_unit.gameObject.GetComponent<UnitEnvironment>().Moving(1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != 8) return;
            if (collision.gameObject.GetComponent<UnitData>().Color == _unit.Color) return;
            if(!_isFighting)
                Fight(collision.gameObject);
            
            
        }
               
        //todo here you can get the score points
        private void Fight(GameObject enemy)
        {
            //_isFighting = true;        
            StartCoroutine(MoveToEnemy(enemy));
            StartCoroutine(HitAnEnemy(enemy));
            
            //todo add hit animation
            
            //enemy.GetComponent<UnitData>().GetDamage(_unit.SetDamage());

            //enemy.GetComponent<UnitData>();
        }
        
        private IEnumerator HitAnEnemy(GameObject enemy)
        {
            while (_isFighting)
            {
                enemy.GetComponent<UnitData>().GetDamage(_unit.SetDamage());
                print("Damage");
            }
            if(enemy.GetComponent<UnitData>().Health <= 0)
            {
                StopCoroutine(MoveToEnemy(enemy));
                StopCoroutine(HitAnEnemy(enemy));
            }
            yield return null;
            StartCoroutine(HitAnEnemy(enemy));
        }
        private IEnumerator MoveToEnemy(GameObject enemy)
        {
            float destination = Vector3.Distance(_unit.transform.position, enemy.transform.position);
            while (destination > _unit.MaxDestinationToEnemy)
            {
                _isFighting = false;
                //transform.LookAt(enemy.transform);
                MoveTo(enemy.transform.position);
                destination = Vector3.Distance(_unit.transform.position, enemy.transform.position);
                /*
                if (destination <= 1)
                {
                    yield return null;
                }*/
            }
            _isFighting = true;
            print("IsFighting");
            yield return null;
            StartCoroutine(MoveToEnemy(enemy));
        }
    }
}
