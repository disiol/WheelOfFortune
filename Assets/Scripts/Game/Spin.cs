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
        private int _currentScore;
        private string _segmentName;
        private int _wheelSegmentCoinAmount;
        private GameObject _gameSegments;
        [SerializeField] private float decelerationSpeed; // The rate at which the wheel decelerates

        void Start()
        {
            _wheelOfFortuneController = gameObject.GetComponent<WheelOfFortuneController>();

            _gameSegments = GameObject.Find("Segments");


            spinButton.onClick.AddListener(StartSpin);
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
            _currentScore = ScoreManager.LoadScore();
            _segmentName = GameObject.Find("SpinCenterButton").GetComponent<GetCurrentSegmentName>().segmentName;

            Debug.Log("Spin _segmentName " + _segmentName);

            _wheelSegmentCoinAmount = GameObject.Find(_segmentName).GetComponent<WheelSegment>().GetCoinAmount();

            Debug.Log("Spin _wheelSegmentCoinAmount " + _wheelSegmentCoinAmount);


            int wheelSegmentCoinAmount = _currentScore + _wheelSegmentCoinAmount;

            ScoreManager.SaveScore(wheelSegmentCoinAmount);
            _wheelOfFortuneController.UpdateScoreText();
        }
    }
}