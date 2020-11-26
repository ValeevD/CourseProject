using System;
using System.Collections.Generic;
using UnityEngine;

public class GameViewProvider : MonoBehaviour, ISynchronize
{
    private List<ISynchronize> gameObjectsToUpdate;
    private BattleStateController stateController;

    private PlayerManagerProvider player1;
    private PlayerManagerProvider player2;

    public bool freeze;

    private void Awake() {
        gameObjectsToUpdate = new List<ISynchronize>();
        freeze = false;
    }


    public void Synchronize()
    {
        // Debug.Log("syncing");
        foreach(ISynchronize obj in gameObjectsToUpdate)
        {
            obj.Synchronize();
        }
    }

    public void RecieveintermediateMessage(int secondsRemaining)
    {
        player1.RecieveIntermediateMessage(secondsRemaining);
    }

    public void CheckRoundResult()
    {
        player1.EndRound(stateController.GetState());
        player2.EndRound(stateController.GetState());

        UnfreezeCharacters();
    }

    public void Initialize(BattleStateController _battleStateController, PlayerManagerProvider _player1, PlayerManagerProvider _player2)
    {
        stateController = _battleStateController;
        player1 = _player1;
        player2 = _player2;

        AddSyncronizedObject(player1);
        AddSyncronizedObject(player2);

        stateController.changeStateDelegate += Synchronize;
        stateController.intermediateMessage += RecieveintermediateMessage;
        stateController.checkRoundResult    += CheckRoundResult;
        stateController.roundFinishedEvent  += FreezeCharacters;
        stateController.gameFinished        += SetEndGame;
    }

    private void SetEndGame(GameResult result)
    {
        player1.SetEndGame(result);
    }

    private void FreezeCharacters()
    {
        freeze = player1.freeze = player2.freeze = true;
    }

    private void UnfreezeCharacters()
    {
        freeze = player1.freeze = player2.freeze = false;
    }

    public void AddSyncronizedObject(ISynchronize _gameObject)
    {
        gameObjectsToUpdate.Add(_gameObject);
    }

    private void OnDestroy() {
        stateController.changeStateDelegate -= Synchronize;
        stateController.intermediateMessage -= RecieveintermediateMessage;
        stateController.checkRoundResult    -= CheckRoundResult;
        stateController.roundFinishedEvent  -= FreezeCharacters;

        if(player1 != null)
            Destroy(player1.gameObject);

        if(player2 != null)
            Destroy(player2.gameObject);

        gameObjectsToUpdate.Clear();
    }
}
