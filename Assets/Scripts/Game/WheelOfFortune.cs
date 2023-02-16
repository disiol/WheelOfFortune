using System.Collections.Generic;
using Safe;
using TMPro;
using UnityEngine;

namespace Game
{
    public class WheelOfFortune : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;

        private int _currentScore;
        public List<int> values = new List<int>();

        private void Start()
        {
            _currentScore = ScoreManager.LoadScore();
            UpdateScoreText();
            values = new List<int>();
            
        }

        private void UpdateScoreText()
        {
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

        public void SpinWheel()
        {
            if (IsWheelSpinning())
            {
                return;
            }


            // select random value for current spin
            int selectedValue = values[Random.Range(0, 16)];

            _currentScore += selectedValue;

            // TODO update score text  wen stop animation
            UpdateScoreText();

            // save score
            ScoreManager.SaveScore(_currentScore);

            //TODO start wheel spin animation
            // колесо крутится пысля зу пинення поя вляеться в ыкны потрыбна сума ы додаеться
            //TODO start wheel spin saund
            // ...
        }

        public  void GeneraterandomValues()
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

        private bool IsWheelSpinning()
        {
            // check if wheel is currently spinning
            return false;
        }
    }
}