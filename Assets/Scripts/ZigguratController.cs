using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class ZigguratController : MonoBehaviour
    {
        [SerializeField] UnitData _unitPrefab = null;
        [SerializeField] float _spawnFrequency = 3f;
        [SerializeField] UnitColor _zigguratColor;
        [SerializeField] private GameManager _manager;

        private Vector3 _spawnPoint;
        private List<UnitData> _units;
        


        public UnitColor ZigguratColor { get => _zigguratColor; }

        private void Awake()
        {
            _spawnPoint = GetComponentInChildren<SpawnPoint>().GetCoordinates();
            _units = new List<UnitData>();
            _manager = new GameManager();
            StartCoroutine(SpawnUnit());
        }



        private IEnumerator SpawnUnit()
        {
            var unit = Instantiate(_unitPrefab.gameObject, _spawnPoint, Quaternion.identity);
            unit.GetComponent<UnitData>().SetColor(_zigguratColor);
            unit.layer = 8;
            unit.GetComponent<UnitData>().SetZiggurat(this);
            //unit.GetComponentInChildren<ActiveRadius>().gameObject.layer = 8;
            _units.Add(unit.GetComponent<UnitData>());
            _manager.AddUnitToAll(unit.GetComponent<UnitData>());
            yield return new WaitForSeconds(_spawnFrequency);
            StartCoroutine(SpawnUnit());
        }

        public List<UnitData> GetTargets() => _units;
    }
}
