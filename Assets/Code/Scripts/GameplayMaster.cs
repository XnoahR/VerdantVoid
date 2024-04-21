
using UnityEngine;

public class GameplayMaster : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    public bool isGamePaused;
    public bool isGameplay;
    public bool isCutscene;

    private GameObject player;
    private void Awake()
    {
        vcam = GameObject.Find("Virtual Camera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        player = GameObject.Find("Player");
        isGameplay = true;
        isCutscene = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CutsceneTime();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameplayTime();
        }
    }

    void CutsceneTime()
    {
        isCutscene = true;
        isGameplay = false;
        // Play cutscene
        vcam.Follow = null;
        Debug.Log("Cutscene");
    }

    void GameplayTime()
    {
        isGameplay = true;
        isCutscene = false;
        // Play gameplay
        vcam.Follow = player.transform;
        Debug.Log("Gameplay");
    }
}
