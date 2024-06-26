using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interaction", menuName = "ScriptableObjects/Interaction")]
public class InteractionObject : ScriptableObject
{
    public string objectName;
    public string objectDescription;

    [System.Serializable]
    public class Interaction
    {
        public string speakerName;

        [TextArea(5, 10)]
        public string paragraphs;
    } 

    public List<Interaction> interactions = new List<Interaction>();
}
