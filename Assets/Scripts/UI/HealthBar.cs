using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ziggurat
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private UnitData _unit;
        [SerializeField] private Image _healthBarInner;

        // Update is called once per frame
        void Update()
        {
            _healthBarInner.fillAmount = Mathf.Clamp(_unit.Health / _unit.MaxHealth, 0, 1f);
            //_healthBarInner.fillAmount = _unit.Health / _unit.MaxHealth;
        }
    }
}
