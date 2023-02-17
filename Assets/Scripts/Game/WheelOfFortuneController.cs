using System.Collections.Generic;
using Game.Segment;
using Safe;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class WheelOfFortuneController : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;

        private ulong _currentScore;
        public List<ulong> values;
        private string _segmentName;
        private ulong _wheelSegmentCoinAmount;

        [HideInInspector] public int quantityVales;

        private void Start()
        {
            values = new List<ulong>();

            UpdateScoreText();
        }

        public void UpdateScoreText()
        {
            _currentScore = ScoreManager.LoadScore();

            SetScoreText(_currentScore);
        }

        private void SetScoreText(ulong currentScore)
        {
            if (currentScore >= 1000000)
            {
                scoreText.text = (currentScore / 1000000.0f).ToString("f2") + "m";
            }
            else if (currentScore >= 1000)
            {
                scoreText.text = (currentScore / 1000.0f).ToString("f2") + "k";
            }
            else
            {
                scoreText.text = currentScore.ToString();
            }
        }


        public void GeneraterandomValues()
        {
            quantityVales = 16;

            // generate random values for wheel segments

            for (int i = 0; i < quantityVales; i++)
            {
                ulong newValue;

                do
                {
                    newValue = (ulong)Random.Range(1000, 100000);
                } while (values.Contains(newValue) || newValue % 1000 != 0);

                values.Add(newValue);
            }
        }
    }
}