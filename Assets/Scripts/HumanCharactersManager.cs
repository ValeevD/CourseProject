using System;
using System.Collections.Generic;
using UnityEngine;

public class HumanCharactersManager : ICharactersManager
{
    private List<ICharacter> characters;
    private int partOfCircle;

    public HumanCharactersManager(int _partOfCircle)
    {
        characters = new List<ICharacter>();
        characters.Add(new Character(false));
        partOfCircle = _partOfCircle;
    }

    public List<ICharacter> GetCharacters()
    {
        return characters;
    }

    public BattleManagerInformation GetBattleInformation()
    {
        throw new System.NotImplementedException();
    }

    public void RecieveMessage(BattleState state)
    {
        switch (state)
        {
            case BattleState.StartPlanning :
            {
                foreach(ICharacter character in characters)
                {
                    //character.SetRandomStartPosition(15, partOfCircle);
                }
                break;
            }
            case BattleState.FigthPlanning :
            {
                foreach(ICharacter character in characters)
                {
                    //character.SetRandomPosition(15);
                }
                break;
            }
            default:
                break;
        }
    }

    public bool IsHuman()
    {
        return true;
    }

    public bool GetRediness()
    {
        return false;
    }

    public bool AllUnitsDead()
    {
        foreach(ICharacter unit in characters)
        {
            if(unit.Health > 0)
                return false;
        }

        return true;
    }

    public void Cleanup()
    {
        foreach(ICharacter unit in characters)
            unit.Cleanup();
    }
}
