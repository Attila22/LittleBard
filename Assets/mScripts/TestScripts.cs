using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TestScripts : MonoBehaviour {


	void OnGUI()
	{
		if (GUILayout.Button ("Add")) {
			EventManager.Instance.AddEvent(GameEventType.LogData,LogData);
		}
		
		if (GUILayout.Button ("Remove")) {
			EventManager.Instance.DeleteEvent(GameEventType.LogData,LogData);
		}
		
		if (GUILayout.Button ("Invoke")) {
			EventManager.Instance.TriggerEvent(GameEventType.LogData,"Hehe");
		}
	}

	void LogData(object obj)
	{
		Debug.Log ("Log");
	}

    public Shader mshader;

    [ContextMenu("Do")]
    void Get()
    {
		Texture2D[] group = Resources.LoadAll<Texture2D>("FragmentShow/BgTex1");
		group.ApplyAllItem(C=>C.name = C.name.Replace("oil street-",""));
    }
}
