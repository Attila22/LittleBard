using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class UIPrefabData
{
	public UIType mType;
	public GameObject mPrefab;
}


public enum UIType
{
    FragmentSelectUI,
	BottleUI,
	MainUI,
	Collections,
	ScenesSelect,
	MainGameSetting,
	LevelSelet,
	GamePlay,
	GamePlaySetting,
	GamePause,
	GameOver,
    MissUI,
	BeginnerGuideUI,
	BlackUI,
}