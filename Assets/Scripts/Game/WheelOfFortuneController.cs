using System.Collections.Generic;
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

        private void Start()
        {
            UpdateScoreText();
            values = new List<int>();
        }

        public void UpdateScoreText()
        {
            _currentScore = ScoreManager.LoadScore();

            if (_currentScore >= 1000000)
            {
                scoreText.text = (_currentScore / 1000000.0f).ToString("f2") + "m";
            }
            else if (_currentScore >= 1000)
            {
                scoreText.text = (_currentScore / 1000.0f).ToString("f2") + "k";
            }
            else
            {
                scoreText.text = _currentScore.ToString();
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