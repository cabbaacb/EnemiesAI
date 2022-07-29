using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class UnitData : MonoBehaviour
    {
        [SerializeField] [Range(1f, 100f)] private int _health;
        [SerializeField] [Range(1, 50)] private float _speed = 2f;
        [SerializeField] [Range(1f, 100f)] private int _lowDamage;
        [SerializeField] [Range(1f, 100f)] private int _strongDamage;
        [SerializeField] [Range(0, 1)] private float _missChance;
        [SerializeField] [Range(0, 1)] private float _doubleDamageChance;
        [SerializeField] [Range(0, 100)] private int _lowToStrongAttackChanceRatio; //in percentage terms
        [SerializeField] private float _detectionRadius = 2;
        
        [SerializeField] 
        private UnitColor _color;

        public int Health { get => _health; }
        public float Speed { get => _speed; }
        public int LowDamage { get => _lowDamage; }
        public int StrongDamage { get => _strongDamage; }
        public float MissChance { get => _missChance; }
        public float DoubleDamageChance { get => _doubleDamageChance; }
        public int LowToStrongAttackChanceRatio { get => _lowToStrongAttackChanceRatio; }
        public float DetectionRadius { get => _detectionRadius; }
        public UnitColor Color { get => _color; }

        public void SetHealth(int health) => _health = health;
        public void SetSpeed(float speed) => _speed = speed;
        public void SetLowDamage(int damage) => _lowDamage = damage;
        public void SetStrongDamage(int damage) => _strongDamage = damage;
        public void SetMissChance(float chance) => _missChance = chance;
        public void SetDoubleDamageChance(float chance) => _doubleDamageChance = chance;
        public void SetLowToStrongAttackChanceRatio(int ratio) => _lowToStrongAttackChanceRatio = ratio;
        public void SetColor(UnitColor color) => _color = color;

        public void GetDamage(int damage) => _health -= damage;

        //todo
        public int SetDamage()
        {
            return _lowDamage;
        }
    }
}
