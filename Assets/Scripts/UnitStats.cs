using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public abstract class UnitStats : MonoBehaviour
    {
        [SerializeField] [Range(1f, 100f)] protected int _health = 50;
        [SerializeField] [Range(1f, 50f)] protected float _speed = 2f;
        [SerializeField] [Range(1f, 100f)] protected int _fastAttackDamage = 10;
        [SerializeField] [Range(1f, 100f)] protected int _strongAttackDamage = 15;
        [SerializeField] [Range(1f, 10f)] protected float _attackInterval = 2f;
        [SerializeField] [Range(1f, 100f)] protected float _chanceToMiss = 20;
        [SerializeField] [Range(1f, 100f)] protected float _doubleDamageChance = 10;
        [SerializeField] [Range(1f, 100f)] protected int _fastToStrongAttackChanceRatio = 70; //in percentage terms
        [SerializeField] [Range(1f, 10f)] protected float _detectionRadius = 2;
        [SerializeField] [Range(1f, 10f)] protected float _spawnRate = 3f;
        [SerializeField] protected UnitColor _color;

        public int Health { get => _health; }
        public float Speed { get => _speed; }
        public int FastAttackDamage { get => _fastAttackDamage; }
        public int StrongAttackDamage { get => _strongAttackDamage; }

        public float AttackInterval { get => _attackInterval; }
        public float ChanceToMiss { get => _chanceToMiss; }
        public float DoubleDamageChance { get => _doubleDamageChance; }
        public int FastToStrongAttackChanceRatio { get => _fastToStrongAttackChanceRatio; }
        public float DetectionRadius { get => _detectionRadius; }
        public float SpawnRate { get => _spawnRate; }
        public UnitColor Color { get => _color; }

        public virtual void SetHealth(int health)
        {
            if (health > 0)
            {
                _health = health;
            }
        }

        public virtual void SetSpeed(float speed) => _speed = speed;
        public virtual void SetFastAttackDamage(int damage) => _fastAttackDamage = damage;
        public virtual void SetStrongAttackDamage(int damage) => _strongAttackDamage = damage;
        public virtual void SetAttackInterval(float attackInterval) => _attackInterval = attackInterval;
        public virtual void SetChancetoMiss(float chance) => _chanceToMiss = chance;
        public virtual void SetDoubleDamageChance(float chance) => _doubleDamageChance = chance;
        public virtual void SetFastToStrongAttackChanceRatio(int ratio) => _fastToStrongAttackChanceRatio = ratio;
        public virtual void SetDetectionRadius(float radius) => _detectionRadius = radius;
        public virtual void SetSpawnRate(float frequency) => _spawnRate = frequency;
        public void SetColor(UnitColor color) => _color = color;
    }
}
