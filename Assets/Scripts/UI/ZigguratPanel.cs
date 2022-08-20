using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ziggurat
{
    public class ZigguratPanel : MonoBehaviour
    {
        [SerializeField] private Text _zigguratText = null;

        //[SerializeField] private Slider _zigguratSlider = null;
        [SerializeField] private Button _hideButton = null;

        private ZigguratController _zigguratController = null;
        private ZigguratController Ziggurat
        {
            get { return _zigguratController; }
            set
            { 
                _zigguratController = value;
                _zigguratText.text = value.ZigguratColor.ToString() + " Ziggurat";
            }
        }


        private Vector2 _hiddenPosition;
        private Vector2 _shownPosition = new Vector2(126, 66);

        private bool _isActive = false;
        private bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                if (_isActive)
                {
                    StartCoroutine(MoveFromTo(_hiddenPosition, _shownPosition, 1f));
                }
                if (!_isActive)
                {
                    StartCoroutine(MoveFromTo(_shownPosition, _hiddenPosition, 1f));
                }
            }
        }

        void Start()
        {
            _hiddenPosition = transform.position;
        }

        private void OnEnable()
        {
            ZigguratController.OnClickEvent += SetZiggurat;
            _hideButton.onClick.AddListener(ShowMenu);
        }

        private void OnDisable()
        {
            ZigguratController.OnClickEvent -= SetZiggurat;
            _hideButton.onClick.RemoveAllListeners();
        }


        
        private void SetZiggurat(ZigguratController ziggurat)
        {
            if(!IsActive)
            {
                IsActive = true;
            }

            Ziggurat = ziggurat;
        }

        private void ShowMenu()
        {
            IsActive = !IsActive;
        }

        private IEnumerator MoveFromTo(Vector3 startPosition, Vector3 endPosition, float time)
        {
            var currentTime = 0f;//текущее время смещения
            while (currentTime < time)//асинхронный цикл, выполняется time секунд
            {
                //Lerp - в зависимости от времени (в относительных единицах, то есть от 0 до 1
                //смещает объект от startPosition к endPosition
                transform.position = Vector3.Lerp(transform.position, endPosition, 1 - (time - currentTime) / time);
                currentTime += Time.deltaTime;//обновление времени, для смещения
                yield return null;//ожидание следующего кадра
            }
            //Из-за неточности времени между кадрами, без этой строчки вы не получите точное значение endPosition
            transform.position = endPosition;
        }

    }

}
