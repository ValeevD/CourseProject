using System.Collections.Generic;
using UnityEngine;

public class AICharactersManager : ICharactersManager
{
    private List<ICharacter> characters;
    private int partOfCircle;

    public AICharactersManager(int _partOfCircle)
    {
        characters = new List<ICharacter>();
        characters.Add(new Character(true));
        partOfCircle = _partOfCircle;
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

    public BattleManagerInformation GetBattleInformation()
    {
        throw new System.NotImplementedException();
    }

    public List<ICharacter> GetCharacters()
    {
        return characters;
    }

    public bool GetRediness()
    {
        throw new System.NotImplementedException();
    }

    public bool IsHuman()
    {
        return false;
    }

    public void RecieveIntermediateMessage(BattleState state, int secondsRemaining)
    {
    }

    public void RecieveMessage(BattleState state)
    {
        switch (state)
        {
            case BattleState.StartPlanning :
            {
                foreach(ICharacter character in characters)
                {
                    character.SetRandomStartPosition(15, partOfCircle);
                }
                break;
            }
            case BattleState.FigthPlanning :
            {
                foreach(ICharacter character in characters)
                {
                    character.SetRandomPosition(15);
                }
                break;
            }
            default:
                break;
        }
    }
}
