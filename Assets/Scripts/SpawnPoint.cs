using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class SpawnPoint : MonoBehaviour
    {

        public delegate void ClickEventHandler(ZigguratController controller);
        public static event ClickEventHandler OnClickEvent;
        public Vector3 GetCoordinates() => transform.position;


        private ZigguratController _ziggurat;
        private UnitColor _zigguratColor;
        public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventdata)
        {
            OnClickEvent?.Invoke(_ziggurat);
            Debug.Log(_zigguratColor + "Ziggurat");
        }

        private void Start()
        {
            _ziggurat = GetComponentInParent<ZigguratController>();
            _zigguratColor = _ziggurat.ZigguratColor;
        }

    }
}
