using System;
using UnityEngine;

namespace Ziggurat
{
    public class Unit : UnitStats
    {
        [SerializeField] private int _currentHealth = 50;

        private UnitController _unitController;

        private GameObject _healthBar;

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

        private bool _showHealthBar;

        public bool ShowHealthBar
        {
            get { return _showHealthBar; }
            set
            { 
                _showHealthBar = value;
                _healthBar.SetActive(value);
            }
        }


        private void Start()
        {
            _unitController = GetComponent<UnitController>();
            _healthBar = GetComponentInChildren<HealthBar>().gameObject;
            _currentHealth = _health;
        }

        private void OnEnable()
        {
            Statistics.OnKillEveryone += Die;
        }

        private void OnDisable()
        {
            Statistics.OnKillEveryone -= Die;
        }

        public void TakeDamage(int damage)
        {
            _unitController.TakeDamage();
            CurrentHealth -= damage;
            OnHealthChange?.Invoke();
        }

        public void Die()
        {
            _unitController.Die();
        }

        public event Action OnHealthChange;
    }
}
