using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Segment
{
    public class WheelSegment : MonoBehaviour
    {
        private int _coinAmount;

        private TextMeshProUGUI _coinAmountText;

        private void Awake()
        {
            _coinAmountText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetCoinAmount(int amount)
        {
            transform.GetComponent<Image>().color = GetRandomColor();

            _coinAmount = amount;
            _coinAmountText.text = amount.ToString();
        }

       public int GetCoinAmount()
        {
            return _coinAmount;
        }
        
        
        public Color GetRandomColor()
        {
            float red = Random.Range(0.5f, 1f);
            float green = Random.Range(0, 0.5f);
            float blue = 0;

            return new Color(red, green, blue);
        }
    }
}