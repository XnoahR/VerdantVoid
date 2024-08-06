using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace VerdantVoid.Managers
{
    public class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager instance;

        public List<ObjectiveObject> chapterObjectives = new List<ObjectiveObject>();

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
            GameplayMaster.currentChapter = chapter;
            GameplayMaster.currentStage = stage;
        }

        public bool IsObjectiveFulfilled(ObjectiveObject objective)
        {
            if (objective.requiredItem)
            {
                foreach (string item in objective.requiredItems.items)
                {
                    if (!GameplayMaster.inventory.Contains(item)) return false;
                }
            }
            return true;
        }

        public void CheckObjectives()
        {
            foreach (var objective in chapterObjectives)
            {
                if (objective.Chapter == GameplayMaster.currentChapter && objective.Stage == GameplayMaster.currentStage)
                {
                    if (IsObjectiveFulfilled(objective))
                    {
                        AdvanceToNextStage();
                        break;
                    }
                }
            }
        }

        private void AdvanceToNextStage()
        {
            ObjectiveObject currentObjective = chapterObjectives.Find(obj => obj.Chapter == GameplayMaster.currentChapter && obj.Stage == GameplayMaster.currentStage);
            GameplayMaster.currentStage++;
            var maxStages = chapterObjectives.FindAll(obj => obj.Chapter == GameplayMaster.currentChapter).Count;

            if (GameplayMaster.currentStage > maxStages)
            {
                GameplayMaster.currentChapter++;
                GameplayMaster.currentStage = 1;
            }

            SetProgress(GameplayMaster.currentChapter, GameplayMaster.currentStage);
            GameplayMaster.isDemo = true;
            LoadingScreenManager.instance.SwitchtoScene("DemoEnding");
        }


    }
}