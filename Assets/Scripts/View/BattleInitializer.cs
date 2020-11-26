using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInitializer : MonoBehaviour
{
    [SerializeField] private GameObject environment;
    [SerializeField] private CharacterProvider playerPrefab;
    [SerializeField] private CharacterProvider enemyPrefab;
    [SerializeField] private PlayerManagerProvider playerManagerPrefab;
    [SerializeField] private PlayerManagerProvider enemyManagerPrefab;

    private BattleStateController battleStateController;
    private GameViewProvider battleProvider;
    private GameObject battleField;


    //private List<PlayerManagerProvider> PlayersManagers;

    private void Awake() {
        InitializeGame();
    }

    public void InitializeGame()
    {
        ICharactersManager player1 = new HumanCharactersManager(1);
        ICharactersManager player2 = new AICharactersManager(2);

        battleStateController = new BattleStateController(player1, player2);

        battleField = Instantiate(environment);

        List<ICharacter> player1Characters = player1.GetCharacters();
        List<ICharacter> player2Characters = player2.GetCharacters();

        GameObject emptyGameObject = new GameObject();

        emptyGameObject.name = "BattleHandler";

        PlayerManagerProvider player1ManagerProvider = Instantiate(playerManagerPrefab);
        PlayerManagerProvider player2ManagerProvider = Instantiate(enemyManagerPrefab);

        GameController.Instance.AddPlayer(player1, player1ManagerProvider);
        GameController.Instance.AddPlayer(player2, player2ManagerProvider);

        player1ManagerProvider.Initialize(player1, CreatePlayersCharactersView(player1Characters, playerPrefab));
        player2ManagerProvider.Initialize(player2, CreatePlayersCharactersView(player2Characters, enemyPrefab));

        battleProvider = emptyGameObject.AddComponent<GameViewProvider>();
        battleProvider.Initialize(battleStateController, player1ManagerProvider, player2ManagerProvider);

        //battleProvider.AddSyncronizedObject(player1ManagerProvider);
        //battleProvider.AddSyncronizedObject(player2ManagerProvider);

        battleStateController.SetStartPlanningState();
    }

    private void Update() {
        battleStateController.Update(Time.deltaTime);
    }

    private List<CharacterProvider> CreatePlayersCharactersView(List<ICharacter> playersCharacters, CharacterProvider _prefab)
    {
        List<CharacterProvider> listOfViews = new List<CharacterProvider>();

        foreach(ICharacter character in playersCharacters)
        {
            CharacterProvider newCharacter = Instantiate(_prefab);
            newCharacter.Initialize(character);

            listOfViews.Add(newCharacter);
        }

        return listOfViews;
    }

    public void Cleanup()
    {
        Destroy(battleProvider.gameObject);
        Destroy(battleField);
        battleStateController.Cleanup();
    }


}
