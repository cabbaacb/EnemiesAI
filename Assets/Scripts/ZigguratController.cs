using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ziggurat
{
    public class ZigguratController : MonoBehaviour
    {
        [SerializeField] UnitData _unitPrefab = null;
        [SerializeField] float _spawnFrequency = 3f;
        [SerializeField] UnitColor _zigguratColor = default;

        private Vector3 _spawnPoint;
        private List<UnitData> _units;


        public delegate void ClickEventHandler(ZigguratController controller);
        public static event ClickEventHandler OnClickEvent;
        public UnitColor ZigguratColor { get => _zigguratColor; }
        public int UnitNumber { get => _units.Count; }

        private void Awake()
        {
            _spawnPoint = GetComponentInChildren<SpawnPoint>().GetCoordinates();
            _units = new List<UnitData>();
            StartCoroutine(SpawnUnit());
        }

        public void OnPointerClick(PointerEventData eventdata) => OnClickEvent?.Invoke(this);

        private IEnumerator SpawnUnit()
        {
            GameObject unit = Instantiate(_unitPrefab.gameObject, _spawnPoint, Quaternion.identity);
            UnitData unitData = unit.GetComponent<UnitData>();
            unitData.SetColor(_zigguratColor);
            unit.layer = 8;
            unit.name = _zigguratColor.ToString() + "Knight";

            _units.Add(unit.GetComponent<UnitData>());
            yield return new WaitForSeconds(_spawnFrequency);
            //StartCoroutine(SpawnUnit());
        }

        public List<UnitData> GetTargets() => _units;

        public void SetParams(UnitData unit, int health)
        {
            unit.SetHealth(health);
        }
    }
}
