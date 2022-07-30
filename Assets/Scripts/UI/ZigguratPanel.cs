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
        // Start is called before the first frame update
        void Start()
        {
            
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
            if(_isActive = false)
            {
                _isActive = true;
            }

            _zigguratController = ziggurat;
            //transform.LeanMoveLocal()

        }

    }
}
