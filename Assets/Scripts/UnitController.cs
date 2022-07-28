using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

namespace Ziggurat
{
    public class UnitController : MonoBehaviour
    {
        private UnitData _unit;
        private bool _isFighting = false;

        private Vector3 _targetPoint = Vector3.zero;

        private NavMeshAgent _navMeshAgent;
        private GameManager _manager;
        //private ZigguratController _ziggurat;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _unit = GetComponent<UnitData>();
            _manager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            //_ziggurat = FindObjectsOfType<ZigguratController>().Where(z => z.ZigguratColor == _unit.Color).ElementAt(0);
            _navMeshAgent.speed = _unit.Speed;
            MoveTo(_targetPoint);
            GetToCenterAndFindTarget();
        }

        private void GetToCenterAndFindTarget()
        {
            while (Vector3.Distance(transform.position, Vector3.zero) >= 25) { }
            FindTarget();
            if (_unit.Target != null)
                Fight(_unit.Target.gameObject);

        }

        private void MoveTo(Vector3 targetPoint)
        {
            //transform.position = Vector3.MoveTowards(transform.position, targetPoint, _speed);
            _navMeshAgent.destination = targetPoint;
            //todo moving animation
            // TATSNOT MOVING ANIMATION!!_unit.gameObject.GetComponent<UnitEnvironment>().Moving(1f);
        }

        /*
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
               
        */

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


        private void FindTarget()
        {
            List<UnitData> myUnits = _unit.Ziggurat.GetTargets();
            float dist = 150;
            UnitData targ = null;
            var targets = _manager.AllUnits;
            
            foreach (var target in targets)
            {
                if (myUnits.Contains(target)) return;
                var d = Vector3.Distance(transform.position, target.transform.position);
                if (d < dist)
                {
                    dist = d;
                    targ = target;
                }
            }
            _unit.SetTarget(targ);
        }
    }
}
