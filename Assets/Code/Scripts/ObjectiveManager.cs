using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance;

    public List<RequiredItems> chapterObjectives; // List of objectives per chapter
    private int currentChapter;
    private int currentStage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetProgress(int chapter, int stage)
    {
        currentChapter = chapter;
        currentStage = stage;
        GameplayMaster.currentChapter = chapter;
        GameplayMaster.currentStage = stage;
    }

    public void CheckObjectives()
    {
        if (IsObjectiveFulfilled(chapterObjectives[currentChapter]))
        {
            AdvanceToNextStage();
        }
    }

    private bool IsObjectiveFulfilled(RequiredItems requiredItems)
    {
        foreach (string item in requiredItems.items)
        {
            if (!GameplayMaster.inventory.Contains(item)) return false;
        }
        return true;
    }

    private void AdvanceToNextStage()
    {
        // Logic to advance to the next stage or chapter
        currentStage++;
        if (currentStage > chapterObjectives.Count - 1)
        {
            currentChapter++;
            currentStage = 0;
        }
        SetProgress(currentChapter, currentStage);
        // Trigger any additional logic for progressing in the game
    }
}
