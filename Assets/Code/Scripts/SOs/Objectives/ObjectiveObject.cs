using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Objective", menuName = "ScriptableObjects/Objective")]
public class ObjectiveObject : ScriptableObject
{
    public string objectiveName;
    [TextAreaAttribute]
    public string objectiveDescription;
    public bool requiredItem;
    public RequiredItems requiredItems;
    public string nextScene;

    public int Chapter;
    public int Stage;
}
