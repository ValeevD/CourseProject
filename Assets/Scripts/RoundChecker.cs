using System;
using UnityEngine;

public static class RoundChecker {
    public static void Check(ICharactersManager player1, ICharactersManager player2)
    {
        foreach(ICharacter player1Unit in player1.GetCharacters())
        {
            foreach(ICharacter player2Unit in player2.GetCharacters())
            {
                float dist1 = PositionChecker.GetDistance(player1Unit.GetPosition(), player1Unit.GetDirection(), player2Unit.GetPosition());

                //Debug.Log(dist1);
                if(PositionChecker.GetDistance(player1Unit.GetPosition(), player1Unit.GetDirection(), player2Unit.GetPosition()) < 1.0f)
                {
                    player2Unit.Health -= player1Unit.Damage;
                //    Debug.Log("Player 2 Hit Player 1");
                }

                if(PositionChecker.GetDistance(player2Unit.GetPosition(), player2Unit.GetDirection(), player1Unit.GetPosition()) < 1.0f)
                {
                    player1Unit.Health -= player2Unit.Damage;
                  //  Debug.Log("Player 1 Hit Player 2");
                }
            }
        }
    }
}
