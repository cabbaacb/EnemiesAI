using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ziggurat
{
    public class ZigguratPanel : MonoBehaviour
    {
        [SerializeField] private Text _zigguratText;

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

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
