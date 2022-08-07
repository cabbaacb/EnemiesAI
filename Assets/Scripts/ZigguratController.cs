using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class ZigguratController : MonoBehaviour
    {
        [SerializeField] UnitData _unitPrefab = null;
        [SerializeField] float _spawnFrequency = 3f;
        [SerializeField] UnitColor _zigguratColor = default;

        private Vector3 _spawnPoint;
        private List<UnitData> _units;
        private bool _showHealthBar = true;

        
        public UnitColor ZigguratColor { get => _zigguratColor; }
        public int UnitsNumber { get => _units.Count; }

        private void Awake()
        {
            _spawnPoint = GetComponentInChildren<SpawnPoint>().GetCoordinates();
            _units = new List<UnitData>();
            StartCoroutine(SpawnUnit());
        }
        private void OnEnable()
        {
            UnitController.OnDeathEvent += DeleteUnit;
            UnitNumber.OnShowHealthBar += ShowHealthBars;
            UnitNumber.OnKillEveryone += KillEveryone;
        }

        private void OnDisable()
        {
            UnitController.OnDeathEvent -= DeleteUnit;
            UnitNumber.OnShowHealthBar -= ShowHealthBars;
            UnitNumber.OnKillEveryone -= KillEveryone;
        }

        private void DeleteUnit(UnitData unit)
        {
            if(_units.Contains(unit))
                _units.Remove(unit);
        }

        private IEnumerator SpawnUnit()
        {
            GameObject unit = Instantiate(_unitPrefab.gameObject, _spawnPoint, Quaternion.identity);
            UnitData unitData = unit.GetComponent<UnitData>();
            unitData.SetColor(_zigguratColor);
            unit.layer = 8;
            unit.name = _zigguratColor.ToString() + "Knight";
            if (_showHealthBar)
                unit.GetComponentInChildren<HealthBar>().gameObject.SetActive(true);
            else
                unit.GetComponentInChildren<HealthBar>().gameObject.SetActive(false);


            _units.Add(unit.GetComponent<UnitData>());
            yield return new WaitForSeconds(_spawnFrequency);
            //StartCoroutine(SpawnUnit());
        }

        public List<UnitData> GetUnits() => _units;

        public UnitData SetParams(UnitData unit, int health)
        {
            unit.SetHealth(health);

            return unit;
        }


        private void ShowHealthBars(bool show)
        {
            if(show)
                foreach (var unit in _units)
                {
                    unit.GetComponentInChildren<HealthBar>().gameObject.SetActive(true);
                    _showHealthBar = true;
                }
            else
                foreach(var unit in _units)
                {
                    unit.GetComponentInChildren<HealthBar>().gameObject.SetActive(false);
                    _showHealthBar = false;
                }

        }


        private void KillEveryone()
        {
            foreach (var unit in _units)
                DeleteUnit(unit);
        }

    }
}
