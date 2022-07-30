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
        [SerializeField] private Button _button;

        private ZigguratController _zigguratController;
        private bool _isActive = false;

        private Vector2 _hidenPosition = new Vector2(-126, 66);
        private Vector2 _shownPosition = new Vector2(126, 66);

        // Start is called before the first frame update
        void Start()
        {
            transform.position = _hidenPosition;
            //_zigguratText.text =
        }

        private void OnEnable()
        {
            SpawnPoint.OnClickEvent += SetZiggurat;
            _button.onClick.AddListener(ShowMenu);
        }

        private void OnDisable()
        {
            SpawnPoint.OnClickEvent -= SetZiggurat;
            _button.onClick.RemoveAllListeners();
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

        private void ShowMenu()
        {
            if (!_isActive)
            {
                StartCoroutine(MoveFromTo(_hidenPosition, _shownPosition, 1f));
                _isActive = true;
                return;
            }
            if (_isActive)
            {
                StartCoroutine(MoveFromTo(_shownPosition, _hidenPosition, 1f));
                _isActive = false;
            }
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
