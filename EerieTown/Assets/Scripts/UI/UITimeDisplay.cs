using TMPro;
using UnityEngine;

namespace UI
{
    public class UITimeDisplay : MonoBehaviour
    {
        #region Dependencies

        [SerializeField] private GameManager _gameManager;

        [SerializeField] private TextMeshProUGUI _tickDisplay;
        [SerializeField] private TextMeshProUGUI _pauseButtonText;
    
        #endregion

        private void Awake()
        {
            _gameManager.TickEvent += GameManagerOnTickEvent;
        }

        #region Event Handlers

        private void GameManagerOnTickEvent()
        {
            UpdateTimeDisplay();
        }

        #endregion

        #region Button Methods

        public void PauseUnpause()
        {
            _gameManager.ChangePause();

            _pauseButtonText.text = _gameManager.Paused ? "Unpause" : "Pause";
        }

        #endregion

        private void UpdateTimeDisplay()
        {
            _tickDisplay.text = $"{_gameManager.Hour:00}:{_gameManager.Minutes:00}";
        }
    }
}
