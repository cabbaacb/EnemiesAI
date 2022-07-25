using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class ZigguratController : MonoBehaviour
    {
        [SerializeField] GameObject _unitPrefab;
        [SerializeField] float _spawnFrequency = 3f;

        private Vector3 _spawnPoint;


        private void Awake()
        {
            _spawnPoint = GetComponentInChildren<SpawnPoint>().GetCoordinates();
            StartCoroutine(SpawnUnit());
        }


        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator SpawnUnit()
        {
            Instantiate(_unitPrefab, _spawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(_spawnFrequency);
            StartCoroutine(SpawnUnit());
        }
    }
}
