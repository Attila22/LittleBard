using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public static class CommonTools {


    public static void ClearChild(this Transform sourceGameObject)
    {
        var childCount = sourceGameObject.childCount;
        if (childCount > 0)
        {
            for (int i = childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(sourceGameObject.GetChild(i).gameObject);
            }
        }
    }

    public static void SetText(this UILabel SourceLabel, string str)
    {
        if (SourceLabel != null)
        {
            SourceLabel.text = str;
        }
        else
        {
            Debug.LogError("labelÎª¿Õ£º"+str);
        }
    }

    public static void SetText(this UILabel SourceLabel, int Number)
    {
        SetText(SourceLabel,Number.ToString());
    }
	
	public static GameObject CreatObject(this Transform ParentTransform,GameObject gameobjectPrefab)
	{
        if (gameobjectPrefab == null)
            return null;
		
        GameObject CreatObject = (GameObject)GameObject.Instantiate(gameobjectPrefab);
        CreatObject.transform.parent = ParentTransform;
        CreatObject.transform.localPosition = gameobjectPrefab.transform.localPosition;
        CreatObject.transform.localRotation = gameobjectPrefab.transform.localRotation;
        CreatObject.transform.localScale = gameobjectPrefab.transform.localScale;
		return CreatObject;
	}
    public static void ApplyAllItem<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (var t in collection)
        {
            action(t);
        }
    }
	
}
