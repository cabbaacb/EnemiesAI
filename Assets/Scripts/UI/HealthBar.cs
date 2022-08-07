using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ziggurat
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private UnitData _unit = null;
        [SerializeField] private Image _healthBarInner = null;

        public void UpdateHealthBar()
        {
            _healthBarInner.fillAmount = Mathf.Clamp((float)_unit.Health / (float)_unit.MaxHealth, 0f, 1f);
        }

        private void OnEnable()
        {
            _unit.OnHealthChange += UpdateHealthBar;
        }

        private void OnDisable()
        {
            _unit.OnHealthChange -= UpdateHealthBar;
        }
    }
}
