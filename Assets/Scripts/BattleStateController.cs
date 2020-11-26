using System;
using UnityEngine;

public partial class BattleStateController
{
    private ICharactersManager player1;
    private ICharactersManager player2;

    private BattleState currentState;

    private float remainingTime;
    private float totalTimeStartPlanning;
    private float totalTimeBattle;

    private int lastSecond;

    private bool player1Ready;
    private bool player2Ready;
    private bool roundFinished;

    public delegate void ChangeState();
    public event ChangeState changeStateDelegate;//подписываемся из GameViewProvider

    public delegate void IntermediateMessage(int _secondsRemaining);
    public event IntermediateMessage intermediateMessage;

    public delegate void CheckRoundResult();
    public event CheckRoundResult checkRoundResult;

    public delegate void RoundFinished();
    public event RoundFinished roundFinishedEvent;

    public delegate void GameFinished(GameResult result);
    public event GameFinished gameFinished;

    public BattleStateController(ICharactersManager manager1, ICharactersManager manager2)
    {
        player1 = manager1;
        player2 = manager2;

        //TODO:
        totalTimeStartPlanning = 2.0f;
        totalTimeBattle = 2.0f;

        currentState = BattleState.None;

        player1Ready = false;
        player2Ready = false;
        roundFinished = false;
    }

    public BattleState GetState()
    {
        return currentState;
    }

    private bool GetPlayerRediness(ICharactersManager _player)
    {
        return GameController.Instance.GetPlayerView(_player).GetReadiness();
    }

    private bool AllPlayersReady()
    {
        if(!player1Ready)
            player1Ready = GetPlayerRediness(player1);

        if(!player2Ready)
            player2Ready = GetPlayerRediness(player2);

        return player1Ready && player2Ready;
    }

    private void SetState(BattleState _state)
    {
        currentState = _state;
        roundFinished = false;
        player1Ready = false;
        player2Ready = false;
    }

    private void SendMessageToManager(ICharactersManager manager, BattleState _state)
    {
        manager.RecieveMessage(_state);
    }

    public void SetStartPlanningState()
    {
        SetState(BattleState.StartPlanning);
        remainingTime = totalTimeStartPlanning;
        lastSecond    = (int)totalTimeStartPlanning;

        SendMessageToManager(player1, BattleState.StartPlanning);
        SendMessageToManager(player2, BattleState.StartPlanning);

        changeStateDelegate.Invoke();
        //Debug.Log("Start planning phase!");
    }

    public void SetFightPlanningState()
    {
        SetState(BattleState.FigthPlanning);

        remainingTime = totalTimeBattle;
        lastSecond    = (int) totalTimeBattle;

        SendMessageToManager(player1, BattleState.FigthPlanning);
        SendMessageToManager(player2, BattleState.FigthPlanning);

        //changeStateDelegate.Invoke();
    }

    private void SendNextSecondMessage(int secondsRemaining)
    {
        intermediateMessage.Invoke(secondsRemaining);
    }

    private void GameEndCheck()
    {
        bool player1Dead = player1.AllUnitsDead();
        bool player2Dead = player2.AllUnitsDead();

        if(!(player1Dead || player2Dead))
            return;

        GameResult result = GameResult.Defeat;

        if(player1Dead && player2Dead)
        {
            result = GameResult.Draw;
        }
        else if(player2Dead)
        {
            result = GameResult.Victory;
        }

        SetState(BattleState.GameEnd);
        gameFinished.Invoke(result);
    }

    public void Update(float deltaTime)
    {
        switch(currentState){
            case BattleState.StartPlanning : {

                if (roundFinished)
                {
                    if(AllPlayersReady())
                    {
                        SetFightPlanningState();
                        changeStateDelegate.Invoke();
                        //checkRoundResult.Invoke();
                    }

                    break;
                }

                if(remainingTime <= 0.0f)
                {
                    roundFinished = true;
                    //changeStateDelegate.Invoke();
                    break;
                }

                if(remainingTime <= lastSecond)
                {
                    SendNextSecondMessage(lastSecond);
                    lastSecond--;
                }

                remainingTime -= deltaTime;

                break;
            }
            case BattleState.FigthPlanning : {

                if (roundFinished)
                {
                    if(AllPlayersReady()){
                        RoundChecker.Check(player1, player2);
                        SetFightPlanningState();
                        checkRoundResult.Invoke();
                        GameEndCheck();
                    }

                    break;
                }

                if(remainingTime <= 0.0f)
                {
                    roundFinished = true;
                    roundFinishedEvent.Invoke();
                    changeStateDelegate.Invoke();
                    break;
                }

                if(remainingTime <= lastSecond)
                {
                    SendNextSecondMessage(lastSecond);
                    lastSecond--;
                }

                remainingTime -= deltaTime;

                break;
            }
        }
    }

    public void Cleanup()
    {
        player1.Cleanup();
        player2.Cleanup();
    }
}

public enum BattleState{
    None,
    StartPlanning,
    FigthPlanning,
    GameEnd
}

public enum GameResult
{
    Victory,
    Defeat,
    Draw
}
