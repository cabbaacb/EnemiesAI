using System;
using UnityEngine;

namespace Ziggurat
{
    public class UnitData : UnitStats
    {
        [SerializeField] private int _currentHealth = 50;

        private UnitController _unitController;

        public int CurrentHealth 
        { 
            get => _currentHealth;
            private set
            {
                _currentHealth = value;
                if (_currentHealth <= 0)
                {
                    Invoke("Die", 0.2f);    //i should've done this in a more proper way, but i'm a bit lazy for now
                }
            }
        }

        private void Start()
        {
            _unitController = GetComponent<UnitController>();
            _currentHealth = _health;
        }

        public void TakeDamage(int damage)
        {
            _unitController.TakeDamage();
            CurrentHealth -= damage;
            OnHealthChange?.Invoke();
        }

        private void Die()
        {
            _unitController.Die();
        }

        public event Action OnHealthChange;
    }
}
