using System.Collections;
using System.Collections.Generic;
using GameSystems;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _restartButton;
   
    // Start is called before the first frame update
    void Start()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    private void OnRestartButtonClicked()
    {
        GameStateSystem.SetState(GameState.StartingMenu);
        GameManager.Instance.Reset();
    }

    private void OnStartButtonClicked()
    {
        GameManager.Instance.Init();
        GameStateSystem.SetState(GameState.Playing);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
