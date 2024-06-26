using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RequiredItems", menuName = "ScriptableObjects/RequiredItems")]
public class RequiredItems : ScriptableObject
{
    public string Title;

    public List<string> items = new List<string>();
}
