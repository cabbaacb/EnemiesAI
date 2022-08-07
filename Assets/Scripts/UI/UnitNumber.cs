using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ziggurat
{
    public class UnitNumber : MonoBehaviour
    {
        [SerializeField] private ZigguratController _greenZig = null;
        [SerializeField] private ZigguratController _redZig = null;
        [SerializeField] private ZigguratController _blueZig = null;
        [SerializeField] private Text _stats = null;
        [SerializeField] private Button _showButton = null;
        [SerializeField] private Button _healthButton = null;
        [SerializeField] private Button _killButton = null;

        public delegate void ShowHealthBarHandler(bool show);
        public static event ShowHealthBarHandler OnShowHealthBar;
        public delegate void KillEveryoneHandler();
        public static event KillEveryoneHandler OnKillEveryone;

        private Vector2 _shownPosition = new Vector2(350, 160);
        private Vector2 _hidenPosition = new Vector2(800, 300);
        private bool _isActive = false;
        private bool _isHealthShown = true;

        private void ChangeInfo() =>
            _stats.text = string.Format("{0}\n\n{1}\n\n{2}", _greenZig.UnitsNumber, _redZig.UnitsNumber, _blueZig.UnitsNumber);

        // Update is called once per frame
        void Update()
        {
            ChangeInfo();
        }

        private void OnEnable()
        {
            _showButton.onClick.AddListener(ShowMenu);
            _healthButton.onClick.AddListener(ShowHealthBars);
            _killButton.onClick.AddListener(KillEveryOne);
        }

        private void OnDisable()
        {
            _showButton.onClick.RemoveAllListeners();
            _healthButton.onClick.RemoveAllListeners();
            _killButton.onClick.RemoveAllListeners();
        }

        private void ShowMenu()
        {
            if (!_isActive)
            {
                _healthButton.interactable = false;
                _killButton.interactable = false;
                _showButton.interactable = false;
                StartCoroutine(MoveTo(_shownPosition, 2f));
                _isActive = true;
                _healthButton.interactable = true;
                _killButton.interactable = true;
                _showButton.interactable = true;
                return;
            }
            if (_isActive)
            {
                _healthButton.interactable = false;
                _killButton.interactable = false;
                _showButton.interactable = false;
                StartCoroutine(MoveTo(_hidenPosition, 1f));
                _isActive = false;
                _healthButton.interactable = true;
                _killButton.interactable = true;
                _showButton.interactable = true;

            }
        }

        private void ShowHealthBars()
        {
            if(_isHealthShown)
            {
                OnShowHealthBar?.Invoke(false);
                _isHealthShown = false;
            }
            else
            {
                OnShowHealthBar?.Invoke(true);
                _isHealthShown = true;
            }

            /*
            List<UnitData> units = _greenZig.GetUnits();
            units.AddRange(_redZig.GetUnits());
            units.AddRange(_blueZig.GetUnits());

            foreach (var unit in units)
            {
                if (_isHealthShown)
                {
                    unit.GetComponentInChildren<HealthBar>().gameObject.SetActive(false);
                    _isHealthShown = false;
                }
                else
                {
                    unit.GetComponentInChildren<HealthBar>().gameObject.SetActive(true);
                    _isHealthShown = true;
                }
            }
            */

        }

        private void KillEveryOne() => OnKillEveryone?.Invoke();

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
