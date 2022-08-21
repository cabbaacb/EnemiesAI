using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ziggurat
{
    public class Statistics : MonoBehaviour
    {
        [SerializeField] private ZigguratController _greenZig = null;
        [SerializeField] private ZigguratController _redZig = null;
        [SerializeField] private ZigguratController _blueZig = null;
        [SerializeField] private Text _aliveCount = null;
        [SerializeField] private Text _deadCount = null;
        [SerializeField] private Text _spawnInCount = null;
        [SerializeField] private Button _showButton = null;
        [SerializeField] private Button _healthButton = null;
        [SerializeField] private Button _killButton = null;

        public delegate void ShowHealthBarHandler(bool show);
        public static event ShowHealthBarHandler OnShowHealthBar;
        public delegate void KillEveryoneHandler();
        public static event KillEveryoneHandler OnKillEveryone;

        private Vector2 _shownPosition;
        private Vector2 _hiddenPosition;
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
        private bool _isHealthShown = true;
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _hiddenPosition = transform.position;
            _shownPosition = transform.position;
            _shownPosition.y -= 185;
            _canvasGroup = GetComponent<CanvasGroup>();
        }

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
        private void ChangeInfo()
        {
            _aliveCount.text = string.Format("{0}\n\n{1}\n\n{2}", _greenZig.UnitsNumber, _redZig.UnitsNumber, _blueZig.UnitsNumber);
            _deadCount.text = string.Format("{0}\n\n{1}\n\n{2}", _greenZig.DeadCount, _redZig.DeadCount, _blueZig.DeadCount);
            _spawnInCount.text = string.Format("{0}\n\n{1}\n\n{2}", _greenZig.SpawnTimer, _redZig.SpawnTimer, _blueZig.SpawnTimer);
        }

        public void ShowMenu()
        {
            IsActive = !IsActive;
        }

        public void ClearDeadCount()
        {
            _greenZig.ClearDeadCount();
            _redZig.ClearDeadCount();
            _blueZig.ClearDeadCount();
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
        }

        private void KillEveryOne() => OnKillEveryone?.Invoke();

        private IEnumerator MoveFromTo(Vector3 startPosition, Vector3 endPosition, float time)
        {
            _canvasGroup.interactable = false;
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
            _canvasGroup.interactable = true;
        }
    }
}
