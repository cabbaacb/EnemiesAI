using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.UI;

namespace Ziggurat
{
    public class UnitController : MonoBehaviour
    {
        private UnitData _unit;

        private NavMeshAgent _navMeshAgent;
        [SerializeField] private UnitData _target = null;
        private UnitEnvironment _unitEnvironment;

        private Coroutine _attackRoutine;

        public delegate void DeathEventHandler(UnitData unit);
        public static event DeathEventHandler OnDeathEvent;

        [SerializeField] private bool _isMoving = false;



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
            MoveTo(Vector3.zero);
        }

        private void Update()
        {
            if (gameObject.layer != 8) return;

            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 1f)
            {
                _navMeshAgent.isStopped = true;
                IsMoving = false;
            }

            if (_target == null)
            {
                FindTarget();
                if (_target == null && !IsMoving)
                {
                    Wander();
                }
            }
            else if (_target != null && _target.gameObject.layer != 8)
            {
                _target = null;
            }
            else if (_target != null && Vector3.Distance (transform.position, _target.transform.position) > 2f)
            {
                MoveTo(_target.transform.position);
            }
            else if (_target != null && Vector3.Distance(transform.position, _target.transform.position) <= 2f)
            {
                if (IsMoving)
                {
                    _navMeshAgent.isStopped = true;
                    IsMoving = false;
                    _attackRoutine = StartCoroutine(AttackRoutine());
                }
            }
        }

        public void TakeDamage()
        {
            _unitEnvironment.StartAnimation("Impact");
        }

        public void Die()
        {
            gameObject.layer = 0;
            if (_attackRoutine != null)
                StopCoroutine(_attackRoutine);
            OnDeathEvent?.Invoke(_unit);
            _unitEnvironment.StartAnimation("Die");
        }

        private void MoveTo(Vector3 targetPoint)
        {
            _navMeshAgent.ResetPath();
            _navMeshAgent.destination = targetPoint;
            IsMoving = true;
        }

        private void FindTarget()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _unit.DetectionRadius);
            if (hitColliders.Length > 0)
            {
                float minDistance = Mathf.Infinity;
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.layer == 8 && hitCollider.name != name)
                    {
                        float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            UnitData target = hitCollider.GetComponent<UnitData>();
                            //print(gameObject.name + " " + target.name);
                            if (target != null && target.CurrentHealth > 0) _target = target;
                        }
                    }
                }
            }
        }

        private IEnumerator AttackRoutine()
        {
            if (_target == null || _target.CurrentHealth <= 0) yield break;

            transform.LookAt(_target.transform);    //todo: write proper logic for rotation towards target
            SelectAndStartAttack();
            yield return new WaitForSeconds(_unit.AttackInterval);
            
            _attackRoutine = StartCoroutine(AttackRoutine());
        }

        private void SelectAndStartAttack()
        {
            int chanceToStrongAttack = Random.Range(1, 100);
            if (chanceToStrongAttack <= _unit.FastToStrongAttackChanceRatio)
            {
                _unitEnvironment.StartAnimation("Fast");
            }
            else
            {
                _unitEnvironment.StartAnimation("Strong");
            }
        }

        private void Wander()
        {
            Vector3 newDestination = Random.insideUnitSphere * _unit.DetectionRadius;
            MoveTo(newDestination);
        }
    }
}
