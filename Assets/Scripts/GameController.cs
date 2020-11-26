using System;
using System.Collections.Generic;

public class GameController{
    private static GameController instance;

    public static GameController Instance{
        get{
            if(instance == null)
                instance = new GameController();

            return instance;
        }
        set{
            instance = value;
        }
    }

    private Dictionary<ICharactersManager, PlayerManagerProvider> playersView;

    public GameController()
    {
        playersView = new Dictionary<ICharactersManager, PlayerManagerProvider>();
    }

    public void AddPlayer(ICharactersManager controller, PlayerManagerProvider view)
    {
        playersView.Add(controller, view);
    }

    public PlayerManagerProvider GetPlayerView(ICharactersManager controller)
    {
        return playersView[controller];
    }
}
