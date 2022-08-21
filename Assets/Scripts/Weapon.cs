using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class Weapon : MonoBehaviour
    {
        private string _attackName;

        public string AttackName
        {
            get { return _attackName; }
            set { _attackName = value; }
        }

        private Unit _unit;

        void Start()
        {
            _unit = GetComponentInParent<Unit>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name != _unit.name)
            {
                if (other.TryGetComponent(out Unit target))
                {
                    int chanceToMiss = Random.Range(1, 100);
                    if (chanceToMiss >= _unit.ChanceToMiss)
                    {
                        int damage = 0;
                        if (_attackName == "FastAttack")
                        {
                            damage = _unit.FastAttackDamage;
                        }
                        else if (_attackName == "StrongAttack")
                        {
                            damage = _unit.StrongAttackDamage;
                        }
                        int chanceToCrit = Random.Range(1, 100);
                        if (chanceToCrit <= _unit.DoubleDamageChance)
                        {
                            damage *= 2;
                        }
                        target.TakeDamage(damage);
                    }
                }
            }
        }
    }
}
