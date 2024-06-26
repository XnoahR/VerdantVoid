using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "Timeline", menuName = "ScriptableObjects/TimelineStateData", order = 1)]
public class TimelineObject : ScriptableObject
{

[System.Serializable]
    public class TimelineData
    {
        public string timelineKey;
        public PlayableAsset playable;
        public bool isPlayed;
        public bool isTrigger;
        public int stage;
    }


    public List<TimelineData> timelines = new List<TimelineData>();

}
