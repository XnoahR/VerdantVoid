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

    //Current Chapter
    public static int currentChapter = 0;   

    //Current State
    public static GameState currentGameState;
    private GameState previousGameState;
    private GameObject PauseScreen;

    public static GameObject player;
    [SerializeField] Vector3 objectposition;

    private void Awake()
    {
        vcam = GameObject
            .Find("Virtual Camera")
            .GetComponent<Cinemachine.CinemachineVirtualCamera>();
        player = GameObject.Find("Player");
        //set enum to gameplay
        currentGameState = GameState.Gameplay;
        
    }

    private void Start() {
        PauseScreen = GameObject.Find("PauseScreen");
        PauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //save the previous state if pausedd
        if (currentGameState != GameState.Pause && currentGameState != GameState.Cutscene)
        {
            previousGameState = currentGameState;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && currentGameState != GameState.Pause && LoadingScreenManager.isLoading == false)
        {
            //not interacting
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
        checkState();
    }

    public void RestoreState()
    {
        currentGameState = previousGameState;
        Time.timeScale = 1;
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

    public void setStateCutscene()
    {
        currentGameState = GameState.Cutscene;
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
        //check if null
        if (PauseScreen == null)
        {
           return;
        }
        else PauseScreen.SetActive(false);
        // currentGameState = GameState.Gameplay;
        // Play gameplay
        vcam.Follow = player.transform;
        // Debug.Log("Gameplay");
    }

    void InteractingTime()
    {
        // currentGameState = GameState.Interacting;
        // Play interacting
       
    }

    void PauseTime()
    {
        //Freeze the game and show pause screen
        Time.timeScale = 0;
        currentGameState = GameState.Pause;
        PauseScreen.SetActive(true);
        Debug.Log("Pause");
    }
}
