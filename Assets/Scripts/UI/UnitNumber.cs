using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ziggurat
{
    public class UnitNumber : MonoBehaviour
    {
        [SerializeField] private ZigguratController _greenZig;
        [SerializeField] private ZigguratController _redZig;
        [SerializeField] private ZigguratController _blueZig;
        [SerializeField] private Text _stats;

        private void ChangeInfo() =>
            _stats.text = string.Format("{0}\n\n{1}\n\n{2}", _greenZig.UnitNumber, _redZig.UnitNumber, _blueZig.UnitNumber);

        // Update is called once per frame
        void Update()
        {
            ChangeInfo();
        }
    }
}
