using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class GameManager : MonoBehaviour
    {
        private List<UnitData> _allUnits;

        public List<UnitData> AllUnits { get => _allUnits; }

        public void AddUnitToAll(UnitData unit) => _allUnits.Add(unit);

    }
}
