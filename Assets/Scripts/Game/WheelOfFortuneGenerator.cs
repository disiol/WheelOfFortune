using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    public class WheelOfFortuneGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject segmentPrefab;
        [SerializeField] private Button spinButton;
        [SerializeField] private int numberOfSegments = 16;
        [SerializeField] private float radius = 0.5f;
        [SerializeField] private float spinTime = 5f;

        private List<GameObject> segments = new();
        private WheelOfFortune _wheelOfFortune;
        private bool _isSpinning = false;

        private void Awake()
        {
            _wheelOfFortune = gameObject.GetComponent<WheelOfFortune>();
            GenerateSegments();
        }


        private void GenerateSegments()
        {
            float angleStep = 360f / numberOfSegments;

            for (int i = 0; i < numberOfSegments; i++)
            {
                Vector3 center = Vector3.zero;
                float angle = i * angleStep;
                float x = center.x + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
                float y = center.y + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                Vector3 position = new Vector3(x, y, center.z);
                Quaternion rotation = Quaternion.Euler(0, 0, -angle);
                GameObject segment = Instantiate(segmentPrefab, position, rotation, transform);

                segments.Add(segment);
            }

            _wheelOfFortune.GeneraterandomValues();
            SetSegmentValues(_wheelOfFortune.values);
            spinButton.onClick.AddListener(Spin);
        }

        public void SetSegmentValues(List<int> segmentValues)
        {
            for (int i = 0; i < numberOfSegments; i++)
            {
                segments[i].GetComponent<WheelSegment>().SetCoinAmount(segmentValues[i]);
            }
        }

        public void Spin()
        {
            if (!_isSpinning)
            {
                _isSpinning = true;
                float targetAngle = Random.Range(0f, 360f);
                StartCoroutine(SpinCoroutine(targetAngle));
            }
        }

        private IEnumerator SpinCoroutine(float targetAngle)
        {
            float angle = 0f;
            float speed = 1000f;
            float time = 0f;

            while (time < spinTime)
            {
                float delta = Time.deltaTime;
                angle += speed * delta;
                time += delta;

                transform.Rotate(new Vector3(0, 0, angle));

                if (angle >= targetAngle)
                {
                    speed = Mathf.Lerp(speed, 0, time / spinTime);
                }

                yield return null;
            }

            _isSpinning = false;
        }
    }
}