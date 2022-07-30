using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ziggurat
{
    public class ZigguratPanel : MonoBehaviour
    {
        [SerializeField] private Text _zigguratText;

        [SerializeField] private Slider _zigguratSlider;

        private ZigguratController _zigguratController;
        private bool _isActive = false;

        private Vector2 _hidenPosition = new Vector2(-126, 72);
        private Vector2 _shownPosition = new Vector2(126, 72);

        // Start is called before the first frame update
        void Start()
        {
            transform.position = _hidenPosition;
            //_zigguratText.text =
        }

        private void OnEnable()
        {
            ZigguratController.OnClickEvent += SetZiggurat;
        }

        private void OnDisable()
        {
            ZigguratController.OnClickEvent -= SetZiggurat;
        }

        private void SetZiggurat(ZigguratController ziggurat)
        {
            if(!_isActive)
            {
                transform.LeanMoveLocal(_shownPosition, 1).setEaseOutQuart();
                _isActive = true;
            }
            if(_isActive)
            {
                transform.LeanMoveLocal(_hidenPosition, 1).setEaseOutQuart();
                _isActive = false;
            }

            _zigguratController = ziggurat;
            

        }

    }
}
