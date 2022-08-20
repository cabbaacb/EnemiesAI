using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ziggurat
{
    public class ZigguratController : UnitStats, IPointerClickHandler
    {
        [SerializeField] private Unit _unitPrefab = null;
        [SerializeField] private UnitColor _zigguratColor = default;

        private Vector3 _spawnPoint;
        private List<Unit> _units;
        private bool _showHealthBar = true;

        public int _deadCount;

        public int DeadCount
        {
            get { return _deadCount; }
            private set { _deadCount = value; }
        }
        private float _spawnTimer = 0;

        public float SpawnTimer
        {
            get { return _spawnTimer; }
        }


        public UnitColor ZigguratColor { get => _zigguratColor; }
        public int UnitsNumber { get => _units.Count; }

        public delegate void ClickEventHandler(ZigguratController controller);
        public static event ClickEventHandler OnClickEvent;

        private void Awake()
        {
            _spawnPoint = GetComponentInChildren<SpawnPoint>().GetCoordinates();
            _units = new List<Unit>();
            _spawnTimer = SpawnRate;
            StartCoroutine(SpawnUnit());
        }
        private void OnEnable()
        {
            UnitController.OnDeathEvent += DeleteUnit;
            Statistics.OnShowHealthBar += ShowHealthBars;
        }

        private void OnDisable()
        {
            UnitController.OnDeathEvent -= DeleteUnit;
            Statistics.OnShowHealthBar -= ShowHealthBars;
        }

        private void Update()
        {
            _spawnTimer = (Mathf.Ceil((_spawnTimer - Time.deltaTime) * 100)) / 100;
        }

        public void OnPointerClick(PointerEventData eventdata)
        {
            OnClickEvent?.Invoke(this);
        }

        public void ClearDeadCount()
        {
            DeadCount = 0;
        }

        private void DeleteUnit(Unit unit)
        {
            if (_units.Contains(unit))
            {
                DeadCount++;
                _units.Remove(unit);
            }
        }

        private IEnumerator SpawnUnit()
        {
            GameObject unit = Instantiate(_unitPrefab.gameObject, _spawnPoint, Quaternion.identity);
            Unit unitData = unit.GetComponent<Unit>();
            unit.layer = 8;
            unit.name = _zigguratColor.ToString() + "Knight";
            if (_showHealthBar)
                unit.GetComponentInChildren<HealthBar>().gameObject.SetActive(true);
            else
                unit.GetComponentInChildren<HealthBar>().gameObject.SetActive(false);

            SetUnitStats(unitData);
            _units.Add(unit.GetComponent<Unit>());
            yield return new WaitForSeconds(SpawnRate);
            _spawnTimer = SpawnRate;
            StartCoroutine(SpawnUnit());
        }

        private void SetUnitStats(UnitStats unit)
        {
            unit.SetHealth(Health);
            unit.SetSpeed(Speed);
            unit.SetFastAttackDamage(FastAttackDamage);
            unit.SetStrongAttackDamage(StrongAttackDamage);
            unit.SetChancetoMiss(ChanceToMiss);
            unit.SetDoubleDamageChance(DoubleDamageChance);
            unit.SetFastToStrongAttackChanceRatio(FastToStrongAttackChanceRatio);
            unit.SetDetectionRadius(DetectionRadius);
            unit.SetColor(Color);

        }


        private void ShowHealthBars(bool show)
        {
            foreach (var unit in _units)
            {
                unit.ShowHealthBar = show;
            }
            _showHealthBar = show;
        }
    }
}
