using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager{

	static EventManager instance;
	public static EventManager Instance{
		get{
			if(instance == null)
				instance = new EventManager();
			return instance;
		}
	}


	public delegate void EventCallBack(object obj);

	Dictionary<GameEventType,EventCallBack> EventGroup = new Dictionary<GameEventType, EventCallBack>();

	public void AddEvent(GameEventType type,EventCallBack callBack)
	{
		if (EventGroup.ContainsKey (type)) {
			EventGroup [type] += callBack;		
		} else {
			EventGroup[type] = callBack;
		}
	}

	public void DeleteEvent(GameEventType type,EventCallBack callBack)
	{
		if (EventGroup.ContainsKey (type)) {
			EventGroup[type]-=callBack;
		}
	}

	public void TriggerEvent(GameEventType type,object value)
	{
		if (EventGroup.ContainsKey (type)&&EventGroup[type]!=null) {
			EventGroup[type](value);
		}
	}

}


public enum GameEventType
{
	LogData,
	BeginnerGuideEvent,
}