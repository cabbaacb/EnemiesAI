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

        private UnitEnvironment _unitEnvironment;

        private bool _isMoving = false;
        public bool IsMoving
        {
            get { return _isMoving; }
            private set 
            { 
                _isMoving = value;
                if (value) _unitEnvironment.Moving(1f);
                else _unitEnvironment.Moving(0f);
            }
        }


        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _unit = GetComponent<UnitData>();
            _unitEnvironment = _unit.gameObject.GetComponent<UnitEnvironment>();
        }

        private void Start()
        {
            _navMeshAgent.speed = _unit.Speed;
            MoveTo(_targetPoint);
        }

        private void Update()
        {
            if (!_navMeshAgent.pathPending && !_navMeshAgent.hasPath)
            {
                IsMoving = false;
            }
        }

        private void OnEnable()
        {
            ActiveRadius.OnFightCollision += CollisionEnter;

        }

        private void OnDisable()
        {
            ActiveRadius.OnFightCollision -= CollisionEnter;
        }

        private void CollisionEnter(Collision collision)
        {
            //if (collision.gameObject.layer != 8) return;
            //if (collision.gameObject.GetComponent<UnitData>().Color == _unit.Color) return;
            print("collision");
            if(!_isFighting)
                Fight(collision.gameObject.GetComponentInParent<UnitData>().gameObject);
            
            
        }

        private void MoveTo(Vector3 targetPoint)
        {
            _navMeshAgent.destination = targetPoint;
            IsMoving = true;
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
                print("moving");
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
