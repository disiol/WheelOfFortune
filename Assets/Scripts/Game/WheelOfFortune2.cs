using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WheelOfFortune2 : MonoBehaviour
{
    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private Button spinButton;
    [SerializeField] private int numberOfSegments = 16;
    [SerializeField] private float radius = 100f;
    [SerializeField] private float spinTime = 5f;

    private List<GameObject> segments = new List<GameObject>();
    private bool isSpinning = false;

    private void Start()
    {
        GenerateSegments();
    }

    private void GenerateSegments()
    {
        float angleStep = 360f / numberOfSegments;

        for (int i = 0; i < numberOfSegments; i++)
        {
            GameObject segment = Instantiate(segmentPrefab, transform);
            segment.transform.Rotate(new Vector3(0, 0, i * angleStep));
            segments.Add(segment);
        }
        
        spinButton.onClick.AddListener(Spin);
        
        
    }

    public void Spin()
    {
        if (!isSpinning)
        {
            isSpinning = true;
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

        isSpinning = false;
    }
}