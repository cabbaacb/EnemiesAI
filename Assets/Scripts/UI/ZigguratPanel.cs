using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ziggurat
{
    public class ZigguratPanel : UnitStats
    {
        [SerializeField] private Text _zigguratText = null;

        [SerializeField] private Slider _heathSlider = null;
        [SerializeField] private Slider _speedSlider = null;
        [SerializeField] private Slider _fastAttackSlider = null;
        [SerializeField] private Slider _strongAttackSlider = null;
        [SerializeField] private Slider _AttackIntervalSlider = null;
        [SerializeField] private Slider _ChanceToMissSlider = null;
        [SerializeField] private Slider _doubleDamageSlider = null;
        [SerializeField] private Slider _FastToStrongRatioSlider = null;
        [SerializeField] private Slider _detectionRadiusSlider = null;

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
        private Vector2 _shownPosition;

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
            _shownPosition = transform.position;
            _shownPosition.x += 330;
        }

        private void OnEnable()
        {
            ZigguratController.OnClickEvent += SetZiggurat;
        }

        private void OnDisable()
        {
            ZigguratController.OnClickEvent -= SetZiggurat;
        }

        public void ShowMenu()
        {
            IsActive = !IsActive;
        }

        public void SetHealth(float health)
        { 
            _health = (int)health;
            Ziggurat.SetHealth(Health);
        }

        public override void SetSpeed(float speed)
        {
            base.SetSpeed(speed);
            Ziggurat.SetSpeed(Speed);
        }
        public void SetFastAttackDamage(float damage)
        { 
            _fastAttackDamage = (int)damage;
            Ziggurat.SetFastAttackDamage(FastAttackDamage);
        }
        public void SetStrongAttackDamage(float damage)
        { 
            _strongAttackDamage = (int)damage;
            Ziggurat.SetStrongAttackDamage(StrongAttackDamage);
        }

        public override void SetAttackInterval(float attackInterval)
        {
            base.SetAttackInterval(attackInterval);
            Ziggurat.SetAttackInterval(AttackInterval);
        }

        public override void SetChancetoMiss(float chance)
        {
            base.SetChancetoMiss(chance);
            Ziggurat.SetChancetoMiss(ChanceToMiss);
        }

        public override void SetDoubleDamageChance(float chance)
        {
            base.SetDoubleDamageChance(chance);
            Ziggurat.SetDoubleDamageChance(DoubleDamageChance);
        }
        public void SetFastToStrongAttackChanceRatio(float ratio)
        { 
            _fastToStrongAttackChanceRatio = (int)ratio;
            Ziggurat.SetFastToStrongAttackChanceRatio(FastToStrongAttackChanceRatio);
        }

        public override void SetDetectionRadius(float radius)
        {
            base.SetDetectionRadius(radius);
            Ziggurat.SetDetectionRadius(DetectionRadius);
        }

        private void SetZiggurat(ZigguratController ziggurat)
        {
            if(!IsActive)
            {
                IsActive = true;
            }

            Ziggurat = ziggurat;

            _heathSlider.value = Ziggurat.Health;
            _speedSlider.value = Ziggurat.Speed;
            _fastAttackSlider.value = Ziggurat.FastAttackDamage;
            _strongAttackSlider.value = Ziggurat.StrongAttackDamage;
            _AttackIntervalSlider.value = Ziggurat.AttackInterval;
            _ChanceToMissSlider.value = Ziggurat.ChanceToMiss;
            _doubleDamageSlider.value = Ziggurat.DoubleDamageChance;
            _FastToStrongRatioSlider.value = Ziggurat.FastToStrongAttackChanceRatio;
            _detectionRadiusSlider.value = Ziggurat.DetectionRadius;
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
