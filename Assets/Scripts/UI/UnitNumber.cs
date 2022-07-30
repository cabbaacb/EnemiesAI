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
        [SerializeField] private Button _showButton;
        [SerializeField] private Button _healthButton;
        [SerializeField] private Button _killButton;



        private Vector2 _shownPosition = new Vector2(350, 160);
        private Vector2 _hidenPosition = new Vector2(800, 300);
        private bool _isActive = false;

        private void ChangeInfo() =>
            _stats.text = string.Format("{0}\n\n{1}\n\n{2}", _greenZig.UnitNumber, _redZig.UnitNumber, _blueZig.UnitNumber);

        // Update is called once per frame
        void Update()
        {
            ChangeInfo();
        }

        private void OnEnable()
        {
            _showButton.onClick.AddListener(ShowMenu);
        }

        private void OnDisable()
        {
            _showButton.onClick.RemoveAllListeners();
        }

        private void ShowMenu()
        {
            if (!_isActive)
            {
                StartCoroutine(MoveTo(_shownPosition, 1f));
                _isActive = true;
                return;
            }
            if (_isActive)
            {
                StartCoroutine(MoveTo(_hidenPosition, 1f));
                _isActive = false;
            }
        }


        private IEnumerator MoveTo(Vector3 endPosition, float time)
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
