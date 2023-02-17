using System.Collections.Generic;
using Game.Segment;
using Safe;
using TMPro;
using UnityEngine;

namespace Game
{
    public class WheelOfFortuneController : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;

        private int _currentScore;
        public List<int> values = new List<int>();
        private string _segmentName;
        private int _wheelSegmentCoinAmount;

        private void Start()
        {
            values = new List<int>();
        }

        public void UpdateScoreText()
        {
            _currentScore = ScoreManager.LoadScore();

            SetScoreText(_currentScore);
        }

        private void SetScoreText(int currentScore)
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
            // generate random values for wheel segments

            for (int i = 0; i < 16; i++)
            {
                int newValue;

                do
                {
                    newValue = Random.Range(1000, 100000);
                } while (values.Contains(newValue) || newValue % 1000 != 0);

                values.Add(newValue);
            }
        }
        
       
    }
}