using UnityEngine;

public class GameplayMaster : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    //state machine using enum
    public enum GameState
    {
        Cutscene,
        Gameplay,
        Interacting,
        Pause
    }

    //Current State
    public static GameState currentGameState;
    private GameState previousGameState;

    public static GameObject player;

    private void Awake()
    {
        vcam = GameObject
            .Find("Virtual Camera")
            .GetComponent<Cinemachine.CinemachineVirtualCamera>();
        player = GameObject.Find("Player");
        //set enum to gameplay
        currentGameState = GameState.Gameplay;
    }

    // Update is called once per frame
    void Update()
    {
        //save the previous state if pausedd
        if (currentGameState != GameState.Pause && currentGameState != GameState.Cutscene)
        {
            previousGameState = currentGameState;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && currentGameState != GameState.Pause)
        {
            currentGameState = GameState.Pause;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && currentGameState == GameState.Pause)
        {
            currentGameState = previousGameState;
        }

        checkState();
    }

    void checkState()
    {
        switch (currentGameState)
        {
            case GameState.Cutscene:
                CutsceneTime();
                break;
            case GameState.Gameplay:
                GameplayTime();
                break;
            case GameState.Interacting:
                InteractingTime();
                break;
            case GameState.Pause:
                PauseTime();
                break;
        }
    }

    void CutsceneTime()
    {
        //    currentGameState = GameState.Cutscene;
        // Play cutscene
        vcam.Follow = null;
        Debug.Log("Cutscene");
    }

    void GameplayTime()
    {
        // currentGameState = GameState.Gameplay;
        // Play gameplay
        vcam.Follow = player.transform;
        // Debug.Log("Gameplay");
    }

    void InteractingTime()
    {
        // currentGameState = GameState.Interacting;
        // Play interacting
        Debug.Log("Interacting");
    }

    void PauseTime()
    {
        // currentGameState = GameState.Pause;
        // Play pause
        Debug.Log("Pause");
    }
}
