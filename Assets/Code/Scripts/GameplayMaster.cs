using System.Collections.Generic;
using UnityEngine;

public class GameplayMaster : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera vcam;

    public enum GameState
    {
        Cutscene,
        Gameplay,
        Interacting,
        Pause
    }

    [SerializeField] public static int currentChapter = 1;
    [SerializeField] public static int currentStage = 1;
    [SerializeField] public static List<string> inventory = new List<string>();
    public static bool isDemo = false;

    public static GameState currentGameState;
    private GameState previousGameState;
    private GameObject PauseScreen;

    public static GameObject player;
    [SerializeField] Vector3 objectposition;

    private void Awake()
    {
        if(!isDemo) vcam = GameObject.Find("Virtual Camera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        if (vcam == null) Debug.Log("vcam is null");
        if(!isDemo) player = GameObject.Find("Player");
        // Debug.Log("Player: " + player.name);

        currentGameState = GameState.Gameplay;
    }

    private void Start()
    {
        PauseScreen = GameObject.Find("PauseScreen");
        PauseScreen.SetActive(false);
    }

    void Update()
    {
        if (currentGameState != GameState.Pause && currentGameState != GameState.Cutscene)
        {
            previousGameState = currentGameState;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && currentGameState != GameState.Pause && !LoadingScreenManager.isLoading)
        {
            if (currentGameState != GameState.Interacting)
            {
                currentGameState = GameState.Pause;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && currentGameState == GameState.Pause)
        {
            RestoreState();
        }
        

        objectposition = player.transform.position;
        CheckState();
    }

    public void RestoreState()
    {
        currentGameState = previousGameState;
        Time.timeScale = 1;
    }

    void CheckState()
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

    public void SetStateCutscene()
    {
        currentGameState = GameState.Cutscene;
    }

    public void SetStateGameplay()
    {
        currentGameState = GameState.Gameplay;
    }

    void CutsceneTime()
    {
        vcam.Follow = null;
    }

    void GameplayTime()
    {
        if (PauseScreen == null) return;
        PauseScreen.SetActive(false);
        vcam.Follow = player.transform;
    }

    void InteractingTime()
    {
    }

    void PauseTime()
    {
        Time.timeScale = 0;
        currentGameState = GameState.Pause;
        PauseScreen.SetActive(true);
        Debug.Log("Pause");
    }

    public void SetProgress(int chapter, int stage)
    {
        currentChapter = chapter;
        currentStage = stage;
        ObjectiveManager.instance.SetProgress(chapter, stage);
    }
}
