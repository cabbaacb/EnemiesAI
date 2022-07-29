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


        public UnitColor ZigguratColor { get => _zigguratColor; }

        private void Awake()
        {
            _spawnPoint = GetComponentInChildren<SpawnPoint>().GetCoordinates();
            _units = new List<UnitData>();
            StartCoroutine(SpawnUnit());
        }



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
    }
}
