using System;
using System.Collections;
using Game.Segment;
using Safe;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game
{
    public class Spin : MonoBehaviour
    {
        [SerializeField] private float spinTime = 5f;
        [SerializeField] private GameObject spinSound;
        [SerializeField] private GameObject cointsSound;
        [SerializeField] private Button spinButton;
        private WheelOfFortuneController _wheelOfFortuneController;


        private bool _isSpinning = false;
        private ulong _currentScore;
        private int _segmentName;
        private int _wheelSegmentWinCoinAmount;
        private GameObject _gameSegments;
        [SerializeField] private float decelerationSpeed; // The rate at which the wheel decelerates
        private int _wheelZeroSegmentCoinAmount;
        private WheelSegment _getwheelSegmentWin;
        private WheelSegment _getWheelZeroSegment;
        private int _quantityVales;

        void Start()
        {
            _wheelOfFortuneController = gameObject.GetComponent<WheelOfFortuneController>();

            _gameSegments = GameObject.Find("Segments");
            _currentScore = ScoreManager.LoadScore();
            spinButton.onClick.AddListener(StartSpin);
            _quantityVales = _wheelOfFortuneController.quantityVales;
        }

        private void StartSpin()
        {
            if (!_isSpinning)
            {
                _isSpinning = true;
            }

            if (_isSpinning)
            {
                float targetAngle = Random.Range(0f, 360f);
                StartCoroutine(SpinCoroutine(targetAngle));
                cointsSound.SetActive(false);
                spinSound.SetActive(true);
            }
        }

        private IEnumerator SpinCoroutine(float targetAngle)
        {
            float angle = 0f;
            float speed = 1000f;
            float time = 0f;
            float delta;


            while (speed > spinTime)
            {
                delta = Time.deltaTime;
                angle += speed * delta;
                time += delta;

                _gameSegments.transform.Rotate(new Vector3(0, 0, angle));

                if (angle >= targetAngle)
                {
                    speed = Mathf.Lerp(speed, 0, time / spinTime); // Decelerate the wheel smoothly
                    speed -= decelerationSpeed * delta;
                }

                yield return null;
            }

            //TODO  генерація вій граша поменять значение вій грашной клетки с нулевой ицвета если она не нулевая

            _gameSegments.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

            _isSpinning = false;
            spinSound.SetActive(false);


            if (!_isSpinning)
            {
                cointsSound.SetActive(true);
                GetCurrentSuresPlasAndSaveSurses();
            }
        }


        public void GetCurrentSuresPlasAndSaveSurses()
        {
            _segmentName = Random.Range(0, _quantityVales);

            Debug.Log("Spin _segmentName " + _segmentName);

            _getwheelSegmentWin = GameObject.Find(_segmentName.ToString()).GetComponent<WheelSegment>();
            _wheelSegmentWinCoinAmount = _getwheelSegmentWin.GetCoinAmount();
           
            Debug.Log("Spin _wheelSegmentWinCoinAmount " + _wheelSegmentWinCoinAmount);

            ulong wheelSegmentCoinAmount = _currentScore + (ulong)_wheelSegmentWinCoinAmount;
            ScoreManager.SaveScore(wheelSegmentCoinAmount);


            SetVineNaberstoZenterSegment();


            _wheelOfFortuneController.UpdateScoreText();
        }

        private void SetVineNaberstoZenterSegment()
        {
            _getWheelZeroSegment = GameObject.Find(0.ToString()).GetComponent<WheelSegment>();
            _wheelZeroSegmentCoinAmount = _getWheelZeroSegment.GetCoinAmount();
            Debug.Log("Spin _wheelZeroSegmentCoinAmount " + _wheelZeroSegmentCoinAmount);


            _getWheelZeroSegment.SetCoinAmount(_wheelSegmentWinCoinAmount);
            _getwheelSegmentWin.SetCoinAmount(_wheelZeroSegmentCoinAmount);
        }
    }
}