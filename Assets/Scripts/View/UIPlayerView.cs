using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIPlayerView : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private TextMeshProUGUI secondsRemainingText;
    [SerializeField] private CanvasGroup gameResultGroup;
    [SerializeField] private TextMeshProUGUI gameResultLabel;

    private Camera mainCamera;
    private int secondsRemaining;

    private BattleInitializer battleInitializer;

    public void SetNewBattleState(BattleState state)
    {

    }

    public void RecieveSecondsRemaining(int _secondsRemaining)
    {
        secondsRemainingText.text = $"Seconds remaining: {_secondsRemaining}";
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        mainCanvas.worldCamera = mainCamera;
        battleInitializer = FindObjectOfType<BattleInitializer>();
    }

    public void SetGameResult(GameResult gameResult)
    {
        gameResultLabel.text = gameResult.ToString();
        SetGameResultGroupVisible(true);
    }

    private void SetGameResultGroupVisible(bool _set)
    {
        gameResultGroup.interactable = _set;
        gameResultGroup.blocksRaycasts = _set;
        gameResultGroup.alpha = _set ? 1 : 0;
    }

    public void RestartGame()
    {
        SetGameResultGroupVisible(false);
        battleInitializer.Cleanup();
        battleInitializer.InitializeGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
