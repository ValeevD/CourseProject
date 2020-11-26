using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerProvider : MonoBehaviour, ISynchronize
{
    [SerializeField] private UIPlayerView currentPlayerUI;

    private CharacterProvider currentUnit;
    public  CharacterProvider CurrentUnit{
        get {return currentUnit;}
        set {currentUnit = value;}
    }

    private ICharactersManager player;
    private List<CharacterProvider> units;
    private int secondsRemaining;

    public bool freeze;

    public void Synchronize()
    {
        foreach(CharacterProvider unit in units)
            unit.Synchronize();
    }

    public void AddUnit(CharacterProvider newUnit)
    {
        units.Add(newUnit);

        if(units.Count == 1)
        {
            currentUnit = units[0];
        }
    }

    public void RemoveUnit(CharacterProvider unitToRemove)
    {
        units.Remove(unitToRemove);
    }

    public void Initialize(ICharactersManager _player, List<CharacterProvider> unitsView = null)
    {
        player = _player;

        if(unitsView == null)
        {
            units = new List<CharacterProvider>();
            return;
        }

        units = unitsView;
        CurrentUnit = units[0];
        secondsRemaining = 0;
    }

    public void RecieveIntermediateMessage(int _secondsRemaining)
    {
        if(player.IsHuman())
            currentPlayerUI.RecieveSecondsRemaining(_secondsRemaining);
    }

    public bool GetReadiness()
    {
        bool ready = true;

        foreach(CharacterProvider unit in units)
        {
            ready = ready && unit.UnitReady();
        }

        return ready;
    }

    public void EndRound(BattleState state)
    {
        foreach(CharacterProvider unit in units)
            unit.EndRound(state);
    }

    private void OnDestroy() {
        foreach(CharacterProvider unit in units)
            if(unit != null)
                Destroy(unit.gameObject);

        units.Clear();
    }

    public void SetEndGame(GameResult result)
    {
        currentPlayerUI.SetGameResult(result);

        freeze = true;
    }

}
