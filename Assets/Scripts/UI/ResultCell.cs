using UnityEngine;
using TMPro;
using TestOverMobile.SaveSystem;

namespace TestOverMobile.UI
{
    public class ResultCell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private TextMeshProUGUI _playerScore;

        public void SetData(PlayerCard card)
        {
            _playerName.text = card.Name;
            _playerScore.text = card.Score.ToString();
        }
    }
}