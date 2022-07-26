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

        private Vector3 _spawnPoint;
        private List<UnitData> _units;

        private void Awake()
        {
            _spawnPoint = GetComponentInChildren<SpawnPoint>().GetCoordinates();
            StartCoroutine(SpawnUnit());
        }



        private IEnumerator SpawnUnit()
        {
            var unit = Instantiate(_unitPrefab.gameObject, _spawnPoint, Quaternion.identity);
            unit.GetComponent<UnitData>().SetColor(_zigguratColor);
            //unit.layer = 8;
            unit.GetComponentInChildren<ActiveRadius>().gameObject.layer = 8;
            //_units.Add(unit.GetComponent<UnitData>());
            yield return new WaitForSeconds(_spawnFrequency);
            StartCoroutine(SpawnUnit());
        }
    }
}
