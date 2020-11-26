using System;
using System.Collections.Generic;

public interface ICharactersManager
{
    BattleManagerInformation GetBattleInformation();
    List<ICharacter> GetCharacters();
    void RecieveMessage(BattleState state);
    bool GetRediness();

    bool AllUnitsDead();
    bool IsHuman();

    void Cleanup();
}
