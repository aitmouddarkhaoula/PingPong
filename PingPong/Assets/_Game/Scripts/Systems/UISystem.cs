using System;
using System.Collections;
using GameSystems;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystems {
    public class UISystem : Singleton<UISystem>
    {

        public Score score;
        [SerializeField] private GameObject _inGamePanel;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _startMenuPanel;
        [SerializeField] private GameObject _winPanel;

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _RestartButton;

      

        private bool _isInitialised;

        public void Awake() {
            GameStateSystem.OnGameStateChanged += UpdateUI;
            UpdateUI();
        }

        public void UpdateUI() {
            var currentState = GameStateSystem.GetState();

            switch (currentState) {
                case GameState.Playing:
                    SetActivePanel(inGamePanel_: true);
                    break;
                case GameState.GameOver:
                    SetActivePanel(gameOverPanel_: true);
                    break;
                case GameState.StartingMenu:
                    SetActivePanel(startMenuPanel_: true);
                    break;
                case GameState.GameWon:
                    SetActivePanel(winPanel_: true);
                    break;
            }
        }


        private void SetActivePanel(bool inGamePanel_ = false, bool gameOverPanel_ = false, bool startMenuPanel_ = false, bool winPanel_ = false) {
            _inGamePanel.SetActive(inGamePanel_);
            _gameOverPanel.SetActive(gameOverPanel_);
            _startMenuPanel.SetActive(startMenuPanel_);
            _winPanel.SetActive(winPanel_);
        }



        public override void OnDestroy() {
            GameStateSystem.OnGameStateChanged -= UpdateUI;
            base.OnDestroy();
        }
    }
}
