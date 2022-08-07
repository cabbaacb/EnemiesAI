using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class UnitData : MonoBehaviour
    {
        [SerializeField] [Range(1f, 100f)] private int _health = 100;
        [SerializeField] [Range(1, 50)] private float _speed = 2f;
        [SerializeField] [Range(1f, 100f)] private int _fastAttackDamage = 10;
        [SerializeField] [Range(1f, 100f)] private int _strongAttackDamage = 15;
        [SerializeField] [Range(1.1f, 50)] private float _attackInterval = 2f;
        [SerializeField] [Range(0, 100)] private float _chanceToMiss = 20;
        [SerializeField] [Range(0, 100)] private float _doubleDamageChance = 10;
        [SerializeField] [Range(0, 100)] private int _fastToStrongAttackChanceRatio = 70; //in percentage terms
        [SerializeField] private float _detectionRadius = 2;
        
        [SerializeField] 
        private UnitColor _color;

        private UnitController _unitController;
        private int _maxHealth = 50;


        public int MaxHealth { get => _maxHealth; }
        public int Health 
        { 
            get => _health;
            private set
            {
                _health = value;
                if (_health <= 0)
                {
                    Invoke("Die", 0.2f);    //i should've done this more proper way, but i'm a bit lazy for now
                }
            }
        }
        public float Speed { get => _speed; }
        public int FastAttackDamage { get => _fastAttackDamage; }
        public int StrongAttackDamage { get => _strongAttackDamage; }

        public float AttackInterval { get => _attackInterval; }
        public float ChanceToMiss { get => _chanceToMiss; }
        public float DoubleDamageChance { get => _doubleDamageChance; }
        public int FastToStrongAttackChanceRatio { get => _fastToStrongAttackChanceRatio; }
        public float DetectionRadius { get => _detectionRadius; }
        public UnitColor Color { get => _color; }

        public void SetHealth(int health)
        {
            if (health > 0)
            {
                _health = health;
            }
        }

        public void SetMaxHealth(int health)
        {
            _maxHealth = health;
        }

        public void SetSpeed(float speed) => _speed = speed;
        public void SetFastDamage(int damage) => _fastAttackDamage = damage;
        public void SetStrongDamage(int damage) => _strongAttackDamage = damage;
        public void SetMissChance(float chance) => _chanceToMiss = chance;
        public void SetDoubleDamageChance(float chance) => _doubleDamageChance = chance;
        public void SetFastToStrongAttackChanceRatio(int ratio) => _fastToStrongAttackChanceRatio = ratio;
        public void SetColor(UnitColor color) => _color = color;

        private void Start()
        {
            _unitController = GetComponent<UnitController>();
        }

        public void TakeDamage(int damage)
        {
            _unitController.TakeDamage();
            Health -= damage;
            OnHealthChange?.Invoke();
        }

        private void Die()
        {
            gameObject.layer = 0;
            _unitController.Die();
        }

        public event System.Action OnHealthChange;
    }
}
