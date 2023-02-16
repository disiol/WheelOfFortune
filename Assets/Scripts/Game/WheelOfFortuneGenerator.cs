using System.Collections;
using System.Collections.Generic;
using Game.Segment;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    public class WheelOfFortuneGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject segmentPrefab;
        [SerializeField] private int numberOfSegments = 16;
        [SerializeField] private float radius = 0.5f;
        

        private List<GameObject> segments = new();
        private WheelOfFortuneController _wheelOfFortuneController;

        private void Awake()
        {
            _wheelOfFortuneController = gameObject.GetComponent<WheelOfFortuneController>();
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
                segment.name = i.ToString();
                
                segments.Add(segment);
            }

            _wheelOfFortuneController.GeneraterandomValues();
            SetSegmentValues(_wheelOfFortuneController.values);
        }

        public void SetSegmentValues(List<int> segmentValues)
        {
            for (int i = 0; i < numberOfSegments; i++)
            {
                segments[i].GetComponent<WheelSegment>().SetCoinAmount(segmentValues[i]);
            }
        }

      
    }
}