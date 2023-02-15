using System.Collections.Generic;
using Safe;
using TMPro;
using UnityEngine;

namespace Game
{
    public class WheelOfFortune : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;

        private int currentScore;

        private void Start()
        {
            currentScore = ScoreManager.LoadScore();
            UpdateScoreText();
        }

        private void UpdateScoreText()
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

        public void SpinWheel()
        {
            if (IsWheelSpinning())
            {
                return;
            }

            // generate random values for wheel segments
            List<int> values = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                int newValue;
                do
                {
                    newValue = UnityEngine.Random.Range(1000, 100000);
                } while (values.Contains(newValue) || newValue % 1000 != 0);

                values.Add(newValue);
            }

            // select random value for current spin
            int selectedValue = values[UnityEngine.Random.Range(0, 16)];
            currentScore += selectedValue;

            // update score text
            UpdateScoreText();

            // save score
            ScoreManager.SaveScore(currentScore);

            // start wheel spin animation
            // ...
        }

        private bool IsWheelSpinning()
        {
            // check if wheel is currently spinning
            return false;
        }
    }

}