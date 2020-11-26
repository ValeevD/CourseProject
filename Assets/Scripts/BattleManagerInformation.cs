using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManagerInformation
{
    private List<KeyValuePair<ICharacter, CharacterPosition>> charactersPositions;

    public BattleManagerInformation(List<KeyValuePair<ICharacter, CharacterPosition>> _charactersPositions)
    {
        charactersPositions = _charactersPositions;
    }
}

public struct CharacterPosition{
    Vector2 position;
    float rotation;
}
