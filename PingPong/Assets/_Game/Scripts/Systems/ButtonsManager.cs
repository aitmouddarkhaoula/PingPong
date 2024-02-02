using System.Collections;
using System.Collections.Generic;
using GameSystems;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _restartwINButton;
    [SerializeField] Button _restartLoseButton;
   
    // Start is called before the first frame update
    void Start()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _restartLoseButton.onClick.AddListener(OnStartButtonClicked);
        _restartwINButton.onClick.AddListener(OnStartButtonClicked);
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
