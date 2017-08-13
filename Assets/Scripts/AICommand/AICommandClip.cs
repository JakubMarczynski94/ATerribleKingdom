using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class AICommandClip : PlayableAsset, ITimelineClipAsset
{
	public AICommand.CommandType actionType;
	public Vector3 targetPosition; //for movement
	public ExposedReference<Unit> targetUnit; //for attacks

	[HideInInspector]
    public AICommandBehaviour template = new AICommandBehaviour ();

    public ClipCaps clipCaps
    {
		get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<AICommandBehaviour>.Create(graph, template);
		AICommandBehaviour clone = playable.GetBehaviour();
		clone.actionType = actionType;
		clone.targetPosition = targetPosition;
		//clone.targetUnit = targetUnit;
		clone.targetUnit = targetUnit.Resolve(graph.GetResolver());
        return playable;
    }
}