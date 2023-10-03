using System;

namespace GameSystems {
    public enum GameState {
        StartingMenu,
        Playing,
        GameOver,
        GameWon,
    }
    
    public static class GameStateSystem {
        public static event Action OnGameStateChanged;

        private static GameState _currentState = GameState.StartingMenu;


        public static GameState GetState() => _currentState;

        public static void SetState(GameState newState) {
            if (_currentState == newState) return;
            _currentState = newState;
            OnGameStateChanged?.Invoke();
            

            
            UISystem.Instance.UpdateUI();
            
            
        }
    }
}